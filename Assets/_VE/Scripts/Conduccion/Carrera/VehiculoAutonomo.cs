using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculoAutonomo : MonoBehaviour
{
    public Transform[] puntosReferencias;  // Array de waypoints
    public float speed = 10f;      // Velocidad del vehículo
    public float rotationSpeed = 5f; // Velocidad de rotación
    public float stoppingDistance = 0.5f; // Distancia de tolerancia al llegar al waypoint
    private int currentWaypointIndex = 0;  // Índice del waypoint actual
    private Rigidbody rb;  // Si estás usando un Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Si el vehículo tiene un Rigidbody
    }

    void Update()
    {
        // Si hay waypoints disponibles
        if (puntosReferencias.Length == 0)
            return;

        // Obtener el waypoint objetivo
        Transform targetWaypoint = puntosReferencias[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        direction.y = 0;  // Opcional: evitar que el vehículo cambie de altura si la pista es plana

        // Calcular la distancia al waypoint
        float distanceToWaypoint = direction.magnitude;

        // Si el vehículo está lo suficientemente cerca del waypoint, pasar al siguiente
        if (distanceToWaypoint < stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % puntosReferencias.Length;
            return; // Salir para evitar que siga moviéndose en esta actualización
        }

        // Rotar gradualmente hacia el siguiente waypoint de manera más suave
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Moverse hacia el waypoint usando Rigidbody (si está presente)
        if (rb != null)
        {
            Vector3 movement = transform.forward * speed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            // Alternativa si no tienes un Rigidbody
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
        }
    }
}
