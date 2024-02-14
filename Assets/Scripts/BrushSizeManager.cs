using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushSizeManager : MonoBehaviour
{
    public Slider sizeSlider;
    public SprayTexturePainter painter;
    // Start is called before the first frame update
    public void UpdateSizeSlider()
    {
        painter.SetBrushSize(sizeSlider.value);
    }
}
