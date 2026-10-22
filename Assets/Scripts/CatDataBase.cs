using UnityEngine;

[CreateAssetMenu(fileName = "CatDataBase", menuName = "Scriptable Objects/CatDataBase")]
public class CatDataBase : ScriptableObject
{
    public string catName;
    public Sprite catSprite;
    public string ProblemDescription;
    public Sprite problemIcon;
    public int minGreen;
    public int maxGreen;
}
