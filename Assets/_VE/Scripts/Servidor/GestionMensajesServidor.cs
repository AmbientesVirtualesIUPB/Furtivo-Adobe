using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Morion Servidor/Gestion Mensajes Servidor")]
public class GestionMensajesServidor : MonoBehaviour
{
    public static GestionMensajesServidor singeton;
	public bool debugEnConsola = false;
	public List<MorionTransform> morionTransforms = new List<MorionTransform>();

	private void Awake()
	{
		singeton = this;
	}

	public void RecibirMensaje(string mensaje)
	{
		if (debugEnConsola) print("MENSAJE:" + mensaje);
		string codigo = mensaje.Substring(0, 4);
		string msj = mensaje.Substring(4);
		switch (codigo)
		{
			case "PR00":
				PR00(msj);
				break;
			case "AT00":
				AT00(msj);
				break;
			case "AC00":
				AC00(msj);
				break;
			default:
				break;
		}
	}
	public void EnviarMensaje(string msj)
	{
		Servidor.singleton.EnviarMensaje(msj);
	}
	public void PR00(string msj)
	{
		
	}
	public void AT00(string msj)
	{
		if(debugEnConsola) print("Mensaje AT00 :::::::> " + msj);
		Posicion0 po0 = JsonUtility.FromJson<Posicion0>(msj);
		for (int i = 0; i < morionTransforms.Count; i++)
		{
			if (morionTransforms[i].morionID.GetID() == po0.id_con)
			{
				morionTransforms[i].ActualizarObjetivos(po0);
			}
		}
	}
	public void AC00(string msj)
	{

	}

	public void EnviarActualizacionTransform(MorionID morionID, Transform trans) 
	{
		Posicion0 pos = new Posicion0();
		pos.id_con = morionID.GetID();
		pos.rotacion = trans.eulerAngles;
		pos.posicion = trans.position;
		string msj = "AT00" + JsonUtility.ToJson(pos);
		Servidor.singleton.EnviarMensaje(msj);
	}

	public void MatricularMorionTransform(MorionTransform mt)
	{
		for (int i = 0; i < morionTransforms.Count; i++)
		{
			if (mt.morionID.GetID().Equals(morionTransforms[i].morionID.GetID()))
			{
				morionTransforms[i] = mt;
				return;
			}
		}
		morionTransforms.Add(mt);
	}
}
