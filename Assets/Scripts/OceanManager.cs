using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public GameObject water;
    public int rows = 100, cols = 100;
    public float offset = 10;
    public float initx = 0, initz = 0, inity = 0;
    private Vector3 initPos = new Vector3(0, 0, 0);

    private void GenerateMap()
    {
        for (int i = 0; i < rows; ++i)
        {
            float x = initx + i * offset;
            for (int j = 0; j < cols; ++j)
            {
                float z = initz + j * offset;
                Instantiate(water, new Vector3(x, inity, z), Quaternion.identity);
            }
        }
    }

    void Start()
    {
        GenerateMap();
    }
}
