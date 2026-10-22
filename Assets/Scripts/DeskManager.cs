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

    [Header("Master Lists (assign in Inspector)")]
    public List<CatDataBase> masterCatPool;

    [Header("Scene Slots")]
    public List<CatSlot> activeSlots;

    [Header("ActiveCats")]
    public List<GameObject> activeCats;

    [Header("Potion Controls")]
    public Slider greenPotionSlider;

    private int Round;

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI loseText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        livesRemaining = StartingLives;
        Debug.Log($"The round starts with {livesRemaining} lives.", this);
        GenerateWorkday();
        Round= 1;
        loseText.enabled = false;

    }

    private void Update()
    {
        roundText.text = Round.ToString();
        livesText.text = livesRemaining.ToString();
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

        if (greenPotionSlider == null)
        {
            Debug.LogError("Assign the Green Potion Slider in the Inspector to reset it.", this);
        }
        else
        {
            greenPotionSlider.value = greenPotionSlider.minValue;
        }

        Debug.Log($"Game restarted with {livesRemaining} lives.", this);

        DestroyCats();
        GenerateWorkday();
        loseText.enabled = false;
        roundText.enabled = true;

        Round = 1;
    }

    public void BrewPotion()
    {
        if (livesRemaining <= 0)
        {
            Debug.Log("Game over. There are no lives remaining.", this);
            return;
        }

        if (greenPotionSlider == null)
        {
            Debug.LogError("Assign the Green Potion Slider in the Inspector.", this);
            return;
        }

        int greenAmount = Mathf.RoundToInt(greenPotionSlider.value);
        Debug.Log($"You brewed a potion with {greenAmount} green.", this);

        bool allCatsAreSafe = true; // Declare and initialize here

        foreach (GameObject catObject in activeCats)
        {
            CatCompononet cat = catObject.GetComponent<CatCompononet>();
            bool catIsSafe = CheckCat(cat.catinfo.catName, greenAmount, cat.probleminfo.minGreen, cat.probleminfo.maxGreen);

            if (!catIsSafe)
            {
                allCatsAreSafe = false;
                break;
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
            Debug.Log($"One or more cats are not safe. Lives remaining: {livesRemaining}");
            DestroyCats();
            GenerateWorkday();
            Round++;
            if (livesRemaining <= 0)
            {
                Debug.Log("Game over. You have no lives remaining.");
                DestroyCats();
                roundText.enabled = false;
                loseText.enabled = true;
            }

        }
    }

    private bool CheckCat(string catName, int greenAmount, int minimumSafe, int maximumSafe)
    {
        if (greenAmount < minimumSafe)
        {
            Debug.Log($"{catName}: not enough green to help.", this);
            return false;
        }

        if (greenAmount > maximumSafe)
        {
            Debug.Log($"{catName}: too much green; this could harm the cat.", this);
            return false;
        }

        Debug.Log($"{catName}: the green amount is safe.", this);
        return true;
    }
}
