using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSexi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal")) * 5 * Time.deltaTime);
    }
}
