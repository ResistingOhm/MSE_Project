using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    private AudioSource musicAudio;

    public AudioClip title;
    public AudioClip levelOne;
    public AudioClip levelTwo;
    // Start is called before the first frame update
    void Awake()
    {
        if (soundManager != null && soundManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            soundManager = GetComponent<SoundManager>();
            DontDestroyOnLoad(this.gameObject); // Makes the gameobject survive across scene changes
        }

        musicAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {

    }
    
    public void SetTitleBGM()
    {
        musicAudio.clip = title;
        musicAudio.Play();
    }

    public void SetLevelOneBGM()
    {
        musicAudio.clip = levelOne;
        musicAudio.Play();
    }

    public void SetLevelTwoBGM()
    {
        musicAudio.clip = levelTwo;
        musicAudio.Play();
    }
}
