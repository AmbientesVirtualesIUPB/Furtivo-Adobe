using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTerceraPersona : MonoBehaviour
{
    public Transform    vehiculo; // El vehículo al que la cámara seguirá
    public float        velocidadRotacion = 5f; // Velocidad de rotación de la cámara, se puede modificar a gusto
    public float        velocidadPosicion = 20f; // Velocidad de posicion de la cámara, se puede modificar a gusto
    public Vector3      poscionInicial; // Posicion inicial de la cámara

    /// <summary>
    /// Metodo utilizado que se actualiza frame a frame para mantener la fisica actualizada correctamente
    /// </summary>
    private void FixedUpdate()
    {
        if (vehiculo != null)
        {
            // Calcula la posición deseada de la cámara
            Vector3 posicionDeseada = vehiculo.position + vehiculo.rotation * poscionInicial;

            // Lerp Interpola suavemente la posición de la cámara
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, Time.deltaTime * velocidadPosicion);

            // LookRotation Calcula la rotación objetivo de la cámara basada en la rotación del vehículo
            Quaternion rotacionDeseada = Quaternion.LookRotation(vehiculo.position - transform.position);

            // Slerp Interpolamos suavemente la rotación de la cámara para seguir suavemente la forma esferica
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * velocidadRotacion);
        }
        else
        {
            Debug.LogWarning("Asignar vehiculo en el inspector.");
        }
    }
}
