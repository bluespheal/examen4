using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCalculator : MonoBehaviour
{
    public Text restitucionText;
    public Text choqueText;
    public Text masaText;
    public Text velocidadText;
    public Text miuText;
    public Text formText;

    public float m1;
    public float m2;

    public float v1;
    public float v2;

    public float u1;
    public float u2;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateInputData(Rigidbody object1, Rigidbody object2)
    {
        m1 = object1.mass;
        v1 = object1.velocity.magnitude;

        m2 = object2.mass;
        v2 = object2.velocity.magnitude;

        UpdateMass();
        UpdateSpeed();
    }

    public void UpdateOutputData(Rigidbody object1, Rigidbody object2)
    {
        u1 = object1.velocity.magnitude;
        u2 = object2.velocity.magnitude;

        UpdateRestitution();
        UpdateMiu();
        UpdateForm();
    }


    public void UpdateData()
    {
        UpdateRestitution();
    }


    private void UpdateMass()
    {
        masaText.text = "Masa: " + m1;
    }
    private void UpdateSpeed()
    {
        velocidadText.text = "Velocidad: " + v1;
    }
    private void UpdateMiu()
    {
        miuText.text = "Miu: " + u1;
    }
    private void UpdateRestitution()
    {
        restitucionText.text = "Valor de Restitución: " + calculateRestitution();
        UpdateChoque(calculateRestitution());
    }

    private float calculateRestitution()
    {
        return -((u2 - u1) / (v1 - v2));
    }

    private void UpdateChoque(float restValue)
    {
        if (restValue >= 0.9f)
        {
            choqueText.text = "Tipo de Choque: " + "Elástico";
        }
        else if (restValue <= 0.1f)
        {
            choqueText.text = "Tipo de Choque: " + "Inelástico";
        }
        else
        {
            choqueText.text = "Tipo de Choque: " + "Parcialmente elástico";
        }
    }

    private void UpdateForm()
    {
        // (m1 * v1) + (m2 * u2) = (m1 * v1) + (m2 * v2)
        formText.text = m1 + " * " + v1 + " + " + m2 + " * " + v2 + " = " + m1 + " * " + u1 + " + " + m2 + " * " + u2 + "\n" + (m1 * v1) + (m2 * u2) + " = " + (m1 * v1) + (m2 * v2);

    }

}
