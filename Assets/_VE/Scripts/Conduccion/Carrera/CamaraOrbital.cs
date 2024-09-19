using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraOrbital : MonoBehaviour
{
    public Transform objetivo; // El objeto que la c�mara estar� enfocando (Padre)
    public float velocidadOrbita = 10.0f; // Velocidad de rotaci�n alrededor del objeto

    void Update()
    {
        if (objetivo != null)
        {
            // Rotar la c�mara alrededor del objeto en el eje Y
            transform.RotateAround(objetivo.position, Vector3.up, velocidadOrbita * Time.deltaTime);

            // Hacer que la c�mara siempre mire hacia el objeto
            //transform.LookAt(objetivo);
        }
    }
}
