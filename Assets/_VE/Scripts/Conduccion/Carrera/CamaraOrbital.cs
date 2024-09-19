using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraOrbital : MonoBehaviour
{
    public Transform objetivo; // El objeto que la cámara estará enfocando (Padre)
    public float velocidadOrbita = 10.0f; // Velocidad de rotación alrededor del objeto

    void Update()
    {
        if (objetivo != null)
        {
            // Rotar la cámara alrededor del objeto en el eje Y
            transform.RotateAround(objetivo.position, Vector3.up, velocidadOrbita * Time.deltaTime);

            // Hacer que la cámara siempre mire hacia el objeto
            //transform.LookAt(objetivo);
        }
    }
}
