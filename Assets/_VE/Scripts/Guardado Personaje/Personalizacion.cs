using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Personalizacion : MonoBehaviour
{
    public ElementoPersonalizable[] partesHombre;
    public ElementoPersonalizable[] partesMujer;
    public ElementoPersonalizable[] partesOtros; 
    public Color[]                  paletaCejas;
    public Color[]                  paletaCabello;
    public Color[]                  paletaOjos;
    public Color[]                  paletaRopa;
    public Color[]                  paletaPiel;
    public Material                 materialInicialPielHombre;
    public Material                 materialInicialPielMujer;
    
    public int                      genero;
    // Variables utilizadas para el guardado de datos
    //public EnvioDatosBD             managerBD;
    public SaveManager              saveManager;
    public int[]                    pos = new int[15];
    public int[]                    colores = new int[5];
    public bool                     esColor;

    [SerializeField]
    private string url_consulta_p = "http://localhost/apiwebm/CRUD/Read/leer_datos_personalizacion.php";   // URL para consultar la informacion de la personalizacion
    private ProcesadorDeDatos procesador;

    /// <summary>
    /// Metodo invocado antes de iniciar la scena
    /// </summary>
    private void Awake()
    {
        // Cargamos los datos que se puedan tener guardados
        saveManager.CargarDatos();
    }

    // Start is called before the first frame update
    void Start()
    {
        procesador = gameObject.AddComponent<ProcesadorDeDatos>();
        InicializarElementos();
        TransicionDeGenero(pos[14]);

        // Busca el objeto por nombre para buscar la referencia al objeto que administra la base de datos, ya que este pasar� entre escenas
        GameObject obj = GameObject.Find("EnvioBD");
        // O por tag
        // GameObject obj = GameObject.FindWithTag("TagDelObjeto");

        //if (obj != null)
        //{
        //    managerBD = obj.GetComponent<EnvioDatosBD>(); // Si encuentra el objeto lo almacenamos en la variable
        //    TraerInformacionPersonalizacion(managerBD.AsignarDatosGuardados()); // Ejecutamos el metodo para traer la informacion de la personalizacion guardada
        //}
        //else
        //{
        //    managerBD = null;
        //}
    }

    /// <summary>
    ///  Metodo que puede ser invocado para traer la personalizacion que se tenga guardada en la base de datos, pasandole el numero de cedula
    /// </summary>
    /// <param name="id"></param>
    public void TraerInformacionPersonalizacion(int id)
    {
        // Llamamos la currutina
        StartCoroutine(ObtenerPersonalizacion(id, procesador));
    }

    /// <summary>
    /// Currutina encargada de consultar la base de datos y traer la informacion del usuario especificado
    /// </summary>
    /// <param name="idUsuario"> Cedula del usuario </param>
    /// <param name="procesador"> Referencia al scrip procesador de informacion </param>
    /// <returns></returns>
    public IEnumerator ObtenerPersonalizacion(int idUsuario, ProcesadorDeDatos procesador)
    {
        // Creación del formulario
        WWWForm form = new WWWForm();
        // Enviamos la cedula que este logueada
        form.AddField("id_usuario", idUsuario);

        //Enviamos la solicitud Post
        using (UnityWebRequest www = UnityWebRequest.Post(url_consulta_p, form))
        {
            yield return www.SendWebRequest();

            //Si la solicitud es correcta y exitosa
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Acciones a realizar
                Debug.Log("Respuesta recibida: " + www.downloadHandler.text);
                // Parsear la respuesta
                int[] resultado = procesador.RespuestaProcesada(www.downloadHandler.text);

                //Asignamos el valor de genero
                pos[0] = resultado[0];

                //Asignamos la nueva respuesta del servidor a nuestras posiciones
                for (int i = 1; i < 5; i++)
                {    
                    //Si es masculino
                    if (pos[0] == 1)
                    {
                        pos[i] = resultado[i + 1];
                    }
                    //Si es femenina
                    else if (pos[0] == 0)
                    {
                        pos[i + 4] = resultado[i];
                    }
                }
                // Asignamos los datos generales
                for (int i = 0; i < 4; i++)
                {
                    //Si es masculino
                    if (pos[0] == 1)
                    {
                        pos[10 + i] = resultado[6 + i];
                    }
                    //Si es femenina
                    else if (pos[0] == 0)
                    {
                        pos[10 + i] = resultado[6 + i];
                    }
                }
                //Asignamos los colores
                for (int i = 0; i < 5; i++)
                {
                    colores[i] = resultado[10 + i];
                }
                // Cargamos la personalizacion que tenga guardada con anterioridad
                TransicionDeGenero(pos[0]);
            }
            else
            {
                // Acciones a realizar
                Debug.LogError("Error al realizar la solicitud: " + www.error);
            }
        }
    }

    /// <summary>
    /// Metodo invocado desde el boton GuardarPersonalizacion en la escena
    /// </summary>
    public void PasarInformacionBD()
    {
        ////Pasamos el genero
        //managerBD.datos[0] = genero;

        ////Pasamos los datos de la personalizacion, dependiendo del genero elegido           
        //for (int i = 0; i < 5; i++)
        //{
        //    // Si es mujer
        //    if (genero == 0)
        //    {
        //       managerBD.datos[i + 1] = pos[i + 5];
        //    }
        //    // Si es hombre
        //    if (genero == 1)
        //    {
        //        managerBD.datos[i + 1] = pos[i];
        //    }
        //}

        //// Pasamos datos generales
        //for (int i = 0; i < 3; i++)
        //{
        //    managerBD.datos[i + 6] = pos[i + 10];
        //}

        //// Pasamos Tamaño
        //managerBD.datos[9] = pos[13];

        ////Pasamos Colores
        //for (int i = 0; i < 5; i++)
        //{
        //    managerBD.datos[10 + i] = colores[i];
        //}

        //// Enviamos la informacion a la base de datos
        //managerBD.EnviarDatosP(); 
    }

    /// <summary>
    /// Metodo invocado desde el script de Cambioboton
    /// </summary>
    public void Engordar(float valor)
    {
        //Asignamos el tamaño a cada elemento
        for (int i = 0; i < partesHombre.Length; i++)
        {
            partesHombre[i].CambiarTamaño(valor);
            partesMujer[i].CambiarTamaño(valor);
        }

        for (int i = 0; i < partesOtros.Length; i++)
        {
            partesOtros[i].CambiarTamaño(valor);
        }
        
        // Convertimos el valor a entero y lo guardamos en la posicion para su posterior guardado de datos
        pos[13] = (int)valor;
        ConvertirATexto();
    }
    

    /// <summary>
    /// Pasar elemento a elemento las caracteristicas unicamente pertenecientes al genero Masculino
    /// </summary>
    /// <param name="cual"> nos indica cual posicion vamos a modificar </param>
    public void SiguienteParteHombre(int cual)
    {
        Posiciones(0,cual);
        partesHombre[cual].Siguiente();
    }

    /// <summary>
    /// Pasar elemento a elemento las caracteristicas unicamente pertenecientes al genero Femenino
    /// </summary>
    /// <param name="cual"> nos indica cual posicion vamos a modificar </param>
    public void SiguienteParteMujer(int cual)
    {
        Posiciones(1, cual);
        partesMujer[cual].Siguiente();
    }

    /// <summary>
    /// Invocado desde los botones de personalizacion. Pasar elemento a elemento las caracteristicas pertenecientes a ambos generos
    /// </summary>
    /// <param name="cual"> nos indica cual posicion vamos a modificar </param>
    public void SiguienteParteOtro(int cual)
    {
        Posiciones(2, cual);
        partesOtros[cual].Siguiente();
    }

    /// <summary>
    /// Metodo invocado desde los botones de personalizacion, enviado el dato correspondiente a la caracteristica a modificar
    /// </summary>
    /// <param name="cual"> nos indica cual posicion vamos a modificar </param>
    public void SiguienteParteGay(int cual)
    {
        if (genero==0)
        {
            SiguienteParteMujer(cual);
        }
        else
        {
            SiguienteParteHombre(cual);
        }
    }

    /// <summary>
    /// Metodo para guardar las posiciones de cada elemento para su posterior guardado
    /// </summary>
    /// <param name="num"> para validar si es una parte masculina, femenina u otra </param>
    /// <param name="posicion"> nos indica cual posicion vamos a modificar </param>
    public void Posiciones(int num, int posicion)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            //Si es hombre
            if (num == 0)
            {
                pos[posicion] = (partesHombre[posicion].activo + 1) % partesHombre[posicion].elementos.Length;
            }
            //Si es mujer
            else if (num == 1)
            {
                pos[posicion + 5] = (partesMujer[posicion].activo + 1) % partesMujer[posicion].elementos.Length;
            }
            //Si son otras partes
            else if (num == 2)
            {
                pos[posicion + 10] = (partesOtros[posicion].activo + 1) % partesOtros[posicion].elementos.Length;
            }
        }
        // Convertimos esos datos a una sola cadena de texto
        ConvertirATexto();
    }

    /// <summary>
    /// Para convertir las posiciones en una sola cadena de texto y poder guardarlas
    /// </summary>
    /// <returns></returns>
    public string ConvertirATexto()
    {
        string texto = "";
        // Validamos si lo que estamos personalizando es el color
        if (esColor == true)
        {
            for (int i = 0; i < colores.Length; i++)
            {
                texto += (texto.Length > 0) ? "|" : "";
                texto += colores[i].ToString();
            }

            // Enviamos los datos que queremos guardar
            saveManager.PersonalizacionColores(texto);
        }
        // Si lo que estamos personalizando son los elementos, entonces
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                texto += (texto.Length > 0) ? "|" : "";
                texto += pos[i].ToString();
            }

            // Enviamos los datos que queremos guardar
            saveManager.PersonalizacionPersonaje(texto);
        }

        esColor = false;
        return texto;
    }

    /// <summary>
    /// Metodo invocado desde el script SaveManager. Para convertir las posiciones de texto a enteros y cargarlas
    /// </summary>
    /// <param name="texto"></param>
    public void ConvertirDesdeTexto(string texto)
    {
        string[] nombre = texto.Split("|");
        if (texto.Length > 0)
        {
            for (int i = 0; i < nombre.Length; i++)
            {
                pos[i] = int.Parse(nombre[i]);
            }
        }
    }

    /// <summary>
    /// Metodo invocado desde el script SaveManager. Para convertir las posiciones de texto a enteros y cargarlas
    /// </summary>
    /// <param name="texto"></param>
    public void ConvertirDesdeTextoColores(string texto)
    {
        string[] nombre = texto.Split("|");
        if (texto.Length > 0)
        {
            for (int i = 0; i < nombre.Length; i++)
            {
                colores[i] = int.Parse(nombre[i]);
            }
        }
    }

    /// <summary>
    /// Metodo invocado desde BtnMasculino y BtnFemenino para el cambio de genero
    /// </summary>
    /// <param name="cual"> Nos indica el genero </param>
    public void TransicionDeGenero(int cual)
    {
        // Guardamos el genero
        pos[14] = cual;
        ConvertirATexto();

        genero = cual;
        // Si es cero, es femenino, establecemos los elementos de dicho genero y desactivamos los masculinos
        if (genero==0)
        {
            for (int i = 0; i < partesHombre.Length; i++)
            {
                partesHombre[i].Desactivar();
                partesMujer[i].Establecer();
            }     
        }
        // Si es uno, es masculino, establecemos los elementos de dicho genero y desactivamos los femeninos
        else
        {
            for (int i = 0; i < partesHombre.Length; i++)
            {
                partesHombre[i].Establecer();
                partesMujer[i].Desactivar();
            }
        }

        // Establecemos los elementos de ambos generos
        for (int i = 0;i < partesOtros.Length; i++)
        {
            partesOtros[i].Establecer();
        }
        // Cargamos la personalizacion que tenga guardada con anterioridad
        PersonalizacionSave();
    }

    /// <summary>
    /// Dependiendo del genero, traemos la personalizacion que tenga guardada con anterioridad, sino tiene se establece por defecto
    /// </summary>
    public void PersonalizacionSave()
    {
        //Se cargar tambien los valores de la variable activo para que continue desde el elemento de personalizacion anteriormente seleccionado
        for (int i = 0; i < pos.Length; i++)
        {
            // Hacemos una validacion para asignar correctamente a cada objeto sus posiciones
            if (i <= 4)
            {
                partesHombre[i].activo = pos[i];
            }
            else if (i <= 9)
            {
                partesMujer[i - 5].activo = pos[i];
            }
            else if (i <= 12)
            {
                partesOtros[i - 10].activo = pos[i];
            }
        }
        

        // Si es mujer
        if (genero == 0)
        {
            for (int i = 0; i < partesMujer.Length; i++)
            {
                for (int j = 0; j < partesMujer[i].elementos.Length; j++)
                {
                    partesMujer[i].elementos[j].SetActive(pos[i + 5] == j);
                }
            }
        }
        // Si es hombre
        else
        {
            for (int i = 0; i < partesHombre.Length; i++)
            {
                for (int j = 0; j < partesHombre[i].elementos.Length; j++)
                {
                    partesHombre[i].elementos[j].SetActive(pos[i] == j);
                }
            }
        }
        // Objetos generales
        for (int i = 0; i < partesOtros.Length; i++)
        {
            for (int j = 0; j < partesOtros[i].elementos.Length; j++)
            {
                partesOtros[i].elementos[j].SetActive(pos[i + 10] == j);
            }
        }
        // Engordamos con el valor de la posicion guardada convertida a flotante
        for (int i = 0; i < partesHombre.Length; i++)
        {
            partesHombre[i].CambiarTamaño((float)pos[13]);
            partesMujer[i].CambiarTamaño((float)pos[13]);
        }

        for (int i = 0; i < partesOtros.Length; i++)
        {
            partesOtros[i].CambiarTamaño((float)pos[13]);
        }
        

        // Establecemos los colores
        partesOtros[3].EstablecerColorGeneral(colores[0]);
        partesHombre[4].EstablecerColorGeneral(colores[1]);
        partesHombre[3].EstablecerColorGeneral(colores[1]);
        partesMujer[4].EstablecerColorGeneral(colores[1]);
        partesMujer[3].EstablecerColorGeneral(colores[1]);
        for (int i = 0; i < partesHombre.Length; i++)
        {
            partesHombre[i].EstablecerColorPrincipal(colores[2]);
            partesMujer[i].EstablecerColorPrincipal(colores[2]);
            partesHombre[i].EstablecerColorSecundario(colores[3]);
            partesMujer[i].EstablecerColorSecundario(colores[3]);
            partesHombre[i].EstablecerMaterialPiel(colores[4]);
            partesMujer[i].EstablecerMaterialPiel(colores[4]);
        }

        for (int i = 0; i < partesOtros.Length; i++)
        {
            partesOtros[i].EstablecerColorPrincipal(colores[2]);
            partesOtros[i].EstablecerColorSecundario(colores[3]);
        }
    }

    /// <summary>
    /// Metodo para cambiar el color de ojos
    /// </summary>
    /// <param name="num"> para indicar cual color</param>
    public void CambiarColorOjos(int num)
    {
        // Asigamos a la posicion correcta el color seleccionado
        colores[0] = num;
        // Indicamos que estamos cambiando color
        esColor = true;
        ConvertirATexto();
        // Establecemos el color
        partesOtros[3].EstablecerColorGeneral(num);
    }

    /// <summary>
    /// Metodo para cambiar el color de cabello
    /// </summary>
    /// <param name="num"> para indicar cual color </param>
    public void CambiarColorCabello(int num)
    {
        // Asigamos a la posicion correcta el color seleccionado
        colores[1] = num;
        esColor = true;
        ConvertirATexto();
        // Establecemos el color
        partesHombre[4].EstablecerColorGeneral(num);
        partesHombre[3].EstablecerColorGeneral(num);
        partesMujer[4].EstablecerColorGeneral(num);
        partesMujer[3].EstablecerColorGeneral(num);
    }

    /// <summary>
    /// Metodo para cambiar el color principal de la ropa
    /// </summary>
    /// <param name="num"> para indicar cual color </param>
    public void CambioColorPrincipal(int num)
    {
        // Asigamos a la posicion correcta el color seleccionado
        colores[2] = num;
        esColor = true;
        ConvertirATexto();

        // Establecemos el color
        for (int i = 0; i < partesHombre.Length; i++)
        {
            partesHombre[i].EstablecerColorPrincipal(num);
            partesMujer[i].EstablecerColorPrincipal(num);
        }

        for (int i = 0; i < partesOtros.Length; i++)
        {
            partesOtros[i].EstablecerColorPrincipal(num);
        }
    }

    /// <summary>
    /// Metodo para cambiar el color secundario de la ropa
    /// </summary>
    /// <param name="num"> para indicar cual color </param>
    public void CambioColorSecundario(int num)
    {
        // Asigamos a la posicion correcta el color seleccionado
        colores[3] = num;
        esColor = true;
        ConvertirATexto();

        // Establecemos el color
        for (int i = 0; i < partesHombre.Length; i++)
        {
            partesHombre[i].EstablecerColorSecundario(num);
            partesMujer[i].EstablecerColorSecundario(num);
        }

        for (int i = 0; i < partesOtros.Length; i++)
        {
            partesOtros[i].EstablecerColorSecundario(num);
        }
    }

    /// <summary>
    /// Para cambio de color de piel en el material
    /// </summary>
    /// <param name="cual"> para verificar si es hombre o mujer</param>
    public void CambiarMaterialPiel(int cual)
    {
        // Asigamos a la posicion correcta el color seleccionado
        colores[4] = cual;
        esColor = true;
        ConvertirATexto();

        // Establecemos el color
        for (int i = 0; i < partesHombre.Length; i++)
        {
            partesHombre[i].EstablecerMaterialPiel(cual);
            partesMujer[i].EstablecerMaterialPiel(cual);
        }
    }

    /// <summary>
    /// Metodo para cambiar inicializar todos los elementos en el start
    /// </summary>
    public void InicializarElementos()
    {
        for (int i = 0; i < partesHombre.Length; i++)
        {
            partesHombre[i].Inicializar();
            partesHombre[i].padre = this;
            partesHombre[i].EstablecerMaterialPiel(0);
            partesHombre[i].EstablecerColorPrincipal(0);
            partesHombre[i].EstablecerColorSecundario(0);

            partesMujer[i].Inicializar();
            partesMujer[i].padre = this;
            partesMujer[i].EstablecerMaterialPiel(0);
            partesMujer[i].EstablecerColorPrincipal(0);
            partesMujer[i].EstablecerColorSecundario(0);
        }

        for (int i = 0; i < partesOtros.Length; i++)
        {
            partesOtros[i].padre = this;
            partesOtros[i].Inicializar();
        }
    }

    /// <summary>
    /// Metodo para obtener los colores de la paleta
    /// </summary>
    /// <param name="t"> identifica el elemento</param>
    /// <param name="i"> la posicion </param>
    /// <returns></returns>
    public Color GetColor(TipoElemento t, int i)
    {
        switch (t)
        {
            case TipoElemento.maleta:
                return paletaCejas[i];
            case TipoElemento.cabello:
                return paletaCabello[i];
            case TipoElemento.ojos:
                return paletaOjos[i];
            case TipoElemento.cejas:
                return paletaCabello[i];
            case TipoElemento.ropa:
                return paletaRopa[i];
            case TipoElemento.piel:
                return paletaPiel[i];
            default:
                return paletaRopa[i];
        }
    }

    /// <summary>
    /// Metodo para los colores de la paleta de piel
    /// </summary>
    /// <param name="i"> la posicon de la paleta</param>
    /// <returns></returns>
    public Color GetColorPiel(int i)
    {
        return paletaPiel[i];
    }

    /// <summary>
    /// Metodo para obtener la paleta de colores
    /// </summary>
    /// <param name="t"> identifica el elemento </param>
    /// <returns></returns>
    public Color[] GetPaleta(TipoElemento t)
    {
        switch (t)
        {
            case TipoElemento.maleta:
                return paletaCejas;
            case TipoElemento.cabello:
                return paletaCabello;
            case TipoElemento.ojos:
                return paletaOjos;
            case TipoElemento.cejas:
                return paletaCabello;
            case TipoElemento.ropa:
                return paletaRopa;
            case TipoElemento.piel:
                return paletaPiel;
            default:
                return paletaRopa;
        }
    }
}


[System.Serializable]
public class ElementoPersonalizable
{
    public string           nombre;
    public TipoElemento     tipo;
    public Personalizacion  padre;
    public GameObject[]     elementos;
    public int              activo;
    public List<Material>   materialesPiel;
    public List<Material>   materialesGenerales;
    int                     iColor1;
    int                     iColor2;


    public void CambiarTamaño(float valor)
    {
        for (int i = 0; i < elementos.Length; i++)
        {
            // Validamos que el objeto SkinnedMeshRenderer si cuente con la propiedad BlendShapes
            if (elementos[i].gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh != null && elementos[i].gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.blendShapeCount > 0)
            {
                // Aumentamos o disminuimos el valor de BlendShapes
                elementos[i].gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, valor);
            }
        }
    }

    /// <summary>
    /// Establecemos el elemento activo en personalizacion
    /// </summary>
    public void Establecer()
    {
        for (int i = 0; i < elementos.Length; i++)
        {
            elementos[i].SetActive(i == activo);
        }
    }

    /// <summary>
    /// Inicializamos los elementos
    /// </summary>
    public void Inicializar()
	{
        materialesPiel = new List<Material>();
        materialesGenerales = new List<Material>();

        Renderer mr;
		for (int i = 0; i < elementos.Length; i++)
		{
            mr = elementos[i].GetComponent<Renderer>();
			if (mr != null)
			{
                for (int j = 0; j < mr.materials.Length; j++)
                {
                    if (mr.materials[j].name.Substring(0, 3).Equals("SKN"))
                    {
                        materialesPiel.Add(mr.materials[j]);
                    }
                    else
                    {
                        materialesGenerales.Add(mr.materials[j]);
                    }
                }
            }
		}
	}

    /// <summary>
    /// Establecemos el material para el color de piel
    /// </summary>
    /// <param name="num"> numero del material </param>
    public void EstablecerMaterialPiel(int num)
    {
        iColor1 = num;
        for (int i = 0; i < materialesPiel.Count; i++)
        {
            materialesPiel[i].SetColor("_ColorPrincipal", padre.GetColorPiel(iColor1));
        }
    }

    /// <summary>
    /// Establecemos un color principal de ropa
    /// </summary>
    /// <param name="num"> numero del material </param>
    public void EstablecerColorPrincipal(int num)
    {
        iColor1 = num;
        for (int i = 0; i < materialesGenerales.Count; i++)
        {
            materialesGenerales[i].SetColor("_ColorPrincipal", padre.GetColor(tipo, iColor1));
        }
    }

    /// <summary>
    /// Establecemos un color secundario de ropa
    /// </summary>
    /// <param name="num"> numero del material </param>
    public void EstablecerColorSecundario(int num)
    {
        iColor2 = num;
        for (int i = 0; i < materialesGenerales.Count; i++)
        {
            materialesGenerales[i].SetColor("_ColorSecundario", padre.GetColor(tipo, iColor2));
        }
    }

    /// <summary>
    /// Establecemos un color de piel
    /// </summary>
    /// <param name="num" >numero del material</param>
    public void EstablecerColorPiel(int num)
    {
        iColor2 = num;
        for (int i = 0; i < materialesPiel.Count; i++)
        {
            materialesPiel[i].SetColor("_ColorPrincipal", padre.GetColorPiel(iColor2));
        }
    }

    /// <summary>
    /// Establecemos un color general
    /// </summary>
    /// <param name="num"> numero del material</param>
    public void EstablecerColorGeneral(int num)
    {
        iColor1 = num;
        for (int i = 0; i < materialesGenerales.Count; i++)
        {
            materialesGenerales[i].color = padre.GetColor(tipo, iColor1);
        }

    }

    /// <summary>
    /// Para cambiar entre elementos
    /// </summary>
    public void Siguiente()
    {
        activo = (activo+1) % elementos.Length;
        Establecer();
    }

    /// <summary>
    /// Para desactivar los elementos al cambiar de genero
    /// </summary>
    public void Desactivar()
    {
        for (int i = 0; i < elementos.Length; i++)
        {
            elementos[i].SetActive(false);
        }
    }
}

/// <summary>
/// Para identificar el tipo de elemento a seleccionar
/// </summary>
public enum TipoElemento
{
    maleta,
    cabello,
    ojos,
    cejas,
    ropa,
    piel
}