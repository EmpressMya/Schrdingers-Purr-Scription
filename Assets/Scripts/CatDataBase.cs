
using UnityEngine;

public enum PotionColor
{
    Red,
    Yellow,
    Blue,
    Green,
    Magenta
}
[CreateAssetMenu(fileName = "CatDataBase", menuName = "Scriptable Objects/CatDataBase")]

[System.Serializable]
public class CatDataBase : ScriptableObject
{
    [Header("Cat Information")]
    public string catName;
    public Sprite catSprite;

    [Header("Problem Information")]
    public string ProblemDescription;
    public Sprite problemIcon;

    [Header("Required Material")]
    public PotionColor requiredMaterial;
    public int requiredAmount;

    [Header("Death Material")]
    public PotionColor deathMaterial;
    public int deathAmount;

    [Header("Tolerance")]
    public int tolerance = 1;
}

