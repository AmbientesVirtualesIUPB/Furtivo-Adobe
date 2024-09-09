using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarVolante : MonoBehaviour
{
    public int cual;
    public VRControlCarro control;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			control.ActivarInput(cual);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			control.DesactivarInput(cual);
		}
	}
}
