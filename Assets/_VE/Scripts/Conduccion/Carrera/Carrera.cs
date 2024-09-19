
using UnityEngine;
using TMPro;
using System.Collections;


public class Carrera : MonoBehaviour
{
    public GameObject[] camaras; // Camaras entre las que queremos cambiar vistas
    public GameObject camaraPrincipal; // Camara de conduccion
    public GameObject camaraOrbital; // Camara de conduccion
    public float tiempoEspera = 3.1f;   // Tiempo de espera entre activaciones
    public bool puedoCambiar;
    public Collider colliderPosicionador;
    public GameObject conductor;
    
    public GameObject[] luces = new GameObject[4];  // Un array para almacenar las 3 luces del semaforo, Amarilla, Verde y Roja
    public TextMeshProUGUI textoArranqueSombra,textoArranque; // Referencia al componente TextMeshProUGUI
    public IniciarIntefazVehiculo interfazVehiculo;
    public AudioSource sonidoArranque; // Sonido para indicar el inicio de la carrera
    public Conducir conducir;
    

    [ContextMenu("Iniciar")]
    public void Iniciar()
    {
        // Asegúrate de tener una referencia al Renderer
        if (interfazVehiculo == null || textoArranque == null || luces == null)
        {
            Debug.Log(" Falta inicializar componenetes en el script");
        }
        else
        {
            StartCoroutine(IniciarCarrera());
        }
    }

    public IEnumerator IniciarCarrera()
    {

        yield return new WaitForSeconds(2f);

        luces[0].gameObject.SetActive(true);
        textoArranqueSombra.gameObject.SetActive(true);
        textoArranque.color = Color.red;
        sonidoArranque.Play();

        yield return new WaitForSeconds(1.5f);

        luces[1].gameObject.SetActive(true);
        textoArranque.color = Color.red;
        textoArranque.text = "2";
        textoArranqueSombra.text = "2";

        yield return new WaitForSeconds(1.3f);

        luces[2].gameObject.SetActive(true);
        textoArranque.color = Color.yellow;
        textoArranque.text = "1";
        textoArranqueSombra.text = "1";

        yield return new WaitForSeconds(1.4f);
        luces[3].gameObject.SetActive(true);
        textoArranque.color = Color.green;
        textoArranque.text = "GO";
        textoArranqueSombra.text = "GO";
        interfazVehiculo.iniciarPista = true;

        yield return new WaitForSeconds(1f);
        textoArranqueSombra.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        colliderPosicionador.enabled = true;
    }

    public void ColisionFinal()
    {
        conductor.SetActive(true);
        StartCoroutine(DetenerScriptCarrera());
        StartCoroutine(JuegoCamaras());
        Debug.Log("Llegaste a la meta");
    }

    public IEnumerator DetenerScriptCarrera()
    {
        conducir.descargado = true; // Indicamos que el vehiculo no se puede conducir
        yield return new WaitForSeconds(2f);
        conducir.enabled = false;
    }

    public IEnumerator JuegoCamaras()
    {
        camaraPrincipal.SetActive(false); // Desactivamos la camara de conduccion
        while (!puedoCambiar) // Repetir indefinidamente o hasta que lo controles de otra manera
        {
            for (int i = 0; i < camaras.Length; i++)
            {
                // Desactivar todas las camaras
                foreach (GameObject cam in camaras)
                {
                    cam.SetActive(false);
                }

                // Activar la camara en la posición actual del array
                camaras[i].SetActive(true);

                // Esperar el tiempo definido antes de pasar a la siguiente
                yield return new WaitForSeconds(tiempoEspera);
            }
        }
    }

    public void FinalizarJuegoCamaras()
    {
        puedoCambiar = true;
        StopAllCoroutines();

        // Desactivar todas las camaras
        foreach (GameObject cam in camaras)
        {
            cam.SetActive(false);
        }
        camaraOrbital.SetActive(true);
    }
}
