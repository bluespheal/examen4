using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    protected LineRenderer line;
    protected WhiteBall WhiteBall;
    Color Jugador1 = Color.red;
    Color Jugador2 = Color.blue;
    public Text Text;
    public Text Text2;
    protected int PuntosP1 = 0;
    protected int PuntosP2 = 0;
    protected NormalBall[] balls;
    public bool turno;
    public bool cambiando;
    float alpha = 1.0f;
    Gradient gradient;
    // Use this for initialization
    void Start()
    {

    public goal[] goals;
    void Start()
    {
        line = GameObject.FindGameObjectWithTag("apuntador").gameObject.GetComponent<LineRenderer>();
        super_branch_de_chema
        WhiteBall = FindObjectOfType<WhiteBall>();
        Text.text = "Jugador 1: " + PuntosP1;
        Text2.text = "Jugador 2: " + PuntosP2;
        balls = FindObjectsOfType<NormalBall>();
        turno = true;
        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Jugador1, 0.0f), new GradientColorKey(Jugador1, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        line.gameObject.GetComponent<LineRenderer>().colorGradient = gradient;
        goals = FindObjectsOfType<goal>();
        dibujarLineas();
    }

    // Update is called once per frame
    void Update()
    {
        dibujarLineas();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var direction = Vector3.zero;

        if (Physics.Raycast(ray, out hit))
        {
            var ballPos = new Vector3(WhiteBall.transform.position.x, 0.1f, WhiteBall.transform.position.z);
            var mousePos = new Vector3(hit.point.x, 0.1f, hit.point.z);
            line.SetPosition(0, mousePos);
            line.SetPosition(1, ballPos);
            direction = (mousePos - ballPos);
        }

        if (Input.GetMouseButtonDown(0) && line.gameObject.activeSelf)
        {
            line.gameObject.SetActive(false);
            WhiteBall.GetComponent<Rigidbody>().velocity = direction*4f;
        }

        if (!line.gameObject.activeSelf && WhiteBall.GetComponent<Rigidbody>().velocity.magnitude < 0.1f && !cambiando)
        {
            cambiando = true;            
            StartCoroutine(pasaturno());
            line.gameObject.SetActive(true);
        }
    }

    public void Reset()
    {
        WhiteBall.ResetBall();
        foreach (var ball in balls)
        {
            ball.gameObject.SetActive(true);
            ball.ResetBall();
        }
        PuntosP1 = 0;
        PuntosP2 = 0;
    }

    IEnumerator pasaturno()
    {
        yield return new WaitForSeconds(5f);
        WhiteBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (NormalBall bola in balls)
        {
            bola.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        
        turno = !turno;
        if(turno)
        {
            gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Jugador1, 0.0f), new GradientColorKey(Jugador1, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
            line.gameObject.GetComponent<LineRenderer>().colorGradient = gradient;
        }
        else
        {
            gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Jugador2, 0.0f), new GradientColorKey(Jugador2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
            line.gameObject.GetComponent<LineRenderer>().colorGradient = gradient;
        }
        line.gameObject.SetActive(true);
        cambiando = false;
    }

    public void aumentaPuntos()
    {
        if(turno)
        {
            PuntosP1++;
            Text.text = "Jugador 1: " + PuntosP1; 
        }
        else
        {
            PuntosP2++;
            Text2.text = "Jugador 2: " + PuntosP2;
        }
    }
    public void dibujarLineas() {
        Vector3 b;
        Vector3 g;
        Vector3 min_g = goals[0].transform.position;
        for (int i = 0; i < balls.Length; i++) {
            min_g = goals[0].transform.position;
            b = balls[i].transform.position;
            for (int j = 0; j < goals.Length; j++)
            {
                g = goals[j].transform.position;
                if (Vector3.Distance(b,g)<Vector3.Distance(b, min_g)) {
                    min_g=g;
                    print(goals[j]);
                }
            }
            print(min_g);
            balls[i].goal_cercano = min_g;
        }
    }
}
