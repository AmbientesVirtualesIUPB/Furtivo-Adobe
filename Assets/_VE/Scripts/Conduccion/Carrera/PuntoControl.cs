using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoControl : MonoBehaviour
{
    private void Start()
    {
        ManagerPuntoControl.Instance.controls.Add(this);  
    }

}
