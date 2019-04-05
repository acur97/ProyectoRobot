﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject[] prefabs = new GameObject[4];

    [Range(0, 5)]
    public int minutosStart;
    public float tiempoBlurStart;
    [Space]
    public Text timer;
    public Color timerRojo;
    public bool comenzar;
    [Space]
    public int puntajePorPersona = 100;
    public int puntajeAutoGolpe = 50;
    public Text puntaje1;
    public Text puntaje2;
    public Text puntaje3;
    public Text puntaje4;
    [Header("Respawn´s")]
    public Transform[] respawns;
    [Space]
    public Image StartBlack;
    public GameObject PlayCanvas;
    public GameObject PausaCanvas;
    public GameObject StartCanvas;
    private Image[] StartImgs;
    public GameObject FinalCanvas;
    public PostProcessVolume post;
    public CinemachineVirtualCamera Vcamera;
    public AudioMixer mixer;
    public AudioSource source1;
    public AudioSource source2;
    [Space]
    public AudioClip Aintroduccion;
    public AudioClip A321;
    public AudioClip Afaltan30;
    public AudioClip Afaltan10;
    public AudioClip Afaltan5;
    public AudioClip AalarmaFin;
    [Space]
    public GameObject naveRobot;
    public GameObject jugador1;
    public Transform jug1animRoot;
    public MeshRenderer esferaRespaw1;
    private Material esferaMatRespaw1;
    public GameObject jugador2;
    public Transform jug2animRoot;
    public MeshRenderer esferaRespaw2;
    private Material esferaMatRespaw2;
    public GameObject jugador3;
    public Transform jug3animRoot;
    public MeshRenderer esferaRespaw3;
    private Material esferaMatRespaw3;
    public GameObject jugador4;
    public Transform jug4animRoot;
    public MeshRenderer esferaRespaw4;
    private Material esferaMatRespaw4;
    public CinemachineTargetGroup camGroup;
    [Space]
    public AudioClip[] SonidosVictoria;
    private CinemachineTargetGroup.Target finalizador1;
    private CinemachineTargetGroup.Target finalizador2;
    private CinemachineTargetGroup.Target finalizador3;
    [Space]
    public StandaloneInputModule eventSystem;

    private float mili;
    private int segundos;
    private int segundosS;
    private int minutos;
    private DepthOfField depth;
    private ChromaticAberration chromatic;
    private Bloom bloom;
    private float impactChromatic;
    private float impactBloomDif;
    private CinemachineBasicMultiChannelPerlin Camnoise;
    private CinemachineGroupComposer groupc;
    private float speed1;
    private float speed2;
    private float speed3;

    private int vaGanando;
    private int puntuacion1;
    private int puntuacion2;
    private int puntuacion3;
    private int puntuacion4;

    public static string P1_H;
    public static string P1_V;
    public static string P1_F;
    public static string P1_P;

    public static string P2_H;
    public static string P2_V;
    public static string P2_F;
    public static string P2_P;

    public static string P3_H;
    public static string P3_V;
    public static string P3_F;
    public static string P3_P;

    public static string P4_H;
    public static string P4_V;
    public static string P4_F;
    public static string P4_P;

    public static int PN;

    private void Awake()
    {
        if (esferaMatRespaw1 != null)
        {
            esferaRespaw1.material = esferaMatRespaw1;
        }
        if (esferaMatRespaw2 != null)
        {
            esferaRespaw2.material = esferaMatRespaw2;
        }
        if (esferaMatRespaw3 != null)
        {
            esferaRespaw3.material = esferaMatRespaw3;
        }
        if (esferaMatRespaw4 != null)
        {
            esferaRespaw4.material = esferaMatRespaw4;
        }

        objects = SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].CompareTag("Jug1Seleccion"))
            {
                prefabs[0] = objects[i];
                jug1animRoot.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
                prefabs[0].transform.SetParent(jug1animRoot);
                prefabs[0].transform.localPosition = new Vector3(0, 0, 0);
                prefabs[0].transform.eulerAngles = new Vector3(0, 0, 0);
                prefabs[0].transform.localScale = new Vector3(10.876f, 10.876f, 10.876f);
                jugador1.GetComponent<JugadorController>().anim = prefabs[0].GetComponent<Animator>();
            }
            if (objects[i].CompareTag("Jug2Seleccion"))
            {
                prefabs[1] = objects[i];
                jug2animRoot.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
                prefabs[1].transform.SetParent(jug2animRoot);
                prefabs[1].transform.localPosition = new Vector3(0, 0, 0);
                prefabs[1].transform.eulerAngles = new Vector3(0, 0, 0);
                prefabs[1].transform.localScale = new Vector3(10.876f, 10.876f, 10.876f);
                jugador2.GetComponent<JugadorController>().anim = prefabs[1].GetComponent<Animator>();
            }
            if (objects[i].CompareTag("Jug3Seleccion"))
            {
                prefabs[2] = objects[i];
                jug3animRoot.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
                prefabs[2].transform.SetParent(jug3animRoot);
                prefabs[2].transform.localPosition = new Vector3(0, 0, 0);
                prefabs[2].transform.eulerAngles = new Vector3(0, 0, 0);
                prefabs[2].transform.localScale = new Vector3(10.876f, 10.876f, 10.876f);
                jugador3.GetComponent<JugadorController>().anim = prefabs[2].GetComponent<Animator>();
            }
            if (objects[i].CompareTag("Jug4Seleccion"))
            {
                prefabs[3] = objects[i];
                jug4animRoot.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
                prefabs[3].transform.SetParent(jug4animRoot);
                prefabs[3].transform.localPosition = new Vector3(0, 0, 0);
                prefabs[3].transform.eulerAngles = new Vector3(0, 0, 0);
                prefabs[3].transform.localScale = new Vector3(10.876f, 10.876f, 10.876f);
                jugador4.GetComponent<JugadorController>().anim = prefabs[3].GetComponent<Animator>();
                continue;
            }
        }

        mixer.SetFloat("Volumen_master", 0);
        naveRobot.SetActive(false);
        comenzar = false;
        StartImgs = StartCanvas.GetComponentsInChildren<Image>();

        jugador1.GetComponent<JugadorController>().Horizontal = P1_H;
        jugador1.GetComponent<JugadorController>().Vertical = P1_V;
        jugador1.GetComponent<JugadorController>().Fire = P1_F;
        jugador1.GetComponent<JugadorController>().Power = P1_P;

        jugador2.GetComponent<JugadorController>().Horizontal = P2_H;
        jugador2.GetComponent<JugadorController>().Vertical = P2_V;
        jugador2.GetComponent<JugadorController>().Fire = P2_F;
        jugador2.GetComponent<JugadorController>().Power = P2_P;

        jugador3.GetComponent<JugadorController>().Horizontal = P3_H;
        jugador3.GetComponent<JugadorController>().Vertical = P3_V;
        jugador3.GetComponent<JugadorController>().Fire = P3_F;
        jugador3.GetComponent<JugadorController>().Power = P3_P;

        jugador4.GetComponent<JugadorController>().Horizontal = P4_H;
        jugador4.GetComponent<JugadorController>().Vertical = P4_V;
        jugador4.GetComponent<JugadorController>().Fire = P4_F;
        jugador4.GetComponent<JugadorController>().Power = P4_P;

        eventSystem.horizontalAxis = P1_H;
        eventSystem.verticalAxis = P1_V;
        eventSystem.submitButton = P1_F;
        //eventSystem.cancelButton = P1_P;

        puntaje1.text = 0.ToString();
        puntaje2.text = 0.ToString();
        puntaje3.text = 0.ToString();
        puntaje4.text = 0.ToString();

        StartCanvas.SetActive(true);
        PausaCanvas.SetActive(false);
        PlayCanvas.SetActive(true);
        FinalCanvas.SetActive(false);

        minutos = minutosStart;
        timer.text = minutos + ":00";

        post.profile.TryGetSettings(out depth);
        post.profile.TryGetSettings(out chromatic);
        post.profile.TryGetSettings(out bloom);
        Camnoise = Vcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        groupc = Vcamera.GetCinemachineComponent<CinemachineGroupComposer>();
        groupc.m_ScreenY = 0.55f;
        depth.focusDistance.value = 0.1f;
        StartBlack.color = new Color32(0, 0, 0, 128);
    }

    #region Funciones

    public void RecibeImpacto()
    {
        impactChromatic = 0.25f;
        impactBloomDif = 9;
    }

    public void IniciarJuego()
    {
        StartCoroutine(ChangeSpeed1(0.1f, 10, tiempoBlurStart));
        StartCoroutine(ChangeSpeed2(128, 0, tiempoBlurStart));
        StartCoroutine(ChangeSpeed3(255, 0, tiempoBlurStart));

        StartCoroutine(ComenzarAudios());
    }

    IEnumerator ComenzarAudios()
    {
        yield return new WaitForSecondsRealtime(tiempoBlurStart);
        naveRobot.SetActive(true);
        yield return new WaitForSecondsRealtime(tiempoBlurStart);
        source1.clip = Aintroduccion;
        source1.Play();
        yield return new WaitForSecondsRealtime(0.3f);
        source2.clip = A321;
        source2.Play();
        yield return new WaitForSecondsRealtime(2.2f);
        StartCoroutine(ChangeSpeed1(10, 35, 8));
        yield return new WaitForSecondsRealtime(1f);
        comenzar = true;
        yield return new WaitForSecondsRealtime(8);
        naveRobot.SetActive(false);
    }

    public void Pausar()
    {
        if (comenzar)
        {
            mixer.SetFloat("Volumen_master", -20);
            Time.timeScale = 0;
            PlayCanvas.SetActive(false);
            PausaCanvas.SetActive(true);
            depth.focusDistance.value = 0.1f;
            StartBlack.color = new Color32(0, 0, 0, 128);
            comenzar = false;
        }
    }

    public void Reanudar()
    {
        mixer.SetFloat("Volumen_master", 0);
        comenzar = true;
        Time.timeScale = 1;
        PlayCanvas.SetActive(true);
        PausaCanvas.SetActive(false);
        depth.focusDistance.value = 200;
        StartBlack.color = new Color32(0, 0, 0, 0);
    }

    public void VolverMenu()
    {
        Time.timeScale = 1;
        Initiate.Fade("Game", Color.black, 1);
    }

    public void FinalizarJuego()
    {
        PlayCanvas.SetActive(false);
        FinalCanvas.SetActive(true);
        depth.active = false;
        StartBlack.color = new Color32(0, 0, 0, 128);
        groupc.m_ScreenY = 0.425f;

        //camGroup.m_Targets = new CinemachineTargetGroup.Target[1];
        if (vaGanando == 1)
        {
            finalizador1.target = jugador1.transform;
            finalizador1.weight = 2;
            finalizador1.radius = 2;

            jugador1.GetComponent<JugadorController>().Bailar();
            jugador2.SetActive(false);
            jugador3.SetActive(false);
            jugador4.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador1, 2);
            camGroup.m_Targets.SetValue(finalizador1, 3);
        }
        if (vaGanando == 2)
        {
            finalizador1.target = jugador2.transform;
            finalizador1.weight = 2;
            finalizador1.radius = 2;

            jugador2.GetComponent<JugadorController>().Bailar();
            jugador1.SetActive(false);
            jugador3.SetActive(false);
            jugador4.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador1, 2);
            camGroup.m_Targets.SetValue(finalizador1, 3);
        }
        if (vaGanando == 3)
        {
            finalizador1.target = jugador3.transform;
            finalizador1.weight = 2;
            finalizador1.radius = 2;

            jugador3.GetComponent<JugadorController>().Bailar();
            jugador1.SetActive(false);
            jugador2.SetActive(false);
            jugador4.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador1, 2);
            camGroup.m_Targets.SetValue(finalizador1, 3);
        }
        if (vaGanando == 4)
        {
            finalizador1.target = jugador4.transform;
            finalizador1.weight = 2;
            finalizador1.radius = 2;

            jugador4.GetComponent<JugadorController>().Bailar();
            jugador1.SetActive(false);
            jugador2.SetActive(false);
            jugador3.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador1, 2);
            camGroup.m_Targets.SetValue(finalizador1, 3);
        }
        if (vaGanando == 5)
        {
            finalizador1.target = jugador1.transform;
            finalizador1.weight = 1.6f;
            finalizador1.radius = 1.6f;
            finalizador2.target = jugador2.transform;
            finalizador2.weight = 1.6f;
            finalizador2.radius = 1.6f;

            jugador1.GetComponent<JugadorController>().Bailar();
            jugador2.GetComponent<JugadorController>().Bailar();
            jugador3.SetActive(false);
            jugador4.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador2, 3);
        }
        if (vaGanando == 6)
        {
            finalizador1.target = jugador2.transform;
            finalizador1.weight = 1.6f;
            finalizador1.radius = 1.6f;
            finalizador2.target = jugador3.transform;
            finalizador2.weight = 1.6f;
            finalizador2.radius = 1.6f;

            jugador2.GetComponent<JugadorController>().Bailar();
            jugador3.GetComponent<JugadorController>().Bailar();
            jugador1.SetActive(false);
            jugador4.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador2, 3);
        }
        if (vaGanando == 7)
        {
            finalizador1.target = jugador3.transform;
            finalizador1.weight = 1.6f;
            finalizador1.radius = 1.6f;
            finalizador2.target = jugador4.transform;
            finalizador2.weight = 1.6f;
            finalizador2.radius = 1.6f;

            jugador3.GetComponent<JugadorController>().Bailar();
            jugador4.GetComponent<JugadorController>().Bailar();
            jugador1.SetActive(false);
            jugador2.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador2, 3);
        }
        if (vaGanando == 8)
        {
            finalizador1.target = jugador1.transform;
            finalizador1.weight = 1.6f;
            finalizador1.radius = 1.6f;
            finalizador2.target = jugador4.transform;
            finalizador2.weight = 1.6f;
            finalizador2.radius = 1.6f;

            jugador1.GetComponent<JugadorController>().Bailar();
            jugador4.GetComponent<JugadorController>().Bailar();
            jugador2.SetActive(false);
            jugador3.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador2, 3);
        }
        if (vaGanando == 9)
        {
            finalizador1.target = jugador1.transform;
            finalizador1.weight = 1.6f;
            finalizador1.radius = 1.6f;
            finalizador2.target = jugador3.transform;
            finalizador2.weight = 1.6f;
            finalizador2.radius = 1.6f;

            jugador1.GetComponent<JugadorController>().Bailar();
            jugador3.GetComponent<JugadorController>().Bailar();
            jugador2.SetActive(false);
            jugador4.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador2, 3);
        }
        if (vaGanando == 10)
        {
            finalizador1.target = jugador2.transform;
            finalizador1.weight = 1.6f;
            finalizador1.radius = 1.6f;
            finalizador2.target = jugador4.transform;
            finalizador2.weight = 1.6f;
            finalizador2.radius = 1.6f;

            jugador2.GetComponent<JugadorController>().Bailar();
            jugador4.GetComponent<JugadorController>().Bailar();
            jugador1.SetActive(false);
            jugador3.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador2, 3);
        }
        if (vaGanando == 11)
        {
            finalizador1.target = jugador1.transform;
            finalizador1.weight = 1.3f;
            finalizador1.radius = 1.3f;
            finalizador2.target = jugador2.transform;
            finalizador2.weight = 1.3f;
            finalizador2.radius = 1.3f;
            finalizador3.target = jugador3.transform;
            finalizador3.weight = 1.3f;
            finalizador3.radius = 1.3f;

            jugador1.GetComponent<JugadorController>().Bailar();
            jugador2.GetComponent<JugadorController>().Bailar();
            jugador3.GetComponent<JugadorController>().Bailar();
            jugador4.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador3, 3);
        }
        if (vaGanando == 12)
        {
            finalizador1.target = jugador1.transform;
            finalizador1.weight = 1.3f;
            finalizador1.radius = 1.3f;
            finalizador2.target = jugador2.transform;
            finalizador2.weight = 1.3f;
            finalizador2.radius = 1.3f;
            finalizador3.target = jugador4.transform;
            finalizador3.weight = 1.3f;
            finalizador3.radius = 1.3f;

            jugador1.GetComponent<JugadorController>().Bailar();
            jugador2.GetComponent<JugadorController>().Bailar();
            jugador4.GetComponent<JugadorController>().Bailar();
            jugador3.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador3, 3);
        }
        if (vaGanando == 13)
        {
            finalizador1.target = jugador3.transform;
            finalizador1.weight = 1.3f;
            finalizador1.radius = 1.3f;
            finalizador2.target = jugador4.transform;
            finalizador2.weight = 1.3f;
            finalizador2.radius = 1.3f;
            finalizador3.target = jugador1.transform;
            finalizador3.weight = 1.3f;
            finalizador3.radius = 1.3f;

            jugador3.GetComponent<JugadorController>().Bailar();
            jugador4.GetComponent<JugadorController>().Bailar();
            jugador1.GetComponent<JugadorController>().Bailar();
            jugador2.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador3, 3);
        }
        if (vaGanando == 14)
        {
            finalizador1.target = jugador2.transform;
            finalizador1.weight = 1.3f;
            finalizador1.radius = 1.3f;
            finalizador2.target = jugador3.transform;
            finalizador2.weight = 1.3f;
            finalizador2.radius = 1.3f;
            finalizador3.target = jugador4.transform;
            finalizador3.weight = 1.3f;
            finalizador3.radius = 1.3f;

            jugador2.GetComponent<JugadorController>().Bailar();
            jugador3.GetComponent<JugadorController>().Bailar();
            jugador4.GetComponent<JugadorController>().Bailar();
            jugador1.SetActive(false);

            camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador3, 3);
        }
        if (vaGanando == 0)
        {
            /*finalizador1.target = jugador2.transform;
            finalizador1.weight = 1;
            finalizador1.radius = 1;
            finalizador2.target = jugador3.transform;
            finalizador2.weight = 1;
            finalizador2.radius = 1;
            finalizador3.target = jugador4.transform;
            finalizador3.weight = 1;
            finalizador3.radius = 1;*/

            jugador1.GetComponent<JugadorController>().Bailar();
            jugador2.GetComponent<JugadorController>().Bailar();
            jugador3.GetComponent<JugadorController>().Bailar();
            jugador4.GetComponent<JugadorController>().Bailar();

            /*camGroup.m_Targets.SetValue(finalizador1, 0);
            camGroup.m_Targets.SetValue(finalizador1, 1);
            camGroup.m_Targets.SetValue(finalizador2, 2);
            camGroup.m_Targets.SetValue(finalizador3, 3);*/
        }
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void ComprobarVaGanando()
    {
        if (puntuacion1 > puntuacion2 && puntuacion1 > puntuacion3 && puntuacion1 > puntuacion4)
        {
            //gana 1
            vaGanando = 1;
        }
        else if (puntuacion2 > puntuacion1 && puntuacion2 > puntuacion3 && puntuacion2 > puntuacion4)
        {
            //gana 2
            vaGanando = 2;
        }
        else if (puntuacion3 > puntuacion1 && puntuacion3 > puntuacion2 && puntuacion3 > puntuacion4)
        {
            //gana 3
            vaGanando = 3;
        }
        else if (puntuacion4 > puntuacion1 && puntuacion4 > puntuacion2 && puntuacion4 > puntuacion3)
        {
            //gana 4
            vaGanando = 4;
        }

        if (puntuacion1 > puntuacion3 && puntuacion1 > puntuacion4 && puntuacion1 == puntuacion2)
        {
            //gana 1 y 2
            vaGanando = 5;
        }
        else if (puntuacion2 > puntuacion1 && puntuacion2 > puntuacion4 && puntuacion2 == puntuacion3)
        {
            //gana 2 y 3
            vaGanando = 6;
        }
        else if (puntuacion3 > puntuacion1 && puntuacion3 > puntuacion2 && puntuacion3 == puntuacion4)
        {
            //gana 3 y 4
            vaGanando = 7;
        }
        else if (puntuacion4 > puntuacion2 && puntuacion4 > puntuacion3 && puntuacion1 == puntuacion4)
        {
            //gana 1 y 4
            vaGanando = 8;
        }
        else if (puntuacion1 > puntuacion2 && puntuacion1 > puntuacion4 && puntuacion1 == puntuacion3)
        {
            //gana 1 y 3
            vaGanando = 9;
        }
        else if (puntuacion2 > puntuacion3 && puntuacion2 > puntuacion1 && puntuacion2 == puntuacion4)
        {
            //gana 2 y 4
            vaGanando = 10;
        }

        if (puntuacion1 > puntuacion4 && puntuacion1 == puntuacion2 && puntuacion1 == puntuacion3)
        {
            //gana 1, 2 y 3
            vaGanando = 11;
        }
        else if (puntuacion1 > 3 && puntuacion1 == puntuacion2 && puntuacion1 == puntuacion4)
        {
            //gana 1, 2 y 4
            vaGanando = 12;
        }
        else if (puntuacion3 > puntuacion2 && puntuacion3 == puntuacion1 && puntuacion3 == puntuacion4)
        {
            //gana 3, 4 y 1
            vaGanando = 13;
        }
        else if (puntuacion2 > puntuacion1 && puntuacion2 == puntuacion3 && puntuacion2 == puntuacion4)
        {
            //gana 2, 3 y 4
            vaGanando = 14;
        }

        if (puntuacion1 == puntuacion2 && puntuacion1 == puntuacion3 && puntuacion1 == puntuacion4)
        {
            //empate
            vaGanando = 0;
        }
    }

    public void SubirPuntos1(bool autogolpe)
    {
        RecibeImpacto();
        if (autogolpe)
        {
            puntuacion1 -= puntajeAutoGolpe;
        }
        else
        {
            puntuacion1 += puntajePorPersona;
        }
        puntaje1.text = puntuacion1.ToString();

        ComprobarVaGanando();
    }
    public void SubirPuntos2(bool autogolpe)
    {
        RecibeImpacto();
        if (autogolpe)
        {
            puntuacion2 -= puntajeAutoGolpe;
        }
        else
        {
            puntuacion2 += puntajePorPersona;
        }
        puntaje2.text = puntuacion2.ToString();

        ComprobarVaGanando();
    }
    public void SubirPuntos3(bool autogolpe)
    {
        RecibeImpacto();
        if (autogolpe)
        {
            puntuacion3 -= puntajeAutoGolpe;
        }
        else
        {
            puntuacion3 += puntajePorPersona;
        }
        puntaje3.text = puntuacion3.ToString();

        ComprobarVaGanando();
    }
    public void SubirPuntos4(bool autogolpe)
    {
        RecibeImpacto();
        if (autogolpe)
        {
            puntuacion4 -= puntajeAutoGolpe;
        }
        else
        {
            puntuacion4 += puntajePorPersona;
        }
        puntaje4.text = puntuacion4.ToString();

        ComprobarVaGanando();
    }

    public Vector3 DameRespawn()
    {
        Vector3 trans = respawns[Random.Range(0, respawns.Length)].position;
        return trans;
    }

    public IEnumerator ChangeSpeed1(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed1 = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
            depth.focusDistance.value = speed1;
        }
        speed1 = v_end;
        depth.focusDistance.value = speed1;
        StartCanvas.SetActive(false);
    }
    public IEnumerator ChangeSpeed2(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed2 = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
            StartBlack.color = new Color32(0, 0, 0, (byte)speed2);
        }
        speed2 = v_end;
        StartBlack.color = new Color32(0, 0, 0, (byte)speed2);
    }
    public IEnumerator ChangeSpeed3(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed3 = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;

            for (int i = 0; i < StartImgs.Length; i++)
            {
                StartImgs[i].color = new Color32(255, 255, 255, (byte)speed3);
            }
        }
        speed3 = v_end;

        for (int i = 0; i < StartImgs.Length; i++)
        {
            StartImgs[i].color = new Color32(255, 255, 255, (byte)speed3);
        }
    }

    #endregion

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pausar();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausar();
        }

        if (comenzar)
        {
            if (impactChromatic >= 0.05f)
            {
                impactChromatic -= 0.01f;
                chromatic.intensity.value = impactChromatic;
            }
            if (impactBloomDif >= 7)
            {
                impactBloomDif -= 0.1f;
                bloom.diffusion.value = impactBloomDif;
            }

            mili += Time.unscaledDeltaTime;
            if (mili >= 1)
            {
                mili = 0;
                segundos -= 1;
                if (segundos <= -1)
                {
                    segundos = 59;
                    minutos -= 1;
                }
            }

            if (minutos == 0)
            {
                timer.color = timerRojo;
            }

            if (minutos < 0)
            {
                timer.text = "0:00";
                FinalizarJuego();
                comenzar = false;
            }

            if (segundos == -1)
            {
                segundosS = 0;
            }
            else
            {
                segundosS = segundos;
            }
            timer.text = minutos + ":" + segundosS.ToString("00");
            if (segundos == 0)
            {
                segundosS = 60;
            }
        }
    }
}
