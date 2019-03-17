using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    public InGameController controller;
    public Disparo disparo;
    /*Public variables*/
    public enum jugadores {Jugador1, Jugador2, Jugador3, Jugador4}
    public jugadores jug;

    public float speed = 5f;
    public float gravity = 9.81f;
    public float dashdistance = 5f;
    public Vector3 Drag;
    public float esperaEntreDrag = 5;
    [Space]
    public AudioSource source;
    public Animator anim;
    public GameObject Rmat;
    public Disparo disp;
    public float tiempoRespawnMat = 3;

    private readonly string correr = "corriendo";
    private readonly string baile = "baile";

    private CharacterController _controller;
    private Vector3 _velocity;

    private Vector3 move;
    private float dashLimit = 1;

    public string Horizontal;
    public string Vertical;
    public string Fire;
    public string Power;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        StartCoroutine(Respawn());
    }

    private void Awake()
    {
        disparo.shooter = Fire;
        if (jugadores.Jugador1 == jug)
        {
            disparo.Njug = 1;
        }
        if (jugadores.Jugador2 == jug)
        {
            disparo.Njug = 2;
        }
        if (jugadores.Jugador3 == jug)
        {
            disparo.Njug = 3;
        }
        if (jugadores.Jugador4 == jug)
        {
            disparo.Njug = 4;
        }
    }

/*Move and Power Dash of player */
    private void Update()
    {
        if (controller.comenzar)
        {
            dashLimit -= esperaEntreDrag;

            move = new Vector3(Input.GetAxis(Vertical), 0, Input.GetAxis(Horizontal));
            _controller.Move(move * Time.deltaTime * speed);
            if (move != Vector3.zero)
            {
                transform.forward = move;
                anim.SetBool(correr, true);
            }
            else
            {
                anim.SetBool(correr, false);
            }

            if (Input.GetButtonDown(Power))
            {
                if (dashLimit <= 0)
                {
                    dashLimit = esperaEntreDrag;
                    Debug.Log("Dash");
                    _velocity += (Vector3.Scale(transform.forward, dashdistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 2)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 2)) / -Time.deltaTime))) * 2);
                    source.Stop();
                    source.Play();
                }
            }

            //_velocity.y += gravity * Time.deltaTime;

            _velocity.x /= 1 + Drag.x * Time.deltaTime;
            //_velocity.y /= 1 + Drag.y * Time.deltaTime;
            _velocity.y = 0;
            _velocity.z /= 1 + Drag.z * Time.deltaTime;

            _controller.Move(_velocity * Time.deltaTime);
        }
    }

    /*Dead Player*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            Bala bal = collision.gameObject.GetComponent<Bala>();
            bal.Morir(false);
            if (bal.dueno == 1)
            {
                if (jugadores.Jugador1 == jug)
                {
                    controller.SubirPuntos1(true);
                }
                else
                {
                    controller.SubirPuntos1(false);
                }
            }
            if (bal.dueno == 2)
            {
                if (jugadores.Jugador2 == jug)
                {
                    controller.SubirPuntos2(true);
                }
                else
                {
                    controller.SubirPuntos2(false);
                }
            }
            if (bal.dueno == 3)
            {
                if (jugadores.Jugador3 == jug)
                {
                    controller.SubirPuntos3(true);
                }
                else
                {
                    controller.SubirPuntos3(false);
                }
            }
            if (bal.dueno == 4)
            {
                if (jugadores.Jugador4 == jug)
                {
                    controller.SubirPuntos4(true);
                }
                else
                {
                    controller.SubirPuntos4(false);
                }
            }

            Vector3 pos = controller.DameRespawn();
            pos = new Vector3(pos.x, transform.position.y, pos.z);
            move = new Vector3(0, transform.position.y, 0);
            _velocity = new Vector3(0, transform.position.y, 0);
            Drag = new Vector3(0, transform.position.y, 0);
            _controller.Move(pos);
            transform.position = pos;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        Rmat.SetActive(true);
        yield return new WaitForSecondsRealtime(tiempoRespawnMat);
        Rmat.SetActive(false);
    }

    public void Bailar()
    {
        anim.SetTrigger("baile");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CambiarTipoBala1")
        {
            disp.tipoBala = 1;
        }
        if (other.tag == "CambiarTipoBala2")
        {
            disp.tipoBala = 2;
        }
        if (other.tag == "CambiarTipoBala3")
        {
            disp.tipoBala = 3;
        }

        other.gameObject.GetComponent<RespawnMat>().Apagar();
    }
}
