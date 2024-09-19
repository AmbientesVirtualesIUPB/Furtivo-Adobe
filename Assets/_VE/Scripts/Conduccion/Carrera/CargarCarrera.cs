using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarCarrera : MonoBehaviour
{
    // Llama a este método desde un botón o algún evento
    public void CargarEscenaJugador1()
    {
        SceneManager.LoadScene("jugador1");
    }

    
    public void CargarEscenaJugador2()
    {
        SceneManager.LoadScene("jugador2");
    }
}
