using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Music")]
    public AudioSource[] musica;
    private int audioActI = 0;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioSource dashAudioSource;
    public AudioClip[] clips;
    public AudioClip dash;

    [Header("Pitch")]
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    void Start()
    {
        if (musica.Length == 0) return;

        //Inicializaci�n para evitar problemas
        for (int i = 0; i < musica.Length; ++i)
        {
            musica[i].Play();
            musica[i].Stop();
        }

        audioActI = 0;
        musica[audioActI].Play();
    }

    void Update()
    {
        if (audios.Length == 0) return;
        if (!audios[audioActI].isPlaying)
        {
            audioActI++;
            if (audioActI == audios.Length) audioActI = 0;
            audios[audioActI].Play();
        }
    }



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
}
