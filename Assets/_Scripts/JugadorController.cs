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
        disparo.shooter = Fire;
        disparo.anim = anim;
        StartCoroutine(Respawn());
    }

    private void Awake()
    {
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
            dashLimit -= Time.unscaledDeltaTime;

            move = new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical));
            _controller.Move(move * Time.unscaledDeltaTime * speed);
            if (move != Vector3.zero)
            {
                transform.forward = move;
                //if es jugador 2 o 4 que tienen el bug
                //if (jugadores.Jugador2 == jug)
                //{
                //    float valor = -(Mathf.Clamp01(move.x + move.z) * 45);
                //    anim.transform.localEulerAngles = new Vector3(0, valor, 0);
                //}
                //if (jugadores.Jugador4 == jug)
                //{
                //    float valor = -(Mathf.Clamp01(move.x + move.z) * 45);
                //    anim.transform.localEulerAngles = new Vector3(0, valor, 0);
                //}
                anim.SetBool(correr, true);
            }
            else
            {
                anim.SetBool(correr, false);
                //if es jugador 2 o 4 que tienen el bug
                //if (jugadores.Jugador1 == jug)
                //{
                //    anim.transform.localEulerAngles = new Vector3(0, 0, 0);
                //}
                //if (jugadores.Jugador2 == jug)
                //{
                //    anim.transform.localEulerAngles = new Vector3(0, 0, 0);
                //}
            }

            if (Input.GetButtonDown(Power))
            {
                if (dashLimit <= 0)
                {
                    _velocity += (Vector3.Scale(transform.forward, dashdistance * new Vector3((Mathf.Log(1f / (Time.unscaledDeltaTime * Drag.x + 2)) / -Time.unscaledDeltaTime), 0, (Mathf.Log(1f / (Time.unscaledDeltaTime * Drag.z + 2)) / -Time.unscaledDeltaTime))) * 2);
                    source.Stop();
                    source.Play();
                    dashLimit = esperaEntreDrag;
                }
            }

            //_velocity.y += gravity * Time.deltaTime;

            _velocity.x /= 1 + Drag.x * Time.unscaledDeltaTime;
            //_velocity.y /= 1 + Drag.y * Time.deltaTime;
            _velocity.y = 0;
            _velocity.z /= 1 + Drag.z * Time.unscaledDeltaTime;

            _controller.Move(_velocity * Time.unscaledDeltaTime);

            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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

            _controller.enabled = false;
            Vector3 pos = controller.DameRespawn();
            pos = new Vector3(pos.x, transform.position.y, pos.z);
            transform.position = pos;
            _controller.enabled = true;
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
        anim.SetTrigger(baile);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CambiarTipoBala1")
        {
            disparo.tipoBala = 1;
        }
        if (other.tag == "CambiarTipoBala2")
        {
            disparo.tipoBala = 2;
        }
        if (other.tag == "CambiarTipoBala3")
        {
            disparo.tipoBala = 3;
        }

        other.gameObject.GetComponent<RespawnMat>().Apagar();
    }
}
