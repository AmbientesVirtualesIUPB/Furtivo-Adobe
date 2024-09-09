using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bateria : MonoBehaviour
{
    public Conducir conducir;
    public Slider   slCarga;
    public Image    imgFill;
    public Gradient colores;
    public float    cargaActual;
    public float    capacidadMaxima;
    public float    tasaDescarga;
    public bool     encendida;


    // Start is called before the first frame update
    void Start()
    {
        cargaActual = capacidadMaxima; // Asignamos la carga actual de la bateria
        imgFill.color = colores.Evaluate(1); // Asignamos un color base a la bateria, en este caso verde azul
    }

    // Update is called once per frame
    void Update()
    {
        // Validamos que este asignada la referencia de conducir y la capacidad maxima asignada
        if (conducir != null && capacidadMaxima != 0) 
        {
            // Validamos que este encendida la interfaz del vehiculo desde el script IniciarInterfazVehiculo
            if (encendida)
            {
                // Validamos si estamos acelerando para solo reducir la carga cuando acelere
                if (conducir.acelerando)
                {
                    // Asignamos a la carga actual el valor dependiendo del tiempo de manejo transcurrido
                    cargaActual = Mathf.Max(0, cargaActual - tasaDescarga * Time.deltaTime);
                    // Le damos un efecto visual a la bateria a medida que se descarga
                    imgFill.color = colores.Evaluate(cargaActual / capacidadMaxima);

                    // Si la carga actual llega a cero
                    if (cargaActual <= 0)
                    {
                        // Ya no se puede manejar          
                        conducir.descargado = true;
                        conducir.carEngineSound.Stop();
                        conducir.carEngineEnd.enabled = true;
                    }
                }

                // validamos que tengamos una referencia al slider
                if (slCarga != null)
                {
                    // Le damos un valor al slider dependiendo de la carga actual de la bateria
                    slCarga.value = cargaActual / capacidadMaxima;
                }
            }
        }
        else
        {
            Debug.LogError("Falta inicializar componentes en la bateria");
        }
    }
}
