using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPuntoControl : MonoBehaviour
{
    public static ManagerPuntoControl Instance;
    public List<PuntoControl> controls = new List<PuntoControl>();

    private void Awake()
    {
        Instance = this;
    }
  
    public Transform ObtenerPuntoCercano(Vector3 punto)
    {
        float distancia = 10000;
        Transform min = controls[0].transform;

        for (int i = 0; i < controls.Count; i++)
        {
            float d = (punto - controls[i].transform.position).sqrMagnitude;
            if (d < distancia)
            {
                distancia = d;
                min = controls[i].transform;
            }
        }

        return min;
    }
}
