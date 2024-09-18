using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InicidorCarrera : MonoBehaviour
{
	public UnityEvent evento;
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			evento.Invoke();
		}
	}
}
