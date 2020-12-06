using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bola : MonoBehaviour
{
    Rigidbody rigi;
    Collider col;
    GameManager controlador;
    // Start is called before the first frame update
    void Start()
    {
        controlador = FindObjectOfType<GameManager>();
        rigi = gameObject.GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("goal")) {
            col.enabled = false;
            rigi.constraints = RigidbodyConstraints.None;
            controlador.aumentaPuntos();
        }
    }

}
