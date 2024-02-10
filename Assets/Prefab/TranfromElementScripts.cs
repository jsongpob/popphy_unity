using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranfromElementScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3((float)0.1, (float)0.1, 0);
        transform.localScale += new Vector3((float)0.1, (float)0.1, 0);
    }
}
