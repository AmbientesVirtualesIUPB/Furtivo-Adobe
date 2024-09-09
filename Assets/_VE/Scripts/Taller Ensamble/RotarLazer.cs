using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarLazer : MonoBehaviour
{
    public float        anguloRotacion = 45f; // El ángulo máximo de rotación en grados
    public float        velocidadRotacion = 30f; // Velocidad de rotación
    private Quaternion  rotacionInicial; // Rotación inicial del objeto padre
    private bool        pueroRotar = true; // Dirección de rotación

    void Start()
    {
        // Guardar la rotación inicial del objeto padre
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
            // Rotar hacia atrás
            transform.Rotate(Vector3.forward * - velocidadRotacion * Time.deltaTime);
            // Si es menor o igual al angulo que asignamos
            if (angle <= 1)
            {
                pueroRotar = true;
                transform.localRotation = rotacionInicial; // Reset a la rotación inicial exacta
            }
        }
    }
}
