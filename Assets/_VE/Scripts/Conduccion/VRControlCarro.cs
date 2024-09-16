using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRControlCarro : MonoBehaviour
{
    public Inputable inpDerecha;
    public Inputable inpIzquierda;

    public GameObject controlIzquierdo;
    public GameObject controlDerecho;
    bool grabIz, grabDer;

    public float modificadorDistancia;
    public float modificadorAngulo;
    public Transform pivote;

    public Conducir conducir;

    public bool autoRegularPosicion;
    public Transform offsetCamara;
    public Transform camara;
    public Transform puntoDeControl;

	private IEnumerator Start()
	{
        yield return new WaitForSeconds(3);
		if (autoRegularPosicion)
		{
            offsetCamara.position = offsetCamara.position + (puntoDeControl.position - camara.position);
			
        }
	}
	void Update()
    {
        grabIz = inpIzquierda.grab.action.ReadValue<float>() > 0.5f;
        grabDer = inpDerecha.grab.action.ReadValue<float>() > 0.5f;

        if (grabDer && inpDerecha.activable)
		{
            controlDerecho.SetActive(false);
            if (inpDerecha.activable)
            {
                inpDerecha.touchInput.buttonPressed = inpDerecha.accion.action.ReadValue<float>() > 0.5f;
            }
		}
		else
		{
            controlDerecho.SetActive(true);
        }

		if (grabIz && inpIzquierda.activable)
		{
            controlIzquierdo.SetActive(false);
            if (inpIzquierda.activable)
            {
                inpIzquierda.touchInput.buttonPressed = inpIzquierda.accion.action.ReadValue<float>() > 0.5f;
            }
        }
        else
        {
            controlIzquierdo.SetActive(true);
        }

		if (grabIz && grabDer && inpIzquierda.activable && inpDerecha.activable)
		{
            ActualizarRotacionCabrilla();
		}
    }

    void ActualizarRotacionCabrilla()
	{
        float d = controlDerecho.transform.position.y - controlIzquierdo.transform.position.y;
        d *= modificadorDistancia;
        d = Mathf.Clamp(d, -1, 1);
        conducir.steeringAxis = -d;
        print("Paso 1");
        pivote.localEulerAngles = Vector3.forward * d * modificadorAngulo;
        print("Paso 2: " + pivote.localEulerAngles + "///" + (Vector3.forward * d * modificadorAngulo));
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
    public InputActionProperty grab;
    public InputActionProperty accion;
    public bool activable = false;
}
