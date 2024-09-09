    using JetBrains.Annotations;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;



public class Split : MonoBehaviour
{

    //Variable para las posiciones de guardado
    public int[] pos;
    public string textoCargado;
    //Referencia al objeto de guardado
    public GameObject SaveManagers;


    //variables para identificar el genero
    public bool genero;
    public int genNum;

    //Arrays para almacenar todos los objetos de cada caracteristica
    public GameObject[] accesorios_Reloj;
    public GameObject[] maleta_F;
    public GameObject[] maleta_M;
    public GameObject[] ropas_F;
    public GameObject[] ropas_M;
    public GameObject[] cabezas_F;
    public GameObject[] cabezas_M;
    public GameObject[] cejas_F;
    public GameObject[] cejas_M;
    public GameObject[] pelos_F;
    public GameObject[] pelos_M;
    public GameObject[] sombreros;
    public GameObject[] zapatos;

    private void Awake()
    {
        //Inicializamos posiciones
        pos = new int[13];
        //Cargamos datos
        //SaveManagers.GetComponent<SaveManager>().CargarDatos();
    }

    public void Start()
    {
        Gen();
    }
    
    //Metodo invocado desde BtnReloj para el cambio de estilo del reloj
    public void Reloj() 
    {
        //Asignamos la posicion para los objetos de la lista
        pos[0]++;

        //Si sobrepasa el tamaño del array lo volvemos a cero
        if (pos[0] > accesorios_Reloj.Length - 1)
        {
            pos[0] = 0;
        }

        //Al presionar el botón Pasamos y activamos uno a uno los estilos
        for (int i = 0; i < accesorios_Reloj.Length; i++)
        {
            accesorios_Reloj[i].SetActive(pos[0] == i);
        }

        //Guardamos el ultimo item elegido en la posicion indicada
        //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[0] , 0);
    }

    //Metodo invocado desde BtnMaleta para el cambio de estilo de la maleta
    public void Maleta()
    {
        //Validamos si es Femenino o Masculino
        //Femenino
        if (genero)
        {
            //Asignamos la posicion
            pos[1]++;

            //Si sobrepasa el tamaño del array lo volvemos a cero
            if (pos[1] > maleta_F.Length - 1)
            {
                pos[1] = 0;
            }

            //Al presionar el botón Pasamos y activamos uno a uno los estilos
            for (int i = 0; i < maleta_F.Length; i++)
            {
                maleta_F[i].SetActive(pos[1] == i);
            }

            //Guardamos el ultimo item elegido en la posicion indicada
            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[1], 1);
        }
        else
        //Masculino
        {
            pos[2]++;

            if (pos[2] > maleta_M.Length - 1)
            {
                pos[2] = 0;
            }

            for (int i = 0; i < maleta_M.Length; i++)
            {
                maleta_M[i].SetActive(pos[2] == i);
            }

            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[2], 2);
        }
    }
   
    //Metodo invocado desde BtnCuerpo para el cambio de estilo de la ropa
    public void Ropa()
    {
        //Validamos si es Femenino o Masculino
        //Femenino
        if (genero)
        {
            //Asignamos la posicion
            pos[3]++;

            //Si sobrepasa el tamaño del array lo volvemos a cero
            if (pos[3] > ropas_F.Length - 1)
            {
                pos[3] = 0;
            }

            //Al presionar el botón Pasamos y activamos uno a uno los estilos
            for (int i = 0; i < ropas_F.Length; i++)
            {
                ropas_F[i].SetActive(pos[3] == i);
            }

            //Guardamos el ultimo item elegido en la posicion indicada
            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[3], 3);
        }
        //Masculino
        else
        {
            pos[4]++;

            if (pos[4] > ropas_M.Length - 1)
            {
                pos[4] = 0;
            }

            for (int i = 0; i < ropas_M.Length; i++)
            {
                ropas_M[i].SetActive(pos[4] == i);
            }

            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[4], 4);
        }
    }

    //Metodo invocado desde BtnCabeza para el cambio de estilo de la cara
    public void Cabeza()
    {
        //Validamos si es Femenino o Masculino
        //Femenino
        if (genero)
        {
            //Asignamos la posicion
            pos[5]++;

            //Si sobrepasa el tamaño del array lo volvemos a cero
            if (pos[5] > cabezas_F.Length - 1)
            {
                pos[5] = 0;
            }

            //Al presionar el botón Pasamos y activamos uno a uno los estilos
            for (int i = 0; i < cabezas_F.Length; i++)
            {
                cabezas_F[i].SetActive(pos[5] == i);
            }

            //Guardamos el ultimo item elegido en la posicion indicada
            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[5], 5);
        }
        else
        //Masculino
        {
            pos[6]++;

            if (pos[6] > cabezas_M.Length - 1)
            {
                pos[6] = 0;
            }

            for (int i = 0; i < cabezas_M.Length; i++)
            {
                cabezas_M[i].SetActive(pos[6] == i);
            }

            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[6], 6);
        }
    }


    //Metodo invocado desde BtnCejas para el cambio de estilo de las cejas
    public void Cejas()
    {
        //Validamos si es Femenino o Masculino
        //Femenino
        if (genero)
        {
            //Asignamos la posicion
            pos[7]++;

            //Si sobrepasa el tamaño del array lo volvemos a cero
            if (pos[7] > cejas_F.Length - 1)
            {
                pos[7] = 0;
            }

            //Al presionar el botón Pasamos y activamos uno a uno los estilos
            for (int i = 0; i < cejas_F.Length; i++)
            {
                cejas_F[i].SetActive(pos[7] == i);
            }
            //Guardamos el ultimo item elegido en la posicion indicada
            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[7], 7);
        }
        else
        //Masculino
        {
            pos[8]++;

            if (pos[8] > cejas_M.Length - 1)
            {
                pos[8] = 0;
            }

            for (int i = 0; i < cejas_M.Length; i++)
            {
                cejas_M[i].SetActive(pos[8] == i);
            }
            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[8], 8);
        }
    }


    //Metodo invocado desde BtnPelos para el cambio de estilo del peinado
    public void Pelos()
    {
        //Validamos si es Femenino o Masculino
        //Femenino
        if (genero)
        {
            //Asignamos la posicion
            pos[9]++;

            //Si sobrepasa el tamaño del array lo volvemos a cero
            if (pos[9] > pelos_F.Length - 1)
            {
                pos[9] = 0;
            }

            //Al presionar el botón Pasamos y activamos uno a uno los estilos
            for (int i = 0; i < pelos_F.Length; i++)
            {
                pelos_F[i].SetActive(pos[9] == i);
            }
            //Guardamos el ultimo item elegido en la posicion indicada
            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[9], 9);
        }
        else
        //Masculino
        {
            pos[10]++;

            if (pos[10] > pelos_M.Length - 1)
            {
                pos[10] = 0;
            }

            for (int i = 0; i < pelos_M.Length; i++)
            {
                pelos_M[i].SetActive(pos[10] == i);
            }
            //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[10], 10);
        }
    }

    //Metodo invocado desde BtnSombrero para el cambio de estilo del sombrero
    public void Sombrero()
    {
        //Asignamos la posicion
        pos[11]++;

        //Si sobrepasa el tamaño del array lo volvemos a cero
        if (pos[11] > sombreros.Length - 1)
        {
            pos[11] = 0;
        }

        //Al presionar el botón Pasamos y activamos uno a uno los estilos
        for (int i = 0; i < sombreros.Length; i++)
        {
            sombreros[i].SetActive(pos[11] == i);
        }
        //Guardamos el ultimo item elegido en la posicion indicada
        //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[11], 11);
    }

    //Metodo invocado desde BtnZapatos para el cambio de estilo de zapatos
    public void Zapatos()
    {
        //Asignamos la posicion
        pos[12]++;

        //Si sobrepasa el tamaño del array lo volvemos a cero
        if (pos[12] > zapatos.Length - 1)
        {
            pos[12] = 0;
        }

        //Al presionar el botón Pasamos y activamos uno a uno los estilos
        for (int i = 0; i < zapatos.Length; i++)
        {
            zapatos[i].SetActive(pos[12] == i);
        }
        //Guardamos el ultimo item elegido en la posicion indicada
        //SaveManagers.GetComponent<SaveManager>().PesonalizacionPersonaje(pos[12], 12);
    }

    //Traemos los datos de cada caracteristica
    public void TomarDatos()
    {   //0
        for (int i = 0; i < accesorios_Reloj.Length; i++)
        {
            accesorios_Reloj[i].SetActive(pos[0] == i);
        }
        //1
        for (int i = 0; i < maleta_F.Length; i++)
        {
            maleta_F[i].SetActive(pos[1] == i);
        }
        //2
        for (int i = 0; i < maleta_M.Length; i++)
        {
            maleta_M[i].SetActive(pos[2] == i);
        }
        //3
        for (int i = 0; i < ropas_F.Length; i++)
        {
            ropas_F[i].SetActive(pos[3] == i);
        }
        //4
        for (int i = 0; i < ropas_M.Length; i++)
        {
            ropas_M[i].SetActive(pos[4] == i);
        }
        //5
        for (int i = 0; i < cabezas_F.Length; i++)
        {
            cabezas_F[i].SetActive(pos[5] == i);
        }
        //6     
        for (int i = 0; i < cabezas_M.Length; i++)
        {
            cabezas_M[i].SetActive(pos[6] == i);
        }
        //7 
        for (int i = 0; i < cejas_F.Length; i++)
        {
            cejas_F[i].SetActive(pos[7] == i);
        }
        //8
        for (int i = 0; i < cejas_M.Length; i++)
        {
            cejas_M[i].SetActive(pos[8] == i);
        }
        //9
        for (int i = 0; i < pelos_F.Length; i++)
        {
            pelos_F[i].SetActive(pos[9] == i);
        }
        //10 
        for (int i = 0; i < pelos_M.Length; i++)
        {
            pelos_M[i].SetActive(pos[10] == i);
        }//11   
        for (int i = 0; i < sombreros.Length; i++)
        {
            sombreros[i].SetActive(pos[11] == i);
        }
        //12  
        for (int i = 0; i < zapatos.Length; i++)
        {
            zapatos[i].SetActive(pos[12] == i);
        }
    }

    //Cambiar genero a Maculino
    public void Masculino() 
    {
        genero = false;
        PlayerPrefs.SetInt("genero", 0);
        
        Gen();
    }

    //Cambiar genero a Femenino
    public void Femenino() 
    {
        genero = true;
        PlayerPrefs.SetInt("genero", 1);
        Gen();
    }

    public void Gen()
    {
        //Obtenemos el genero para saber el ultimo escogido
        genNum = PlayerPrefs.GetInt("genero", 0);

        //Validamos si es 1 = Femenino, 0 = Maculino
        if (genNum == 1)
        {
            genero = true;
        }
        else
        {
            genero = false;
        }

        //Traemos los datos
        TomarDatos();

        //Si no es femenino desactivamos los objetos de ese genero
        if (!genero)
        {
            for (int i = 0; i < ropas_F.Length; i++)
            {
                ropas_F[i].SetActive(false);
            }
            for (int i = 0; i < cabezas_F.Length; i++)
            {
                cabezas_F[i].SetActive(false);
            }
            for (int i = 0; i < cejas_F.Length; i++)
            {
                cejas_F[i].SetActive(false);

            }
            for (int i = 0; i < pelos_F.Length; i++)
            {
                pelos_F[i].SetActive(false);
            }
            for (int i = 0; i < maleta_F.Length; i++)
            {
                maleta_F[i].SetActive(false);
            }
        }

        //Si no es masculino desactivamos los objetos de ese genero
        if (genero)
        {
            for (int i = 0; i < ropas_M.Length; i++)
            {
                ropas_M[i].SetActive(false);
            }
            for (int i = 0; i < cabezas_M.Length; i++)
            {
                cabezas_M[i].SetActive(false);
            }
            for (int i = 0; i < cejas_M.Length; i++)
            {
                cejas_M[i].SetActive(false);

            }
            for (int i = 0; i < pelos_M.Length; i++)
            {
                pelos_M[i].SetActive(false);
            }
            for (int i = 0; i < maleta_M.Length; i++)
            {
                maleta_M[i].SetActive(false);
            }
        }
    }

    public string ConvertirATexto()
    {
        string texto = "";
        for (int i = 0;i < pos.Length;i++)
        {
            texto += (texto.Length > 0) ? "|" : "";
            texto += pos[i].ToString();
        }
        return texto;

    }

    public void ConvertirDesdeTexto(string texto)
    {
        string[] nombre = texto.Split("|");

        for (int i = 0; i < nombre.Length; i++)
        {
            pos[i] = int.Parse(nombre[i]);
        }
        TomarDatos();
    }

    [ContextMenu("Generar")]
    public void Generar()
    {

        print(ConvertirATexto());
    }

    [ContextMenu("Cargar")]
    public void Cargar()
    {

        ConvertirDesdeTexto(textoCargado);

    }
}
