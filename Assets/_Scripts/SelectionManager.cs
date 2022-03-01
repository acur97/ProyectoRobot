using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public string[] controlesList;

    [Space]
    public GameObject scena;

    [Serializable]
    public class Ops1
    {
        public string nombre;
        public Color32 colorUI;
        [ColorUsage(false)]
        public Color colorAlbedo;
        [ColorUsage(false, true)]
        public Color colorEmission;
        [ColorUsage(true)]
        public Color Respaw;
        [ColorUsage(true)]
        public Color MiraPiso;
        [ColorUsage(true, true)]
        public Color Bala;
    }

    [Serializable]
    public class Ops2_prefabs
    {
        public string nombre;
        public GameObject prefab;
    }

    [Serializable]
    public class Ops2
    {
        public string nombre;
        public Material matModelos;
        public Ops2_prefabs[] prefabs;
    }

    [Header("Seleccionables")]
    public Ops1[] opciones1;
    public Ops2[] opciones2;

    [Space]
    public GameObject P1_canvasSelect;
    public GameObject P1_canvasIniciado;
    public Text P1_nombreControl;
    public GameObject P1_selectModeloImg;
    public Text P1_selectModeloTxt;
    public GameObject P1_selectColorImg;
    public Text P1_selectColorTxt;
    public GameObject P1_select;
    public GameObject P1_ready;

    [Space]
    public GameObject P2_canvasSelect;
    public GameObject P2_canvasIniciado;
    public Text P2_nombreControl;
    public GameObject P2_selectModeloImg;
    public Text P2_selectModeloTxt;
    public GameObject P2_selectColorImg;
    public Text P2_selectColorTxt;
    public GameObject P2_select;
    public GameObject P2_ready;

    [Space]
    public GameObject P3_canvasSelect;
    public GameObject P3_canvasIniciado;
    public Text P3_nombreControl;
    public GameObject P3_selectModeloImg;
    public Text P3_selectModeloTxt;
    public GameObject P3_selectColorImg;
    public Text P3_selectColorTxt;
    public GameObject P3_select;
    public GameObject P3_ready;

    [Space]
    public GameObject P4_canvasSelect;
    public GameObject P4_canvasIniciado;
    public Text P4_nombreControl;
    public GameObject P4_selectModeloImg;
    public Text P4_selectModeloTxt;
    public GameObject P4_selectColorImg;
    public Text P4_selectColorTxt;
    public GameObject P4_select;
    public GameObject P4_ready;

    //[Space]
    //public RenderTexture rTexture1;
    //public RenderTexture rTexture2;
    //public RenderTexture rTexture3;
    //public RenderTexture rTexture4;

    private int contadorNcontrol = 1;

    [Header("test")]
    public bool lleno;

    private int controlesP1;
    private bool encontradoP1;
    private int selecP1 = 1;
    private float P1_delay = 0.5f;
    private int P1_cont1;
    private int P1_cont2;
    private bool readyP1;

    private int controlesP2;
    private bool encontradoP2;
    private int selecP2 = 1;
    private float P2_delay = 0.5f;
    private int P2_cont1;
    private int P2_cont2;
    private bool readyP2;

    private int controlesP3;
    private bool encontradoP3;
    private int selecP3 = 1;
    private float P3_delay = 0.5f;
    private int P3_cont1;
    private int P3_cont2;
    private bool readyP3;

    private int controlesP4;
    private bool encontradoP4;
    private int selecP4 = 1;
    private float P4_delay = 0.5f;
    private int P4_cont1;
    private int P4_cont2;
    private bool readyP4;

    private int excepcion1;
    private int excepcion2;
    private int excepcion3;

    public float countWait;
    public GameObject readyStatus;

    private AsyncOperation sceneAsync;

    private const string _J = "J";
    private const string _F = "_F";
    private const string _H = "_H";
    private const string _V = "_V";
    private const string _P = "_P";
    private const string _Pause = "Pause";
    private readonly int _BaseColor = Shader.PropertyToID("_BaseColor");
    private readonly int _Color = Shader.PropertyToID("_Color");
    private readonly int _EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        //int valor = Screen.height;
        //int valor2 = (int)(valor * 0.89f);

        //rTexture1.width = valor2;
        //rTexture1.height = valor;
        //rTexture2.width = valor2;
        //rTexture2.height = valor;
        //rTexture3.width = valor2;
        //rTexture3.height = valor;
        //rTexture4.width = valor2;
        //rTexture4.height = valor;

        P1_canvasSelect.SetActive(true);
        P1_canvasIniciado.SetActive(false);
        P1_selectModeloImg.SetActive(true);
        P1_selectColorImg.SetActive(false);
        P1_select.SetActive(true);
        P1_ready.SetActive(false);

        P2_canvasSelect.SetActive(true);
        P2_canvasIniciado.SetActive(false);
        P2_selectModeloImg.SetActive(true);
        P2_selectColorImg.SetActive(false);
        P2_select.SetActive(true);
        P2_ready.SetActive(false);

        P3_canvasSelect.SetActive(true);
        P3_canvasIniciado.SetActive(false);
        P3_selectModeloImg.SetActive(true);
        P3_selectColorImg.SetActive(false);
        P3_select.SetActive(true);
        P3_ready.SetActive(false);

        P4_canvasSelect.SetActive(true);
        P4_canvasIniciado.SetActive(false);
        P4_selectModeloImg.SetActive(true);
        P4_selectColorImg.SetActive(false);
        P4_select.SetActive(true);
        P4_ready.SetActive(false);
    }

    IEnumerator LoadSceneMove(int sceneIndex)
    {
        opciones2[0].prefabs[P1_cont1].prefab.transform.SetParent(null);
        opciones2[1].prefabs[P2_cont1].prefab.transform.SetParent(null);
        opciones2[2].prefabs[P3_cont1].prefab.transform.SetParent(null);
        opciones2[3].prefabs[P4_cont1].prefab.transform.SetParent(null);

        //opciones2[0].prefabs[P1_cont1].prefab.name = "Robot1";
        //opciones2[1].prefabs[P2_cont1].prefab.name = "Robot2";
        //opciones2[2].prefabs[P3_cont1].prefab.name = "Robot3";
        //opciones2[3].prefabs[P4_cont1].prefab.name = "Robot4";
        //opciones2[0].prefabs[1].prefab.transform.SetParent(null);

        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        //AsyncOperation scene = SceneManager.LoadSceneAsync(sceneIndex);
        scene.allowSceneActivation = false;
        sceneAsync = scene;

        while (scene.progress < 0.9f)
        {
            Debug.Log("Cargando: " + scene.progress);
            yield return null;
        }
        //Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(sceneIndex);
        //SceneManager.MoveGameObjectToScene(opciones2[0].prefabs[0].prefab, sceneToLoad);
        OnFinishedLoad();
    }

    private void EnableScene(int sceneIndex)
    {
        //sceneAsync.allowSceneActivation = true;

        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(sceneIndex);
        if (sceneToLoad.IsValid())
        {
            Debug.Log("Valido");

            if (readyP1)
            {
                SceneManager.MoveGameObjectToScene(opciones2[0].prefabs[P1_cont1].prefab, sceneToLoad);
                ColoresPass coloresPas = opciones2[0].prefabs[P1_cont1].prefab.GetComponent<ColoresPass>();
                coloresPas.RespawColor = opciones1[P1_cont2].Respaw;
                coloresPas.MiraPisoColor = opciones1[P1_cont2].MiraPiso;
                coloresPas.BalaColor = opciones1[P1_cont2].Bala;
                coloresPas.colorUI = opciones1[P1_cont2].colorUI;
            }
            if (readyP2)
            {
                SceneManager.MoveGameObjectToScene(opciones2[1].prefabs[P2_cont1].prefab, sceneToLoad);
                ColoresPass coloresPas = opciones2[1].prefabs[P2_cont1].prefab.GetComponent<ColoresPass>();
                coloresPas.RespawColor = opciones1[P2_cont2].Respaw;
                coloresPas.MiraPisoColor = opciones1[P2_cont2].MiraPiso;
                coloresPas.BalaColor = opciones1[P2_cont2].Bala;
                coloresPas.colorUI = opciones1[P2_cont2].colorUI;
            }
            if (readyP3)
            {
                SceneManager.MoveGameObjectToScene(opciones2[2].prefabs[P3_cont1].prefab, sceneToLoad);
                ColoresPass coloresPas = opciones2[2].prefabs[P3_cont1].prefab.GetComponent<ColoresPass>();
                coloresPas.RespawColor = opciones1[P3_cont2].Respaw;
                coloresPas.MiraPisoColor = opciones1[P3_cont2].MiraPiso;
                coloresPas.BalaColor = opciones1[P3_cont2].Bala;
                coloresPas.colorUI = opciones1[P3_cont2].colorUI;
            }
            if (readyP4)
            {
                SceneManager.MoveGameObjectToScene(opciones2[3].prefabs[P4_cont1].prefab, sceneToLoad);
                ColoresPass coloresPas = opciones2[3].prefabs[P4_cont1].prefab.GetComponent<ColoresPass>();
                coloresPas.RespawColor = opciones1[P4_cont2].Respaw;
                coloresPas.MiraPisoColor = opciones1[P4_cont2].MiraPiso;
                coloresPas.BalaColor = opciones1[P4_cont2].Bala;
                coloresPas.colorUI = opciones1[P4_cont2].colorUI;
            }

            sceneAsync.allowSceneActivation = true;
        }
    }

    private void OnFinishedLoad()
    {
        Debug.Log("Escena cargada");
        EnableScene(1);
        Debug.Log("Escena activada");
        scena.SetActive(false);
    }

    private void Update()
    {
        controlesList = Input.GetJoystickNames();

        //Input.GetButtonDown()

        //Debug.LogWarning("Pasar color de material dependiendo del jugador al material de Respaw InGameController y en awake poner ese .setcolor del .material");
        //Debug.LogWarning("Pasar color de bala, Opcional, o que sea diferente prefab, aunque prefiero un solo color de bala");
        //Debug.LogWarning("Pasar color de Mira piso");
        //Debug.LogWarning("Bugazo con mascaras de animaciones y rotaciones de caminar");

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    SceneManager.LoadScene(1);
        //}
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            StartCoroutine(LoadSceneMove(1));
        }

        #region Busqueda controles

        if (!lleno)
        {
            countWait -= 0.05f;
            if (contadorNcontrol == 1)
            {
                for (int i = 0; i < 16; i++)
                {
                    //controlesList = Input.GetJoystickNames();
                    Debug.Log("Buscando jugador 1");
                    if (Input.GetButtonDown(_J + (i + 1) + _F))
                    {
                        Debug.Log("jugador 1 encontrado");
                        P1_canvasSelect.SetActive(false);
                        P1_canvasIniciado.SetActive(true);
                        P1_selectModeloTxt.text = opciones2[0].prefabs[P1_cont1].nombre;
                        opciones2[0].prefabs[0].prefab.SetActive(true);
                        opciones2[0].prefabs[1].prefab.SetActive(false);
                        opciones2[0].prefabs[2].prefab.SetActive(false);
                        opciones2[0].prefabs[3].prefab.SetActive(false);

                        opciones2[0].matModelos.SetColor(_Color, opciones1[P1_cont2].colorAlbedo);
                        opciones2[0].matModelos.SetColor(_EmissionColor, opciones1[P1_cont2].colorEmission);

                        P1_selectColorTxt.text = opciones1[P1_cont2].nombre;
                        excepcion1 = i + 1;

                        controlesP1 = excepcion1;
                        InGameController.P1_H = _J + controlesP1 + _H;
                        InGameController.P1_V = _J + controlesP1 + _V;
                        InGameController.P1_F = _J + controlesP1 + _F;
                        InGameController.P1_P = _J + controlesP1 + _P;

                        P1_nombreControl.text = controlesList[i].ToString();
                        encontradoP1 = true;
                        contadorNcontrol += 1;
                        countWait = 1;
                        break;
                    }
                }
            }
            if (contadorNcontrol == 2 && countWait < 0)
            {
                for (int i = 0; i <= 15; i++)
                {
                    controlesList = Input.GetJoystickNames();
                    Debug.Log("Buscando jugador 2");
                    if (Input.GetButtonDown(_J + excepcion1 + _F))
                    {
                        Debug.Log("salto");
                        continue;
                    }
                    if (Input.GetButtonDown(_J + (i + 1) + _F))
                    {
                        Debug.Log("jugador 2 encontrado");
                        P2_canvasSelect.SetActive(false);
                        P2_canvasIniciado.SetActive(true);
                        P2_selectModeloTxt.text = opciones2[1].prefabs[P2_cont1].nombre;
                        opciones2[1].prefabs[0].prefab.SetActive(true);
                        opciones2[1].prefabs[1].prefab.SetActive(false);
                        opciones2[1].prefabs[2].prefab.SetActive(false);
                        opciones2[1].prefabs[3].prefab.SetActive(false);

                        opciones2[1].matModelos.SetColor(_Color, opciones1[P2_cont2].colorAlbedo);
                        opciones2[1].matModelos.SetColor(_EmissionColor, opciones1[P2_cont2].colorEmission);

                        P2_selectColorTxt.text = opciones1[P2_cont2].nombre;
                        excepcion2 = i + 1;

                        controlesP2 = excepcion2;
                        InGameController.P2_H = _J + controlesP2 + _H;
                        InGameController.P2_V = _J + controlesP2 + _V;
                        InGameController.P2_F = _J + controlesP2 + _F;
                        InGameController.P2_P = _J + controlesP2 + _P;

                        P2_nombreControl.text = controlesList[i].ToString();
                        encontradoP2 = true;
                        contadorNcontrol += 1;
                        countWait = 1;
                        break;
                    }
                }
            }
            if (contadorNcontrol == 3 && countWait < 0)
            {
                for (int i = 0; i <= 15; i++)
                {
                    controlesList = Input.GetJoystickNames();
                    Debug.Log("Buscando jugador 3");
                    if (Input.GetButtonDown(_J + excepcion1 + _F))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown(_J + excepcion2 + _F))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown(_J + (i + 1) + _F))
                    {
                        Debug.Log("jugador 3 encontrado");
                        P3_canvasSelect.SetActive(false);
                        P3_canvasIniciado.SetActive(true);
                        P3_selectModeloTxt.text = opciones2[2].prefabs[P3_cont1].nombre;
                        opciones2[2].prefabs[0].prefab.SetActive(true);
                        opciones2[2].prefabs[1].prefab.SetActive(false);
                        opciones2[2].prefabs[2].prefab.SetActive(false);
                        opciones2[2].prefabs[3].prefab.SetActive(false);

                        opciones2[2].matModelos.SetColor(_Color, opciones1[P3_cont2].colorAlbedo);
                        opciones2[2].matModelos.SetColor(_EmissionColor, opciones1[P3_cont2].colorEmission);

                        P3_selectColorTxt.text = opciones1[P3_cont2].nombre;
                        excepcion3 = i + 1;

                        controlesP3 = excepcion3;
                        InGameController.P3_H = _J + controlesP3 + _H;
                        InGameController.P3_V = _J + controlesP3 + _V;
                        InGameController.P3_F = _J + controlesP3 + _F;
                        InGameController.P3_P = _J + controlesP3 + _P;

                        P3_nombreControl.text = controlesList[i].ToString();
                        encontradoP3 = true;
                        contadorNcontrol += 1;
                        countWait = 1;
                        break;
                    }
                }
            }
            if (contadorNcontrol == 4 && countWait < 0)
            {
                for (int i = 0; i <= 15; i++)
                {
                    controlesList = Input.GetJoystickNames();
                    Debug.Log("Buscando jugador 4");
                    if (Input.GetButtonDown(_J + excepcion1 + _F))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown(_J + excepcion2 + _F))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown(_J + excepcion3 + _F))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown(_J + (i + 1) + _F))
                    {
                        Debug.Log("jugador 4 encontrado");
                        P4_canvasSelect.SetActive(false);
                        P4_canvasIniciado.SetActive(true);
                        P4_selectModeloTxt.text = opciones2[3].prefabs[P4_cont1].nombre;
                        opciones2[3].prefabs[0].prefab.SetActive(true);
                        opciones2[3].prefabs[1].prefab.SetActive(false);
                        opciones2[3].prefabs[2].prefab.SetActive(false);
                        opciones2[3].prefabs[3].prefab.SetActive(false);

                        opciones2[3].matModelos.SetColor(_Color, opciones1[P4_cont2].colorAlbedo);
                        opciones2[3].matModelos.SetColor(_EmissionColor, opciones1[P4_cont2].colorEmission);

                        P4_selectColorTxt.text = opciones1[P4_cont2].nombre;

                        controlesP4 = (Array.IndexOf(controlesList, controlesList[i]) + 1);
                        InGameController.P4_H = _J + controlesP4 + _H;
                        InGameController.P4_V = _J + controlesP4 + _V;
                        InGameController.P4_F = _J + controlesP4 + _F;
                        InGameController.P4_P = _J + controlesP4 + _P;

                        P4_nombreControl.text = controlesList[i].ToString();
                        encontradoP4 = true;
                        contadorNcontrol += 1;
                        break;
                    }
                }
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (contadorNcontrol == 5)
        {
            lleno = true;
        }

        #region Seleccion modelo y color

        if (encontradoP1)
        {
            if (!readyP1)
            {
                P1_delay -= (Time.deltaTime * 2);
                //Debug.Log(Input.GetAxisRaw("J" + controlesP1 + "_V"));
                if (Input.GetAxisRaw(_J + controlesP1 + _V) > 0.25f)
                {
                    P1_selectModeloImg.SetActive(true);
                    P1_selectColorImg.SetActive(false);
                    selecP1 = 1;
                }
                if (Input.GetAxisRaw(_J + controlesP1 + _V) < -0.25f)
                {
                    P1_selectModeloImg.SetActive(false);
                    P1_selectColorImg.SetActive(true);
                    selecP1 = 2;
                }

                if (Input.GetAxisRaw(_J + controlesP1 + _H) > 0.25f && P1_delay < 0.25f)
                {
                    if (selecP1 == 1)
                    {
                        P1_delay = 0.5f;
                        P1_cont1 += 1;
                        if (P1_cont1 == opciones2.Length)
                        {
                            P1_cont1 = 0;
                        }
                        P1_selectModeloTxt.text = opciones2[0].prefabs[P1_cont1].nombre;
                        if (P1_cont1 == 0)
                        {
                            opciones2[0].prefabs[0].prefab.SetActive(true);
                            opciones2[0].prefabs[1].prefab.SetActive(false);
                            opciones2[0].prefabs[2].prefab.SetActive(false);
                            opciones2[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 1)
                        {
                            opciones2[0].prefabs[0].prefab.SetActive(false);
                            opciones2[0].prefabs[1].prefab.SetActive(true);
                            opciones2[0].prefabs[2].prefab.SetActive(false);
                            opciones2[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 2)
                        {
                            opciones2[0].prefabs[0].prefab.SetActive(false);
                            opciones2[0].prefabs[1].prefab.SetActive(false);
                            opciones2[0].prefabs[2].prefab.SetActive(true);
                            opciones2[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 3)
                        {
                            opciones2[0].prefabs[0].prefab.SetActive(false);
                            opciones2[0].prefabs[1].prefab.SetActive(false);
                            opciones2[0].prefabs[2].prefab.SetActive(false);
                            opciones2[0].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P1_delay = 0.5f;
                        P1_cont2 += 1;
                        if (P1_cont2 == opciones1.Length)
                        {
                            P1_cont2 = 0;
                        }
                        P1_selectColorTxt.text = opciones1[P1_cont2].nombre;

                        opciones2[0].matModelos.SetColor(_BaseColor, opciones1[P1_cont2].colorAlbedo);
                        opciones2[0].matModelos.SetColor(_EmissionColor, opciones1[P1_cont2].colorEmission);
                    }
                }
                if (Input.GetAxisRaw(_J + controlesP1 + _H) < -0.25f && P1_delay < 0.25f)
                {
                    if (selecP1 == 1)
                    {
                        P1_delay = 0.5f;
                        P1_cont1 -= 1;
                        if (P1_cont1 == -1)
                        {
                            P1_cont1 = opciones2.Length - 1;
                        }
                        P1_selectModeloTxt.text = opciones2[0].prefabs[P1_cont1].nombre;
                        if (P1_cont1 == 0)
                        {
                            opciones2[0].prefabs[0].prefab.SetActive(true);
                            opciones2[0].prefabs[1].prefab.SetActive(false);
                            opciones2[0].prefabs[2].prefab.SetActive(false);
                            opciones2[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 1)
                        {
                            opciones2[0].prefabs[0].prefab.SetActive(false);
                            opciones2[0].prefabs[1].prefab.SetActive(true);
                            opciones2[0].prefabs[2].prefab.SetActive(false);
                            opciones2[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 2)
                        {
                            opciones2[0].prefabs[0].prefab.SetActive(false);
                            opciones2[0].prefabs[1].prefab.SetActive(false);
                            opciones2[0].prefabs[2].prefab.SetActive(true);
                            opciones2[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 3)
                        {
                            opciones2[0].prefabs[0].prefab.SetActive(false);
                            opciones2[0].prefabs[1].prefab.SetActive(false);
                            opciones2[0].prefabs[2].prefab.SetActive(false);
                            opciones2[0].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P1_delay = 0.5f;
                        P1_cont2 -= 1;
                        if (P1_cont2 == -1)
                        {
                            P1_cont2 = opciones1.Length - 1;
                        }
                        P1_selectColorTxt.text = opciones1[P1_cont2].nombre;

                        opciones2[0].matModelos.SetColor(_BaseColor, opciones1[P1_cont2].colorAlbedo);
                        opciones2[0].matModelos.SetColor(_EmissionColor, opciones1[P1_cont2].colorEmission);
                    }
                }

                if (Input.GetButtonDown(_J + controlesP1 + _F) && P1_delay < 0.25f)
                {
                    readyP1 = true;
                    Debug.Log("Player 1 Ready");
                    P1_select.SetActive(false);
                    P1_ready.SetActive(true);
                }
            }
            else
            {
                if (Input.GetButtonDown(_J + controlesP1 + _P))
                {
                    readyP1 = false;
                    Debug.Log("Player 1 NO-Ready");
                    P1_select.SetActive(true);
                    P1_ready.SetActive(false);
                }
            }
        }
        if (encontradoP2)
        {
            if (!readyP2)
            {
                P2_delay -= (Time.deltaTime * 2);
                if (Input.GetAxisRaw(_J + controlesP2 + _V) > 0.25f)
                {
                    P2_selectModeloImg.SetActive(true);
                    P2_selectColorImg.SetActive(false);
                    selecP2 = 1;
                }
                if (Input.GetAxisRaw(_J + controlesP2 + _V) < -0.25f)
                {
                    P2_selectModeloImg.SetActive(false);
                    P2_selectColorImg.SetActive(true);
                    selecP2 = 2;
                }

                if (Input.GetAxisRaw(_J + controlesP2 + _H) > 0.25f && P2_delay < 0.25f)
                {
                    if (selecP2 == 1)
                    {
                        P2_delay = 0.5f;
                        P2_cont1 += 1;
                        if (P2_cont1 == opciones2.Length)
                        {
                            P2_cont1 = 0;
                        }
                        P2_selectModeloTxt.text = opciones2[1].prefabs[P2_cont1].nombre;
                        if (P2_cont1 == 0)
                        {
                            opciones2[1].prefabs[0].prefab.SetActive(true);
                            opciones2[1].prefabs[1].prefab.SetActive(false);
                            opciones2[1].prefabs[2].prefab.SetActive(false);
                            opciones2[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 1)
                        {
                            opciones2[1].prefabs[0].prefab.SetActive(false);
                            opciones2[1].prefabs[1].prefab.SetActive(true);
                            opciones2[1].prefabs[2].prefab.SetActive(false);
                            opciones2[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 2)
                        {
                            opciones2[1].prefabs[0].prefab.SetActive(false);
                            opciones2[1].prefabs[1].prefab.SetActive(false);
                            opciones2[1].prefabs[2].prefab.SetActive(true);
                            opciones2[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 3)
                        {
                            opciones2[1].prefabs[0].prefab.SetActive(false);
                            opciones2[1].prefabs[1].prefab.SetActive(false);
                            opciones2[1].prefabs[2].prefab.SetActive(false);
                            opciones2[1].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P2_delay = 0.5f;
                        P2_cont2 += 1;
                        if (P2_cont2 == opciones1.Length)
                        {
                            P2_cont2 = 0;
                        }
                        P2_selectColorTxt.text = opciones1[P2_cont2].nombre;

                        opciones2[1].matModelos.SetColor(_BaseColor, opciones1[P2_cont2].colorAlbedo);
                        opciones2[1].matModelos.SetColor(_EmissionColor, opciones1[P2_cont2].colorEmission);
                    }
                }
                if (Input.GetAxisRaw(_J + controlesP2 + _H) < -0.25f && P2_delay < 0.25f)
                {
                    if (selecP2 == 1)
                    {
                        P2_delay = 0.5f;
                        P2_cont1 -= 1;
                        if (P2_cont1 == -1)
                        {
                            P2_cont1 = opciones2.Length - 1;
                        }
                        P2_selectModeloTxt.text = opciones2[1].prefabs[P2_cont1].nombre;
                        if (P2_cont1 == 0)
                        {
                            opciones2[1].prefabs[0].prefab.SetActive(true);
                            opciones2[1].prefabs[1].prefab.SetActive(false);
                            opciones2[1].prefabs[2].prefab.SetActive(false);
                            opciones2[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 1)
                        {
                            opciones2[1].prefabs[0].prefab.SetActive(false);
                            opciones2[1].prefabs[1].prefab.SetActive(true);
                            opciones2[1].prefabs[2].prefab.SetActive(false);
                            opciones2[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 2)
                        {
                            opciones2[1].prefabs[0].prefab.SetActive(false);
                            opciones2[1].prefabs[1].prefab.SetActive(false);
                            opciones2[1].prefabs[2].prefab.SetActive(true);
                            opciones2[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 3)
                        {
                            opciones2[1].prefabs[0].prefab.SetActive(false);
                            opciones2[1].prefabs[1].prefab.SetActive(false);
                            opciones2[1].prefabs[2].prefab.SetActive(false);
                            opciones2[1].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P2_delay = 0.5f;
                        P2_cont2 -= 1;
                        if (P2_cont2 == -1)
                        {
                            P2_cont2 = opciones1.Length - 1;
                        }
                        P2_selectColorTxt.text = opciones1[P2_cont2].nombre;

                        opciones2[1].matModelos.SetColor(_BaseColor, opciones1[P2_cont2].colorAlbedo);
                        opciones2[1].matModelos.SetColor(_EmissionColor, opciones1[P2_cont2].colorEmission);
                    }
                }

                if (Input.GetButtonDown(_J + controlesP2 + _F) && P2_delay < 0.25f)
                {
                    readyP2 = true;
                    Debug.Log("Player 2 Ready");
                    P2_select.SetActive(false);
                    P2_ready.SetActive(true);
                }
            }
            else
            {
                if (Input.GetButtonDown(_J + controlesP2 + _P))
                {
                    readyP2 = false;
                    Debug.Log("Player 2 NO-Ready");
                    P2_select.SetActive(true);
                    P2_ready.SetActive(false);
                }
            }
        }
        if (encontradoP3)
        {
            if (!readyP3)
            {
                P3_delay -= (Time.deltaTime * 2);
                if (Input.GetAxisRaw(_J + controlesP3 + _V) > 0.25f)
                {
                    P3_selectModeloImg.SetActive(true);
                    P3_selectColorImg.SetActive(false);
                    selecP3 = 1;
                }
                if (Input.GetAxisRaw(_J + controlesP3 + _V) < -0.25f)
                {
                    P3_selectModeloImg.SetActive(false);
                    P3_selectColorImg.SetActive(true);
                    selecP3 = 2;
                }

                if (Input.GetAxisRaw(_J + controlesP3 + _H) > 0.25f && P3_delay < 0.25f)
                {
                    if (selecP3 == 1)
                    {
                        P3_delay = 0.5f;
                        P3_cont1 += 1;
                        if (P3_cont1 == opciones2.Length)
                        {
                            P3_cont1 = 0;
                        }
                        P3_selectModeloTxt.text = opciones2[2].prefabs[P3_cont1].nombre;
                        if (P3_cont1 == 0)
                        {
                            opciones2[2].prefabs[0].prefab.SetActive(true);
                            opciones2[2].prefabs[1].prefab.SetActive(false);
                            opciones2[2].prefabs[2].prefab.SetActive(false);
                            opciones2[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 1)
                        {
                            opciones2[2].prefabs[0].prefab.SetActive(false);
                            opciones2[2].prefabs[1].prefab.SetActive(true);
                            opciones2[2].prefabs[2].prefab.SetActive(false);
                            opciones2[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 2)
                        {
                            opciones2[2].prefabs[0].prefab.SetActive(false);
                            opciones2[2].prefabs[1].prefab.SetActive(false);
                            opciones2[2].prefabs[2].prefab.SetActive(true);
                            opciones2[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 3)
                        {
                            opciones2[2].prefabs[0].prefab.SetActive(false);
                            opciones2[2].prefabs[1].prefab.SetActive(false);
                            opciones2[2].prefabs[2].prefab.SetActive(false);
                            opciones2[2].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P3_delay = 0.5f;
                        P3_cont2 += 1;
                        if (P3_cont2 == opciones1.Length)
                        {
                            P3_cont2 = 0;
                        }
                        P3_selectColorTxt.text = opciones1[P3_cont2].nombre;

                        opciones2[2].matModelos.SetColor(_BaseColor, opciones1[P3_cont2].colorAlbedo);
                        opciones2[2].matModelos.SetColor(_EmissionColor, opciones1[P3_cont2].colorEmission);
                    }
                }
                if (Input.GetAxisRaw(_J + controlesP3 + _H) < -0.25f && P3_delay < 0.25f)
                {
                    if (selecP3 == 1)
                    {
                        P3_delay = 0.5f;
                        P3_cont1 -= 1;
                        if (P3_cont1 == -1)
                        {
                            P3_cont1 = opciones2.Length - 1;
                        }
                        P3_selectModeloTxt.text = opciones2[2].prefabs[P3_cont1].nombre;
                        if (P3_cont1 == 0)
                        {
                            opciones2[2].prefabs[0].prefab.SetActive(true);
                            opciones2[2].prefabs[1].prefab.SetActive(false);
                            opciones2[2].prefabs[2].prefab.SetActive(false);
                            opciones2[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 1)
                        {
                            opciones2[2].prefabs[0].prefab.SetActive(false);
                            opciones2[2].prefabs[1].prefab.SetActive(true);
                            opciones2[2].prefabs[2].prefab.SetActive(false);
                            opciones2[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 2)
                        {
                            opciones2[2].prefabs[0].prefab.SetActive(false);
                            opciones2[2].prefabs[1].prefab.SetActive(false);
                            opciones2[2].prefabs[2].prefab.SetActive(true);
                            opciones2[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 3)
                        {
                            opciones2[2].prefabs[0].prefab.SetActive(false);
                            opciones2[2].prefabs[1].prefab.SetActive(false);
                            opciones2[2].prefabs[2].prefab.SetActive(false);
                            opciones2[2].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P3_delay = 0.5f;
                        P3_cont2 -= 1;
                        if (P3_cont2 == -1)
                        {
                            P3_cont2 = opciones1.Length - 1;
                        }
                        P3_selectColorTxt.text = opciones1[P3_cont2].nombre;

                        opciones2[2].matModelos.SetColor(_BaseColor, opciones1[P3_cont2].colorAlbedo);
                        opciones2[2].matModelos.SetColor(_EmissionColor, opciones1[P3_cont2].colorEmission);
                    }
                }

                if (Input.GetButtonDown(_J + controlesP3 + _F) && P3_delay < 0.25f)
                {
                    readyP3 = true;
                    Debug.Log("Player 3 Ready");
                    P3_select.SetActive(false);
                    P3_ready.SetActive(true);
                }
            }
            else
            {
                if (Input.GetButtonDown(_J + controlesP3 + _P))
                {
                    readyP3 = false;
                    Debug.Log("Player 3 NO-Ready");
                    P3_select.SetActive(true);
                    P3_ready.SetActive(false);
                }
            }
        }
        if (encontradoP4)
        {
            if (!readyP4)
            {
                P4_delay -= (Time.deltaTime * 2);
                if (Input.GetAxisRaw(_J + controlesP4 + _V) > 0.25f)
                {
                    P4_selectModeloImg.SetActive(true);
                    P4_selectColorImg.SetActive(false);
                    selecP4 = 1;
                }
                if (Input.GetAxisRaw(_J + controlesP4 + _V) < -0.25f)
                {
                    P4_selectModeloImg.SetActive(false);
                    P4_selectColorImg.SetActive(true);
                    selecP4 = 2;
                }

                if (Input.GetAxisRaw(_J + controlesP4 + _H) > 0.25f && P4_delay < 0.25f)
                {
                    if (selecP4 == 1)
                    {
                        P4_delay = 0.5f;
                        P4_cont1 += 1;
                        if (P4_cont1 == opciones2.Length)
                        {
                            P4_cont1 = 0;
                        }
                        P4_selectModeloTxt.text = opciones2[3].prefabs[P4_cont1].nombre;
                        if (P4_cont1 == 0)
                        {
                            opciones2[3].prefabs[0].prefab.SetActive(true);
                            opciones2[3].prefabs[1].prefab.SetActive(false);
                            opciones2[3].prefabs[2].prefab.SetActive(false);
                            opciones2[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 1)
                        {
                            opciones2[3].prefabs[0].prefab.SetActive(false);
                            opciones2[3].prefabs[1].prefab.SetActive(true);
                            opciones2[3].prefabs[2].prefab.SetActive(false);
                            opciones2[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 2)
                        {
                            opciones2[3].prefabs[0].prefab.SetActive(false);
                            opciones2[3].prefabs[1].prefab.SetActive(false);
                            opciones2[3].prefabs[2].prefab.SetActive(true);
                            opciones2[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 3)
                        {
                            opciones2[3].prefabs[0].prefab.SetActive(false);
                            opciones2[3].prefabs[1].prefab.SetActive(false);
                            opciones2[3].prefabs[2].prefab.SetActive(false);
                            opciones2[3].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P4_delay = 0.5f;
                        P4_cont2 += 1;
                        if (P4_cont2 == opciones1.Length)
                        {
                            P4_cont2 = 0;
                        }
                        P4_selectColorTxt.text = opciones1[P4_cont2].nombre;

                        opciones2[3].matModelos.SetColor(_BaseColor, opciones1[P4_cont2].colorAlbedo);
                        opciones2[3].matModelos.SetColor(_EmissionColor, opciones1[P4_cont2].colorEmission);
                    }
                }
                if (Input.GetAxisRaw(_J + controlesP4 + _H) < -0.25f && P4_delay < 0.25f)
                {
                    if (selecP4 == 1)
                    {
                        P4_delay = 0.5f;
                        P4_cont1 -= 1;
                        if (P4_cont1 == -1)
                        {
                            P4_cont1 = opciones2.Length - 1;
                        }
                        P4_selectModeloTxt.text = opciones2[3].prefabs[P4_cont1].nombre;
                        if (P4_cont1 == 0)
                        {
                            opciones2[3].prefabs[0].prefab.SetActive(true);
                            opciones2[3].prefabs[1].prefab.SetActive(false);
                            opciones2[3].prefabs[2].prefab.SetActive(false);
                            opciones2[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 1)
                        {
                            opciones2[3].prefabs[0].prefab.SetActive(false);
                            opciones2[3].prefabs[1].prefab.SetActive(true);
                            opciones2[3].prefabs[2].prefab.SetActive(false);
                            opciones2[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 2)
                        {
                            opciones2[3].prefabs[0].prefab.SetActive(false);
                            opciones2[3].prefabs[1].prefab.SetActive(false);
                            opciones2[3].prefabs[2].prefab.SetActive(true);
                            opciones2[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 3)
                        {
                            opciones2[3].prefabs[0].prefab.SetActive(false);
                            opciones2[3].prefabs[1].prefab.SetActive(false);
                            opciones2[3].prefabs[2].prefab.SetActive(false);
                            opciones2[3].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P4_delay = 0.5f;
                        P4_cont2 -= 1;
                        if (P4_cont2 == -1)
                        {
                            P4_cont2 = opciones1.Length - 1;
                        }
                        P4_selectColorTxt.text = opciones1[P4_cont2].nombre;

                        opciones2[3].matModelos.SetColor(_BaseColor, opciones1[P4_cont2].colorAlbedo);
                        opciones2[3].matModelos.SetColor(_EmissionColor, opciones1[P4_cont2].colorEmission);
                    }
                }

                if (Input.GetButtonDown(_J + controlesP4 + _F) && P3_delay < 0.25f)
                {
                    readyP4 = true;
                    Debug.Log("Player 4 Ready");
                    P4_select.SetActive(false);
                    P4_ready.SetActive(true);
                }
            }
            else
            {
                if (Input.GetButtonDown(_J + controlesP4 + _P))
                {
                    readyP4 = false;
                    Debug.Log("Player 4 NO-Ready");
                    P4_select.SetActive(true);
                    P4_ready.SetActive(false);
                }
            }
        }

        #endregion

        if (readyP1 && readyP2)
        {
            if (!readyStatus.activeSelf)
            {
                readyStatus.SetActive(true);
            }
            if (Input.GetButtonDown(_Pause))
            {
                StartCoroutine(LoadSceneMove(1));
            }
        }
        else
        {
            if (readyStatus.activeSelf)
            {
                readyStatus.SetActive(false);
            }
        }
    }
}