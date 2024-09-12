using UnityEngine;
using UnityEngine.AI;


public class Persecucion : MonoBehaviour
{
    public  Transform       objetoPerseguido; // El vehículo que será perseguido
    private NavMeshAgent    policia; // El agente de policia

    void Start()
    {
        policia = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (objetoPerseguido != null)
        {
            // Establece el destino del policia en el vehículo a seguir
            policia.SetDestination(objetoPerseguido.position);

            float distanceToTarget = Vector3.Distance(transform.position, objetoPerseguido.position); // Calculamos diferencia  de la distancia entre los dos objetivos

            // Si la distancia es mayor a 15 unidades
            if (distanceToTarget > 15.0f)
            {
                policia.speed = 20; // Aumentamos la velocidad de persecucion a 20 unidades
            }else
            {
                policia.speed = 10; // Sino dejamos la velocidad en 10 por defecto
            }
        }
    }
}
