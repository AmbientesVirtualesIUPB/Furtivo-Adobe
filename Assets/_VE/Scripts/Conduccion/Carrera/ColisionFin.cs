
using UnityEngine;

public class ColisionFin : MonoBehaviour
{
    public Carrera carrera;  // Referencia al objeto principal

    void OnTriggerEnter(Collider coche)
    {
        if (coche.transform.root.CompareTag("coche")) // Verifica que el objeto padre que colisiona tenga el tag "coche"
        {
            carrera.ColisionFinal();

            ManipularTextos manipularTextos = coche.GetComponentInParent<ManipularTextos>();
            RotarLlantas rotarLlantas = coche.GetComponentInParent<RotarLlantas>();
            FinalCarrera finalCarrera = coche.GetComponentInParent<FinalCarrera>();

            if (manipularTextos != null && rotarLlantas != null && finalCarrera != null)
            {
                // Manipula el script del primer vehículo que pasa
                manipularTextos.CambiarTextos();
                rotarLlantas.enabled = true;
                finalCarrera.enabled = true;
                finalCarrera.DispararProyectiles();
            }        
        }
    }
}
