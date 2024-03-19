using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gxanimation : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
       // animator = GetComponent<Animator>();
    }
    public void PlayAnimation(bool updateState)
    {
        Debug.Log("clicked");
        animator.SetBool("isPlay", updateState);
    }
}
