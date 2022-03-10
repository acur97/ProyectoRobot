using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject scena;
    [SerializeField] private GameObject loading;

    [Header("Seleccionables")]
    [SerializeField] private PlayerCustoms playerColors;
    [Serializable]
    public class Ops_prefabs
    {
        public string nombreRobot;
        public GameObject prefab;
    }
    [Serializable]
    public class Ops
    {
        public string nombreJugador;
        public Material matModelos;
        public Ops_prefabs[] prefabs;
    }
    [SerializeField] private Ops[] playerOps;

    [Header("P1")]
    [SerializeField] private Text P1_playerName;
    [SerializeField] private GameObject P1_canvasSelect;
    [SerializeField] private GameObject P1_canvasIniciado;
    [SerializeField] private Text P1_nombreControl;
    [SerializeField] private GameObject P1_selectModeloImg;
    [SerializeField] private Text P1_selectModeloTxt;
    [SerializeField] private GameObject P1_selectColorImg;
    [SerializeField] private Text P1_selectColorTxt;
    [SerializeField] private GameObject P1_select;
    [SerializeField] private GameObject P1_selectDisparo;
    [SerializeField] private GameObject P1_selectPoder;
    [SerializeField] private GameObject P1_ready;

    [Header("P2")]
    [SerializeField] private Text P2_playerName;
    [SerializeField] private GameObject P2_canvasSelect;
    [SerializeField] private GameObject P2_canvasIniciado;
    [SerializeField] private Text P2_nombreControl;
    [SerializeField] private GameObject P2_selectModeloImg;
    [SerializeField] private Text P2_selectModeloTxt;
    [SerializeField] private GameObject P2_selectColorImg;
    [SerializeField] private Text P2_selectColorTxt;
    [SerializeField] private GameObject P2_select;
    [SerializeField] private GameObject P2_selectDisparo;
    [SerializeField] private GameObject P2_selectPoder;
    [SerializeField] private GameObject P2_ready;

    [Header("P3")]
    [SerializeField] private Text P3_playerName;
    [SerializeField] private GameObject P3_canvasSelect;
    [SerializeField] private GameObject P3_canvasIniciado;
    [SerializeField] private Text P3_nombreControl;
    [SerializeField] private GameObject P3_selectModeloImg;
    [SerializeField] private Text P3_selectModeloTxt;
    [SerializeField] private GameObject P3_selectColorImg;
    [SerializeField] private Text P3_selectColorTxt;
    [SerializeField] private GameObject P3_select;
    [SerializeField] private GameObject P3_selectDisparo;
    [SerializeField] private GameObject P3_selectPoder;
    [SerializeField] private GameObject P3_ready;

    [Header("P4")]
    [SerializeField] private Text P4_playerName;
    [SerializeField] private GameObject P4_canvasSelect;
    [SerializeField] private GameObject P4_canvasIniciado;
    [SerializeField] private Text P4_nombreControl;
    [SerializeField] private GameObject P4_selectModeloImg;
    [SerializeField] private Text P4_selectModeloTxt;
    [SerializeField] private GameObject P4_selectColorImg;
    [SerializeField] private Text P4_selectColorTxt;
    [SerializeField] private GameObject P4_select;
    [SerializeField] private GameObject P4_selectDisparo;
    [SerializeField] private GameObject P4_selectPoder;
    [SerializeField] private GameObject P4_ready;

    private int contadorNcontrol = 1;

    private bool lleno = false;
    private bool enBusqueda = false;

    private int controlesP1;
    private bool encontradoP1;
    private bool listoDisparoP1;
    private bool listoPoderP1;
    private int selecP1 = 1;
    private float P1_delay = 0.5f;
    private int P1_cont1;
    private int P1_cont2;
    private bool readyP1;

    private int controlesP2;
    private bool encontradoP2;
    private bool listoDisparoP2;
    private bool listoPoderP2;
    private int selecP2 = 1;
    private float P2_delay = 0.5f;
    private int P2_cont1;
    private int P2_cont2;
    private bool readyP2;

    private int controlesP3;
    private bool encontradoP3;
    private bool listoDisparoP3;
    private bool listoPoderP3;
    private int selecP3 = 1;
    private float P3_delay = 0.5f;
    private int P3_cont1;
    private int P3_cont2;
    private bool readyP3;

    private int controlesP4;
    private bool encontradoP4;
    private bool listoDisparoP4;
    private bool listoPoderP4;
    private int selecP4 = 1;
    private float P4_delay = 0.5f;
    private int P4_cont1;
    private int P4_cont2;
    private bool readyP4;

    private int excepcion1 = -1;
    private string excepcion1_1;
    private int excepcion2 = -1;
    private string excepcion2_1;
    private int excepcion3 = -1;
    private string excepcion3_1;
    private int excepcion4 = -1;
    private string excepcion4_1;

    private bool P1desconeccion;
    private bool P2desconeccion;
    private bool P3desconeccion;
    private bool P4desconeccion;

    [Space]
    [SerializeField] private float countWait = 1;
    [SerializeField] private GameObject readyStatus;

    private AsyncOperation sceneAsync;
    private Scene currentScene;

    private StringBuilder builder = new StringBuilder();
    private string builderResult = string.Empty;
    private const string _joystick = "joystick ";
    private const string _button = " button ";

    private const string _WhiteSpace = " ";
    private const string _J = "J";
    private const string _H = "_H";
    private const string _V = "_V";
    private const string _Pause = "Pause";
    private const string _TyM = "Teclado y Mouse";
    private const string _Pattern = @"([a-zA-Z]+)(\d+)|(?<!^)(?=[A-Z])";
    private const string _J0_F = "J0_F";
    private readonly int _BaseColor = Shader.PropertyToID("_BaseColor");
    private readonly int _Color = Shader.PropertyToID("_Color");
    private readonly int _EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        P1_canvasSelect.SetActive(true);
        P1_canvasIniciado.SetActive(false);
        P1_selectModeloImg.SetActive(true);
        P1_selectColorImg.SetActive(false);
        P1_select.SetActive(false);
        P1_selectDisparo.SetActive(false);
        P1_selectPoder.SetActive(false);
        P1_ready.SetActive(false);

        P2_canvasSelect.SetActive(true);
        P2_canvasIniciado.SetActive(false);
        P2_selectModeloImg.SetActive(true);
        P2_selectColorImg.SetActive(false);
        P2_select.SetActive(false);
        P2_selectDisparo.SetActive(false);
        P2_selectPoder.SetActive(false);
        P2_ready.SetActive(false);

        P3_canvasSelect.SetActive(true);
        P3_canvasIniciado.SetActive(false);
        P3_selectModeloImg.SetActive(true);
        P3_selectColorImg.SetActive(false);
        P3_select.SetActive(false);
        P3_selectDisparo.SetActive(false);
        P3_selectPoder.SetActive(false);
        P3_ready.SetActive(false);

        P4_canvasSelect.SetActive(true);
        P4_canvasIniciado.SetActive(false);
        P4_selectModeloImg.SetActive(true);
        P4_selectColorImg.SetActive(false);
        P4_select.SetActive(false);
        P4_selectDisparo.SetActive(false);
        P4_selectPoder.SetActive(false);
        P4_ready.SetActive(false);
    }

    private void OnEnable()
    {
        enBusqueda = true;

        ControlsUpdate.OnDisconnect += OnDisConect;
        //ControlsUpdate.OnConnect += OnConect;
        ControlsUpdate.OnReconnect += OnReconect;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            StartLoadScene();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(0);
        }

        #region Busqueda controles

        if (enBusqueda && !lleno)
        {
            countWait -= 0.05f;
            if (contadorNcontrol == 1 && countWait < 0)
            {
                Debug.Log("Buscando jugador 1");
                for (int i = 1; i < 17; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        builder = new StringBuilder();
                        builder.Append(_joystick);
                        builder.Append(i);
                        builder.Append(_button);
                        builder.Append(j);
                        builderResult = builder.ToString();
                        if (Input.GetKeyDown(builderResult))
                        {
                            JugadorFind(1, i, builderResult);
                            break;
                        }
                        else if (Input.GetKeyDown(KeyCode.Mouse0) ||
                            Input.GetKeyDown(KeyCode.Mouse1) ||
                            Input.GetKeyDown(KeyCode.Space) ||
                            Input.GetKeyDown(KeyCode.Return))
                        {
                            JugadorFind(1, 0, _J0_F);
                            break;
                        }
                    }
                }
            }
            if (contadorNcontrol == 2 && countWait < 0)
            {
                Debug.Log("Buscando jugador 2");
                for (int i = 1; i < 17; i++)
                {
                    if (excepcion1 == i)
                    {
                        continue;
                    }
                    for (int j = 0; j < 20; j++)
                    {
                        builder = new StringBuilder();
                        builder.Append(_joystick);
                        builder.Append(i);
                        builder.Append(_button);
                        builder.Append(j);
                        builderResult = builder.ToString();
                        if (Input.GetKeyDown(builderResult))
                        {
                            JugadorFind(2, i, builderResult);
                            break;
                        }
                        else if (Input.GetKeyDown(KeyCode.Mouse0) ||
                            Input.GetKeyDown(KeyCode.Mouse1) ||
                            Input.GetKeyDown(KeyCode.Space) ||
                            Input.GetKeyDown(KeyCode.Return))
                        {
                            if (excepcion1 == 0)
                            {
                                continue;
                            }
                            JugadorFind(2, 0, _J0_F);
                            break;
                        }
                    }
                }
            }
            if (contadorNcontrol == 3 && countWait < 0)
            {
                Debug.Log("Buscando jugador 3");
                for (int i = 1; i < 17; i++)
                {
                    if (excepcion1 == i || excepcion2 == i)
                    {
                        continue;
                    }
                    for (int j = 0; j < 20; j++)
                    {
                        builder = new StringBuilder();
                        builder.Append(_joystick);
                        builder.Append(i);
                        builder.Append(_button);
                        builder.Append(j);
                        builderResult = builder.ToString();
                        if (Input.GetKeyDown(builderResult))
                        {
                            JugadorFind(3, i, builderResult);
                            break;
                        }
                        else if (Input.GetKeyDown(KeyCode.Mouse0) ||
                            Input.GetKeyDown(KeyCode.Mouse1) ||
                            Input.GetKeyDown(KeyCode.Space) ||
                            Input.GetKeyDown(KeyCode.Return))
                        {
                            if (excepcion1 == 0 || excepcion2 == 0)
                            {
                                continue;
                            }
                            JugadorFind(3, 0, _J0_F);
                            break;
                        }
                    }
                }
            }
            if (contadorNcontrol == 4 && countWait < 0)
            {
                Debug.Log("Buscando jugador 4");
                for (int i = 1; i < 17; i++)
                {
                    if (excepcion1 == i || excepcion2 == i || excepcion3 == i)
                    {
                        continue;
                    }
                    for (int j = 0; j < 20; j++)
                    {
                        builder = new StringBuilder();
                        builder.Append(_joystick);
                        builder.Append(i);
                        builder.Append(_button);
                        builder.Append(j);
                        builderResult = builder.ToString();
                        if (Input.GetKeyDown(builderResult))
                        {
                            JugadorFind(4, i, builderResult);
                            break;
                        }
                        else if (Input.GetKeyDown(KeyCode.Mouse0) ||
                            Input.GetKeyDown(KeyCode.Mouse1) ||
                            Input.GetKeyDown(KeyCode.Space) ||
                            Input.GetKeyDown(KeyCode.Return))
                        {
                            if (excepcion1 == 0 || excepcion2 == 0 || excepcion3 == 0)
                            {
                                continue;
                            }
                            JugadorFind(4, 0, _J0_F);
                            break;
                        }
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

        #region Config Disparo y Poder Bts

        if (encontradoP1 && !listoDisparoP1 && !listoPoderP1 && countWait < 0)
        {
            if (excepcion1 == 0)
            {
                for (int i = 0; i < 330; i++)
                {
                    if (i >= 8 && i <= 26 ||
                        i == 32 ||
                        i >= 97 && i <= 122 ||
                        i == 127 ||
                        i >= 277 && i <= 281 ||
                        i >= 300 && i <= 304 ||
                        i >= 307 && i <= 308 ||
                        i >= 313 && i <= 329)
                    {
                        if (Input.GetKeyDown((KeyCode)i))
                        {
                            string szs = ((KeyCode)i).ToString();
                            string[] split = Regex.Split(szs, _Pattern);
                            szs = string.Empty;
                            for (int j = 0; j < split.Length; j++)
                            {
                                if (split[j] != string.Empty)
                                {
                                    szs += split[j];
                                    if (j != split.Length - 1)
                                    {
                                        szs += _WhiteSpace;
                                    }
                                }

                            }
                            szs = szs.Trim().ToLower();
                            JugadorSetDisparo(1, szs);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < 20; j++)
                {
                    builder = new StringBuilder();
                    builder.Append(_joystick);
                    builder.Append(excepcion1);
                    builder.Append(_button);
                    builder.Append(j);
                    builderResult = builder.ToString();
                    if (Input.GetKeyDown(builderResult))
                    {
                        JugadorSetDisparo(1, j);
                        break;
                    }
                }
            }
        }
        if (encontradoP1 && listoDisparoP1 && !listoPoderP1 && countWait < 0)
        {
            if (excepcion1 == 0)
            {
                for (int i = 0; i < 330; i++)
                {
                    if (i >= 8 && i <= 26 ||
                        i == 32 ||
                        i >= 97 && i <= 122 ||
                        i == 127 ||
                        i >= 277 && i <= 281 ||
                        i >= 300 && i <= 304 ||
                        i >= 307 && i <= 308 ||
                        i >= 313 && i <= 329)
                    {
                        if (Input.GetKeyDown(excepcion1_1))
                        {
                            continue;
                        }
                        if (Input.GetKeyDown((KeyCode)i))
                        {
                            string szs = ((KeyCode)i).ToString();
                            string[] split = Regex.Split(szs, _Pattern);
                            szs = string.Empty;
                            for (int j = 0; j < split.Length; j++)
                            {
                                if (split[j] != string.Empty)
                                {
                                    szs += split[j];
                                    if (j != split.Length - 1)
                                    {
                                        szs += _WhiteSpace;
                                    }
                                }

                            }
                            szs = szs.Trim().ToLower();
                            JugadorSetPoder(1, szs);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < 20; j++)
                {
                    builder = new StringBuilder();
                    builder.Append(_joystick);
                    builder.Append(excepcion1);
                    builder.Append(_button);
                    builder.Append(j);
                    builderResult = builder.ToString();
                    if (Input.GetKeyDown(excepcion1_1))
                    {
                        continue;
                    }
                    if (Input.GetKeyDown(builderResult))
                    {
                        JugadorSetPoder(1, j);
                        break;
                    }
                }
            }
        }

        if (encontradoP2 && !listoDisparoP2 && !listoPoderP2 && countWait < 0)
        {
            if (excepcion2 == 0)
            {
                for (int i = 0; i < 330; i++)
                {
                    if (i >= 8 && i <= 26 ||
                        i == 32 ||
                        i >= 97 && i <= 122 ||
                        i == 127 ||
                        i >= 277 && i <= 281 ||
                        i >= 300 && i <= 304 ||
                        i >= 307 && i <= 308 ||
                        i >= 313 && i <= 329)
                    {
                        if (Input.GetKeyDown((KeyCode)i))
                        {
                            string szs = ((KeyCode)i).ToString();
                            string[] split = Regex.Split(szs, _Pattern);
                            szs = string.Empty;
                            for (int j = 0; j < split.Length; j++)
                            {
                                if (split[j] != string.Empty)
                                {
                                    szs += split[j];
                                    if (j != split.Length - 1)
                                    {
                                        szs += _WhiteSpace;
                                    }
                                }

                            }
                            szs = szs.Trim().ToLower();
                            JugadorSetDisparo(2, szs);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < 20; j++)
                {
                    builder = new StringBuilder();
                    builder.Append(_joystick);
                    builder.Append(excepcion2);
                    builder.Append(_button);
                    builder.Append(j);
                    builderResult = builder.ToString();
                    if (Input.GetKeyDown(builderResult))
                    {
                        JugadorSetDisparo(2, j);
                        break;
                    }
                }
            }
        }
        if (encontradoP2 && listoDisparoP2 && !listoPoderP2 && countWait < 0)
        {
            if (excepcion2 == 0)
            {
                for (int i = 0; i < 330; i++)
                {
                    if (i >= 8 && i <= 26 ||
                        i == 32 ||
                        i >= 97 && i <= 122 ||
                        i == 127 ||
                        i >= 277 && i <= 281 ||
                        i >= 300 && i <= 304 ||
                        i >= 307 && i <= 308 ||
                        i >= 313 && i <= 329)
                    {
                        if (Input.GetKeyDown(excepcion2_1))
                        {
                            continue;
                        }
                        if (Input.GetKeyDown((KeyCode)i))
                        {
                            string szs = ((KeyCode)i).ToString();
                            string[] split = Regex.Split(szs, _Pattern);
                            szs = string.Empty;
                            for (int j = 0; j < split.Length; j++)
                            {
                                if (split[j] != string.Empty)
                                {
                                    szs += split[j];
                                    if (j != split.Length - 1)
                                    {
                                        szs += _WhiteSpace;
                                    }
                                }

                            }
                            szs = szs.Trim().ToLower();
                            JugadorSetPoder(2, szs);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < 20; j++)
                {
                    builder = new StringBuilder();
                    builder.Append(_joystick);
                    builder.Append(excepcion2);
                    builder.Append(_button);
                    builder.Append(j);
                    builderResult = builder.ToString();
                    if (Input.GetKeyDown(excepcion2_1))
                    {
                        continue;
                    }
                    if (Input.GetKeyDown(builderResult))
                    {
                        JugadorSetPoder(2, j);
                        break;
                    }
                }
            }
        }

        if (encontradoP3 && !listoDisparoP3 && !listoPoderP3 && countWait < 0)
        {
            if (excepcion3 == 0)
            {
                for (int i = 0; i < 330; i++)
                {
                    if (i >= 8 && i <= 26 ||
                        i == 32 ||
                        i >= 97 && i <= 122 ||
                        i == 127 ||
                        i >= 277 && i <= 281 ||
                        i >= 300 && i <= 304 ||
                        i >= 307 && i <= 308 ||
                        i >= 313 && i <= 329)
                    {
                        if (Input.GetKeyDown((KeyCode)i))
                        {
                            string szs = ((KeyCode)i).ToString();
                            string[] split = Regex.Split(szs, _Pattern);
                            szs = string.Empty;
                            for (int j = 0; j < split.Length; j++)
                            {
                                if (split[j] != string.Empty)
                                {
                                    szs += split[j];
                                    if (j != split.Length - 1)
                                    {
                                        szs += _WhiteSpace;
                                    }
                                }

                            }
                            szs = szs.Trim().ToLower();
                            JugadorSetDisparo(3, szs);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < 20; j++)
                {
                    builder = new StringBuilder();
                    builder.Append(_joystick);
                    builder.Append(excepcion3);
                    builder.Append(_button);
                    builder.Append(j);
                    builderResult = builder.ToString();
                    if (Input.GetKeyDown(builderResult))
                    {
                        JugadorSetDisparo(3, j);
                        break;
                    }
                }
            }
        }
        if (encontradoP3 && listoDisparoP3 && !listoPoderP3 && countWait < 0)
        {
            if (excepcion3 == 0)
            {
                for (int i = 0; i < 330; i++)
                {
                    if (i >= 8 && i <= 26 ||
                        i == 32 ||
                        i >= 97 && i <= 122 ||
                        i == 127 ||
                        i >= 277 && i <= 281 ||
                        i >= 300 && i <= 304 ||
                        i >= 307 && i <= 308 ||
                        i >= 313 && i <= 329)
                    {
                        if (Input.GetKeyDown(excepcion3_1))
                        {
                            continue;
                        }
                        if (Input.GetKeyDown((KeyCode)i))
                        {
                            string szs = ((KeyCode)i).ToString();
                            string[] split = Regex.Split(szs, _Pattern);
                            szs = string.Empty;
                            for (int j = 0; j < split.Length; j++)
                            {
                                if (split[j] != string.Empty)
                                {
                                    szs += split[j];
                                    if (j != split.Length - 1)
                                    {
                                        szs += _WhiteSpace;
                                    }
                                }

                            }
                            szs = szs.Trim().ToLower();
                            JugadorSetPoder(3, szs);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < 20; j++)
                {
                    builder = new StringBuilder();
                    builder.Append(_joystick);
                    builder.Append(excepcion3);
                    builder.Append(_button);
                    builder.Append(j);
                    builderResult = builder.ToString();
                    if (Input.GetKeyDown(excepcion3_1))
                    {
                        continue;
                    }
                    if (Input.GetKeyDown(builderResult))
                    {
                        JugadorSetPoder(3, j);
                        break;
                    }
                }
            }
        }

        if (encontradoP4 && !listoDisparoP4 && !listoPoderP4 && countWait < 0)
        {
            if (excepcion4 == 0)
            {
                for (int i = 0; i < 330; i++)
                {
                    if (i >= 8 && i <= 26 ||
                        i == 32 ||
                        i >= 97 && i <= 122 ||
                        i == 127 ||
                        i >= 277 && i <= 281 ||
                        i >= 300 && i <= 304 ||
                        i >= 307 && i <= 308 ||
                        i >= 313 && i <= 329)
                    {
                        if (Input.GetKeyDown((KeyCode)i))
                        {
                            string szs = ((KeyCode)i).ToString();
                            string[] split = Regex.Split(szs, _Pattern);
                            szs = string.Empty;
                            for (int j = 0; j < split.Length; j++)
                            {
                                if (split[j] != string.Empty)
                                {
                                    szs += split[j];
                                    if (j != split.Length - 1)
                                    {
                                        szs += _WhiteSpace;
                                    }
                                }

                            }
                            szs = szs.Trim().ToLower();
                            JugadorSetDisparo(4, szs);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < 20; j++)
                {
                    builder = new StringBuilder();
                    builder.Append(_joystick);
                    builder.Append(excepcion4);
                    builder.Append(_button);
                    builder.Append(j);
                    builderResult = builder.ToString();
                    if (Input.GetKeyDown(builderResult))
                    {
                        JugadorSetDisparo(4, j);
                        break;
                    }
                }
            }
        }
        if (encontradoP4 && listoDisparoP4 && !listoPoderP4 && countWait < 0)
        {
            if (excepcion4 == 0)
            {
                for (int i = 0; i < 330; i++)
                {
                    if (i >= 8 && i <= 26 ||
                        i == 32 ||
                        i >= 97 && i <= 122 ||
                        i == 127 ||
                        i >= 277 && i <= 281 ||
                        i >= 300 && i <= 304 ||
                        i >= 307 && i <= 308 ||
                        i >= 313 && i <= 329)
                    {
                        if (Input.GetKeyDown(excepcion4_1))
                        {
                            continue;
                        }
                        if (Input.GetKeyDown((KeyCode)i))
                        {
                            string szs = ((KeyCode)i).ToString();
                            string[] split = Regex.Split(szs, _Pattern);
                            szs = string.Empty;
                            for (int j = 0; j < split.Length; j++)
                            {
                                if (split[j] != string.Empty)
                                {
                                    szs += split[j];
                                    if (j != split.Length - 1)
                                    {
                                        szs += _WhiteSpace;
                                    }
                                }

                            }
                            szs = szs.Trim().ToLower();
                            JugadorSetPoder(4, szs);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < 20; j++)
                {
                    builder = new StringBuilder();
                    builder.Append(_joystick);
                    builder.Append(excepcion4);
                    builder.Append(_button);
                    builder.Append(j);
                    builderResult = builder.ToString();
                    if (Input.GetKeyDown(excepcion4_1))
                    {
                        continue;
                    }
                    if (Input.GetKeyDown(builderResult))
                    {
                        JugadorSetPoder(4, j);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Seleccion modelo y color

        if (encontradoP1 && listoDisparoP1 && listoPoderP1)
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
                        if (P1_cont1 == playerOps.Length)
                        {
                            P1_cont1 = 0;
                        }
                        P1_selectModeloTxt.text = playerOps[0].prefabs[P1_cont1].nombreRobot;
                        if (P1_cont1 == 0)
                        {
                            playerOps[0].prefabs[0].prefab.SetActive(true);
                            playerOps[0].prefabs[1].prefab.SetActive(false);
                            playerOps[0].prefabs[2].prefab.SetActive(false);
                            playerOps[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 1)
                        {
                            playerOps[0].prefabs[0].prefab.SetActive(false);
                            playerOps[0].prefabs[1].prefab.SetActive(true);
                            playerOps[0].prefabs[2].prefab.SetActive(false);
                            playerOps[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 2)
                        {
                            playerOps[0].prefabs[0].prefab.SetActive(false);
                            playerOps[0].prefabs[1].prefab.SetActive(false);
                            playerOps[0].prefabs[2].prefab.SetActive(true);
                            playerOps[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 3)
                        {
                            playerOps[0].prefabs[0].prefab.SetActive(false);
                            playerOps[0].prefabs[1].prefab.SetActive(false);
                            playerOps[0].prefabs[2].prefab.SetActive(false);
                            playerOps[0].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P1_delay = 0.5f;
                        P1_cont2 += 1;
                        if (P1_cont2 == playerColors.Colores.Length)
                        {
                            P1_cont2 = 0;
                        }
                        P1_selectColorTxt.text = playerColors.Colores[P1_cont2].nombre;

                        playerOps[0].matModelos.SetColor(_BaseColor, playerColors.Colores[P1_cont2].colorAlbedo);
                        playerOps[0].matModelos.SetColor(_EmissionColor, playerColors.Colores[P1_cont2].colorEmission);
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
                            P1_cont1 = playerOps.Length - 1;
                        }
                        P1_selectModeloTxt.text = playerOps[0].prefabs[P1_cont1].nombreRobot;
                        if (P1_cont1 == 0)
                        {
                            playerOps[0].prefabs[0].prefab.SetActive(true);
                            playerOps[0].prefabs[1].prefab.SetActive(false);
                            playerOps[0].prefabs[2].prefab.SetActive(false);
                            playerOps[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 1)
                        {
                            playerOps[0].prefabs[0].prefab.SetActive(false);
                            playerOps[0].prefabs[1].prefab.SetActive(true);
                            playerOps[0].prefabs[2].prefab.SetActive(false);
                            playerOps[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 2)
                        {
                            playerOps[0].prefabs[0].prefab.SetActive(false);
                            playerOps[0].prefabs[1].prefab.SetActive(false);
                            playerOps[0].prefabs[2].prefab.SetActive(true);
                            playerOps[0].prefabs[3].prefab.SetActive(false);
                        }
                        if (P1_cont1 == 3)
                        {
                            playerOps[0].prefabs[0].prefab.SetActive(false);
                            playerOps[0].prefabs[1].prefab.SetActive(false);
                            playerOps[0].prefabs[2].prefab.SetActive(false);
                            playerOps[0].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P1_delay = 0.5f;
                        P1_cont2 -= 1;
                        if (P1_cont2 == -1)
                        {
                            P1_cont2 = playerColors.Colores.Length - 1;
                        }
                        P1_selectColorTxt.text = playerColors.Colores[P1_cont2].nombre;

                        playerOps[0].matModelos.SetColor(_BaseColor, playerColors.Colores[P1_cont2].colorAlbedo);
                        playerOps[0].matModelos.SetColor(_EmissionColor, playerColors.Colores[P1_cont2].colorEmission);
                    }
                }

                if (Input.GetKeyDown(InGameController.P1_F) && P1_delay < 0.25f)
                {
                    readyP1 = true;
                    //Debug.Log("Player 1 Ready");
                    P1_select.SetActive(false);
                    P1_ready.SetActive(true);
                }
            }
            else
            {
                if (Input.GetKeyDown(InGameController.P1_P))
                {
                    readyP1 = false;
                    //Debug.Log("Player 1 NO-Ready");
                    P1_select.SetActive(true);
                    P1_ready.SetActive(false);
                }
            }
        }
        if (encontradoP2 && listoDisparoP2 && listoPoderP2)
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
                        if (P2_cont1 == playerOps.Length)
                        {
                            P2_cont1 = 0;
                        }
                        P2_selectModeloTxt.text = playerOps[1].prefabs[P2_cont1].nombreRobot;
                        if (P2_cont1 == 0)
                        {
                            playerOps[1].prefabs[0].prefab.SetActive(true);
                            playerOps[1].prefabs[1].prefab.SetActive(false);
                            playerOps[1].prefabs[2].prefab.SetActive(false);
                            playerOps[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 1)
                        {
                            playerOps[1].prefabs[0].prefab.SetActive(false);
                            playerOps[1].prefabs[1].prefab.SetActive(true);
                            playerOps[1].prefabs[2].prefab.SetActive(false);
                            playerOps[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 2)
                        {
                            playerOps[1].prefabs[0].prefab.SetActive(false);
                            playerOps[1].prefabs[1].prefab.SetActive(false);
                            playerOps[1].prefabs[2].prefab.SetActive(true);
                            playerOps[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 3)
                        {
                            playerOps[1].prefabs[0].prefab.SetActive(false);
                            playerOps[1].prefabs[1].prefab.SetActive(false);
                            playerOps[1].prefabs[2].prefab.SetActive(false);
                            playerOps[1].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P2_delay = 0.5f;
                        P2_cont2 += 1;
                        if (P2_cont2 == playerColors.Colores.Length)
                        {
                            P2_cont2 = 0;
                        }
                        P2_selectColorTxt.text = playerColors.Colores[P2_cont2].nombre;

                        playerOps[1].matModelos.SetColor(_BaseColor, playerColors.Colores[P2_cont2].colorAlbedo);
                        playerOps[1].matModelos.SetColor(_EmissionColor, playerColors.Colores[P2_cont2].colorEmission);
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
                            P2_cont1 = playerOps.Length - 1;
                        }
                        P2_selectModeloTxt.text = playerOps[1].prefabs[P2_cont1].nombreRobot;
                        if (P2_cont1 == 0)
                        {
                            playerOps[1].prefabs[0].prefab.SetActive(true);
                            playerOps[1].prefabs[1].prefab.SetActive(false);
                            playerOps[1].prefabs[2].prefab.SetActive(false);
                            playerOps[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 1)
                        {
                            playerOps[1].prefabs[0].prefab.SetActive(false);
                            playerOps[1].prefabs[1].prefab.SetActive(true);
                            playerOps[1].prefabs[2].prefab.SetActive(false);
                            playerOps[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 2)
                        {
                            playerOps[1].prefabs[0].prefab.SetActive(false);
                            playerOps[1].prefabs[1].prefab.SetActive(false);
                            playerOps[1].prefabs[2].prefab.SetActive(true);
                            playerOps[1].prefabs[3].prefab.SetActive(false);
                        }
                        if (P2_cont1 == 3)
                        {
                            playerOps[1].prefabs[0].prefab.SetActive(false);
                            playerOps[1].prefabs[1].prefab.SetActive(false);
                            playerOps[1].prefabs[2].prefab.SetActive(false);
                            playerOps[1].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P2_delay = 0.5f;
                        P2_cont2 -= 1;
                        if (P2_cont2 == -1)
                        {
                            P2_cont2 = playerColors.Colores.Length - 1;
                        }
                        P2_selectColorTxt.text = playerColors.Colores[P2_cont2].nombre;

                        playerOps[1].matModelos.SetColor(_BaseColor, playerColors.Colores[P2_cont2].colorAlbedo);
                        playerOps[1].matModelos.SetColor(_EmissionColor, playerColors.Colores[P2_cont2].colorEmission);
                    }
                }

                if (Input.GetKeyDown(InGameController.P2_F) && P2_delay < 0.25f)
                {
                    readyP2 = true;
                    //Debug.Log("Player 2 Ready");
                    P2_select.SetActive(false);
                    P2_ready.SetActive(true);
                }
            }
            else
            {
                if (Input.GetKeyDown(InGameController.P2_P))
                {
                    readyP2 = false;
                    //Debug.Log("Player 2 NO-Ready");
                    P2_select.SetActive(true);
                    P2_ready.SetActive(false);
                }
            }
        }
        if (encontradoP3 && listoDisparoP3 && listoPoderP3)
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
                        if (P3_cont1 == playerOps.Length)
                        {
                            P3_cont1 = 0;
                        }
                        P3_selectModeloTxt.text = playerOps[2].prefabs[P3_cont1].nombreRobot;
                        if (P3_cont1 == 0)
                        {
                            playerOps[2].prefabs[0].prefab.SetActive(true);
                            playerOps[2].prefabs[1].prefab.SetActive(false);
                            playerOps[2].prefabs[2].prefab.SetActive(false);
                            playerOps[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 1)
                        {
                            playerOps[2].prefabs[0].prefab.SetActive(false);
                            playerOps[2].prefabs[1].prefab.SetActive(true);
                            playerOps[2].prefabs[2].prefab.SetActive(false);
                            playerOps[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 2)
                        {
                            playerOps[2].prefabs[0].prefab.SetActive(false);
                            playerOps[2].prefabs[1].prefab.SetActive(false);
                            playerOps[2].prefabs[2].prefab.SetActive(true);
                            playerOps[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 3)
                        {
                            playerOps[2].prefabs[0].prefab.SetActive(false);
                            playerOps[2].prefabs[1].prefab.SetActive(false);
                            playerOps[2].prefabs[2].prefab.SetActive(false);
                            playerOps[2].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P3_delay = 0.5f;
                        P3_cont2 += 1;
                        if (P3_cont2 == playerColors.Colores.Length)
                        {
                            P3_cont2 = 0;
                        }
                        P3_selectColorTxt.text = playerColors.Colores[P3_cont2].nombre;

                        playerOps[2].matModelos.SetColor(_BaseColor, playerColors.Colores[P3_cont2].colorAlbedo);
                        playerOps[2].matModelos.SetColor(_EmissionColor, playerColors.Colores[P3_cont2].colorEmission);
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
                            P3_cont1 = playerOps.Length - 1;
                        }
                        P3_selectModeloTxt.text = playerOps[2].prefabs[P3_cont1].nombreRobot;
                        if (P3_cont1 == 0)
                        {
                            playerOps[2].prefabs[0].prefab.SetActive(true);
                            playerOps[2].prefabs[1].prefab.SetActive(false);
                            playerOps[2].prefabs[2].prefab.SetActive(false);
                            playerOps[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 1)
                        {
                            playerOps[2].prefabs[0].prefab.SetActive(false);
                            playerOps[2].prefabs[1].prefab.SetActive(true);
                            playerOps[2].prefabs[2].prefab.SetActive(false);
                            playerOps[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 2)
                        {
                            playerOps[2].prefabs[0].prefab.SetActive(false);
                            playerOps[2].prefabs[1].prefab.SetActive(false);
                            playerOps[2].prefabs[2].prefab.SetActive(true);
                            playerOps[2].prefabs[3].prefab.SetActive(false);
                        }
                        if (P3_cont1 == 3)
                        {
                            playerOps[2].prefabs[0].prefab.SetActive(false);
                            playerOps[2].prefabs[1].prefab.SetActive(false);
                            playerOps[2].prefabs[2].prefab.SetActive(false);
                            playerOps[2].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P3_delay = 0.5f;
                        P3_cont2 -= 1;
                        if (P3_cont2 == -1)
                        {
                            P3_cont2 = playerColors.Colores.Length - 1;
                        }
                        P3_selectColorTxt.text = playerColors.Colores[P3_cont2].nombre;

                        playerOps[2].matModelos.SetColor(_BaseColor, playerColors.Colores[P3_cont2].colorAlbedo);
                        playerOps[2].matModelos.SetColor(_EmissionColor, playerColors.Colores[P3_cont2].colorEmission);
                    }
                }

                if (Input.GetKeyDown(InGameController.P3_F) && P3_delay < 0.25f)
                {
                    readyP3 = true;
                    //Debug.Log("Player 3 Ready");
                    P3_select.SetActive(false);
                    P3_ready.SetActive(true);
                }
            }
            else
            {
                if (Input.GetKeyDown(InGameController.P3_P))
                {
                    readyP3 = false;
                    //Debug.Log("Player 3 NO-Ready");
                    P3_select.SetActive(true);
                    P3_ready.SetActive(false);
                }
            }
        }
        if (encontradoP4 && listoDisparoP4 && listoPoderP4)
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
                        if (P4_cont1 == playerOps.Length)
                        {
                            P4_cont1 = 0;
                        }
                        P4_selectModeloTxt.text = playerOps[3].prefabs[P4_cont1].nombreRobot;
                        if (P4_cont1 == 0)
                        {
                            playerOps[3].prefabs[0].prefab.SetActive(true);
                            playerOps[3].prefabs[1].prefab.SetActive(false);
                            playerOps[3].prefabs[2].prefab.SetActive(false);
                            playerOps[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 1)
                        {
                            playerOps[3].prefabs[0].prefab.SetActive(false);
                            playerOps[3].prefabs[1].prefab.SetActive(true);
                            playerOps[3].prefabs[2].prefab.SetActive(false);
                            playerOps[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 2)
                        {
                            playerOps[3].prefabs[0].prefab.SetActive(false);
                            playerOps[3].prefabs[1].prefab.SetActive(false);
                            playerOps[3].prefabs[2].prefab.SetActive(true);
                            playerOps[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 3)
                        {
                            playerOps[3].prefabs[0].prefab.SetActive(false);
                            playerOps[3].prefabs[1].prefab.SetActive(false);
                            playerOps[3].prefabs[2].prefab.SetActive(false);
                            playerOps[3].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P4_delay = 0.5f;
                        P4_cont2 += 1;
                        if (P4_cont2 == playerColors.Colores.Length)
                        {
                            P4_cont2 = 0;
                        }
                        P4_selectColorTxt.text = playerColors.Colores[P4_cont2].nombre;

                        playerOps[3].matModelos.SetColor(_BaseColor, playerColors.Colores[P4_cont2].colorAlbedo);
                        playerOps[3].matModelos.SetColor(_EmissionColor, playerColors.Colores[P4_cont2].colorEmission);
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
                            P4_cont1 = playerOps.Length - 1;
                        }
                        P4_selectModeloTxt.text = playerOps[3].prefabs[P4_cont1].nombreRobot;
                        if (P4_cont1 == 0)
                        {
                            playerOps[3].prefabs[0].prefab.SetActive(true);
                            playerOps[3].prefabs[1].prefab.SetActive(false);
                            playerOps[3].prefabs[2].prefab.SetActive(false);
                            playerOps[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 1)
                        {
                            playerOps[3].prefabs[0].prefab.SetActive(false);
                            playerOps[3].prefabs[1].prefab.SetActive(true);
                            playerOps[3].prefabs[2].prefab.SetActive(false);
                            playerOps[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 2)
                        {
                            playerOps[3].prefabs[0].prefab.SetActive(false);
                            playerOps[3].prefabs[1].prefab.SetActive(false);
                            playerOps[3].prefabs[2].prefab.SetActive(true);
                            playerOps[3].prefabs[3].prefab.SetActive(false);
                        }
                        if (P4_cont1 == 3)
                        {
                            playerOps[3].prefabs[0].prefab.SetActive(false);
                            playerOps[3].prefabs[1].prefab.SetActive(false);
                            playerOps[3].prefabs[2].prefab.SetActive(false);
                            playerOps[3].prefabs[3].prefab.SetActive(true);
                        }
                    }
                    else
                    {
                        P4_delay = 0.5f;
                        P4_cont2 -= 1;
                        if (P4_cont2 == -1)
                        {
                            P4_cont2 = playerColors.Colores.Length - 1;
                        }
                        P4_selectColorTxt.text = playerColors.Colores[P4_cont2].nombre;

                        playerOps[3].matModelos.SetColor(_BaseColor, playerColors.Colores[P4_cont2].colorAlbedo);
                        playerOps[3].matModelos.SetColor(_EmissionColor, playerColors.Colores[P4_cont2].colorEmission);
                    }
                }

                if (Input.GetKeyDown(InGameController.P4_F) && P3_delay < 0.25f)
                {
                    readyP4 = true;
                    //Debug.Log("Player 4 Ready");
                    P4_select.SetActive(false);
                    P4_ready.SetActive(true);
                }
            }
            else
            {
                if (Input.GetKeyDown(InGameController.P4_P))
                {
                    readyP4 = false;
                    //Debug.Log("Player 4 NO-Ready");
                    P4_select.SetActive(true);
                    P4_ready.SetActive(false);
                }
            }
        }

        #endregion

        if (readyP1 && readyP2)
        {
            Debug.LogWarning("redi 1 y 2");
            if (!readyStatus.activeSelf)
            {
                readyStatus.SetActive(true);
            }
            if (Input.GetButtonDown(_Pause))
            {
                StartLoadScene();
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

    private void JugadorFind(int player, int joystick, string keyCode)
    {
        Debug.Log("Find: " + keyCode);

        //Debug.Log("jugador " + player + " encontrado");

        switch (player)
        {
            case 1:
                P1_playerName.text = playerOps[0].nombreJugador;
                P1_canvasSelect.SetActive(false);
                P1_canvasIniciado.SetActive(true);
                P1_selectModeloTxt.text = playerOps[0].prefabs[P1_cont1].nombreRobot;
                playerOps[0].prefabs[0].prefab.SetActive(true);
                playerOps[0].prefabs[1].prefab.SetActive(false);
                playerOps[0].prefabs[2].prefab.SetActive(false);
                playerOps[0].prefabs[3].prefab.SetActive(false);

                playerOps[0].matModelos.SetColor(_Color, playerColors.Colores[P1_cont2].colorAlbedo);
                playerOps[0].matModelos.SetColor(_EmissionColor, playerColors.Colores[P1_cont2].colorEmission);

                P1_selectColorTxt.text = playerColors.Colores[P1_cont2].nombre;
                excepcion1 = joystick;

                controlesP1 = excepcion1;
                InGameController.P1_index = controlesP1;
                InGameController.P1_H = _J + controlesP1 + _H;
                InGameController.P1_V = _J + controlesP1 + _V;

                if (joystick == 0)
                {
                    P1_nombreControl.text = _TyM;
                    ControlsUpdate.Instance.countKeyboard = true;
                }
                else
                {
                    P1_nombreControl.text = ControlsUpdate.Instance.controlesList[joystick - 1];
                }
                encontradoP1 = true;
                P1_selectDisparo.SetActive(true);
                contadorNcontrol = 2;
                break;

            case 2:
                P2_playerName.text = playerOps[1].nombreJugador;
                P2_canvasSelect.SetActive(false);
                P2_canvasIniciado.SetActive(true);
                P2_selectModeloTxt.text = playerOps[1].prefabs[P2_cont1].nombreRobot;
                playerOps[1].prefabs[0].prefab.SetActive(true);
                playerOps[1].prefabs[1].prefab.SetActive(false);
                playerOps[1].prefabs[2].prefab.SetActive(false);
                playerOps[1].prefabs[3].prefab.SetActive(false);

                playerOps[1].matModelos.SetColor(_Color, playerColors.Colores[P2_cont2].colorAlbedo);
                playerOps[1].matModelos.SetColor(_EmissionColor, playerColors.Colores[P2_cont2].colorEmission);

                P2_selectColorTxt.text = playerColors.Colores[P2_cont2].nombre;
                excepcion2 = joystick;

                controlesP2 = excepcion2;
                InGameController.P2_index = controlesP2;
                InGameController.P2_H = _J + controlesP2 + _H;
                InGameController.P2_V = _J + controlesP2 + _V;

                if (joystick == 0)
                {
                    P2_nombreControl.text = _TyM;
                    ControlsUpdate.Instance.countKeyboard = true;
                }
                else
                {
                    P2_nombreControl.text = ControlsUpdate.Instance.controlesList[joystick - 1];
                }
                encontradoP2 = true;
                P2_selectDisparo.SetActive(true);
                contadorNcontrol = 3;
                break;

            case 3:
                P3_playerName.text = playerOps[2].nombreJugador;
                P3_canvasSelect.SetActive(false);
                P3_canvasIniciado.SetActive(true);
                P3_selectModeloTxt.text = playerOps[2].prefabs[P3_cont1].nombreRobot;
                playerOps[2].prefabs[0].prefab.SetActive(true);
                playerOps[2].prefabs[1].prefab.SetActive(false);
                playerOps[2].prefabs[2].prefab.SetActive(false);
                playerOps[2].prefabs[3].prefab.SetActive(false);

                playerOps[2].matModelos.SetColor(_Color, playerColors.Colores[P3_cont2].colorAlbedo);
                playerOps[2].matModelos.SetColor(_EmissionColor, playerColors.Colores[P3_cont2].colorEmission);

                P3_selectColorTxt.text = playerColors.Colores[P3_cont2].nombre;
                excepcion3 = joystick;

                controlesP3 = excepcion3;
                InGameController.P3_index = controlesP3;
                InGameController.P3_H = _J + controlesP3 + _H;
                InGameController.P3_V = _J + controlesP3 + _V;

                if (joystick == 0)
                {
                    P3_nombreControl.text = _TyM;
                    ControlsUpdate.Instance.countKeyboard = true;
                }
                else
                {
                    P3_nombreControl.text = ControlsUpdate.Instance.controlesList[joystick - 1];
                }
                encontradoP3 = true;
                P3_selectDisparo.SetActive(true);
                contadorNcontrol = 4;
                break;

            case 4:
                P4_playerName.text = playerOps[3].nombreJugador;
                P4_canvasSelect.SetActive(false);
                P4_canvasIniciado.SetActive(true);
                P4_selectModeloTxt.text = playerOps[3].prefabs[P4_cont1].nombreRobot;
                playerOps[3].prefabs[0].prefab.SetActive(true);
                playerOps[3].prefabs[1].prefab.SetActive(false);
                playerOps[3].prefabs[2].prefab.SetActive(false);
                playerOps[3].prefabs[3].prefab.SetActive(false);

                playerOps[3].matModelos.SetColor(_Color, playerColors.Colores[P4_cont2].colorAlbedo);
                playerOps[3].matModelos.SetColor(_EmissionColor, playerColors.Colores[P4_cont2].colorEmission);

                P4_selectColorTxt.text = playerColors.Colores[P4_cont2].nombre;
                excepcion4 = joystick;

                controlesP4 = excepcion3;
                InGameController.P4_index = controlesP4;
                InGameController.P4_H = _J + controlesP4 + _H;
                InGameController.P4_V = _J + controlesP4 + _V;

                if (joystick == 0)
                {
                    P4_nombreControl.text = _TyM;
                    ControlsUpdate.Instance.countKeyboard = true;
                }
                else
                {
                    P4_nombreControl.text = ControlsUpdate.Instance.controlesList[joystick - 1];
                }
                encontradoP4 = true;
                P4_selectDisparo.SetActive(true);
                contadorNcontrol = 5;
                break;

            default:
                break;
        }

        countWait = 1;
    }
    private void JugadorSetDisparo(int player, int button)
    {
        switch (player)
        {
            case 1:
                builder = new StringBuilder();
                builder.Append(_joystick);
                builder.Append(excepcion1);
                builder.Append(_button);
                builder.Append(button);
                builderResult = builder.ToString();
                InGameController.P1_F = builderResult;
                listoDisparoP1 = true;
                excepcion1_1 = builderResult;
                P1_selectDisparo.SetActive(false);
                P1_selectPoder.SetActive(true);
                break;

            case 2:
                builder = new StringBuilder();
                builder.Append(_joystick);
                builder.Append(excepcion2);
                builder.Append(_button);
                builder.Append(button);
                builderResult = builder.ToString();
                InGameController.P2_F = builderResult;
                listoDisparoP2 = true;
                excepcion2_1 = builderResult;
                P2_selectDisparo.SetActive(false);
                P2_selectPoder.SetActive(true);
                break;

            case 3:
                builder = new StringBuilder();
                builder.Append(_joystick);
                builder.Append(excepcion3);
                builder.Append(_button);
                builder.Append(button);
                builderResult = builder.ToString();
                InGameController.P3_F = builderResult;
                listoDisparoP3 = true;
                excepcion3_1 = builderResult;
                P3_selectDisparo.SetActive(false);
                P3_selectPoder.SetActive(true);
                break;

            case 4:
                builder = new StringBuilder();
                builder.Append(_joystick);
                builder.Append(excepcion4);
                builder.Append(_button);
                builder.Append(button);
                builderResult = builder.ToString();
                InGameController.P4_F = builderResult;
                listoDisparoP4 = true;
                excepcion4_1 = builderResult;
                P4_selectDisparo.SetActive(false);
                P4_selectPoder.SetActive(true);
                break;

            default:
                break;
        }
        countWait = 1;

        Debug.Log("Set disparo: " + builderResult);
    }
    private void JugadorSetDisparo(int player, string keyCode)
    {
        switch (player)
        {
            case 1:
                InGameController.P1_F = keyCode;
                listoDisparoP1 = true;
                excepcion1_1 = keyCode;
                P1_selectDisparo.SetActive(false);
                P1_selectPoder.SetActive(true);
                break;

            case 2:
                InGameController.P2_F = keyCode;
                listoDisparoP2 = true;
                excepcion2_1 = keyCode;
                P2_selectDisparo.SetActive(false);
                P2_selectPoder.SetActive(true);
                break;

            case 3:
                InGameController.P3_F = keyCode;
                listoDisparoP3 = true;
                excepcion3_1 = keyCode;
                P3_selectDisparo.SetActive(false);
                P3_selectPoder.SetActive(true);
                break;

            case 4:
                InGameController.P4_F = keyCode;
                listoDisparoP4 = true;
                excepcion4_1 = keyCode;
                P4_selectDisparo.SetActive(false);
                P4_selectPoder.SetActive(true);
                break;

            default:
                break;
        }

        countWait = 1;

        Debug.Log("Set disparo: " + keyCode);
    }
    private void JugadorSetPoder(int player, int button)
    {
        switch (player)
        {
            case 1:
                builder = new StringBuilder();
                builder.Append(_joystick);
                builder.Append(excepcion1);
                builder.Append(_button);
                builder.Append(button);
                builderResult = builder.ToString();
                InGameController.P1_P = builderResult;
                listoPoderP1 = true;
                P1_selectPoder.SetActive(false);
                P1_select.SetActive(true);
                break;

            case 2:
                builder = new StringBuilder();
                builder.Append(_joystick);
                builder.Append(excepcion2);
                builder.Append(_button);
                builder.Append(button);
                builderResult = builder.ToString();
                InGameController.P2_P = builderResult;
                listoPoderP2 = true;
                P2_selectPoder.SetActive(false);
                P2_select.SetActive(true);
                break;

            case 3:
                builder = new StringBuilder();
                builder.Append(_joystick);
                builder.Append(excepcion3);
                builder.Append(_button);
                builder.Append(button);
                builderResult = builder.ToString();
                InGameController.P3_P = builderResult;
                listoPoderP3 = true;
                P3_selectPoder.SetActive(false);
                P3_select.SetActive(true);
                break;

            case 4:
                builder = new StringBuilder();
                builder.Append(_joystick);
                builder.Append(excepcion4);
                builder.Append(_button);
                builder.Append(button);
                builderResult = builder.ToString();
                InGameController.P4_P = builderResult;
                listoPoderP4 = true;
                P4_selectPoder.SetActive(false);
                P4_select.SetActive(true);
                break;

            default:
                break;
        }

        countWait = 1;

        Debug.LogWarning("Set poder: " + builderResult);
    }
    private void JugadorSetPoder(int player, string keyCode)
    {
        switch (player)
        {
            case 1:
                InGameController.P1_P = keyCode;
                listoPoderP1 = true;
                P1_selectPoder.SetActive(false);
                P1_select.SetActive(true);
                break;

            case 2:
                InGameController.P2_P = keyCode;
                listoPoderP2 = true;
                P2_selectPoder.SetActive(false);
                P2_select.SetActive(true);
                break;

            case 3:
                InGameController.P3_P = keyCode;
                listoPoderP3 = true;
                P3_selectPoder.SetActive(false);
                P3_select.SetActive(true);
                break;

            case 4:
                InGameController.P4_P = keyCode;
                listoPoderP4 = true;
                P4_selectPoder.SetActive(false);
                P4_select.SetActive(true);
                break;

            default:
                break;
        }

        countWait = 1;

        Debug.LogWarning("Set poder: " + keyCode);
    }

    private void OnDisConect(string controlName, bool conectado, int joystickNum)
    {
        Debug.LogWarning("bai " + controlName);

        if (excepcion1 == joystickNum)
        {
            Debug.LogError("Crack " + 1 + ", se te desconecto el control " + controlName);
            P1desconeccion = true;
        }
        if (excepcion2 == joystickNum)
        {
            Debug.LogError("Crack " + 2 + ", se te desconecto el control " + controlName);
            P2desconeccion = true;
        }
        if (excepcion3 == joystickNum)
        {
            Debug.LogError("Crack " + 3 + ", se te desconecto el control " + controlName);
            P3desconeccion = true;
        }
        if (excepcion4 == joystickNum)
        {
            Debug.LogError("Crack " + 4 + ", se te desconecto el control " + controlName);
            P4desconeccion = true;
        }
    }
    private void OnReconect(string controlName, int joystickNum)
    {
        Debug.LogWarning("REhi " + controlName);

        if (P1desconeccion && excepcion1 == joystickNum)
        {
            P1desconeccion = false;
            Debug.LogError("Crack " + 1 + ", volvio");
        }
        if (P2desconeccion && excepcion2 == joystickNum)
        {
            P2desconeccion = false;
            Debug.LogError("Crack " + 2 + ", volvio");
        }
        if (P3desconeccion && excepcion3 == joystickNum)
        {
            P3desconeccion = false;
            Debug.LogError("Crack " + 3 + ", volvio");
        }
        if (P4desconeccion && excepcion4 == joystickNum)
        {
            P4desconeccion = false;
            Debug.LogError("Crack " + 4 + ", volvio");
        }
    }

    private void StartLoadScene()
    {
        enBusqueda = false;
        currentScene = SceneManager.GetActiveScene();
        loading.SetActive(true);
    }

    public void StartLoadSceneMove()
    {
        StartCoroutine(LoadSceneMove());
    }

    IEnumerator LoadSceneMove()
    {
        playerOps[0].prefabs[P1_cont1].prefab.transform.SetParent(null);
        playerOps[1].prefabs[P2_cont1].prefab.transform.SetParent(null);
        playerOps[2].prefabs[P3_cont1].prefab.transform.SetParent(null);
        playerOps[3].prefabs[P4_cont1].prefab.transform.SetParent(null);

        sceneAsync = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        sceneAsync.allowSceneActivation = false;

        while (sceneAsync.progress < 0.9f)
        {
            //Debug.Log("Cargando: " + sceneAsync.progress);
            yield return null;
        }

        OnFinishedLoad();
    }

    private void OnFinishedLoad()
    {
        InGameController.sceneToUnload = currentScene;
        Debug.Log("Escena cargada");
        EnableScene(1);
        Debug.Log("Escena activada");
        scena.SetActive(false);
    }

    private void EnableScene(int sceneIndex)
    {
        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(sceneIndex);
        if (sceneToLoad.IsValid())
        {
            Debug.Log("Valido");

            if (readyP1)
            {
                SceneManager.MoveGameObjectToScene(playerOps[0].prefabs[P1_cont1].prefab, sceneToLoad);
                ColoresPass coloresPas = playerOps[0].prefabs[P1_cont1].prefab.GetComponent<ColoresPass>();
                coloresPas.RespawColor = playerColors.Colores[P1_cont2].colorRespaw;
                coloresPas.MiraPisoColor = playerColors.Colores[P1_cont2].colorMiraPiso;
                coloresPas.BalaColor = playerColors.Colores[P1_cont2].colorBala;
                coloresPas.colorUI = playerColors.Colores[P1_cont2].colorUI;
            }
            if (readyP2)
            {
                SceneManager.MoveGameObjectToScene(playerOps[1].prefabs[P2_cont1].prefab, sceneToLoad);
                ColoresPass coloresPas = playerOps[1].prefabs[P2_cont1].prefab.GetComponent<ColoresPass>();
                coloresPas.RespawColor = playerColors.Colores[P2_cont2].colorRespaw;
                coloresPas.MiraPisoColor = playerColors.Colores[P2_cont2].colorMiraPiso;
                coloresPas.BalaColor = playerColors.Colores[P2_cont2].colorBala;
                coloresPas.colorUI = playerColors.Colores[P2_cont2].colorUI;
            }
            if (readyP3)
            {
                SceneManager.MoveGameObjectToScene(playerOps[2].prefabs[P3_cont1].prefab, sceneToLoad);
                ColoresPass coloresPas = playerOps[2].prefabs[P3_cont1].prefab.GetComponent<ColoresPass>();
                coloresPas.RespawColor = playerColors.Colores[P3_cont2].colorRespaw;
                coloresPas.MiraPisoColor = playerColors.Colores[P3_cont2].colorMiraPiso;
                coloresPas.BalaColor = playerColors.Colores[P3_cont2].colorBala;
                coloresPas.colorUI = playerColors.Colores[P3_cont2].colorUI;
            }
            if (readyP4)
            {
                SceneManager.MoveGameObjectToScene(playerOps[3].prefabs[P4_cont1].prefab, sceneToLoad);
                ColoresPass coloresPas = playerOps[3].prefabs[P4_cont1].prefab.GetComponent<ColoresPass>();
                coloresPas.RespawColor = playerColors.Colores[P4_cont2].colorRespaw;
                coloresPas.MiraPisoColor = playerColors.Colores[P4_cont2].colorMiraPiso;
                coloresPas.BalaColor = playerColors.Colores[P4_cont2].colorBala;
                coloresPas.colorUI = playerColors.Colores[P4_cont2].colorUI;
            }

            sceneAsync.allowSceneActivation = true;
        }
    }
}