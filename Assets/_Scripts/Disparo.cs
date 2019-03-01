using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public InGameController controller;
    
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
    private string shooter1 = "Fire1";
    private string shooter2 = "Fire2";
    private string shooter3 = "Fire3";
    private string shooter4 = "Fire4";
    public string shooter;

    private void Awake()
    {
        numeroBalas = cantidadBalas;
    }

    private void Update()
    {
        if (controller.comenzar)
        {
            if (Input.GetButtonDown(shooter))
            {
                if (numeroBalas > 0)
                {
                    numeroBalas -= 1;
                    count = 0;
                    GameObject balita = Instantiate(bala, puntoDisparo.position, puntoDisparo.rotation, padre);

                    Bala bali = balita.GetComponent<Bala>();

                    if (shooter == shooter1)
                    {
                        bali.dueno = 1;
                    }
                    if (shooter == shooter2)
                    {
                        bali.dueno = 2;
                    }
                    if (shooter == shooter3)
                    {
                        bali.dueno = 3;
                    }
                    if (shooter == shooter4)
                    {
                        bali.dueno = 4;
                    }
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
}
