using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarVolante : MonoBehaviour
{
    public Transform    pivote;         // Pivote central
    public float        velocidad = 100f;  // Velocidad de rotaci�n
    public float        rotacionMaxima = 30f;  // Rotaci�n m�xima en grados
    public float        velocidadRetorno = 70f; // Velocidad a la que vuelve a la posici�n inicial
    private float       anguloActual = 0.0f; // �ngulo actual de rotaci�n en grados
    private float       anguloInicial = 0.0f; // �ngulo inicial de rotaci�n en grados

    /// <summary>
    /// Metodo invocado al iniciar la scena
    /// </summary>
    void Start()
    {
        // Guardar el �ngulo inicial
        // eulerAngles se usa para obtener o establecer la rotaci�n de un objeto en t�rminos de �ngulos de Euler
        anguloInicial = pivote.localRotation.eulerAngles.z;
    }

    /// <summary>
    /// Metodo invocado frame a frame
    /// </summary>
    void Update()
    {
        if (pivote != null)
        {
            // Inicializamos en cero el cambio en las rotaciones
            float cambioRotacion = 0.0f;

            // Verificar si se presionan las teclas A o D
            if (Input.GetKey(KeyCode.A))
            {
                cambioRotacion = velocidad * Time.deltaTime; // Rotaci�n hacia la izquierda
            }
            else if (Input.GetKey(KeyCode.D))
            {
                cambioRotacion = -velocidad * Time.deltaTime; // Rotaci�n hacia la derecha
            }

            // Calcular el nuevo �ngulo
            float nuevoAngulo = anguloActual + cambioRotacion;

            // Asegurarse de que el nuevo �ngulo est� dentro del rango permitido
            if (nuevoAngulo > rotacionMaxima)
            {
                nuevoAngulo = rotacionMaxima;
            }
            else if (nuevoAngulo < -rotacionMaxima)
            {
                nuevoAngulo = -rotacionMaxima;
            }

            // Aplicar la rotaci�n al pivote en el eje Z = forward
            pivote.Rotate(Vector3.forward, nuevoAngulo - anguloActual);

            // Actualizar el �ngulo actual
            anguloActual = nuevoAngulo;

            // Cuando no se presiona ninguna tecla, volver a la posici�n inicial
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                // Calcular el �ngulo de retorno hacia el �ngulo inicial
                float anguloDeseado = anguloInicial;
                // Mathf.DeltaAngle calcula la diferencia entre dos �ngulos, teniendo en cuenta el valor de la diferencia m�s corta entre ellos en un c�rculo de 360 grados
                float anguloDistancia = Mathf.DeltaAngle(anguloActual, anguloDeseado);
                // Calculamos el paso de retorno multiplicado por la velocidad a la cual queremos retornar
                float retorno = velocidadRetorno * Time.deltaTime;

                // Comprobamos si la distancia angular es mayor que el paso de retorno. Si es as�, movemos el �ngulo actual un paso hacia el �ngulo deseado
                // Mathf.Abs devuelve el valor absoluto de un n�mero. El valor absoluto de un n�mero es la distancia de ese n�mero desde cero en la recta num�rica
                if (Mathf.Abs(anguloDistancia) > retorno)
                {
                    // Mathf.Sign devuelve el signo de un n�mero.Indica si el n�mero es positivo, negativo o cero en la direccion correcta hacia el angulo deseado
                    anguloActual += Mathf.Sign(anguloDistancia) * retorno;
                }
                else
                {
                    // Si la distancia angular es menor o igual al paso de retorno ajusta directamente al �ngulo deseado
                    anguloActual = anguloDeseado;
                }
                // Aplicar la rotaci�n para volver a la posici�n inicial
                pivote.Rotate(Vector3.forward, anguloActual - pivote.localRotation.eulerAngles.z);
            }
        }
    }
}
