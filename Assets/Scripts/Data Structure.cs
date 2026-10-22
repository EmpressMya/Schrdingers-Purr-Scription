using UnityEngine;

[CreateAssetMenu(fileName = "CatData", menuName = "Scriptable Objects/CatData")]
public class CatData : ScriptableObject
{
    public string catName;
    public Sprite catSprite;
}


[CreateAssetMenu(fileName = "NewProblemData", menuName = "Game/Problem Data")]
public class ProblemData : ScriptableObject
{
    public string ProblemDescription;
    public Sprite problemIcon;
    public string correctTreatment;
}