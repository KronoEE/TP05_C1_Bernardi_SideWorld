using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("-------- Audio Clip --------")]
    public AudioClip backgroundMusic;
    public AudioClip coinsSfx;
    public AudioClip jumpSfx;
    public AudioClip WinSfx;
    public AudioClip LooseSfx;
    public AudioClip ShootSfx;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void Stop()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
