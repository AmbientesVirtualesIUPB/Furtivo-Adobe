using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTerceraPersona : MonoBehaviour
{
    public Transform    vehiculo; // El veh�culo al que la c�mara seguir�
    public float        velocidadRotacion = 5f; // Velocidad de rotaci�n de la c�mara, se puede modificar a gusto
    public float        velocidadPosicion = 20f; // Velocidad de posicion de la c�mara, se puede modificar a gusto
    public Vector3      poscionInicial; // Posicion inicial de la c�mara

    /// <summary>
    /// Metodo utilizado que se actualiza frame a frame para mantener la fisica actualizada correctamente
    /// </summary>
    private void FixedUpdate()
    {
        if (vehiculo != null)
        {
            // Calcula la posici�n deseada de la c�mara
            Vector3 posicionDeseada = vehiculo.position + vehiculo.rotation * poscionInicial;

            // Lerp Interpola suavemente la posici�n de la c�mara
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, Time.deltaTime * velocidadPosicion);

            // LookRotation Calcula la rotaci�n objetivo de la c�mara basada en la rotaci�n del veh�culo
            Quaternion rotacionDeseada = Quaternion.LookRotation(vehiculo.position - transform.position);

            // Slerp Interpolamos suavemente la rotaci�n de la c�mara para seguir suavemente la forma esferica
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * velocidadRotacion);
        }
        else
        {
            Debug.LogWarning("Asignar vehiculo en el inspector.");
        }
    }
}
