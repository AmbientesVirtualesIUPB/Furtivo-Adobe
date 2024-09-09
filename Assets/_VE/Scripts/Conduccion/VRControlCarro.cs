using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRControlCarro : MonoBehaviour
{
    public Inputable inpDerecha;
    public Inputable inpIzquierda;
    void Update()
    {
        if (inpDerecha.activable)
        {
            inpDerecha.touchInput.buttonPressed = inpDerecha.accion.action.ReadValue<float>() > 0.5f;
        }
        if (inpIzquierda.activable)
        {
            inpIzquierda.touchInput.buttonPressed = inpIzquierda.accion.action.ReadValue<float>() > 0.5f;
        }
    }

    public void ActivarInput(int cual)
	{
		if (cual == 0)
		{
            inpDerecha.activable = true;
        }
		else
        {
            inpIzquierda.activable = true;
        }
    }
    public void DesactivarInput(int cual)
    {
        if (cual == 0)
        {
            inpDerecha.activable = false;
            inpDerecha.touchInput.buttonPressed = false;
        }
        else
        {
            inpIzquierda.activable = false;
            inpIzquierda.touchInput.buttonPressed = false;
        }
    }
}

[System.Serializable]
public class Inputable
{
    public PrometeoTouchInput touchInput;
    public InputActionProperty accion;
    public bool activable = false;
}
