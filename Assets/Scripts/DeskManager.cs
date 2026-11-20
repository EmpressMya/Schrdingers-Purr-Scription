using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeskManager : MonoBehaviour
{
    private const int StartingLives = 3;
    private int livesRemaining;

    [Header("Master Lists")]
    public List<CatDataBase> masterCatPool;

    [Header("Scene Slots")]
    public List<CatSlot> activeSlots;

    [Header("Active Cats")]
    public List<GameObject> activeCats;

    [Header("Potion Manager")]
    [SerializeField] private PotionManager potionManager;


    private int Round;

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI livesText;
    public GameObject loseBox;
    public GameObject potionNotes;

    public TextMeshProUGUI redAmount;
    public TextMeshProUGUI yellowAmount;
    public TextMeshProUGUI blueAmount;
    public TextMeshProUGUI greenAmount;
    public TextMeshProUGUI magentaAmount;

    [SerializeField] private ScoreText scoreManager;

    void Start()
    {
        livesRemaining = StartingLives;

        Debug.Log($"The round starts with {livesRemaining} lives.", this);

        closeNotes();

        GenerateWorkday();

        Round = 1;
        loseBox.SetActive(false);
    }

    private void Update()
    {
        roundText.text = $"Round: {Round}";
        livesText.text = $"Lives: {livesRemaining}";
        redAmount.text = $"Red: {potionManager.redValue}";
        yellowAmount.text = $"Yellow: {potionManager.yellowValue}";
        blueAmount.text = $"Blue: {potionManager.blueValue}";
        greenAmount.text = $"Green: {potionManager.greenValue}";
        magentaAmount.text = $"Magenta: {potionManager.magentaValue}";
    }

    public void GenerateWorkday()
    {
        foreach (CatSlot slot in activeSlots)
        {
            slot.catProcess();
        }
    }

    public void DestroyCats()
    {
        foreach (GameObject cat in activeCats)
        {
            Destroy(cat);
        }

        activeCats.Clear();
    }

    public void RestartGame()
    {
        livesRemaining = StartingLives;

        if (potionManager == null)
        {
            Debug.LogError("Assign the Potion Manager in the Inspector.", this);
            return;
        }

        potionManager.ResetPotion();

        DestroyCats();
        GenerateWorkday();

        loseBox.SetActive(false);
        roundText.enabled = true;

        Round = 1;

        Debug.Log("Game restarted.", this);
    }

    public void BrewPotion()
    {
        if (livesRemaining <= 0)
        {
            Debug.Log("Game over. There are no lives remaining.", this);
            return;
        }

        if (potionManager == null)
        {
            Debug.LogError("Assign the Potion Manager.", this);
            return;
        }

        int redAmount = potionManager.redValue;
        int yellowAmount = potionManager.yellowValue;
        int blueAmount = potionManager.blueValue;
        int greenAmount = potionManager.greenValue;
        int magentaAmount = potionManager.magentaValue;

        Debug.Log(
            $"Potion: Red {redAmount}, Yellow {yellowAmount}, Blue {blueAmount}, Green {greenAmount}, Magenta {magentaAmount}",
            this
        );

        bool allCatsAreSafe = true;

        foreach (GameObject catObject in activeCats)
        {
            CatCompononet cat = catObject.GetComponent<CatCompononet>();

            if (cat == null)
            {
                Debug.LogError("Cat is missing its CatCompononet.", catObject);
                allCatsAreSafe = false;
                continue;
            }

            bool catIsSafe = CheckCat(
                cat.catinfo.catName,
                redAmount,
                yellowAmount,
                blueAmount,
                greenAmount,
                magentaAmount,
                cat.probleminfo
            );

            if (!catIsSafe)
            {
                allCatsAreSafe = false;
            }
        }

        if (allCatsAreSafe)
        {
            Debug.Log("All cats are safe! You win this round.", this);
            DestroyCats();
            GenerateWorkday();
            Round++;
        }
        else
        {
            livesRemaining--;

            Debug.Log($"One or more cats are unsafe. Lives remaining: {livesRemaining}");

            DestroyCats();

            if (livesRemaining <= 0)
            {
                Debug.Log("Game over. You have no lives remaining.");

                roundText.enabled = false;
                loseBox.SetActive(true);
                closeNotes();
            }
            else
            {
                GenerateWorkday();
                Round++;
            }
        }
    }

    private bool CheckCat(
        string catName,
        int redAmount,
        int yellowAmount,
        int blueAmount,
        int greenAmount,
        int magentaAmount,
        ProblemData problem)
    {
        int requiredValue = GetMaterialValue(
            problem.requiredMaterial,
            redAmount,
            yellowAmount,
            blueAmount,
            greenAmount,
            magentaAmount
        );

        int deathValue = GetMaterialValue(
            problem.deathMaterial,
            redAmount,
            yellowAmount,
            blueAmount,
            greenAmount,
            magentaAmount
        );

        bool requiredSafe =
            Mathf.Abs(requiredValue - problem.requiredAmount) <= problem.tolerance;

        bool deathTriggered = deathValue >= problem.deathAmount;

        if (deathTriggered)
        {
            Debug.Log($"{catName}: Death material threshold reached!", this);
            return false;
        }

        if (!requiredSafe)
        {
            Debug.Log($"{catName}: Required material amount is incorrect.", this);
            return false;
        }

        Debug.Log($"{catName}: All material requirements are safe!", this);
        return true;
    }

    private int GetMaterialValue(
        PotionColor material,
        int redAmount,
        int yellowAmount,
        int blueAmount,
        int greenAmount,
        int magentaAmount)
    {
        switch (material)
        {
            case PotionColor.Red:
                return redAmount;

            case PotionColor.Yellow:
                return yellowAmount;

            case PotionColor.Blue:
                return blueAmount;

            case PotionColor.Green:
                return greenAmount;

            case PotionColor.Magenta:
                return magentaAmount;

            default:
                return 0;
        }
    }

    public void openNotes()
    {
        potionNotes.SetActive(true);
    }

    public void closeNotes()
    {
        potionNotes.SetActive(false);
    }
}