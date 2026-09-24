using UnityEngine;

public class RandomColorGenerator : MonoBehaviour
{
    // List of colors set in the inspector 
    public Color[] colorPalette;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        
        // Random color time
        ApplyRandomColor();
    }

    public void ApplyRandomColor()
    {
        // Make sure I'm not dumb and actually added colors
        if (objectRenderer != null && colorPalette != null && colorPalette.Length > 0)
        {
            // Random Index between 0 and my color palette
            int randomIndex = Random.Range(0, colorPalette.Length);
            
            // This will assign the selected color
            objectRenderer.material.color = colorPalette[randomIndex];
        }
        else
        {
            Debug.LogWarning("No Color Assigned, Pick in Inspector");
        }
    }
}