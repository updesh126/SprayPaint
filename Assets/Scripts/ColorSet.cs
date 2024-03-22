using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSet : MonoBehaviour
{
    public Color[] color;
    public SprayTexturePainter painter;
    public SmokeColor smokeColor;
    // Start is called before the first frame update
    private void Start()
    {
        painter.SelectColor(Color.red);
        smokeColor.UpdateColor(Color.red);
    }
    public void SetColor(int i)
    {
        painter.SelectColor(color[i]);
        smokeColor.UpdateColor(color[i]);
    }
}
