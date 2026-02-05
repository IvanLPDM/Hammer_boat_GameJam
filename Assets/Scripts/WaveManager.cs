using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;

    public float GetWaveHeight(float _x, float _z)
    {
        //return amplitude * Mathf.Sin((_x + _z)/ length + offset);
        return - amplitude * Mathf.Sin(_x / length + offset);
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists");
            Destroy(this);
        }
    }
}
