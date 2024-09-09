using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InicializarFurtivo : MonoBehaviour
{
    public Transform[]              camsPositions; // Posiciones de las camaras
    public GameObject[]             botonesCanvas; // Botones de rotar para poder desactivarlos cuando deseemos
    public Transform                camPrincipal; // Referencia de la camara principal
    public IniciarPlataforma        plataforma; // Referencia al script de la plataforma
    public PersonalizacionFurtivo   furtivo; // Referencia al script de la personalizacion del furtivo
    public BrazoGiratorio           brazo; // Referencia al brazo giratorio para poderlo detener
    public GameObject               canvas; // Canvas principal
    public GameObject               canvasPantalla; // Canvas pantalla que se visualiza al cambiar las baterias
    private Collider                collider; // Referencia al collider para que no se pueda ejecutar repetidas veces

    // Variables provisionales para el manejo de particulas
    public GameObject   particulas;
    public GameObject[] lucesLamparas;

    //Variables para el manejo de camaras
    private float       tiempoTranscurrido; // Variable para controlar el tiempo en transiciones
    private float       tiempoTotal; // Variable para controlar el tiempo en transiciones
    private float       velocidad = 3;           // Velocidad de interpolación
    private float       lerpTime = 0.0f;        // Tiempo de interpolación
    private float       lerpDuration = 2.0f;    // Duración de la interpolación
    private bool        iniciarCamaras = false; // Variable para determinar cuando podemos mover posiciones de camara

    // Vectores con las variables para almacenar las posiciones y rotaciones
    private Vector3     posicionInicial;
    private Quaternion  rotacionInicial;
    private Vector3     posicion;
    private Quaternion  rotacion;
    

    /// <summary>
    /// Metodo invocado antes de iniciar la scena
    /// </summary>
    private void Awake()
    {
        collider = GetComponent<Collider>();
        if (brazo == null || furtivo == null || plataforma == null)
        {
            Debug.LogError("Falta inicializar componenetes del script InicializarFurtivo");
        }
    }
    

    /// <summary>
    /// Metodo invocado al iniciar la scena
    /// </summary>
    private void Start()
    {
        // Guardamos la posicion que tenemos antes de iniciar la personalizacion
        posicionInicial = camPrincipal.transform.position;
        rotacionInicial = camPrincipal.transform.rotation;

        // Guardamos la posicion que tendra inicialmente la interfaz
        posicion = camsPositions[1].transform.position;
        rotacion = camsPositions[1].transform.rotation;
    }


    /// <summary>
    /// Metodo invocado frame a frame
    /// </summary>
    void Update()
    {
        // Controlamos el flujo de tiempo para las transiciones
        tiempoTranscurrido += Time.deltaTime;

        // Validamos si ya podemos hacer cambios de camaras
        if (iniciarCamaras)
        {
            // Si el enfoque actual es diferente a nuestra camara ejecutamos
            if (camPrincipal.position != posicion)
            {
                // Asignamos un tiempo, velocidad y guardamos posicion y rotacion de la nueva posicion
                lerpTime += Time.deltaTime / lerpDuration;
                camPrincipal.position = Vector3.Lerp(camPrincipal.position, posicion, lerpTime);
                camPrincipal.rotation = Quaternion.Lerp(camPrincipal.rotation, rotacion, lerpTime);
            }
        } 
    }


    /// <summary>
    /// Metodo invocado desde los botones del canvas para indicar que enfoque le daremos a la camara
    /// </summary>
    /// <param name="index"> Indica el valor para la posicion de la variable camsPositions </param>
    public void MoverPosicion(int index)
    {
        // Validamos si el enfoque de la camara es diferente al enfoque de la silla o del volante para inhabilitar las flechas de movimiento
        if (index > 1)
        {
            botonesCanvas[0].gameObject.SetActive(false);
            botonesCanvas[1].gameObject.SetActive(false);
        }
        else
        {
            botonesCanvas[0].gameObject.SetActive(true);
            botonesCanvas[1].gameObject.SetActive(true);
        }
        // Asignamos la posicion y rotacion dependiendo del index o de la posicion de la camara
        posicion = camsPositions[index].position;
        rotacion = camsPositions[index].rotation;
        lerpTime = 0.0f;  // Reiniciar el tiempo de interpolación
    }


    /// <summary>
    /// Evento invocado al momento de hacer click con el mouse para iniciar la fase de personalizacion
    /// </summary>
    private void OnMouseDown()
    {
        // Deshabilitamos el collider para evitar que no se siga oprimiendo
        collider.enabled = false;
        // Inicializamos la animacion para la plataforma
        plataforma.MoverArriba();
        // Activamos las baterias para que se vean en escena
        furtivo.baterias.gameObject.SetActive(true);
        // Iniciamos currutinas para cambios de camara e inicio de interfaz
        StartCoroutine(InicializarPlataforma());
    }


    /// <summary>
    /// Currutina para dar un movimiento suavizado a los cambios de camara y preparacion de la plataforma
    /// </summary>
    IEnumerator InicializarPlataforma()
    {
        //Activamos el canvas con la pantalla del vehiculo y las particulas
        canvasPantalla.gameObject.SetActive(true);
        particulas.gameObject.SetActive(true);

        tiempoTranscurrido = 0.0f; // Reiniciamos el flujo de tiempo
        tiempoTotal = 3.0f; // Duración total del ciclo en segundos

        // Mientras que el tiempo transcurrido sea menor al total indicado
        while (tiempoTranscurrido < tiempoTotal)
        {
            // Hacemos el primer cambio de camara
            camPrincipal.transform.position = Vector3.Lerp(camPrincipal.transform.position, camsPositions[0].position, velocidad * Time.deltaTime);
            camPrincipal.transform.rotation = Quaternion.Lerp(camPrincipal.transform.rotation, camsPositions[0].rotation, velocidad * Time.deltaTime);
            
            // Pausa el ciclo hasta el siguiente frame
            yield return null;
        }
        // Encendemos las luces
        StartCoroutine(EncenderLuces());

        tiempoTranscurrido = 0.0f; // Reiniciamos el flujo de tiempo

        yield return new WaitForSeconds(1.5f);

        tiempoTotal = 3.5f; // Duración total del ciclo en segundos

        // Mientras que el tiempo transcurrido sea menor al total indicado
        while (tiempoTranscurrido < tiempoTotal)
        {
            // Hacemos el segundo cambio de camara
            camPrincipal.transform.position = Vector3.Lerp(camPrincipal.transform.position, camsPositions[1].position, velocidad * Time.deltaTime);
            camPrincipal.transform.rotation = Quaternion.Lerp(camPrincipal.transform.rotation, camsPositions[1].rotation, velocidad * Time.deltaTime);
            yield return null;
        }  

        // Confirmamos que la camara quede con la posicon deseada
        camPrincipal.transform.position = camsPositions[1].position;
        camPrincipal.transform.rotation = camsPositions[1].rotation;
        // Habilitamos el canvas
        canvas.gameObject.SetActive(true);
        // Indicamos que ya podemos hacer cambios de camaras
        iniciarCamaras = true;
    }

    /// <summary>
    /// Metodo invocado desde BtnSalir en nuestro canvas para salir de la escena de personalizacion
    /// </summary>
    public void Salir()
    {
        // Indicamos que ya no podemos hacer cambios de camaras
        iniciarCamaras = false;
        // Iniciamos currutina de salida
        StartCoroutine(SalirInicializarPlataforma());
    }


    /// <summary>
    /// Currutina que se ejecuta al salir de la personalizacion
    /// </summary>
    IEnumerator SalirInicializarPlataforma()
    { 
        // Iniciamos la animacion que tiene el canvas
        canvas.gameObject.GetComponent<Animator>().SetBool("hide", true);
   
        yield return new WaitForSeconds(0.8f);
        // Deshabilitamos canvas
        canvas.gameObject.SetActive(false);
        // Detenemos el brazo y se posiciona en la parte inicial
        brazo.Detener();
        // Desactivamos las baterias para que no se vean en escena
        furtivo.baterias.gameObject.SetActive(false);

        tiempoTotal = 2.5f; // Duración total del ciclo en segundos
        tiempoTranscurrido = 0.0f; // Reiniciamos el flujo de tiempo

        // Mientras que el tiempo transcurrido sea menor al total indicado realiza el cambio de camara
        while (tiempoTranscurrido < tiempoTotal)
        {
            camPrincipal.transform.position = Vector3.Lerp(camPrincipal.transform.position, posicionInicial, velocidad * Time.deltaTime);
            camPrincipal.transform.rotation = Quaternion.Lerp(camPrincipal.transform.rotation, rotacionInicial, velocidad * Time.deltaTime);
            // Pausa el ciclo hasta el siguiente frame
            yield return null;
        }

        //Desactivamos el canvas con la pantalla del vehiculo y las particulas
        canvasPantalla.gameObject.SetActive(false);
        particulas.gameObject.SetActive(false);

        // Iniciamos la animacion de esconder del brazo de la plataforma
        plataforma.MoverAbajo();

        // Apagamos las luces
        StartCoroutine(ApagarLuces());

        yield return new WaitForSeconds(7f);

        // Confirmamos que la ultima posicion de la camara sea la inicial antes de la personalizacion
        posicion = camsPositions[1].transform.position;
        rotacion = camsPositions[1].transform.rotation;

        // Confirmamos que al salir las flechas de movimiento queden activas
        botonesCanvas[0].gameObject.SetActive(true);
        botonesCanvas[1].gameObject.SetActive(true);

        // Habilitamos nuevamente el collider
        collider.enabled = true;
    }

    /// <summary>
    /// Currutina para el encendido de las luces
    /// </summary>
    IEnumerator EncenderLuces()
    {
        for (int i = 0; i < lucesLamparas.Length; i++)
        {
            lucesLamparas[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Currutina para el apagado de las luces
    /// </summary>
    IEnumerator ApagarLuces()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < lucesLamparas.Length; i++)
        {
            lucesLamparas[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
    }

}
