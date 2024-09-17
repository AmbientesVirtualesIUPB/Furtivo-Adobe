
using UnityEngine;

public class ColisionInicio : MonoBehaviour
{
    public GameObject objeto;  // Referencia al objeto que deseamos activar

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("coche")) // Verifica que el objeto padre que colisiona tenga el tag "coche"
        {
            objeto.SetActive(true);
        }
    }
}
