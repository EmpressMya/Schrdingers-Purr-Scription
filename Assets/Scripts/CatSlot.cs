using UnityEngine;
using UnityEngine.UI;

public class CatSlot : MonoBehaviour
{
    public Image catImage;
    public Text nameText;

    [HideInInspector] public CatData assignedCat;
    [HideInInspector] public ProblemData assignedProblem;

    public void SetupSlot(CatData cat, ProblemData problem)
    {
        assignedCat = cat;
        assignedProblem = problem;

        catImage.sprite = cat.catSprite;
        if (nameText != null)
        {
            nameText.text = cat.catName;
        }
    }

}
