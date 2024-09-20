using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCarrera : MonoBehaviour
{
    public Transform punto;  // Primer punto de destino
    public Carrera carrera;
    public float tiempoEspera = 12f;
    public GameObject proyectiles;

    void Start()
    {
        //velocidadActual = velocidadInicial;  // Iniciar con la velocidad establecida

        // Alinear la rotaci�n con el objeto puntoA antes de iniciar el movimiento
        StartCoroutine(AlinearPuntoFinal());
    }

    public IEnumerator AlinearPuntoFinal()
    {

        yield return new WaitForSeconds(tiempoEspera);
        AlinearConPunto();
        carrera.FinalizarJuegoCamaras();

        yield return new WaitForSeconds(30f);
        // Carga la escena activa nuevamente
        SceneManager.LoadScene("inicio");
    }

    void AlinearConPunto()
    {
        // Alinear la posici�n con el destino
        transform.position = punto.position;

        // Alinear la rotaci�n con el destino
        transform.rotation = punto.rotation;
    }

    public void DispararProyectiles()
    {
        proyectiles.gameObject.SetActive(true);
    }
}
