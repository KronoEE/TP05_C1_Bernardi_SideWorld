using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip deathClip;

    public void playJumpSound()
    {
        audioSource.PlayOneShot(jumpClip);
    }
    public void playDeathSound()
    {
        audioSource.PlayOneShot(deathClip);
    }
}
