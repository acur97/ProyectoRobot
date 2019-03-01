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
    public Rigidbody rb;
    public SphereCollider coll;
    public GameObject hit;
    public GameObject muzzle;
    public GameObject shoot;
    public AudioSource source;
    [Space]
    public AudioClip Adisparo;
    public AudioClip AdisparoLuego;
    public AudioClip AchocaPared;
    public AudioClip AchocaRobot;
    [Space]
    public int dueno;

    private bool arranque;
    private int contadorChoques;
    private float variable;
    private bool variable1st;

    private void OnEnable()
    {
        StartCoroutine(EsperaArranque());
        hit.SetActive(false);
        muzzle.SetActive(false);
        shoot.SetActive(true);
        source.clip = Adisparo;
        source.Play();
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
        muzzle.SetActive(true);
        source.Stop();
        source.clip = AdisparoLuego;
        source.Play();
        yield return new WaitForSecondsRealtime(tiempoMuerte - tiempoEspera);
        Morir(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (balaT.Basica == tipoDeBala || balaT.VelVariable == tipoDeBala)
        {
            if (collision.gameObject.CompareTag("Objetos"))
            {
                //Destroy(gameObject);
                Morir(true);
            }
        }
        if (balaT.BasicaRebota == tipoDeBala)
        {
            contadorChoques += 1;
            if (contadorChoques == choques && collision.gameObject.CompareTag("Objetos"))
            {
                //Destroy(gameObject);
                Morir(true);
            }
        }
    }

    public void Morir(bool conObjetos)
    {
        source.Stop();
        if (conObjetos)
        {
            source.clip = AchocaPared;
        }
        else
        {
            source.clip = AchocaRobot;
        }
        source.Play();
        rb.isKinematic = true;
        coll.enabled = false;
        muzzle.SetActive(false);
        shoot.SetActive(false);
        hit.SetActive(true);
        StartCoroutine(EsperaMorir());
    }

    IEnumerator EsperaMorir()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }
}
