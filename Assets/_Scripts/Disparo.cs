using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{


    public enum jugadores {Jugador1, Jugador2, Jugador3, Jugador4}
    public jugadores jug;


    public int cantidadBalas;
    private int numeroBalas;
    public float tiempoRecarga;
    [Space]
    public GameObject bala;
    public Transform padre;
    public Transform puntoDisparo;


    /*Private Variables*/
    private float count;

    /*Shooter*/
 
    private string shooter;

    private void Awake()
    {
        

        if (jugadores.Jugador1 == jug)
        {

            shooter = "Fire1";
         
     
        }

        if (jugadores.Jugador2 == jug)
        {

            shooter = "Fire2";
         
        }

        if (jugadores.Jugador3 == jug)
        {

            shooter  = "Fire3";
         
        }

        if (jugadores.Jugador4 == jug)
        {

            shooter  = "Fire4";
        }

        numeroBalas = cantidadBalas;
    }

    private void Update()
    {
        if (Input.GetButtonDown(shooter))
        {
            if (numeroBalas > 0)
            {
                numeroBalas -= 1;
                count = 0;
                Instantiate(bala, puntoDisparo.position, puntoDisparo.rotation, padre);
            }
        }

        if (count >= tiempoRecarga)
        {
            count = 0;

            if (numeroBalas < cantidadBalas)
            {
                numeroBalas += 1;
            }
        }

        count += Time.unscaledDeltaTime;
    }
}
