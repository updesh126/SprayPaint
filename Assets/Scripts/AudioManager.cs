using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static bool islevel1 = true;
    private static bool islevel2 = true;
    private static bool islevel3 = true;
    public AudioSource m_AudioSource;
    // Start is called before the first frame update
    public enum SelectMode
    {
        Level0,
        Level1,
        Level2
    }
    public SelectMode m_SelectMode;
    private void Start()
    {
        
        
    }
    private void Update()
    {
        if (islevel1 == false)
        {
            m_AudioSource.Stop();
            Debug.Log("stope");
        }
        else if (islevel2 == false)
        {
            m_AudioSource.Stop();
        }
        else if (islevel3 == false)
        {
            m_AudioSource.Stop();
        }
    }
    public void AudioBool()
    {
        if(m_SelectMode == SelectMode.Level1 )
        {
            islevel1 = false;
        }
        else if(m_SelectMode == SelectMode.Level2)
        {
            islevel2 = false;
        }
        else if(m_SelectMode == SelectMode.Level2)
        {
            islevel3 = false;
        }

    }
}
