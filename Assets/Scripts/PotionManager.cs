using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public DeskManager deskManager;
    public int blueValue;
    public int yellowValue;
    public int redValue;
    public int greenValue;
    public int magentaValue;

    public void ResetPotion()
    {
        redValue = 0;
        yellowValue = 0;
        blueValue = 0;
        greenValue = 0;
        magentaValue = 0;
    }

    public void addRed(int amount)
    {
        redValue += amount;
    }

    public void addYellow(int amount)
    {
        yellowValue += amount;
    }

    public void addBlue(int amount)
    {
        blueValue += amount;
    }

    public void addGreen(int amount)
    {
        greenValue += amount;
    }

    public void addMagenta(int amount)
    {
        magentaValue += amount;
    }
}
