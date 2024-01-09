using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sm;
    public AudioSource audioSource;
    public AudioClip starSound, diamondSound, gameOverSound, gameStartSound;

    private void Awake()
    {
        if (sm == null)
        {
            sm = this;
        }
    }

    public void StarSound()
    {
        audioSource.clip = starSound;
        audioSource.Play();

    } // End StarSound

    public void DiamondSound()
    {
        audioSource.clip = diamondSound;
        audioSource.Play();
    } // End DiamondSound

    public void GameEndSound()
    {
        audioSource.clip = gameOverSound;
        audioSource.Play();
    } // End GameOverSound

    public void GameStartSound()
    {
        audioSource.clip = gameStartSound;
        audioSource.Play();
    } // End GameStartSound
}
