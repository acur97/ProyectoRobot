using UnityEngine;

public class BalasPool : MonoBehaviour
{
    public int poolSize = 25;

    [Space]
    public GameObject balaPrefab;
    public Material test;

    private GameObject[] Balas;
    private ParticleSystemRenderer[] BalasColor;
    private Bala[] BalasComponent;

    private readonly int _EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        Balas = new GameObject[poolSize];
        BalasColor = new ParticleSystemRenderer[poolSize];
        BalasComponent = new Bala[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            Balas[i] = Instantiate(balaPrefab, transform);
            Balas[i].SetActive(false);
            BalasColor[i] = Balas[i].GetComponentInChildren<ParticleSystemRenderer>();
            BalasComponent[i] = Balas[i].GetComponent<Bala>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OrganizaBala(Vector3.zero, Quaternion.identity, Color.black, 1, 1);
        }
    }

    public void OrganizaBala(Vector3 posicion, Quaternion rotacion, Color color, int tipoDisparo, int enemyID)
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!Balas[i].activeSelf)
            {
                Balas[i].transform.SetPositionAndRotation(posicion, rotacion);
                BalasColor[i].material.SetColor(_EmissionColor, color);
                switch (tipoDisparo)
                {
                    case 1:
                        BalasComponent[i].tipoDeBala = Bala.BalaT.Basica;
                        break;

                    case 2:
                        BalasComponent[i].tipoDeBala = Bala.BalaT.BasicaRebota;
                        break;

                    case 3:
                        BalasComponent[i].tipoDeBala = Bala.BalaT.VelVariable;
                        break;

                    default:
                        break;
                }
                BalasComponent[i].enemyID = enemyID;
                Balas[i].SetActive(true);
                break;
            }
        }
    }
}