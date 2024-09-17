
using UnityEngine;

public class ColisionFin : MonoBehaviour
{
    public Carrera carrera;  // Referencia al objeto principal

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("coche")) // Verifica que el objeto padre que colisiona tenga el tag "coche"
        {
            carrera.ColisionFinal();
        }
    }
}
