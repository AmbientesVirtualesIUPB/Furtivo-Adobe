using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    //Objeto con la informacion de las personalizaciones
    public Personalizacion              personalizacion;
    public PersonalizacionFurtivo       personalizacionFurtivos;

    //Referenciamos el archivo donde guardaremos la informacion
    [SerializeField]
    private SaveSplit split;

    /// <summary>
    /// Metodo para el guardado de archivos JSON
    /// </summary>
    public void Save()
    {
        //Conovertimos el objeto a formato Json
        string splitJson = JsonUtility.ToJson(split);
        //Generamos una ubicacion en disco, persistente para que funcione en cualquier plataforma
        string path = Path.Combine(Application.persistentDataPath, "splitData.data");
        //Guardamos el archibo json
        File.WriteAllText(path, splitJson);
    }

    /// <summary>
    /// Metodo para la carga de archivos JSON
    /// </summary>
    public void Load()
    {
        //Traemos la ruta del archivo
        string path = Path.Combine(Application.persistentDataPath, "splitData.data");

        //Validamos si ya existe un archivo de guardado actual
        if (File.Exists(path))
        {
            //leemos el archivo Json
            string splitJson = File.ReadAllText(path);
            //Convertimos el archivo Json a objeto unity
            SaveSplit splitLoad = JsonUtility.FromJson<SaveSplit>(splitJson);

            split.posiciones = splitLoad.posiciones;
            split.colores = splitLoad.colores;
            split.furtivos = splitLoad.furtivos;
        }
        // Sino existe creamos uno por defecto
        else
        {
            Save();
        }
    }

    /// <summary>
    /// Metodo invocado desde el scrip de personalización, para grabar los datos de las posiciones
    /// </summary>
    /// <param name="texto"> Parametro de texto con las posiciones </param>
    public void PersonalizacionPersonaje(string texto)
    {
        split.posiciones = texto;
        //Guardamos
        Save();
    }

    /// <summary>
    /// Metodo invocado desde el scrip de personalización, para grabar los datos de las posiciones de los colores
    /// </summary>
    /// <param name="texto"> Parametro de texto con las posiciones </param>
    public void PersonalizacionColores(string texto)
    {
        split.colores = texto;
        //Guardamos
        Save();
    }

    /// <summary>
    /// Metodo invocado desde el scrip de personalización, para grabar los datos de las posiciones de los colores
    /// </summary>
    /// <param name="texto"> Parametro de texto con las posiciones </param>
    public void PersonalizacionFurtivos(string texto)
    {
        split.furtivos = texto;
        //Guardamos
        Save();
    }

    /// <summary>
    /// Metodo invocado desde el script de personalización, en el Awake
    /// </summary>
    public void CargarDatos()
    {
        //Cargamos
        Load();

        // Solo ejecuta el resto del código si objeto no es nulo
        if (personalizacion != null)
        {
            //Asignamos los datos al personaje
            personalizacion.ConvertirDesdeTexto(split.posiciones); 
            personalizacion.ConvertirDesdeTextoColores(split.colores); 
        }
        // Solo ejecuta el resto del código si objeto no es nulo
        if (personalizacionFurtivos != null)
        {
            //Asignamos los datos al personaje
            personalizacionFurtivos.ConvertirDesdeTexto(split.furtivos);
        }    
    }
}
