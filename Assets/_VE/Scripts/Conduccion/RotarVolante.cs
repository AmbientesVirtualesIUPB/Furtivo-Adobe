using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarVolante : MonoBehaviour
{
    public Transform    pivote;         // Pivote central
    public float        velocidad = 100f;  // Velocidad de rotación
    public float        rotacionMaxima = 30f;  // Rotación máxima en grados
    public float        velocidadRetorno = 70f; // Velocidad a la que vuelve a la posición inicial
    private float       anguloActual = 0.0f; // Ángulo actual de rotación en grados
    private float       anguloInicial = 0.0f; // Ángulo inicial de rotación en grados

    /// <summary>
    /// Metodo invocado al iniciar la scena
    /// </summary>
    void Start()
    {
        // Guardar el ángulo inicial
        // eulerAngles se usa para obtener o establecer la rotación de un objeto en términos de ángulos de Euler
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
                cambioRotacion = velocidad * Time.deltaTime; // Rotación hacia la izquierda
            }
            else if (Input.GetKey(KeyCode.D))
            {
                cambioRotacion = -velocidad * Time.deltaTime; // Rotación hacia la derecha
            }

            // Calcular el nuevo ángulo
            float nuevoAngulo = anguloActual + cambioRotacion;

            // Asegurarse de que el nuevo ángulo esté dentro del rango permitido
            if (nuevoAngulo > rotacionMaxima)
            {
                nuevoAngulo = rotacionMaxima;
            }
            else if (nuevoAngulo < -rotacionMaxima)
            {
                nuevoAngulo = -rotacionMaxima;
            }

            // Aplicar la rotación al pivote en el eje Z = forward
            pivote.Rotate(Vector3.forward, nuevoAngulo - anguloActual);

            // Actualizar el ángulo actual
            anguloActual = nuevoAngulo;

            // Cuando no se presiona ninguna tecla, volver a la posición inicial
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                // Calcular el ángulo de retorno hacia el ángulo inicial
                float anguloDeseado = anguloInicial;
                // Mathf.DeltaAngle calcula la diferencia entre dos ángulos, teniendo en cuenta el valor de la diferencia más corta entre ellos en un círculo de 360 grados
                float anguloDistancia = Mathf.DeltaAngle(anguloActual, anguloDeseado);
                // Calculamos el paso de retorno multiplicado por la velocidad a la cual queremos retornar
                float retorno = velocidadRetorno * Time.deltaTime;

                // Comprobamos si la distancia angular es mayor que el paso de retorno. Si es así, movemos el ángulo actual un paso hacia el ángulo deseado
                // Mathf.Abs devuelve el valor absoluto de un número. El valor absoluto de un número es la distancia de ese número desde cero en la recta numérica
                if (Mathf.Abs(anguloDistancia) > retorno)
                {
                    // Mathf.Sign devuelve el signo de un número.Indica si el número es positivo, negativo o cero en la direccion correcta hacia el angulo deseado
                    anguloActual += Mathf.Sign(anguloDistancia) * retorno;
                }
                else
                {
                    // Si la distancia angular es menor o igual al paso de retorno ajusta directamente al ángulo deseado
                    anguloActual = anguloDeseado;
                }
                // Aplicar la rotación para volver a la posición inicial
                pivote.Rotate(Vector3.forward, anguloActual - pivote.localRotation.eulerAngles.z);
            }
        }
    }
}
