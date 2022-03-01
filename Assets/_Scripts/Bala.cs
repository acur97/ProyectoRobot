using System.Collections;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public enum BalaT {Basica, BasicaRebota, VelVariable};
    public BalaT tipoDeBala;

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
    public int enemyID;

    private bool arranque;
    private int contadorChoques;
    private float variable;
    private bool variable1st;
    private float vel = 0;
    private WaitForSeconds wait1;
    private WaitForSeconds wait2;
    private WaitForSeconds wait3;

    private const string _Objetos = "Objetos";

    private void Awake()
    {
        wait1 = new WaitForSeconds(tiempoEspera);
        wait2 = new WaitForSeconds(tiempoMuerte - tiempoEspera);
        wait3 = new WaitForSeconds(1);
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        hit.SetActive(false);
        muzzle.SetActive(false);
        shoot.SetActive(true);
        source.clip = Adisparo;
        source.Play();
        rb.isKinematic = false;
        force.relativeForce = Vector3.zero;
        coll.enabled = true;
        arranque = false;
        StartCoroutine(EsperaArranque());
    }

    private void Update()
    {
        if (arranque)
        {
            switch (tipoDeBala)
            {
                case BalaT.Basica:
                    force.relativeForce = new Vector3(0, 0, velocidad);
                    break;

                case BalaT.BasicaRebota:
                    force.relativeForce = new Vector3(0, 0, velocidad);
                    break;

                case BalaT.VelVariable:
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
                        vel = ((velocidad * variable) / 10) - (velocidad / 2.5f);
                        force.relativeForce = new Vector3(0, 0, vel);
                    }
                    break;

                default:
                    break;
            }
        }
    }

    IEnumerator EsperaArranque()
    {
        yield return wait1;
        arranque = true;
        muzzle.SetActive(true);
        source.Stop();
        source.clip = AdisparoLuego;
        source.Play();
        yield return wait2;
        Morir(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (tipoDeBala)
        {
            case BalaT.Basica:
                if (collision.gameObject.CompareTag(_Objetos))
                {
                    //Destroy(gameObject);
                    Morir(true);
                }
                break;

            case BalaT.BasicaRebota:
                contadorChoques += 1;
                if (contadorChoques == choques && collision.gameObject.CompareTag(_Objetos))
                {
                    //Destroy(gameObject);
                    Morir(true);
                }
                break;

            case BalaT.VelVariable:
                if (collision.gameObject.CompareTag(_Objetos))
                {
                    //Destroy(gameObject);
                    Morir(true);
                }
                break;

            default:
                break;
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
        force.relativeForce = Vector3.zero;
        coll.enabled = false;
        muzzle.SetActive(false);
        shoot.SetActive(false);
        hit.SetActive(true);
        StartCoroutine(EsperaMorir());
    }

    IEnumerator EsperaMorir()
    {
        yield return wait3;
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}