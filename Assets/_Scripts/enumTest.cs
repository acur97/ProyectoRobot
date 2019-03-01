using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enumTest : MonoBehaviour
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
    
    /*Horizontal Movement*/
    private string horizontal1 = "Horizontal1";
    private string horizontal2 = "Horizontal2";
    private string horizontal3 = "Horizontal3";
    private string horizontal4 = "Horizontal4";
    private string horizontal;

    /*Vertical Movement*/
    private string vertical1 = "Vertical1";
    private string vertical2 = "Vertical2";
    private string vertical3 = "Vertical3";
    private string vertical4 = "Vertical4";
    private string vertical;

    ///*Shooter*/
    private string shooter1 = "Fire1";
    private string shooter2 = "Fire2";
    private string shooter3 = "Fire3";
    private string shooter4 = "Fire4";
    private string shooter;

    /*PowerUP Dash*/
    private string power1 = "Power1";
    private string power2 = "Power2";
    private string power3 = "Power3";
    private string power4 = "Power4";
    private string power;

    private CharacterController _controller;
    private Vector3 _velocity;

    private Vector3 move;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        if (jugadores.Jugador1 == jug)
        {
            //salto = salto1;
            horizontal = horizontal1;
            vertical = vertical1;
            power = power1;
            shooter = shooter1;
        }
        if (jugadores.Jugador2 == jug)
        {
            horizontal = horizontal2;
            vertical = vertical2;
            power = power2;
            shooter = shooter2;
        }
        if (jugadores.Jugador3 == jug)
        {
            horizontal = horizontal3;
            vertical = vertical3;
            power = power3;
            shooter = shooter3;
        }
        if (jugadores.Jugador4 == jug)
        {
            horizontal = horizontal4;
            vertical = vertical4;
            power = power4;
            shooter = shooter4;
        }

        disparo.shooter = shooter;
    }

/*Move and Power Dash of player */
    private void Update()
    {
        if (controller.comenzar)
        {
            move = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical));
            _controller.Move(move * Time.deltaTime * speed);
            if (move != Vector3.zero)
            {
                transform.forward = move;
            }

            if (Input.GetButtonDown(power))
            {
                Debug.Log("Dash");
                _velocity += Vector3.Scale(transform.forward, dashdistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
            }

            _velocity.y += gravity * Time.deltaTime;

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
            Destroy(collision.gameObject);
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
        }
    }
}
