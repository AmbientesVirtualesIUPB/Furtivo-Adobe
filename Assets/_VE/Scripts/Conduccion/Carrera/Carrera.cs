
using UnityEngine;
using TMPro;
using System.Collections;

public class Carrera : MonoBehaviour
{
    public GameObject[] luces = new GameObject[4];  // Un array para almacenar las 3 luces del semaforo, Amarilla, Verde y Roja
    public TextMeshProUGUI textoArranque, textoFin; // Referencia al componente TextMeshProUGUI
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
        textoArranque.gameObject.SetActive(true);
        textoArranque.color = Color.red;
        sonidoArranque.Play();

        yield return new WaitForSeconds(1.5f);

        luces[1].gameObject.SetActive(true);
        textoArranque.color = Color.red;
        textoArranque.text = "2";

        yield return new WaitForSeconds(1.3f);

        luces[2].gameObject.SetActive(true);
        textoArranque.color = Color.yellow;
        textoArranque.text = "1";

        yield return new WaitForSeconds(1.4f);
        luces[3].gameObject.SetActive(true);
        textoArranque.color = Color.green;
        textoArranque.text = "GO";
        interfazVehiculo.iniciarPista = true;
        //interfazVehiculo.ConducirVehiculo();

        yield return new WaitForSeconds(1f);
        textoArranque.gameObject.SetActive(false);
        
    }


    public void ColisionFinal()
    {
        textoFin.gameObject.SetActive(true);
        conducir.enabled = false;
        Debug.Log("Llegaste a la meta");
    }
}
