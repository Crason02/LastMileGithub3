using UnityEngine;

public class SwitchSprite : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    private SpriteRenderer sr;
    private bool isFirst = true;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite1;
    }

    // This acts like OnClick()
    public void OnClick()
    {
        sr.sprite = isFirst ? sprite2 : sprite1;
        isFirst = !isFirst;
    }

    void OnMouseDown()
    {
        OnClick(); // call it when clicked
    }
}
