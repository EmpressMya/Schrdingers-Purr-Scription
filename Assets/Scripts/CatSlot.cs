using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatSlot : MonoBehaviour
{
    public DeskManager deskManager;

    public GameObject CatObject;

    public TextMeshProUGUI catProblemText;
    public TextMeshProUGUI catNameText;

    public GameObject catNotes;

    public void Start()
    {
        closeNotes();
    }

    public void catProcess()
    {
        CatDataBase pickedCat = deskManager.masterCatPool[
            UnityEngine.Random.Range(0, deskManager.masterCatPool.Count)
        ];

        assignCat(pickedCat);
    }

    public void assignCat(CatDataBase cat)
    {
        CatCompononet catComponent = CatObject.GetComponent<CatCompononet>();

        catComponent.catinfo.catName = cat.catName;
        catComponent.catinfo.catSprite = cat.catSprite;

        catComponent.probleminfo.ProblemDescription = cat.ProblemDescription;
        catComponent.probleminfo.problemIcon = cat.problemIcon;

        catComponent.probleminfo.requiredMaterial = cat.requiredMaterial;
        catComponent.probleminfo.requiredAmount = cat.requiredAmount;

        catComponent.probleminfo.deathMaterial = cat.deathMaterial;
        catComponent.probleminfo.deathAmount = cat.deathAmount;

        catComponent.probleminfo.tolerance = cat.tolerance;

        catProblemText.text = cat.ProblemDescription;
        catNameText.text = cat.catName;

        GameObject instantiatedCat = Instantiate(CatObject, transform);

        deskManager.activeCats.Add(instantiatedCat);
    }

    public void openNotes()
    {
        catNotes.SetActive(true);
    }

    public void closeNotes()
    {
        catNotes.SetActive(false);
    }

    private void OnMouseDown()
    {
        openNotes();
    }

}