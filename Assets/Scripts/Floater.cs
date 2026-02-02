using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [Header("Physics")]
    public Rigidbody rb;
    public float depthUp; //La profundidad a la que empieza a flotar
    public float displacementAmount; //Fuerza de Bouyant
    public int floaters;

    [Header("floaters")]
    public float waterDrag; //Coeficiente de friccion en el agua
    public float waterAngularDrag;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
