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
    public AudioSource source;
    [Space]
    public AudioClip AceroBalas;
    public AudioClip Arecarga;
    
    /*Private Variables*/
    private float count;
    public int tipoBala;

    /*Shooter*/
    public string shooter;
    public int Njug;

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
                    if (tipoBala == 1)
                    {
                        bali.tipoDeBala = Bala.balaT.Basica;
                    }
                    if (tipoBala == 2)
                    {
                        bali.tipoDeBala = Bala.balaT.BasicaRebota;
                    }
                    if (tipoBala == 3)
                    {
                        bali.tipoDeBala = Bala.balaT.VelVariable;
                    }

                    if (Njug == 1)
                    {
                        bali.dueno = 1;
                    }
                    if (Njug == 2)
                    {
                        bali.dueno = 2;
                    }
                    if (Njug == 3)
                    {
                        bali.dueno = 3;
                    }
                    if (Njug == 4)
                    {
                        bali.dueno = 4;
                    }
                }
                else
                {
                    source.Stop();
                    source.clip = AceroBalas;
                    source.Play();
                }
            }

            if (count >= tiempoRecarga)
            {
                count = 0;

                if (numeroBalas < cantidadBalas)
                {
                    numeroBalas += 1;
                    source.Stop();
                    source.clip = Arecarga;
                    source.Play();
                }
            }

            count += Time.unscaledDeltaTime;
        }
    }
}
