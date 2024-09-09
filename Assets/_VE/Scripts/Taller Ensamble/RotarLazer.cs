using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarLazer : MonoBehaviour
{
    public float        anguloRotacion = 45f; // El �ngulo m�ximo de rotaci�n en grados
    public float        velocidadRotacion = 30f; // Velocidad de rotaci�n
    private Quaternion  rotacionInicial; // Rotaci�n inicial del objeto padre
    private bool        pueroRotar = true; // Direcci�n de rotaci�n

    void Start()
    {
        // Guardar la rotaci�n inicial del objeto padre
        rotacionInicial = transform.localRotation;
    }

    /// <summary>
    /// Invocado frame a fram, despues de todos los update, en este caso para ejecutar correctamente la rotacion
    /// </summary>
    private void LateUpdate()
    {
        RotateObject();
    }

    /// <summary>
    /// Metodo utilizado para la rotacion del lazer que escanea el vehiculo antes de su personalizacion
    /// </summary>
    void RotateObject()
    {
        float angle = Quaternion.Angle(rotacionInicial, transform.localRotation);

        if (pueroRotar)
        {
            // Rotar hacia adelante
            transform.Rotate(Vector3.forward * velocidadRotacion * Time.deltaTime);
            // Si es mayor o igual al angulo que asignamos
            if (angle >= anguloRotacion)
            {
                pueroRotar = false;
            }
        }
        else
        {
            // Rotar hacia atr�s
            transform.Rotate(Vector3.forward * - velocidadRotacion * Time.deltaTime);
            // Si es menor o igual al angulo que asignamos
            if (angle <= 1)
            {
                pueroRotar = true;
                transform.localRotation = rotacionInicial; // Reset a la rotaci�n inicial exacta
            }
        }
    }
}
