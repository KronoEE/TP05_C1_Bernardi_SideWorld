using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source --------")]
    [SerializeField] private AudioSource musicSource;

    [Header("-------- Audio Clip --------")]
    public AudioClip backgroundMusic;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void Stop()
    {
        musicSource.Stop();
    }
}
