using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarLlantas : MonoBehaviour
{
    

    // Velocidad de rotación en grados por segundo
    public float velocidadRotacion = 50f;
    public bool rotarEnZ, rotarEnY, rotarEnX;

    Vector3 p1, p2;
    public float velocidad;
    private void Start()
    {
        p1 = transform.position;
        p2 = p1;
        InvokeRepeating("ActualizarVelocidad", 0.5f, 0.5f);
    }

    void ActualizarVelocidad()
    {
        velocidad = (p1 - p2).sqrMagnitude;
        p1 = p2;
        p2 = transform.position;
    }

    /// <summary>
    /// Metodo invocado frame a frame
    /// </summary>
    void Update()
    {

        transform.Rotate(velocidadRotacion * velocidad * Time.deltaTime, 0, 0);

    }
}
