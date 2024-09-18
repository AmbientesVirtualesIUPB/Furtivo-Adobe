using UnityEngine;
using UnityEngine.AI;


public class Persecucion : MonoBehaviour
{
    public  Transform       objetoPerseguido, objetoPerseguid2; // El vehículo que será perseguido
    private NavMeshAgent    policia; // El agente de policia
    public int              velocidadMax;
    public int              velocidadMin;

    void Start()
    {
        policia = GetComponent<NavMeshAgent>();
        // Establece el destino del policia en el vehículo a seguir
        policia.SetDestination(objetoPerseguido.position);
    }

    void Update()
    {
        if (objetoPerseguido != null)
        {
            

            float distanceToTarget = Vector3.Distance(transform.position, objetoPerseguido.position); // Calculamos diferencia  de la distancia entre los dos objetivos

            // Si la distancia es mayor a 15 unidades
            if (distanceToTarget > 15.0f)
            {
                policia.speed = velocidadMax; // Aumentamos la velocidad almaximo = 20
            }else
            {
                policia.SetDestination(objetoPerseguid2.position);
                policia.speed = velocidadMin; // Sino dejamos la velocidad estandar = 10
            }
        }
    }
}
