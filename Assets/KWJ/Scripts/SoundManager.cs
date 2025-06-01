using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    private AudioSource audio;

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
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    
    public void SetTitleBGM()
    {
        audio.clip = title;
        audio.Play();
    }

    public void SetLevelOneBGM()
    {
        audio.clip = levelOne;
        audio.Play();
    }

    public void SetLevelTwoBGM()
    {
        audio.clip = levelTwo;
        audio.Play();
    }
}
