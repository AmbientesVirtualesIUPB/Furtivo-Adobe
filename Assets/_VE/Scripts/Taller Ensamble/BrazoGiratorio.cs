using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrazoGiratorio : MonoBehaviour
{
    // Velocidad de rotación en grados por segundo
    public float velocidadRotacion = 100f;
    private bool rotarDerecha = false;
    private bool rotarizquierda = false;


    /// <summary>
    /// Metodo invocado frame a frame
    /// </summary>
    void Update()
    {
        if (rotarDerecha)
        {
            // Rotar hacia la derecha (sentido horario)
            transform.Rotate(0, velocidadRotacion * Time.deltaTime, 0);
        }
        else if (rotarizquierda)
        {
            // Rotar hacia la izquierda (sentido antihorario)
            transform.Rotate(0, -velocidadRotacion * Time.deltaTime, 0);
        }
    }

    /// <summary>
    /// Métodos para iniciar la rotación en sentido a las manecillas del reloj desde ButtonDerecha en la scena
    /// </summary>
    /// <param name="data"></param>
    public void RotarDerechaPresionada(BaseEventData data)
    {
        rotarDerecha = true;
        rotarizquierda = false;
    }

    /// <summary>
    /// Métodos para iniciar la rotación en sentido contrario a las manecillas del relojdesde ButtonIzquierda en la scena
    /// </summary>
    /// <param name="data"></param>
    public void RotarIzquierdaPresionada(BaseEventData data)
    {
        rotarDerecha = false;
        rotarizquierda = true;
    }

    /// <summary>
    /// Metodo invocado al dejar de presionar los botones del canvas desde Event Trigger
    /// </summary>
    /// <param name="data"></param>
    public void NoRotar(BaseEventData data)
    {
        rotarDerecha = false;
        rotarizquierda = false;
    }

    /// <summary>
    /// Metodo invocado desde el script InicializarFurtivo, para detener movimientos
    /// </summary>
    public void Detener()
    {
        // Dejamos las rotaciones en cero
        transform.rotation = Quaternion.identity;
    }
}
