using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSystem : MonoBehaviour
{
    public AudioManager manager;
    public void NewScene(string name)
    {
        //manager.AudioBool(isplay);
        SceneManager.LoadScene(name);
    }
}
