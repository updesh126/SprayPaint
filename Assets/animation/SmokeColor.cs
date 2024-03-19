using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeColor : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public Material mat;
    public ParticleSystem pat;

    private void Update()
    {
        pat.startColor= fcp.color;
        
       mat.color = fcp.color;
    }
}
