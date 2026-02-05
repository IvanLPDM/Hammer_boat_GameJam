using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public GameObject water;
    public int rows = 100, cols = 100;
    public float offset = 10;
    private Vector3 initPos = new Vector3(0, 0, 0);

    private void GenerateMap()
    {
        for (int i = 0; i < rows; ++i)
        {
            float x = i * offset;
            for (int j = 0; j < cols; ++j)
            {
                float z = j * offset;
                Instantiate(water, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }

    void Start()
    {
        GenerateMap();
    }
}
