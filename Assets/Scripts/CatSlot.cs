using System;
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

    public void catProcess()
    {
        CatDataBase pickedCat = deskManager.masterCatPool[UnityEngine.Random.Range(0, deskManager.masterCatPool.Count)];

        assignCat(pickedCat);
    }

    public void assignCat(CatDataBase cat)
    {
        CatObject.GetComponent<CatCompononet>().catinfo.catName = cat.catName;
        CatObject.GetComponent<CatCompononet>().catinfo.catSprite = cat.catSprite;
        CatObject.GetComponent<CatCompononet>().probleminfo.ProblemDescription = cat.ProblemDescription;
        CatObject.GetComponent<CatCompononet>().probleminfo.problemIcon = cat.problemIcon;
        CatObject.GetComponent<CatCompononet>().probleminfo.maxGreen = cat.maxGreen;
        CatObject.GetComponent<CatCompononet>().probleminfo.minGreen = cat.minGreen;

        catProblemText.text = cat.ProblemDescription;
        catNameText.text = cat.catName;

        GameObject instantiatedCat = Instantiate(CatObject, this.transform);
        deskManager.activeCats.Add(instantiatedCat);
    }


}
