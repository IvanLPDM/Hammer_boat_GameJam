using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Music")]
    public AudioSource[] musica;
    private int audioActI = 0;

    [Header("AudioDash")]
    public AudioSource audioSourceHammer;
    public AudioClip[] clipsHammer;
    public AudioSource dashAudioSource;

    [Header("AudioBucle")]
    public AudioSource water;
    public AudioSource gaviolas, madera;

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
        if (musica.Length == 0) return;
        if (!musica[audioActI].isPlaying)
        {
            audioActI++;
            if (audioActI == musica.Length) audioActI = 0;
            musica[audioActI].Play();
        }
    }



    public void PlayHammer()
    {
        if (clipsHammer.Length == 0) return;

        audioSourceHammer.pitch = Random.Range(minPitch, maxPitch);
        audioSourceHammer.clip = clipsHammer[Random.Range(0, clipsHammer.Length)];
        audioSourceHammer.Play();
    }

    public void PlayDash()
    {
        dashAudioSource.Play();
    }
}
