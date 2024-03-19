using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritManager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer.sprite = sprite1;
    }
    public void SpriteUpdate( Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
