using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeColor : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public Material mat;

    private void Update()
    {
        mat.color = fcp.color;
    }
}
