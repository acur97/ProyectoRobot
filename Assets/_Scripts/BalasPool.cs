using System.Collections;
using System.Collections.Generic;
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

    public void OrganizaBala(Vector3 posicion, Quaternion rotacion, Color color, int tipoDisparo, int dueno)
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!Balas[i].activeSelf)
            {
                Balas[i].transform.position = posicion;
                Balas[i].transform.rotation = rotacion;
                BalasColor[i].material.SetColor("_EmissionColor", color);
                if (tipoDisparo == 1)
                {
                    BalasComponent[i].tipoDeBala = Bala.balaT.Basica;
                }
                if (tipoDisparo == 2)
                {
                    BalasComponent[i].tipoDeBala = Bala.balaT.BasicaRebota;
                }
                if (tipoDisparo == 3)
                {
                    BalasComponent[i].tipoDeBala = Bala.balaT.VelVariable;
                }
                BalasComponent[i].dueno = dueno;
                Balas[i].SetActive(true);
                break;
            }
        }
    }
}