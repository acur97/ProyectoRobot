using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enumTest : MonoBehaviour
{
    public enum jugadores {Jugador1, Jugador2, Jugador3}
    public jugadores jug;

    private string salto1 = "Jump1";
    private string salto2 = "Jump2";
    private string salto;

    private void Awake()
    {
        if (jugadores.Jugador1 == jug)
        {
            salto = salto1;
        }
    }

    private void Update()
    {
        Input.GetButtonDown(salto);
    }
}
