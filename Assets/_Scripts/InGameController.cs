using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

public class InGameController : MonoBehaviour
{
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
    public GameObject jugador2;
    public GameObject jugador3;
    public GameObject jugador4;
    public CinemachineTargetGroup camGroup;
    private CinemachineTargetGroup.Target finalizador;

    private float mili;
    private int segundos;
    private int segundosS;
    private int minutos;
    private DepthOfField depth;
    private ChromaticAberration chromatic;
    private float impactChromatic;
    private CinemachineBasicMultiChannelPerlin Camnoise;
    private float chromaPrev;
    private float speed1;
    private float speed2;
    private float speed3;

    private int puntuacion1;
    private int vaGanando;
    private int puntuacion2;
    private int puntuacion3;
    private int puntuacion4;

    private void Awake()
    {
        naveRobot.SetActive(false);
        comenzar = false;
        StartImgs = StartCanvas.GetComponentsInChildren<Image>();

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
        Camnoise = Vcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        chromaPrev = chromatic.intensity.value;
        depth.focusDistance.value = 0.1f;
        StartBlack.color = new Color32(0, 0, 0, 128);
    }

    public void RecibeImpacto()
    {
        impactChromatic = 0.25f;
    }

    public void IniciarJuego()
    {
        StartCoroutine(ChangeSpeed1(0.1f, 35, tiempoBlurStart));
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
        yield return new WaitForSecondsRealtime(3.2f);
        comenzar = true;
        yield return new WaitForSecondsRealtime(8);
        naveRobot.SetActive(false);
    }

    public void Pausar()
    {
        if (comenzar)
        {
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
        comenzar = true;
        Time.timeScale = 1;
        PlayCanvas.SetActive(true);
        PausaCanvas.SetActive(false);
        depth.focusDistance.value = 0.1f;
        StartBlack.color = new Color32(0, 0, 0, 0);
    }

    public void VolverMenu()
    {
        Initiate.Fade("Game", Color.black, 1);
    }

    public void FinalizarJuego()
    {
        PlayCanvas.SetActive(false);
        FinalCanvas.SetActive(true);
        depth.focusDistance.value = 200;
        StartBlack.color = new Color32(0, 0, 0, 128);

        //camGroup.m_Targets = new CinemachineTargetGroup.Target[1];
        if (vaGanando == 1)
        {
            finalizador.target = jugador1.transform;
            finalizador.weight = 1;
            finalizador.radius = 1;

            jugador1.GetComponent<enumTest>().Bailar();

            camGroup.m_Targets.SetValue(finalizador, 0);
            camGroup.m_Targets.SetValue(finalizador, 1);
            camGroup.m_Targets.SetValue(finalizador, 2);
            camGroup.m_Targets.SetValue(finalizador, 3);
        }
        if (vaGanando == 2)
        {
            finalizador.target = jugador2.transform;
            finalizador.weight = 1;
            finalizador.radius = 1;

            jugador2.GetComponent<enumTest>().Bailar();

            camGroup.m_Targets.SetValue(finalizador, 0);
            camGroup.m_Targets.SetValue(finalizador, 1);
            camGroup.m_Targets.SetValue(finalizador, 2);
            camGroup.m_Targets.SetValue(finalizador, 3);
        }
        if (vaGanando == 3)
        {
            finalizador.target = jugador3.transform;
            finalizador.weight = 1;
            finalizador.radius = 1;

            jugador3.GetComponent<enumTest>().Bailar();

            camGroup.m_Targets.SetValue(finalizador, 0);
            camGroup.m_Targets.SetValue(finalizador, 1);
            camGroup.m_Targets.SetValue(finalizador, 2);
            camGroup.m_Targets.SetValue(finalizador, 3);
        }
        if (vaGanando == 4)
        {
            finalizador.target = jugador4.transform;
            finalizador.weight = 1;
            finalizador.radius = 1;

            jugador4.GetComponent<enumTest>().Bailar();

            camGroup.m_Targets.SetValue(finalizador, 0);
            camGroup.m_Targets.SetValue(finalizador, 1);
            camGroup.m_Targets.SetValue(finalizador, 2);
            camGroup.m_Targets.SetValue(finalizador, 3);
        }
    }

    public void Salir()
    {
        Application.Quit();
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

        if (puntuacion1 > puntuacion2 && puntuacion1 > puntuacion3 && puntuacion1 > puntuacion4)
        {
            vaGanando = 1;
        }
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

        if (puntuacion2 > puntuacion1 && puntuacion2 > puntuacion3 && puntuacion2 > puntuacion4)
        {
            vaGanando = 2;
        }
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

        if (puntuacion3 > puntuacion1 && puntuacion3 > puntuacion2 && puntuacion3 > puntuacion4)
        {
            vaGanando = 3;
        }
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

        if (puntuacion4 > puntuacion1 && puntuacion4 > puntuacion2 && puntuacion4 > puntuacion3)
        {
            vaGanando = 4;
        }
    }

    public Vector3 DameRespawn()
    {
        Vector3 trans = respawns[Random.Range(0, respawns.Length)].position;
        return trans;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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
}
