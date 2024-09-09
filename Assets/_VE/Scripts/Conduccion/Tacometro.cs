using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tacometro : MonoBehaviour
{
    public Conducir conducir;
    public Transform aguja;
    float pdt;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (conducir != null && aguja != null)
        {
            //float t = Mathf.Abs(conducir.carSpeed);
            t = Mathf.Lerp(t, Mathf.Abs(conducir.carSpeed / 100), 0.05f);
            pdt = (1 - t) * 220 + t * 76;
            aguja.localEulerAngles = Vector3.forward * pdt;
        }else
        {
            Debug.LogError("Falta inicializar componenetes del tacometro");
        }
    }

}
