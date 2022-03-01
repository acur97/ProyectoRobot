using System.Collections;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    public InGameController controller;
    public Disparo disparo;
    public enum Jugadores {Jugador1, Jugador2, Jugador3, Jugador4}
    public Jugadores jug;
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

    private CharacterController _controller;
    private Vector3 _velocity;
    private Vector3 move;
    private float dashLimit = 1;
    private WaitForSeconds wait;
    private Vector3 respawnPos = Vector3.zero;
    private Bala bal;

    private const string _corriendo = "corriendo";
    private const string _baile = "baile";
    private const string _Bala = "Bala";
    private const string _CambiarTipoBala1 = "CambiarTipoBala1";
    private const string _CambiarTipoBala2 = "CambiarTipoBala2";
    private const string _CambiarTipoBala3 = "CambiarTipoBala3";

    [Space]
    public string Horizontal;
    public string Vertical;
    public string Fire;
    public string Power;

    private void Awake()
    {
        wait = new WaitForSeconds(tiempoRespawnMat);

        switch (jug)
        {
            case Jugadores.Jugador1:
                disparo.Njug = 1;
                break;
            case Jugadores.Jugador2:
                disparo.Njug = 2;
                break;
            case Jugadores.Jugador3:
                disparo.Njug = 3;
                break;
            case Jugadores.Jugador4:
                disparo.Njug = 4;
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        disparo.shooter = Fire;
        disparo.anim = anim;
        StartCoroutine(Respawn());
    }

/*Move and Power Dash of player */
    private void Update()
    {
        if (controller.comenzar)
        {
            dashLimit -= Time.deltaTime;

            move = new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical));
            _controller.Move(speed * Time.deltaTime * move);
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
                anim.SetBool(_corriendo, true);
            }
            else
            {
                anim.SetBool(_corriendo, false);
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
                    _velocity += (Vector3.Scale(transform.forward, dashdistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 2)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 2)) / -Time.deltaTime))) * 2);
                    source.Stop();
                    source.Play();
                    dashLimit = esperaEntreDrag;
                }
            }

            //_velocity.y += gravity * Time.deltaTime;

            _velocity.x /= 1 + Drag.x * Time.deltaTime;
            //_velocity.y /= 1 + Drag.y * Time.deltaTime;
            _velocity.y = 0;
            _velocity.z /= 1 + Drag.z * Time.deltaTime;

            _controller.Move(_velocity * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    /*Dead Player*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_Bala))
        {
            bal = collision.gameObject.GetComponent<Bala>();
            bal.Morir(false);

            switch (bal.enemyID)
            {
                case 1:
                    if (jug == Jugadores.Jugador1)
                    {
                        controller.SubirPuntos1(true);
                    }
                    else
                    {
                        controller.SubirPuntos1(false);
                    }
                    break;

                case 2:
                    if (jug == Jugadores.Jugador2)
                    {
                        controller.SubirPuntos2(true);
                    }
                    else
                    {
                        controller.SubirPuntos2(false);
                    }
                    break;

                case 3:
                    if (jug == Jugadores.Jugador3)
                    {
                        controller.SubirPuntos3(true);
                    }
                    else
                    {
                        controller.SubirPuntos3(false);
                    }
                    break;

                case 4:
                    if (jug == Jugadores.Jugador4)
                    {
                        controller.SubirPuntos4(true);
                    }
                    else
                    {
                        controller.SubirPuntos4(false);
                    }
                    break;

                default:
                    break;
            }

            _controller.enabled = false;
            respawnPos = controller.DameRespawn();
            respawnPos = new Vector3(respawnPos.x, transform.position.y, respawnPos.z);
            transform.position = respawnPos;
            _controller.enabled = true;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        Rmat.SetActive(true);
        yield return wait;
        Rmat.SetActive(false);
    }

    public void Bailar()
    {
        anim.SetTrigger(_baile);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_CambiarTipoBala1))
        {
            disparo.tipoBala = 1;
        }
        else if (other.CompareTag(_CambiarTipoBala2))
        {
            disparo.tipoBala = 2;
        }
        else if (other.CompareTag(_CambiarTipoBala3))
        {
            disparo.tipoBala = 3;
        }

        other.gameObject.GetComponent<RespawnMat>().Apagar();
    }
}