using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spot : MonoBehaviour
{
    public GameObject obje;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(obje.transform);
    }
}
