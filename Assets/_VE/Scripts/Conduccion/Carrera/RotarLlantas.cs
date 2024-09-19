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
        posInicial = transform.position; // Guardamos la posición inicial del vehículo
    }

    void Update()
    {
        if (llantasDelanteras != null && llantaTrasera != null)
        {
            // Calcular la distancia que ha recorrido el vehículo desde el último frame y tomamos la raíz cuadrada de `sqrMagnitude` 
            float distanceTravelled = Mathf.Sqrt((transform.position - posInicial).sqrMagnitude);

            // Determinar si el vehículo está yendo en reversa comparando posiciones Vector.Dot calcula el punto de direccion entre dos vectores
            float direccion = Vector3.Dot(transform.forward, transform.position - posInicial);

            // Si la dirección es menor que cero, le asigno -1, sino le asigno 1
            float sentidoRotacion = direccion < 0 ? -1 : 1;

            // Guardar la nueva posición para el próximo cálculo
            posInicial = transform.position;

            // Calcular la rotación de la llanta en función de la distancia recorrida 
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
