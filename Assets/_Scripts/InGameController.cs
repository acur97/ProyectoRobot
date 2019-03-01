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
    private int puntuacion2;
    private int puntuacion3;
    private int puntuacion4;

    private void Awake()
    {
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
        }
    }

    public void Reanudar()
    {
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
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void SubirPuntos1()
    {
        puntuacion1 += puntajePorPersona;
        puntaje1.text = puntuacion1.ToString();
    }
    public void SubirPuntos2()
    {
        puntuacion2 += puntajePorPersona;
        puntaje2.text = puntuacion2.ToString();
    }
    public void SubirPuntos3()
    {
        puntuacion3 += puntajePorPersona;
        puntaje3.text = puntuacion3.ToString();
    }
    public void SubirPuntos4()
    {
        puntuacion4 += puntajePorPersona;
        puntaje4.text = puntuacion4.ToString();
    }

    public Vector3 DameRespawn()
    {
        Vector3 trans = respawns[Random.Range(0, respawns.Length)].position;
        return trans;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausar();
        }

        if (impactChromatic >= 0.05f)
        {
            impactChromatic -= 0.01f;
            chromatic.intensity.value = impactChromatic;
        }

        if (comenzar)
        {
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
                Debug.Log("Fin");
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
