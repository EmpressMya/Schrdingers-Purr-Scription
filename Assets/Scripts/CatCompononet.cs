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
    public int minGreen;
    public int maxGreen;
}

public class CatCompononet : MonoBehaviour
{
    public CatData catinfo;
    public ProblemData probleminfo;
}