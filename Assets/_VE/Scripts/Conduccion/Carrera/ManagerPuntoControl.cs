using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPuntoControl : MonoBehaviour
{
    public static ManagerPuntoControl   Instance;
    public List<PuntoControl>           controls = new List<PuntoControl>();

    private void Awake()
    {
        Instance = this;
    }
  
    /// <summary>
    /// Metodo invocado desde el script conducir para obtener el punto de referencia mas cercano como checkPoint
    /// </summary>
    /// <param name="punto"> </param>
    /// <returns></returns>
    public Transform ObtenerPuntoCercano(Vector3 punto)
    {
        float distancia = 10000;
        Transform min = controls[0].transform;

        for (int i = 0; i < controls.Count; i++)
        {
            //Validamos la distancia entre nuestro vehiculo y los puntos de guardado
            float d = (punto - controls[i].transform.position).sqrMagnitude;
            // Validamos cual es el mas cercano
            if (d < distancia)
            {
                distancia = d; // Damos un nuevo valor a la distancia
                min = controls[i].transform; // El punto mas cercano lo asignamos
            }
        }

        return min; // Devolvemos el punto de referencia mas cercano a nuestro coche
    }
}
