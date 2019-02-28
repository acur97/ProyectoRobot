using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public enum balaT {Basica, BasicaRebota, VelVariable};
    public balaT tipoDeBala;
    [Space]
    public float velocidad = 50;
    public float tiempoEspera = 2;
    public float tiempoMuerte = 5;
    [Header("Valores Basica rebota")]
    [Range(1, 10)]
    public int choques = 5;
    [Header("Valores Velocidad variable")]
    public float variacion = 1;
    [Space]
    public ConstantForce force;

    private bool arranque;
    private int contadorChoques;
    private float variable;
    private bool variable1st;

    private void OnEnable()
    {
        StartCoroutine(EsperaArranque());
    }

    private void Update()
    {
        if (arranque)
        {
            if (balaT.Basica == tipoDeBala)
            {
                force.relativeForce = new Vector3(0, 0, velocidad);
            }
            if (balaT.BasicaRebota == tipoDeBala)
            {
                force.relativeForce = new Vector3(0, 0, velocidad);
            }
            if (balaT.VelVariable == tipoDeBala)
            {
                variable += variacion;

                if (variable >= 10)
                {
                    variable = 0;
                }

                if (variable >= 1)
                {
                    variable1st = true;
                }

                if (!variable1st)
                {
                    force.relativeForce = new Vector3(0, 0, (velocidad / 2));
                }
                else
                {
                    float vel = ((velocidad * variable) / 10) - (velocidad / 2.5f);
                    force.relativeForce = new Vector3(0, 0, vel);
                }
            }
        }
    }

    IEnumerator EsperaArranque()
    {
        yield return new WaitForSecondsRealtime(tiempoEspera);
        arranque = true;
        yield return new WaitForSecondsRealtime(tiempoMuerte - tiempoEspera);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (balaT.Basica == tipoDeBala || balaT.VelVariable == tipoDeBala)
        {
            if (collision.gameObject.CompareTag("Objetos"))
            {
                Destroy(gameObject);
            }
        }
        if (balaT.BasicaRebota == tipoDeBala)
        {
            contadorChoques += 1;
            if (contadorChoques == choques && collision.gameObject.CompareTag("Objetos"))
            {
                Destroy(gameObject);
            }
        }
    }
}
