using UnityEngine;

public class Disparo : MonoBehaviour
{
    private InGameController controller;
    private BalasPool poolB;

    [SerializeField] private int cantidadBalas;
    private int numeroBalas;
    [SerializeField] private float tiempoRecarga;

    [Space]
    [SerializeField] private GameObject bala;
    [SerializeField] private Transform padre;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private AudioSource source;
    public Animator anim;

    [Space]
    [SerializeField] private AudioClip AceroBalas;
    [SerializeField] private AudioClip Arecarga;
    
    /*Private Variables*/
    private float count;
    public int tipoBala;

    /*Shooter*/
    public string shooter;
    public int Njug;

    [ColorUsage(true, true)]
    public Color colorBala;

    private readonly int _disparo = Animator.StringToHash("disparo");

    private void Awake()
    {
        controller = InGameController.Instance;
        numeroBalas = cantidadBalas;
    }

    private void Start()
    {
        poolB = BalasPool.Instance;
    }

    private void Update()
    {
        if (controller.comenzar)
        {
            if (Input.GetKeyDown(shooter))
            {
                if (numeroBalas > 0)
                {
                    anim.SetTrigger(_disparo);
                    numeroBalas -= 1;
                    count = 0;
                    //GameObject balita = Instantiate(bala, puntoDisparo.position, puntoDisparo.rotation, padre);
                    //Bala bali = balita.GetComponent<Bala>();
                    //if (tipoBala == 1)
                    //{
                    //    bali.tipoDeBala = Bala.balaT.Basica;
                    //}
                    //if (tipoBala == 2)
                    //{
                    //    bali.tipoDeBala = Bala.balaT.BasicaRebota;
                    //}
                    //if (tipoBala == 3)
                    //{
                    //    bali.tipoDeBala = Bala.balaT.VelVariable;
                    //}

                    //if (Njug == 1)
                    //{
                    //    bali.dueno = 1;
                    //}
                    //if (Njug == 2)
                    //{
                    //    bali.dueno = 2;
                    //}
                    //if (Njug == 3)
                    //{
                    //    bali.dueno = 3;
                    //}
                    //if (Njug == 4)
                    //{
                    //    bali.dueno = 4;
                    //}

                    poolB.OrganizaBala(puntoDisparo.position, puntoDisparo.rotation, colorBala, tipoBala, Njug);
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

            count += Time.deltaTime;
        }
    }
}