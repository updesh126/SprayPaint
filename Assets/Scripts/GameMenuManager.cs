using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public Transform Head;
    public float spawndistance = 2;
    public GameObject menu;
    public InputActionProperty ShowButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShowButton.action.WasPerformedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = Head.position + new Vector3(Head.forward.x,0,Head.forward.z).normalized * spawndistance;
        }  
        menu.transform.LookAt(new Vector3(Head.position.x, menu.transform.position.y, Head.position.z));
        menu.transform.forward *= -1;
    }
}
