using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiniciarCarrera : MonoBehaviour
{
    public Transform                starPoint, starPointPolicia;
    public GameObject               vehiculo, policia;
    public Bateria                  bateria;
    public IniciarIntefazVehiculo   iniciarIntefazVehiculo;
    public Conducir                 conducir;

    private void Start()
    {
        // Validamos que los componentes esten asignaods
        if (starPoint == null || starPointPolicia == null || vehiculo == null || policia == null || bateria == null || iniciarIntefazVehiculo == null || conducir == null)
        {
            Debug.LogError("falta inicializar componentes en el script ReiniciarCarrera en el objeto Tablero del canvas");
        }
    }

    /// <summary>
    /// Metodo invocado desde BtnReiniciar en la escena
    /// </summary>
    public void ReiniciaPista()
    {
        StartCoroutine(Reiniciar()); // Llamamos la currutina
    }

    private IEnumerator Reiniciar()
    {
        iniciarIntefazVehiculo.InteractuarInterfaz(); // Apagamos la interfaz
        conducir.ThrottleOff(); // Establecemos el torque del motor en cero para que el vehiculo no avance durante el reinicio

        yield return new WaitForSeconds(3f); // Luego de tres segundos

        // Posicionamos el vehiculo en el punto de inicio
        vehiculo.transform.position = starPoint.position;
        vehiculo.transform.rotation = starPoint.rotation;

        // Posicionamos la policia en el punto de inicio
        policia.transform.position = starPointPolicia.position;
        policia.transform.rotation = starPointPolicia.rotation;

        bateria.cargaActual = bateria.capacidadMaxima; // Devolvemos la carga de la bateria a su valor original
    }
}
