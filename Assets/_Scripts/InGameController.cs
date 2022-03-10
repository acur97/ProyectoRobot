using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance { get; private set; }

    [Header("Config Inicial")]
    [SerializeField] private GameObject[] objects;
    [SerializeField] private GameObject[] prefabs = new GameObject[4];

    [Header("Tiempo inicial"), Range(0, 5)]
    [SerializeField] private int minutosStart;
    [Range(0, 59)]
    [SerializeField] private int segundosStart;

    [Space]
    [SerializeField] private float tiempoBlurStart;

    [Space]
    [SerializeField] private Text timer;
    [SerializeField] private Color timerRojo;
    public bool comenzar;

    [Header("Puntuaciones")]
    [SerializeField] private int puntajePorPersona = 100;
    [SerializeField] private int puntajeAutoGolpe = 50;
    [SerializeField] private Text puntaje1;
    [SerializeField] private Text puntaje2;
    [SerializeField] private Text puntaje3;
    [SerializeField] private Text puntaje4;

    [Header("Respawn´s")]
    [SerializeField] private Transform[] respawns;

    [Header("Configs UI")]
    [SerializeField] private Canvas PlayCanvas;
    [SerializeField] private GameObject man1_1;
    [SerializeField] private GameObject man1_2;
    [SerializeField] private GameObject man2_1;
    [SerializeField] private GameObject man2_2;
    [SerializeField] private GameObject man3_1;
    [SerializeField] private GameObject man3_2;
    [SerializeField] private GameObject man4_1;
    [SerializeField] private GameObject man4_2;
    [SerializeField] private GameObject time_1;
    [SerializeField] private GameObject time_2;

    [Space]
    [SerializeField] private Canvas PausaCanvas;
    [SerializeField] private Canvas StartCanvas;
    private CanvasGroup StartCanvasGroup;
    [SerializeField] private Canvas FinalCanvas;
    [SerializeField] private PostProcessVolume post;
    [SerializeField] private CinemachineVirtualCamera Vcamera;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource source1;
    [SerializeField] private AudioSource source2;

    [Header("Animacion Inicial")]
    [SerializeField] private AudioClip Aintroduccion;
    [SerializeField] private AudioClip A321;
    [SerializeField] private AudioClip Afaltan30;
    [SerializeField] private AudioClip Afaltan10;
    [SerializeField] private AudioClip Afaltan5;
    [SerializeField] private AudioClip AalarmaFin;

    [Space]
    [SerializeField] private GameObject naveRobot;

    [Header("Configs Jugadores")]
    [SerializeField] private GameObject jugador1;
    [SerializeField] private Transform jug1animRoot;
    [SerializeField] private MeshRenderer esferaRespaw1;
    [SerializeField] private MeshRenderer arrow1;
    [SerializeField] private Text UItext1;
    [SerializeField] private Image UI1img1;
    [SerializeField] private Image UI2img1;
    [SerializeField] private GameObject man1;
    [SerializeField] private GameObject man1Static;
    //private Material esferaMatRespaw1;

    [Space]
    [SerializeField] private GameObject jugador2;
    [SerializeField] private Transform jug2animRoot;
    [SerializeField] private MeshRenderer esferaRespaw2;
    [SerializeField] private MeshRenderer arrow2;
    [SerializeField] private Text UItext2;
    [SerializeField] private Image UI1img2;
    [SerializeField] private Image UI2img2;
    [SerializeField] private GameObject man2;
    [SerializeField] private GameObject man2Static;
    //private Material esferaMatRespaw2;

    [Space]
    [SerializeField] private GameObject jugador3;
    [SerializeField] private Transform jug3animRoot;
    [SerializeField] private MeshRenderer esferaRespaw3;
    [SerializeField] private MeshRenderer arrow3;
    [SerializeField] private Text UItext3;
    [SerializeField] private Image UI1img3;
    [SerializeField] private Image UI2img3;
    [SerializeField] private GameObject man3;
    [SerializeField] private GameObject man3Static;
    //private Material esferaMatRespaw3;

    [Space]
    [SerializeField] private GameObject jugador4;
    [SerializeField] private Transform jug4animRoot;
    [SerializeField] private MeshRenderer esferaRespaw4;
    [SerializeField] private MeshRenderer arrow4;
    [SerializeField] private Text UItext4;
    [SerializeField] private Image UI1img4;
    [SerializeField] private Image UI2img4;
    [SerializeField] private GameObject man4;
    [SerializeField] private GameObject man4Static;
    //private Material esferaMatRespaw4;

    [Header("Vcam")]
    [SerializeField] private CinemachineTargetGroup camGroup;

    [Space]
    [SerializeField] private AudioClip[] SonidosVictoria;
    private CinemachineTargetGroup.Target finalizador1;
    private CinemachineTargetGroup.Target finalizador2;
    private CinemachineTargetGroup.Target finalizador3;

    private StandaloneInputModule standaloneInput;

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
    [SerializeField] private bool pausado = false;

    private int vaGanando;
    private int puntuacion1;
    private int puntuacion2;
    private int puntuacion3;
    private int puntuacion4;

    public static int P1_index = -1;
    public static string P1_H;
    public static string P1_V;
    public static string P1_F;
    public static string P1_P;

    public static int P2_index = -1;
    public static string P2_H;
    public static string P2_V;
    public static string P2_F;
    public static string P2_P;

    public static int P3_index = -1;
    public static string P3_H;
    public static string P3_V;
    public static string P3_F;
    public static string P3_P;

    public static int P4_index = -1;
    public static string P4_H;
    public static string P4_V;
    public static string P4_F;
    public static string P4_P;

    private bool P1desconeccion;
    private bool P2desconeccion;
    private bool P3desconeccion;
    private bool P4desconeccion;

    public static int PN;

    public static Scene sceneToUnload;

    private float countPausa = 0;

    private const string _Jug1Seleccion = "Jug1Seleccion";
    private const string _Jug2Seleccion = "Jug2Seleccion";
    private const string _Jug3Seleccion = "Jug3Seleccion";
    private const string _Jug4Seleccion = "Jug4Seleccion";
    private const string _Robot2 = "Robot2";
    private const string _Robot4 = "Robot4";
    private const string _Volumen_master = "Volumen_master";
    //private const string _J1_H = "J1_H";
    //private const string _J1_V = "J1_V";
    //private const string _J1_F = "J1_F";
    private const string _J1_H = "Generic_H";
    private const string _J1_V = "Generic_V";
    private const string _J1_F = "Generic_F";
    private const string _DosPuntos = ":";
    private const string _Seleccion = "Seleccion";
    private const string _Pause = "Pause";
    private const string _Ceros = "0:00";
    private const string _DosCeros = "00";

    private readonly int _BaseColor = Shader.PropertyToID("_BaseColor");

    private void Awake()
    {
        Instance = this;

        //Debug.LogWarning("- hacer que el canvasPlay no se apague al finalizar, lo que se apague, dependiendo de los que ganan," +
        //    " sean la puntuacion de los que pierden y queden la puntuacion de los que ganan");

        //if (esferaMatRespaw1 != null)
        //{
        //    esferaRespaw1.material = esferaMatRespaw1;
        //}
        //if (esferaMatRespaw2 != null)
        //{
        //    esferaRespaw2.material = esferaMatRespaw2;
        //}
        //if (esferaMatRespaw3 != null)
        //{
        //    esferaRespaw3.material = esferaMatRespaw3;
        //}
        //if (esferaMatRespaw4 != null)
        //{
        //    esferaRespaw4.material = esferaMatRespaw4;
        //}

        standaloneInput = EventSystem.current.GetComponent<StandaloneInputModule>();

        jugador1.SetActive(false);
        man1.SetActive(false);
        man1Static.SetActive(false);

        jugador2.SetActive(false);
        man2.SetActive(false);
        man2Static.SetActive(false);

        jugador3.SetActive(false);
        man3.SetActive(false);
        man3Static.SetActive(false);

        jugador4.SetActive(false);
        man4.SetActive(false);
        man4Static.SetActive(false);

        #region Start Players Setters
        objects = SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].CompareTag(_Jug1Seleccion))
            {
                prefabs[0] = objects[i];
                prefabs[0].layer = 10;
                jug1animRoot.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
                prefabs[0].transform.SetParent(jug1animRoot);
                prefabs[0].transform.localPosition = Vector3.zero;
                if (prefabs[0].name == _Robot2 || prefabs[0].name == _Robot4)
                {
                    prefabs[0].transform.eulerAngles = new Vector3(0, -45, 0);
                }
                else
                {
                    prefabs[0].transform.eulerAngles = Vector3.zero;
                }
                prefabs[0].transform.localScale = new Vector3(10.876f, 10.876f, 10.876f);
                jugador1.GetComponent<JugadorController>().anim = prefabs[0].GetComponent<Animator>();
                ColoresPass coloresPas = prefabs[0].GetComponent<ColoresPass>();
                esferaRespaw1.material.SetColor(_BaseColor, coloresPas.RespawColor);
                arrow1.material.SetColor(_BaseColor, coloresPas.MiraPisoColor);
                jugador1.GetComponentInChildren<Disparo>().colorBala = coloresPas.BalaColor;
                Color32 colorUI = coloresPas.MiraPisoColor;
                UI1img1.color = colorUI;
                UI2img1.color = colorUI;
                UItext1.color = coloresPas.colorUI;
                man1.SetActive(true);
                man1Static.SetActive(true);
                jugador1.SetActive(true);
                Debug.Log("Jugador 1 ready");
            }
            if (objects[i].CompareTag(_Jug2Seleccion))
            {
                prefabs[1] = objects[i];
                prefabs[1].layer = 10;
                jug2animRoot.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
                prefabs[1].transform.SetParent(jug2animRoot);
                prefabs[1].transform.localPosition = Vector3.zero;
                if (prefabs[1].name == _Robot2 || prefabs[1].name == _Robot4)
                {
                    prefabs[1].transform.eulerAngles = new Vector3(0, -45, 0);
                }
                else
                {
                    prefabs[1].transform.eulerAngles = Vector3.zero;
                }
                prefabs[1].transform.localScale = new Vector3(10.876f, 10.876f, 10.876f);
                jugador2.GetComponent<JugadorController>().anim = prefabs[1].GetComponent<Animator>();
                ColoresPass coloresPas = prefabs[1].GetComponent<ColoresPass>();
                esferaRespaw2.material.SetColor(_BaseColor, coloresPas.RespawColor);
                arrow2.material.SetColor(_BaseColor, coloresPas.MiraPisoColor);
                jugador2.GetComponentInChildren<Disparo>().colorBala = coloresPas.BalaColor;
                Color32 colorUI = coloresPas.MiraPisoColor;
                UI1img2.color = colorUI;
                UI2img2.color = colorUI;
                UItext2.color = coloresPas.colorUI;
                man2.SetActive(true);
                man2Static.SetActive(true);
                jugador2.SetActive(true);
                Debug.Log("Jugador 2 ready");
            }
            if (objects[i].CompareTag(_Jug3Seleccion))
            {
                prefabs[2] = objects[i];
                prefabs[2].layer = 10;
                jug3animRoot.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
                prefabs[2].transform.SetParent(jug3animRoot);
                prefabs[2].transform.localPosition = Vector3.zero;
                if (prefabs[2].name == _Robot2 || prefabs[2].name == _Robot4)
                {
                    prefabs[2].transform.eulerAngles = new Vector3(0, -45, 0);
                }
                else
                {
                    prefabs[2].transform.eulerAngles = Vector3.zero;
                }
                prefabs[2].transform.localScale = new Vector3(10.876f, 10.876f, 10.876f);
                jugador3.GetComponent<JugadorController>().anim = prefabs[2].GetComponent<Animator>();
                ColoresPass coloresPas = prefabs[2].GetComponent<ColoresPass>();
                esferaRespaw3.material.SetColor(_BaseColor, coloresPas.RespawColor);
                arrow3.material.SetColor(_BaseColor, coloresPas.MiraPisoColor);
                jugador3.GetComponentInChildren<Disparo>().colorBala = coloresPas.BalaColor;
                Color32 colorUI = coloresPas.MiraPisoColor;
                UI1img3.color = colorUI;
                UI2img3.color = colorUI;
                UItext3.color = coloresPas.colorUI;
                man3.SetActive(true);
                man3Static.SetActive(true);
                jugador3.SetActive(true);
                Debug.Log("Jugador 3 ready");
            }
            if (objects[i].CompareTag(_Jug4Seleccion))
            {
                prefabs[3] = objects[i];
                prefabs[3].layer = 10;
                jug4animRoot.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
                prefabs[3].transform.SetParent(jug4animRoot);
                prefabs[3].transform.localPosition = Vector3.zero;
                if (prefabs[3].name == _Robot2 || prefabs[3].name == _Robot4)
                {
                    prefabs[3].transform.eulerAngles = new Vector3(0, -45, 0);
                }
                else
                {
                    prefabs[3].transform.eulerAngles = Vector3.zero;
                }
                prefabs[3].transform.localScale = new Vector3(10.876f, 10.876f, 10.876f);
                jugador4.GetComponent<JugadorController>().anim = prefabs[3].GetComponent<Animator>();
                ColoresPass coloresPas = prefabs[3].GetComponent<ColoresPass>();
                esferaRespaw4.material.SetColor(_BaseColor, coloresPas.RespawColor);
                arrow4.material.SetColor(_BaseColor, coloresPas.MiraPisoColor);
                jugador4.GetComponentInChildren<Disparo>().colorBala = coloresPas.BalaColor;
                Color32 colorUI = coloresPas.MiraPisoColor;
                UI1img4.color = colorUI;
                UI2img4.color = colorUI;
                UItext4.color = coloresPas.colorUI;
                man4.SetActive(true);
                man4Static.SetActive(true);
                jugador4.SetActive(true);
                Debug.Log("Jugador 4 ready");
                //continue;
            }
        }
        if (!jugador1.activeSelf && !jugador2.activeSelf && !jugador3.activeSelf && !jugador4.activeSelf)
        {
            //Test jug 1
            //prefabs[0] = objects[i];
            //prefabs[0].layer = 10;
            //jug1animRoot.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);
            //prefabs[0].transform.SetParent(jug1animRoot);
            //prefabs[0].transform.localPosition = Vector3.zero;
            //if (prefabs[0].name == _Robot2 || prefabs[0].name == _Robot4)
            //{
            //    prefabs[0].transform.eulerAngles = new Vector3(0, -45, 0);
            //}
            //else
            //{
            //    prefabs[0].transform.eulerAngles = Vector3.zero;
            //}
            //prefabs[0].transform.localScale = new Vector3(10.876f, 10.876f, 10.876f);
            //jugador1.GetComponent<JugadorController>().anim = prefabs[0].GetComponent<Animator>();
            //ColoresPass coloresPas = prefabs[0].GetComponent<ColoresPass>();
            //esferaRespaw1.material.SetColor(_BaseColor, coloresPas.RespawColor);
            //arrow1.material.SetColor(_BaseColor, coloresPas.MiraPisoColor);
            //jugador1.GetComponentInChildren<Disparo>().colorBala = coloresPas.BalaColor;
            //Color32 colorUI = coloresPas.MiraPisoColor;
            //UI1img1.color = colorUI;
            //UI2img1.color = colorUI;
            //UItext1.color = coloresPas.colorUI;
            P1_H = "Generic_H";
            P1_V = "Generic_V";
            P1_F = "Generic_F";
            P1_P = "Generic_P";
            man1.SetActive(true);
            man1Static.SetActive(true);
            jugador1.SetActive(true);
            Debug.Log("Jugador 1 test ready");
        }
        #endregion

        mixer.SetFloat(_Volumen_master, 0);
        naveRobot.SetActive(false);
        comenzar = false;
        StartCanvasGroup = StartCanvas.GetComponent<CanvasGroup>();

        if (P1_H != null)
        {
            standaloneInput.horizontalAxis = P1_H;
            standaloneInput.verticalAxis = P1_V;
            //standaloneInput.submitButton = P1_F;
        }
        else
        {
            standaloneInput.horizontalAxis = _J1_H;
            standaloneInput.verticalAxis = _J1_V;
            //standaloneInput.submitButton = _J1_F;
        }
        //eventSystem.cancelButton = P1_P;

        StartCanvas.gameObject.SetActive(true);
        PausaCanvas.gameObject.SetActive(true);
        PausaCanvas.enabled = false;
        PlayCanvas.gameObject.SetActive(true);
        man1_1.SetActive(false);
        man1_2.SetActive(false);
        man2_1.SetActive(false);
        man2_2.SetActive(false);
        man3_1.SetActive(false);
        man3_2.SetActive(false);
        man4_1.SetActive(false);
        man4_2.SetActive(false);
        time_1.SetActive(true);
        time_2.SetActive(true);
        FinalCanvas.gameObject.SetActive(true);
        FinalCanvas.enabled = false;

        minutos = minutosStart;
        segundos = segundosStart;
        timer.text = minutos + _DosPuntos + segundosStart;

        post.profile.TryGetSettings(out depth);
        post.profile.TryGetSettings(out chromatic);
        post.profile.TryGetSettings(out bloom);
        Camnoise = Vcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        groupc = Vcamera.GetCinemachineComponent<CinemachineGroupComposer>();
        groupc.m_ScreenY = 0.55f;
        depth.focusDistance.value = 0.1f;

        if (jugador1.activeSelf)
        {
            jugador1.GetComponent<JugadorController>().Horizontal = P1_H;
            jugador1.GetComponent<JugadorController>().Vertical = P1_V;
            jugador1.GetComponent<JugadorController>().Fire = P1_F;
            jugador1.GetComponent<JugadorController>().Power = P1_P;

            puntaje1.text = 0.ToString();
            man1_1.SetActive(true);
            man1_2.SetActive(true);
        }

        if (jugador2.activeSelf)
        {
            jugador2.GetComponent<JugadorController>().Horizontal = P2_H;
            jugador2.GetComponent<JugadorController>().Vertical = P2_V;
            jugador2.GetComponent<JugadorController>().Fire = P2_F;
            jugador2.GetComponent<JugadorController>().Power = P2_P;

            puntaje2.text = 0.ToString();
            man2_1.SetActive(true);
            man2_2.SetActive(true);
        }

        if (jugador3.activeSelf)
        {
            jugador3.GetComponent<JugadorController>().Horizontal = P3_H;
            jugador3.GetComponent<JugadorController>().Vertical = P3_V;
            jugador3.GetComponent<JugadorController>().Fire = P3_F;
            jugador3.GetComponent<JugadorController>().Power = P3_P;

            puntaje3.text = 0.ToString();
            man3_1.SetActive(true);
            man3_2.SetActive(true);
        }

        if (jugador4.activeSelf)
        {
            jugador4.GetComponent<JugadorController>().Horizontal = P4_H;
            jugador4.GetComponent<JugadorController>().Vertical = P4_V;
            jugador4.GetComponent<JugadorController>().Fire = P4_F;
            jugador4.GetComponent<JugadorController>().Power = P4_P;

            puntaje4.text = 0.ToString();
            man4_1.SetActive(true);
            man4_2.SetActive(true);
        }
    }

    private void Start()
    {
        //if (jugador1.activeSelf)
        //{
        //    ColoresPass coloresPas = prefabs[0].GetComponent<ColoresPass>();
        //    esferaRespaw1.material.SetColor("Color", coloresPas.RespawColor);
        //    arrow1.material.SetColor("Color", coloresPas.MiraPisoColor);
        //    jugador1.GetComponentInChildren<Disparo>().colorBala = coloresPas.BalaColor;
        //}

        if (sceneToUnload.isLoaded)
        {
            SceneManager.UnloadSceneAsync(0);
        }
    }

    private void OnEnable()
    {
        ControlsUpdate.OnDisconnect += OnDisConect;
        //ControlsUpdate.OnConnect += OnConect;
        ControlsUpdate.OnReconnect += OnReconect;
    }

    void Update()
    {
        if (Input.GetButtonDown(_Pause))
        {
            if (!pausado)
            {
                Pausar();
            }
            if (countPausa < -1 && pausado)
            {
                Reanudar();
            }
        }

        if (pausado)
        {
            countPausa -= 1;
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

            mili += Time.deltaTime;
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
                timer.text = _Ceros;
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
            timer.text = minutos + _DosPuntos + segundosS.ToString(_DosCeros);
            if (segundos == 0)
            {
                segundosS = 60;
            }
        }
    }

    #region Funciones

    public void RecibeImpacto()
    {
        impactChromatic = 0.25f;
        impactBloomDif = 9;
    }

    public void IniciarJuego()
    {
        StartCoroutine(ChangeSpeed1(0.1f, 10, tiempoBlurStart * 1.6f));
        StartCoroutine(ChangeSpeed2(1, 0, tiempoBlurStart));

        StartCoroutine(ComenzarAudios());
    }

    IEnumerator ComenzarAudios()
    {
        yield return new WaitForSeconds(tiempoBlurStart);
        naveRobot.SetActive(true);
        yield return new WaitForSeconds(tiempoBlurStart);
        source1.clip = Aintroduccion;
        source1.Play();
        yield return new WaitForSeconds(0.3f);
        source2.clip = A321;
        source2.Play();
        yield return new WaitForSeconds(2.6f);
        StartCoroutine(ChangeSpeed1(10, 35, 4));
        yield return new WaitForSeconds(0.8f);
        comenzar = true;
        yield return new WaitForSeconds(8);
        naveRobot.SetActive(false);
    }

    private void OnDisConect(string controlName, bool conectado, int joystickNum)
    {
        Debug.LogWarning("bai " + controlName);

        if (P1_index == joystickNum)
        {
            Debug.LogError("Crack " + 1 + ", se te desconecto el control " + controlName);
            P1desconeccion = true;
            Pausar();
        }
        if (P1_index == joystickNum)
        {
            Debug.LogError("Crack " + 2 + ", se te desconecto el control " + controlName);
            P2desconeccion = true;
            Pausar();
        }
        if (P1_index == joystickNum)
        {
            Debug.LogError("Crack " + 3 + ", se te desconecto el control " + controlName);
            P3desconeccion = true;
            Pausar();
        }
        if (P1_index == joystickNum)
        {
            Debug.LogError("Crack " + 4 + ", se te desconecto el control " + controlName);
            P4desconeccion = true;
            Pausar();
        }
    }
    private void OnReconect(string controlName, int joystickNum)
    {
        Debug.LogWarning("REhi " + controlName);

        if (P1desconeccion && P1_index == joystickNum)
        {
            P1desconeccion = false;
            Debug.LogError("Crack " + 1 + ", volvio");
            Reanudar();
        }
        if (P2desconeccion && P1_index == joystickNum)
        {
            P2desconeccion = false;
            Debug.LogError("Crack " + 2 + ", volvio");
            Reanudar();
        }
        if (P3desconeccion && P1_index == joystickNum)
        {
            P3desconeccion = false;
            Debug.LogError("Crack " + 3 + ", volvio");
            Reanudar();
        }
        if (P4desconeccion && P1_index == joystickNum)
        {
            P4desconeccion = false;
            Debug.LogError("Crack " + 4 + ", volvio");
            Reanudar();
        }
    }

    public void Pausar()
    {
        if (comenzar)
        {
            //StopAllCoroutines();
            mixer.SetFloat(_Volumen_master, -20);
            Time.timeScale = 0;
            //PlayCanvas1.enabled = false;
            //PlayCanvas2.enabled = false;
            PausaCanvas.enabled = true;
            depth.focusDistance.value = 0.1f;
            //StartBlack.color = new Color32(0, 0, 0, 128);
            comenzar = false;
            pausado = true;
        }
    }

    public void Reanudar()
    {
        countPausa = 0;
        mixer.SetFloat(_Volumen_master, 0);
        Time.timeScale = 1;
        PlayCanvas.enabled = true;
        //PlayCanvas2.enabled = true;
        PausaCanvas.enabled = false;
        depth.focusDistance.value = 200;
        //StartBlack.color = new Color32(0, 0, 0, 0);
        comenzar = true;
        pausado = false;
    }

    public void VolverMenu()
    {
        Time.timeScale = 1;
        Initiate.Fade(_Seleccion, Color.black, 1);
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
        StartCanvas.gameObject.SetActive(false);
    }
    public IEnumerator ChangeSpeed2(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed2 = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
            StartCanvasGroup.alpha = speed2;
        }
        speed2 = v_end;
        StartCanvasGroup.alpha = speed2;
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

    public void FinalizarJuego()
    {
        //PlayCanvas.enabled = false;
        time_1.SetActive(false);
        time_2.SetActive(false);
        FinalCanvas.enabled = true;
        depth.active = false;
        groupc.m_ScreenY = 0.425f;

        //camGroup.m_Targets = new CinemachineTargetGroup.Target[1];

        switch (vaGanando)
        {
            case 0:
                man1_1.SetActive(false);
                man1_2.SetActive(false);
                man2_1.SetActive(false);
                man2_2.SetActive(false);
                man3_1.SetActive(false);
                man3_2.SetActive(false);
                man4_1.SetActive(false);
                man4_2.SetActive(false);

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
                break;

            case 1:
                //man1_1.SetActive(false);
                //man1_2.SetActive(false);
                man2_1.SetActive(false);
                man2_2.SetActive(false);
                man3_1.SetActive(false);
                man3_2.SetActive(false);
                man4_1.SetActive(false);
                man4_2.SetActive(false);

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
                break;

            case 2:
                man1_1.SetActive(false);
                man1_2.SetActive(false);
                //man2_1.SetActive(false);
                //man2_2.SetActive(false);
                man3_1.SetActive(false);
                man3_2.SetActive(false);
                man4_1.SetActive(false);
                man4_2.SetActive(false);

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
                break;

            case 3:
                man1_1.SetActive(false);
                man1_2.SetActive(false);
                man2_1.SetActive(false);
                man2_2.SetActive(false);
                //man3_1.SetActive(false);
                //man3_2.SetActive(false);
                man4_1.SetActive(false);
                man4_2.SetActive(false);

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
                break;

            case 4:
                man1_1.SetActive(false);
                man1_2.SetActive(false);
                man2_1.SetActive(false);
                man2_2.SetActive(false);
                man3_1.SetActive(false);
                man3_2.SetActive(false);
                //man4_1.SetActive(false);
                //man4_2.SetActive(false);

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
                break;

            case 5:
                //man1_1.SetActive(false);
                //man1_2.SetActive(false);
                //man2_1.SetActive(false);
                //man2_2.SetActive(false);
                man3_1.SetActive(false);
                man3_2.SetActive(false);
                man4_1.SetActive(false);
                man4_2.SetActive(false);

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
                break;

            case 6:
                man1_1.SetActive(false);
                man1_2.SetActive(false);
                //man2_1.SetActive(false);
                //man2_2.SetActive(false);
                //man3_1.SetActive(false);
                //man3_2.SetActive(false);
                man4_1.SetActive(false);
                man4_2.SetActive(false);

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
                break;

            case 7:
                man1_1.SetActive(false);
                man1_2.SetActive(false);
                man2_1.SetActive(false);
                man2_2.SetActive(false);
                //man3_1.SetActive(false);
                //man3_2.SetActive(false);
                //man4_1.SetActive(false);
                //man4_2.SetActive(false);

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
                break;

            case 8:
                //man1_1.SetActive(false);
                //man1_2.SetActive(false);
                man2_1.SetActive(false);
                man2_2.SetActive(false);
                man3_1.SetActive(false);
                man3_2.SetActive(false);
                //man4_1.SetActive(false);
                //man4_2.SetActive(false);

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
                break;

            case 9:
                //man1_1.SetActive(false);
                //man1_2.SetActive(false);
                man2_1.SetActive(false);
                man2_2.SetActive(false);
                //man3_1.SetActive(false);
                //man3_2.SetActive(false);
                man4_1.SetActive(false);
                man4_2.SetActive(false);

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
                break;

            case 10:
                man1_1.SetActive(false);
                man1_2.SetActive(false);
                //man2_1.SetActive(false);
                //man2_2.SetActive(false);
                man3_1.SetActive(false);
                man3_2.SetActive(false);
                //man4_1.SetActive(false);
                //man4_2.SetActive(false);

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
                break;

            case 11:
                //man1_1.SetActive(false);
                //man1_2.SetActive(false);
                //man2_1.SetActive(false);
                //man2_2.SetActive(false);
                //man3_1.SetActive(false);
                //man3_2.SetActive(false);
                man4_1.SetActive(false);
                man4_2.SetActive(false);

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
                break;

            case 12:
                //man1_1.SetActive(false);
                //man1_2.SetActive(false);
                //man2_1.SetActive(false);
                //man2_2.SetActive(false);
                man3_1.SetActive(false);
                man3_2.SetActive(false);
                //man4_1.SetActive(false);
                //man4_2.SetActive(false);

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
                break;

            case 13:
                //man1_1.SetActive(false);
                //man1_2.SetActive(false);
                man2_1.SetActive(false);
                man2_2.SetActive(false);
                //man3_1.SetActive(false);
                //man3_2.SetActive(false);
                //man4_1.SetActive(false);
                //man4_2.SetActive(false);

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
                break;

            case 14:
                man1_1.SetActive(false);
                man1_2.SetActive(false);
                //man2_1.SetActive(false);
                //man2_2.SetActive(false);
                //man3_1.SetActive(false);
                //man3_2.SetActive(false);
                //man4_1.SetActive(false);
                //man4_2.SetActive(false);

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
                break;

            default:
                break;
        }
    }

    public void Salir()
    {
        Application.Quit();
    }

    #endregion
}