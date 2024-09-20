using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;   

public class Teclado : MonoBehaviour
{
    public TMP_InputField inputField;
    public string caracter;

    // Start is called before the first frame update
    void Start()
    {
        if (inputField.text == "")
        {
            inputField.text = ConfiguracionGeneral.configuracionDefault.GetIP();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AgregarCaracter()
    {
        inputField.text += caracter;
    }

    public void Borrar()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void GuardarIp()
    {
        ConfiguracionGeneral.configuracionDefault.CambiarIp(inputField.text);
    }
}
