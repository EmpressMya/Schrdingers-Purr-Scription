using UnityEngine;

[System.Serializable]
public class CatData
{
    public string catName;
    public Sprite catSprite;
}

[System.Serializable]
public class ProblemData
{
    public string ProblemDescription;
    public Sprite problemIcon;

    public PotionColor requiredMaterial;
    public int requiredAmount;

    public PotionColor deathMaterial;
    public int deathAmount;

    public int tolerance;
}

public class CatCompononet : MonoBehaviour
{
    public CatData catinfo;
    public ProblemData probleminfo;
}

