using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBall : MonoBehaviour
{

    protected Vector3 Startpos;
    protected bool ResetIt;

    public DataCalculator data;
    public Rigidbody rigi;

    // Use this for initialization
    void Start()
    {
        Startpos = transform.position;
        rigi = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (transform.position.y < -3.0f)
        {
            transform.position = new Vector3(0, 0.08f, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (ResetIt)
        {
            ResetIt = false;
            transform.position = Startpos;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    public void ResetBall()
    {
        ResetIt = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("goal"))
        {
            ResetBall();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("NoBall"))
        {
            print("choco");
            data.UpdateInputData(rigi, other.gameObject.GetComponent<Rigidbody>());
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("NoBall"))
        {
            print("late");
            data.UpdateOutputData(rigi, other.gameObject.GetComponent<Rigidbody>());
        }
    }
}
