using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;
public class tutorialtext : MonoBehaviour
{
    [SerializeField]private GameObject book;
    private bool isBookOpen = false;

    [SerializeField]private TextMeshProUGUI text;

    [TextArea(3, 10)]
    [SerializeField]private string[] pages =
    {
       "Page 1: Instructions: Move left (Left button or A) and right (right button or D).",
       "Page 2: Press E to open/close the book",
       "Page 3: that's it",
       "Page 4: Adios!"
    };

    private int PgNo = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowPg();
    }

    public void NxtPg()
    {
        if (PgNo < pages.Length - 1)
        {
            PgNo++;
            ShowPg();
        }
    }

    public void PrevPg()
    {
        if (PgNo > 0)
        {
            PgNo--;
            ShowPg();
        }
    }
    void ShowPg()
    {
        if (text != null && pages.Length > 0)
        {
            text.text = pages[PgNo];
        }
    }
    void ToggleBook()
    {
        isBookOpen = !isBookOpen;
        book.SetActive(isBookOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleBook();
        }

        if (!isBookOpen)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D))
        {
            NxtPg();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A))
        {
            PrevPg();
        }
    }
}
