using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LWRP;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [Space(-10, order = -3)]
    [Header("   GRAFICOS", order = -2)]
    [Space(order = -1)]

    [Space(-10, order = 0)]
    [Header("--- FPS ---", order = 1)]
    [Space(-10, order = 2)]
    [Header("vSync x2 = 28", order = 3)]
    [Space(-10, order = 4)]
    [Header("vSync x1 = 29", order = 5)]
    [Space(-10, order = 6)]
    [Header("fps infinitos = 251", order = 7)]
    [Range(28, 251)]
    public int targetFps = 29;
    private int settingFps;

    [Header("--- Anti-Aliasing ---")]
    public UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset renderAsset;
    public enum antialiasing { Desactivado, x2, x4, x8 };
    [Space(-0.25f)]
    public antialiasing MSAA;

    [Header("--- Render Scale ---")]
    [Range(0.1f, 2)]
    public float renderScale = 1;

    [Header("--- FullScreen Resolutions ---", order = 0)]
    public bool fullscreen = true;
    [Space(-10, order = 1)]
    [Header("x-Width, y-Heigh, z-Refresh", order = 2)]
    public Vector3[] resoluciones;
    private Resolution[] resolutions;

    [Header("   AUDIO")]
    [Space]
    public AudioMixerGroup mixMaster;
    [Range(0, 1)]
    public float volMaster;
    [Space]
    public AudioMixerGroup mixMusica;
    [Range(0, 1)]
    public float volMusica;
    [Space]
    public AudioMixerGroup mixVfx;
    [Range(0, 1)]
    public float volVfx;
    [Space]
    public AudioMixerGroup mixVoces;
    [Range(0, 1)]
    public float volVoces;

    private void Awake()
    {
        if (targetFps == 28)
        {
            QualitySettings.vSyncCount = 2;
            settingFps = 1;
        }
        else if (targetFps == 29)
        {
            QualitySettings.vSyncCount = 1;
            settingFps = 2;
        }
        else if (targetFps == 251)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
            settingFps = 3;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFps;
            settingFps = 4;
        }

        switch (MSAA)
        {
            case antialiasing.Desactivado:
                renderAsset.msaaSampleCount = 1;
                break;

            case antialiasing.x2:
                renderAsset.msaaSampleCount = 2;
                break;

            case antialiasing.x4:
                renderAsset.msaaSampleCount = 4;
                break;

            case antialiasing.x8:
                renderAsset.msaaSampleCount = 8;
                break;
        }
        renderAsset.renderScale = renderScale;

        resolutions = Screen.resolutions;
        resoluciones = new Vector3[resolutions.Length];
        for (int i = 0; i < resolutions.Length; i++)
        {
            resoluciones[i] = new Vector3(resolutions[i].width, resolutions[i].height, resolutions[i].refreshRate);
        }
        // el ultimo sera la resolucion actual maxima

        mixMaster.audioMixer.GetFloat("Volumen_master", out volMaster);
        mixMusica.audioMixer.GetFloat("Volumen_Musica", out volMusica);
        mixVfx.audioMixer.GetFloat("Volumen_VFX", out volVfx);
        mixVoces.audioMixer.GetFloat("Volumen_Voces", out volVoces);
    }

    private void Start()
    {
        switch (settingFps)
        {
            case 1:
                Debug.Log("vSync: x2 on, fps: " + (Screen.currentResolution.refreshRate / 2));
                break;

            case 2:
                Debug.Log("vSync: x1 on, fps: " + Screen.currentResolution.refreshRate);
                break;

            case 3:
                Debug.Log("vSync: off, fps: infinitos");
                break;

            case 4:
                Debug.Log("vSync: off, fps: " + Application.targetFrameRate);
                break;
        }
    }
}
