using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskManager : MonoBehaviour
{
    private const int StartingLives = 3;
    private int livesRemaining;

    [Header("Master Lists (assign in Inspector)")]
    public List<CatData> masterCatPool;
    public List<ProblemData> masterProblemPool;

    [Header("Scene Slots")]
    public List<CatSlot> activeSlots;

    [Header("Potion Controls")]
    public Slider greenPotionSlider;

    [Header("Temporary Cat Rules")]
    [SerializeField] private int cat1MinSafeGreen = 2;
    [SerializeField] private int cat1MaxSafeGreen = 4;
    [SerializeField] private int cat2MinSafeGreen = 3;
    [SerializeField] private int cat2MaxSafeGreen = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        livesRemaining = StartingLives;
        Debug.Log($"The round starts with {livesRemaining} lives.", this);
        GenerateWorkday();
    }

    public void GenerateWorkday()
    {
        // Implementation for generating the workday
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

        bool cat1IsSafe = CheckCat("Cat 1", greenAmount, cat1MinSafeGreen, cat1MaxSafeGreen);
        bool cat2IsSafe = CheckCat("Cat 2", greenAmount, cat2MinSafeGreen, cat2MaxSafeGreen);

        bool atLeastOneCatWasHarmed =
            greenAmount > cat1MaxSafeGreen || greenAmount > cat2MaxSafeGreen;

        if (atLeastOneCatWasHarmed)
        {
            livesRemaining--;
            Debug.Log($"A cat was harmed. You lost one life. Lives remaining: {livesRemaining}.", this);

            if (livesRemaining == 0)
            {
                Debug.Log("Game over. You are out of lives.", this);
            }
        }
        else if (cat1IsSafe && cat2IsSafe)
        {
            Debug.Log("The potion is safe for every cat this round!", this);
        }
        else
        {
            Debug.Log("The potion was not enough to help every cat. No life was lost.", this);
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
