using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] audios;
    private int audioActI = 0;

    void Start()
    {
        if (audios.Length == 0) return;

        //Inicialización para evitar problemas
        for (int i = 0; i < audios.Length; ++i)
        {
            audios[i].Play();
            audios[i].Stop();
        }

        audioActI = 0;
        audios[audioActI].Play();
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
}
