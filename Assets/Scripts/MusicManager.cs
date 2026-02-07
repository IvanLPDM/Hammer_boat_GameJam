using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioSource dashAudioSource;
    public AudioClip[] clips;
    public AudioClip dash;

    [Header("Pitch")]
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;



    public void PlayHammer()
    {
        if (clips.Length == 0) return;

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
    }

    public void PlayDash()
    {
        dashAudioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
