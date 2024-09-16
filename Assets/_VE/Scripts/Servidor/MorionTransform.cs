using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MorionID))]
[AddComponentMenu("Morion Servidor/Morion Transform")]
public class MorionTransform : MonoBehaviour
{

	public bool     sincronizarPosicion;
	public bool     sincronizarRotacion;
    public bool     debugEnConsola = false;
    public bool     opcionesAvanzadas = false;

    [ConditionalHide("opcionesAvanzadas", true)]
    public Vector3  posicionObjetivo;
    [ConditionalHide("opcionesAvanzadas", true)]
    public Vector3  rotacionObjetivo;

    [ConditionalHide("opcionesAvanzadas", true)]
    public float    toleranciaPosicion  = 0.2f;
    [ConditionalHide("opcionesAvanzadas", true)]
    public float    toleranciaRotacion  = 10f;
    [ConditionalHide("opcionesAvanzadas", true)]
    public float    velRotacion         = 5f;
    [ConditionalHide("opcionesAvanzadas", true)]
    public float    velTranslacion      = 10f;

    [ConditionalHide("opcionesAvanzadas", true)]
    public float    periodoEsperas = 0.2f;

    private float   _toleranciaPosicion;
    private float   _toleranciaRotacion;

    [HideInInspector]
    public MorionID morionID;
	private Vector3 posAnterior;
	private Vector3 rotAnterior;
	private void Awake()
	{
		morionID = GetComponent<MorionID>();
	}
    IEnumerator Start()
    {
        posAnterior         = transform.position;
        rotAnterior         = transform.eulerAngles;
        _toleranciaPosicion = toleranciaPosicion * toleranciaPosicion;
        _toleranciaRotacion = toleranciaRotacion * toleranciaRotacion;
        yield return new WaitUntil(() => Servidor.singleton.conectado);
        StartCoroutine(UpdateLento());
		if (GestionMensajesServidor.singeton == null)
		{
            if (debugEnConsola) Debug.LogError("No se ha encontrado un Gestor de Mensajes en el cuál matricular el MorionTrnsform");
		}
		else
		{
            GestionMensajesServidor.singeton.MatricularMorionTransform(this);
        }
    }
    public void Inicializar(Vector3 posicion, Vector3 rotacion)
    {
        transform.position      = posicion;
        transform.eulerAngles   = rotacion;
        posAnterior             = transform.position;
        rotAnterior             = transform.eulerAngles;
        posicionObjetivo        = posicion;
        rotacionObjetivo        = rotacion;
    }


    private void Update()
    {
		if (!morionID.GetOwner())
		{
            if(sincronizarPosicion)
			{
                //            if((transform.position - posicionObjetivo).magnitude > velTranslacion*Time.deltaTime)
                //{
                //                transform.position += (posicionObjetivo - transform.position).normalized * velTranslacion * Time.deltaTime;
                //}
                //else if ((transform.position - posicionObjetivo).magnitude > 0)
                //{
                //                transform.position = posicionObjetivo;
                //}
                transform.position = Vector3.Lerp(transform.position, posicionObjetivo, Time.deltaTime * velTranslacion);
			}
            if(sincronizarRotacion) 
                transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(rotacionObjetivo), Time.deltaTime * velRotacion);
		}
    }

    IEnumerator UpdateLento()
    {
        while (true)
        {
            yield return new WaitForSeconds(periodoEsperas);
			if (morionID.GetOwner())
			{
                if (((posAnterior - transform.position).sqrMagnitude > _toleranciaPosicion ||
                (rotAnterior - transform.eulerAngles).sqrMagnitude > _toleranciaRotacion))
                {
                    if(sincronizarPosicion || sincronizarRotacion) GestionMensajesServidor.singeton.EnviarActualizacionTransform(morionID, transform);
                    posAnterior = transform.position;
                    rotAnterior = transform.eulerAngles;
                }
            }
        }
    }

    public void ActualizarObjetivos(Posicion0 po0)
	{
        posicionObjetivo = po0.posicion;
        rotacionObjetivo = po0.rotacion;
        print("Actualizando posob" + po0.id_con);
	}
}
