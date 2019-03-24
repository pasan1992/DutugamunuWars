using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] slashSounds;
    public AudioClip[] hitSounds;
    public AudioClip[] blockSounds;
    public AudioClip[] fallSounds;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void playSlathSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(slashSounds[getRandomValue(slashSounds.Length)]);
    }

    public void playHitSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(hitSounds[getRandomValue(hitSounds.Length)]);
    }

    public void playBlockSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(blockSounds[getRandomValue(blockSounds.Length)]);
    }

    public void playFallSound()
    {
        Debug.Log("fall sound");
        audioSource.PlayOneShot(fallSounds[getRandomValue(fallSounds.Length)]);
    }

    private int getRandomValue(int size)
    {
        float randomFloat = Random.Range(0, size);
        return (int)Mathf.Floor(randomFloat);
    }

    
}
