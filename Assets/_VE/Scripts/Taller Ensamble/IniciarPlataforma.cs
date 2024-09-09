using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class IniciarPlataforma : MonoBehaviour
{
    public GameObject   brazo; // El objeto a desplazar
    public Animator     plataforma; // Referencia a las puertas de la plataforma para utilizar su componenete animator
    public Transform[]  posiciones; // Los objetivos a los que queremos movernos
    public float        tiempoSuavizado = 1f; // Tiempo de suavizado para la interpolacion
    private Vector3     velocidad;           // Velocidad de interpolación
    private float       tiempoTranscurrido; // Variable para controlar el tiempo en transiciones
    private float       tiempoTotal = 0.0f; // Variable para controlar el tiempo en transiciones

    private void Awake()
    {
        if (brazo == null || plataforma == null || posiciones == null)
        {
            Debug.LogError("Falta inicializar componenetes del script IniciarPlataforma");
        }

    }


    /// <summary>
    /// Metodo invocado frame a frame
    /// </summary>
    void Update()
    {
        // Controlamos el flujo de tiempo para las transiciones
        tiempoTranscurrido += Time.deltaTime;
    }

    /// <summary>
    /// Funcion para iniciar la currutina del movimiento suave hacia arriba
    /// </summary>
    [ContextMenu("arriba")]
    public void MoverArriba()
    {
        StopAllCoroutines();
        StartCoroutine(MovimientoSuavePlataforma(0));
    }


    /// <summary>
    /// Funcion para iniciar la currutina del movimiento suave hacia abajo
    /// </summary>
    [ContextMenu("abajo")]
    public void MoverAbajo()
    {
        StopAllCoroutines();
        StartCoroutine(MovimientoSuavePlataforma(1));
    }


    /// <summary>
    /// Currutina para iniciar el movimiento suave
    /// </summary>
    /// <param name="posicion"> Indica a que posicon nos vamos a mover</param>
    IEnumerator MovimientoSuavePlataforma(int posicion)
    {
        // Validamos si se esta abriendo la compuerta
        if (posicion == 0)
        {
            tiempoTotal = 10.0f; // Duración total del ciclo en segundos

            // Activamos la animación deseada
            plataforma.SetBool("open", true);
            plataforma.SetBool("close", false);
            // Damos una pequeña espera despues de abrir la compuerta
            yield return new WaitForSeconds(2f);

        }else if (posicion == 1)
        {
            tiempoTotal = 7.0f; // Duración total del ciclo en segundos
        }

        tiempoTranscurrido = 0.0f; // Reiniciamos el flujo de tiempo

        // Mientras la distancia entre la posición actual y la posición objetivo sea menor
        while (tiempoTranscurrido < tiempoTotal)
        {
            // Aplica el movimiento suave, SmoothDamp se utiliza para suavizar la transición de un valor hacia un objetivo
            brazo.gameObject.transform.position = Vector3.SmoothDamp(brazo.gameObject.transform.position, posiciones[posicion].position, ref velocidad, tiempoSuavizado);

            // Pausa el ciclo hasta el siguiente frame
            yield return null;

            // Validamos si se esta cerrando la compuerta y si el tiempo transcurrido son 4 segundos
            if (posicion == 1 && tiempoTranscurrido > 3.0f)
            {
                plataforma.SetBool("open", false);
                plataforma.SetBool("close", true);
            }
        }
        // Una vez que se alcanza la posición objetivo, nos aseguramos de que el objeto esté exactamente en la posición objetivo
        brazo.gameObject.transform.position = posiciones[posicion].position;
    }  
}
