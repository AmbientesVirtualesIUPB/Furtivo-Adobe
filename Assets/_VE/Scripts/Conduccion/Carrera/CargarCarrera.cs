using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarCarrera : MonoBehaviour
{
    // Llama a este m�todo desde un bot�n o alg�n evento
    public void CargarEscenaJugador1()
    {
        SceneManager.LoadScene("jugador1");
    }

    
    public void CargarEscenaJugador2()
    {
        SceneManager.LoadScene("jugador2");
    }
}
