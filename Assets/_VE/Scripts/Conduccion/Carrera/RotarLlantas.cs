using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarLlantas : MonoBehaviour
{
    public Transform[] llantasDelanteras; // Referencia a las llantas delanteras
    public Transform llantaTrasera;  // Referencia a la llantas traseras

    private Vector3 posInicial;

    void Start()
    {
        posInicial = transform.position; // Guardamos la posici�n inicial del veh�culo
    }

    void Update()
    {
        if (llantasDelanteras != null && llantaTrasera != null)
        {
            // Calcular la distancia que ha recorrido el veh�culo desde el �ltimo frame y tomamos la ra�z cuadrada de `sqrMagnitude` 
            float distanceTravelled = Mathf.Sqrt((transform.position - posInicial).sqrMagnitude);

            // Determinar si el veh�culo est� yendo en reversa comparando posiciones Vector.Dot calcula el punto de direccion entre dos vectores
            float direccion = Vector3.Dot(transform.forward, transform.position - posInicial);

            // Si la direcci�n es menor que cero, le asigno -1, sino le asigno 1
            float sentidoRotacion = direccion < 0 ? -1 : 1;

            // Guardar la nueva posici�n para el pr�ximo c�lculo
            posInicial = transform.position;

            // Calcular la rotaci�n de la llanta en funci�n de la distancia recorrida 
            float rotacionLlanta = (distanceTravelled * 360f) * sentidoRotacion ;

            // Rotar las llantas delanteras y trasera alrededor de su eje X
            foreach (Transform llanta in llantasDelanteras)
            {
                llanta.Rotate(Vector3.right, rotacionLlanta);
            }

            llantaTrasera.Rotate(Vector3.right, rotacionLlanta);
        }
        else
        {
            Debug.LogError("Falta inicializar componentes");
        }
    }
}
