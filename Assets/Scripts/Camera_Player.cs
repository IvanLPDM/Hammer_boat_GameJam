using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Player : MonoBehaviour
{
    public Transform target;
    public Vector3 _cameraOffset;

    [Header("Rotation")]
    public bool RotateAroundPlayer = true;
    public float RotationSpeed;
    public float SmoothFactor;




    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Quaternion camTurnAngle =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);

            _cameraOffset = camTurnAngle * _cameraOffset;
        }

        Vector3 newPos = target.transform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        transform.LookAt(target);
    }
}
