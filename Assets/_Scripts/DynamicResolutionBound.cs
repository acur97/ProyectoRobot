using System;
using UnityEngine;
using UnityEngine.UI;

public class DynamicResolutionBound : MonoBehaviour
{
    public GameObject CanvasDebug;
    public bool DebugRes;
    private Text testo;
    [Space]
    [Range(0.5f, 1)]
    public double res;
    private double resA;
    public bool Bound;
    [Range(0, 0.1f)]
    public float limiteBound = 0.075f;
    public float smoothDeltaTime;
    public float porcentaje = 1;
    [Range(0,2)]
    public float velocidadSubida = 1;
    [Range(0,2)]
    public float velocidadBajada = 1;
    [Space]
    public float maxResolutionWidthScale = 1.0f;
    public float maxResolutionHeightScale = 1.0f;
    public float minResolutionWidthScale = 0.5f;
    public float minResolutionHeightScale = 0.5f;
    public float scaleWidthIncrement = 0.1f;
    public float scaleHeightIncrement = 0.1f;

    private float resW;
    private float resH;

    private void OnApplicationQuit()
    {
        ScalableBufferManager.ResizeBuffers(1, 1);
        Debug.Log("Normalidad res");
    }

    private void Awake()
    {
        CanvasDebug.SetActive(DebugRes);
        testo = CanvasDebug.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        porcentaje = 1;
        testo.text = "x1" + "\n" + Screen.width + "x" + Screen.height;
    }

    void Update()
    {
        CanvasDebug.SetActive(DebugRes);
        if (Screen.width != resW | Screen.height != resH)
        {
            ScalableBufferManager.ResizeBuffers((float)res, (float)res);
            testo.text = "x" + res + "\n" + (Screen.width * (float)res) + "x" + (Screen.height * (float)res);
            if (DebugRes)
            {
                Debug.Log("Cambio Res");
            }
        }

        smoothDeltaTime = Time.smoothDeltaTime;
        Bound = (smoothDeltaTime >= limiteBound);
        if (Bound && porcentaje > 0.5f)
        {
            porcentaje -= (Time.unscaledDeltaTime * velocidadSubida);
            Cambio();
        }
        else if (porcentaje < 1)
        {
            porcentaje += (Time.unscaledDeltaTime * velocidadBajada);
            Cambio();
        }

        porcentaje = Mathf.Clamp(porcentaje, 0.5f, 1);

        res = Math.Round(porcentaje, 1);

        resW = Screen.width;
        resH = Screen.height;
    }

    public void Cambio()
    {
        if (res > resA)
        {
            ScalableBufferManager.ResizeBuffers((float)res, (float)res);
            testo.text = "x" + res + "\n" + (Screen.width * (float)res) + "x" + (Screen.height * (float)res);
            if (DebugRes)
            {
                Debug.Log("Subir");
            }
        }
        if (res < resA)
        {
            ScalableBufferManager.ResizeBuffers((float)res, (float)res);
            testo.text = "x" + res + "\n" + (int)(Screen.width * (float)res) + "x" + (int)(Screen.height * (float)res);
            if (DebugRes)
            {
                Debug.Log("Bajar");
            }
        }

        resA = res;
    }
}