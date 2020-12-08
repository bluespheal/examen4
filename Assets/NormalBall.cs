using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviour
{
    protected Vector3 Startpos;
    protected bool ResetIt;
    public Vector3 goal_cercano;
    public LineRenderer line;
    Rigidbody rigi;
    Collider col;

    // Use this for initialization
    void Start()
    {
        Startpos = transform.position;
        rigi = gameObject.GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (transform.position.y < -10.01f)
            gameObject.SetActive(false);

        if (ResetIt)
        {
            ResetIt = false;
            transform.position = Startpos;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    private void Update()
    {
            line.SetPosition(0, transform.position + (transform.position-goal_cercano)/(transform.position - goal_cercano).magnitude/6);
            line.SetPosition(1, goal_cercano);
    }

    public void ResetBall()
    {
        ResetIt = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("goal"))
        {
            line.enabled = false;
            col.enabled = false;
            rigi.velocity = Vector3.zero;
        }
    }
}
