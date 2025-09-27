using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteSwitcher : MonoBehaviour
{
    public Image targetImage;    // the UI Image we’ll change
    public Sprite firstSprite;   // first sprite
    public Sprite secondSprite;  // second sprite

    private bool isFirstActive = true;

    public void SwitchSprite()
    {
        if (targetImage == null) return;

        // Toggle the sprite
        targetImage.sprite = isFirstActive ? secondSprite : firstSprite;
        isFirstActive = !isFirstActive;
    }
}
