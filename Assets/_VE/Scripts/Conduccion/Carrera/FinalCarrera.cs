using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCarrera : MonoBehaviour
{
    public Transform punto;  // Primer punto de destino
    public Carrera carrera;
    public float tiempoEspera = 12f;
    public GameObject proyectiles;

    void Start()
    {
        //velocidadActual = velocidadInicial;  // Iniciar con la velocidad establecida

        // Alinear la rotación con el objeto puntoA antes de iniciar el movimiento
        StartCoroutine(AlinearPuntoFinal());
    }

    public IEnumerator AlinearPuntoFinal()
    {

        yield return new WaitForSeconds(tiempoEspera);
        AlinearConPunto();
        carrera.FinalizarJuegoCamaras();
    }

    void AlinearConPunto()
    {
        // Alinear la posición con el destino
        transform.position = punto.position;

        // Alinear la rotación con el destino
        transform.rotation = punto.rotation;
    }

    public void DispararProyectiles()
    {
        proyectiles.gameObject.SetActive(true);
    }
}
