using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public int cantidadBalas;
    private int numeroBalas;
    public float tiempoRecarga;
    [Space]
    public GameObject bala;
    public Transform padre;
    public Transform puntoDisparo;

    private float count;

    private void Awake()
    {
        numeroBalas = cantidadBalas;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
