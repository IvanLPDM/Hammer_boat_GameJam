using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [Header("Wave")]
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float speed = 1f;
    public Vector2 direction = new Vector2(1, 0);

    public float WaterHeight(Vector3 worldPos)
    {
        float wave =
            Mathf.Sin(Vector2.Dot(new Vector2(worldPos.x, worldPos.z), direction.normalized)
            * frequency + Time.time * speed) * amplitude;

        return transform.position.y + wave;
    }
}