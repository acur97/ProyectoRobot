using UnityEngine;
using UnityEngine.Rendering.LWRP;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public enum Vsync { Desactivado, Activado, Doble }
    [Space(-10, order = -3)]
    [Header("   GRAFICOS", order = -2)]
    [Space(order = -1)]

    [Space(-10, order = 0)]
    [Header("--- FPS ---", order = 1)]
    public Vsync SyncVertical;
    [Header("-1 systema", order = 1)]
    [Range(-1, 300)]
    public int targetFps = -1;

    [Header("--- Anti-Aliasing ---")]
    public UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset renderAsset;
    public enum Antialiasing { Desactivado, x2, x4, x8 };
    [Space(-0.25f)]
    public Antialiasing MSAA;

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
    public float volMaster = 1;
    [Space]
    public AudioMixerGroup mixMusica;
    [Range(0, 1)]
    public float volMusica = 1;
    [Space]
    public AudioMixerGroup mixVfx;
    [Range(0, 1)]
    public float volVfx = 1;
    [Space]
    public AudioMixerGroup mixVoces;
    [Range(0, 1)]
    public float volVoces = 1;

    private const string _Volumen_master = "Volumen_master";
    private const string _Volumen_Musica = "Volumen_Musica";
    private const string _Volumen_VFX = "Volumen_VFX";
    private const string _Volumen_Voces = "Volumen_Voces";

    private void Awake()
    {
        switch (SyncVertical)
        {
            case Vsync.Desactivado:
                QualitySettings.vSyncCount = 0;
                break;

            case Vsync.Activado:
                QualitySettings.vSyncCount = 1;
                break;

            case Vsync.Doble:
                QualitySettings.vSyncCount = 2;
                break;

            default:
                break;
        }

        Application.targetFrameRate = targetFps;

        switch (MSAA)
        {
            case Antialiasing.Desactivado:
                renderAsset.msaaSampleCount = 1;
                break;

            case Antialiasing.x2:
                renderAsset.msaaSampleCount = 2;
                break;

            case Antialiasing.x4:
                renderAsset.msaaSampleCount = 4;
                break;

            case Antialiasing.x8:
                renderAsset.msaaSampleCount = 8;
                break;
            default:
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

        mixMaster.audioMixer.GetFloat(_Volumen_master, out volMaster);
        mixMusica.audioMixer.GetFloat(_Volumen_Musica, out volMusica);
        mixVfx.audioMixer.GetFloat(_Volumen_VFX, out volVfx);
        mixVoces.audioMixer.GetFloat(_Volumen_Voces, out volVoces);
    }
}