using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TMP_Text scoretxt;
    private int score = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoretxt.SetText("Cats Saved: " + score);
    }

    public void AddScore()
    {
        score++;
        scoretxt.SetText("Cats Saved: " + score);
    }

    public void ResetScore()
    {
        score = 0;
        scoretxt.SetText("Cats Saved: " + score);
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
