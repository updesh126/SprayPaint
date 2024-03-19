using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Recenter : MonoBehaviour
{
    public XROrigin rOrigin;
    public Transform target;
    private void Start()
    {
        rOrigin.transform.position = target.position;
        rOrigin.transform.rotation = target.rotation;
    }
}
