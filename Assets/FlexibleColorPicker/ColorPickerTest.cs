using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerTest : MonoBehaviour
{

    public FlexibleColorPicker fcp;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mat.color = fcp.color;
    }
}
