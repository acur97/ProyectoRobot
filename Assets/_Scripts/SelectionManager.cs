using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectionManager : MonoBehaviour
{
    [Serializable]
    public class ops1
    {
        public string nombre;
        public Material colorEmision;
        public Color32 colorUI;
    }
    [Serializable]
    public class ops2_prefabs
    {
        public string nombre;
        public GameObject prefab;
        public SkinnedMeshRenderer[] rotColor;
    }
    [Serializable]
    public class ops2
    {
        public string nombre;
        public ops2_prefabs[] prefabs;
    }
    [Header("Seleccionables")]
    public ops1[] opciones1;
    public ops2[] opciones2;
    [Space]
    public GameObject P1_canvasSelect;
    public GameObject P1_canvasIniciado;
    public Text P1_nombreControl;
    public GameObject P1_selectModeloImg;
    public Text P1_selectModeloTxt;
    public GameObject P1_selectColorImg;
    public Text P1_selectColorTxt;
    [Space]
    public GameObject P2_canvasSelect;
    public GameObject P2_canvasIniciado;
    public Text P2_nombreControl;
    public GameObject P2_selectModeloImg;
    public Text P2_selectModeloTxt;
    public GameObject P2_selectColorImg;
    public Text P2_selectColorTxt;
    [Space]
    public GameObject P3_canvasSelect;
    public GameObject P3_canvasIniciado;
    public Text P3_nombreControl;
    public GameObject P3_selectModeloImg;
    public Text P3_selectModeloTxt;
    public GameObject P3_selectColorImg;
    public Text P3_selectColorTxt;
    [Space]
    public GameObject P4_canvasSelect;
    public GameObject P4_canvasIniciado;
    public Text P4_nombreControl;
    public GameObject P4_selectModeloImg;
    public Text P4_selectModeloTxt;
    public GameObject P4_selectColorImg;
    public Text P4_selectColorTxt;
    [Space]
    public string[] controlesList;
    [Space]
    public RenderTexture rTexture1;
    public RenderTexture rTexture2;
    public RenderTexture rTexture3;
    public RenderTexture rTexture4;

    private int contadorNcontrol = 1;
    private bool lleno;

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

    private string excepcion1;
    private string excepcion2;
    private string excepcion3;

    private float countWait;

    private void Awake()
    {
        int valor = Screen.height;
        int valor2 = (int)(valor * 0.89f);

        rTexture1.width = valor2;
        rTexture1.height = valor;
        rTexture2.width = valor2;
        rTexture2.height = valor;
        rTexture3.width = valor2;
        rTexture3.height = valor;
        rTexture4.width = valor2;
        rTexture4.height = valor;

        P1_canvasSelect.SetActive(true);
        P1_canvasIniciado.SetActive(false);
        P1_selectModeloImg.SetActive(true);
        P1_selectColorImg.SetActive(false);

        P2_canvasSelect.SetActive(true);
        P2_canvasIniciado.SetActive(false);
        P2_selectModeloImg.SetActive(true);
        P2_selectColorImg.SetActive(false);

        P3_canvasSelect.SetActive(true);
        P3_canvasIniciado.SetActive(false);
        P3_selectModeloImg.SetActive(true);
        P3_selectColorImg.SetActive(false);

        P4_canvasSelect.SetActive(true);
        P4_canvasIniciado.SetActive(false);
        P4_selectModeloImg.SetActive(true);
        P4_selectColorImg.SetActive(false);

        controlesList = Input.GetJoystickNames();
    }

    private void Update()
    {
        if (!lleno)
        {
            countWait -= 0.1f;
            if (contadorNcontrol == 1)
            {
                for (int i = 0; i <= 7; i++)
                {
                    controlesList = Input.GetJoystickNames();
                    Debug.Log("Buscando jugador 1");
                    if (Input.GetButtonDown("J" + (i + 1) + "_F"))
                    {
                        Debug.Log("jugador 1 encontrado");
                        P1_canvasSelect.SetActive(false);
                        P1_canvasIniciado.SetActive(true);
                        P1_selectModeloTxt.text = opciones2[0].prefabs[P1_cont1].nombre;
                        opciones2[0].prefabs[0].prefab.SetActive(true);
                        opciones2[0].prefabs[1].prefab.SetActive(false);
                        opciones2[0].prefabs[2].prefab.SetActive(false);
                        opciones2[0].prefabs[3].prefab.SetActive(false);
                        P1_selectColorTxt.text = opciones1[P1_cont2].nombre;
                        excepcion1 = controlesList[i];

                        controlesP1 = (Array.IndexOf(controlesList, excepcion1) + 1);
                        InGameController.P1_H = "J" + controlesP1 + "_H";
                        InGameController.P1_V = "J" + controlesP1 + "_V";
                        InGameController.P1_F = "J" + controlesP1 + "_F";
                        InGameController.P1_P = "J" + controlesP1 + "_P";

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
                for (int i = 0; i <= 7; i++)
                {
                    controlesList = Input.GetJoystickNames();
                    Debug.Log("Buscando jugador 2");
                    if (Input.GetButtonDown("J" + (Array.IndexOf(controlesList, excepcion1) + 1) + "_F"))
                    {
                        Debug.Log("salto");
                        continue;
                    }
                    if (Input.GetButtonDown("J" + (i + 1) + "_F"))
                    {
                        Debug.Log("jugador 2 encontrado");
                        P2_canvasSelect.SetActive(false);
                        P2_canvasIniciado.SetActive(true);
                        P2_selectModeloTxt.text = opciones2[1].prefabs[P2_cont1].nombre;
                        opciones2[1].prefabs[0].prefab.SetActive(true);
                        opciones2[1].prefabs[1].prefab.SetActive(false);
                        opciones2[1].prefabs[2].prefab.SetActive(false);
                        opciones2[1].prefabs[3].prefab.SetActive(false);
                        P2_selectColorTxt.text = opciones1[P2_cont2].nombre;
                        excepcion2 = controlesList[i];

                        controlesP2 = (Array.IndexOf(controlesList, excepcion2) + 1);
                        InGameController.P2_H = "J" + controlesP2 + "_H";
                        InGameController.P2_V = "J" + controlesP2 + "_V";
                        InGameController.P2_F = "J" + controlesP2 + "_F";
                        InGameController.P2_P = "J" + controlesP2 + "_P";

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
                for (int i = 0; i <= 7; i++)
                {
                    controlesList = Input.GetJoystickNames();
                    Debug.Log("Buscando jugador 3");
                    if (Input.GetButtonDown("J" + (Array.IndexOf(controlesList, excepcion1) + 1) + "_F"))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown("J" + (Array.IndexOf(controlesList, excepcion2) + 1) + "_F"))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown("J" + (i + 1) + "_F"))
                    {
                        Debug.Log("jugador 3 encontrado");
                        P3_canvasSelect.SetActive(false);
                        P3_canvasIniciado.SetActive(true);
                        P3_selectModeloTxt.text = opciones2[2].prefabs[P3_cont1].nombre;
                        opciones2[2].prefabs[0].prefab.SetActive(true);
                        opciones2[2].prefabs[1].prefab.SetActive(false);
                        opciones2[2].prefabs[2].prefab.SetActive(false);
                        opciones2[2].prefabs[3].prefab.SetActive(false);
                        P3_selectColorTxt.text = opciones1[P3_cont2].nombre;
                        excepcion3 = controlesList[i];

                        controlesP3 = (Array.IndexOf(controlesList, excepcion3) + 1);
                        InGameController.P3_H = "J" + controlesP3 + "_H";
                        InGameController.P3_V = "J" + controlesP3 + "_V";
                        InGameController.P3_F = "J" + controlesP3 + "_F";
                        InGameController.P3_P = "J" + controlesP3 + "_P";

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
                for (int i = 0; i <= 7; i++)
                {
                    controlesList = Input.GetJoystickNames();
                    Debug.Log("Buscando jugador 4");
                    if (Input.GetButtonDown("J" + (Array.IndexOf(controlesList, excepcion1) + 1) + "_F"))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown("J" + (Array.IndexOf(controlesList, excepcion2) + 1) + "_F"))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown("J" + (Array.IndexOf(controlesList, excepcion3) + 1) + "_F"))
                    {
                        continue;
                    }
                    if (Input.GetButtonDown("J" + (i + 1) + "_F"))
                    {
                        Debug.Log("jugador 4 encontrado");
                        P4_canvasSelect.SetActive(false);
                        P4_canvasIniciado.SetActive(true);
                        P3_selectModeloTxt.text = opciones2[3].prefabs[P4_cont1].nombre;
                        opciones2[3].prefabs[0].prefab.SetActive(true);
                        opciones2[3].prefabs[1].prefab.SetActive(false);
                        opciones2[3].prefabs[2].prefab.SetActive(false);
                        opciones2[3].prefabs[3].prefab.SetActive(false);
                        P3_selectColorTxt.text = opciones1[P3_cont2].nombre;

                        controlesP4 = (Array.IndexOf(controlesList, controlesList[i]) + 1);
                        InGameController.P4_H = "J" + controlesP4 + "_H";
                        InGameController.P4_V = "J" + controlesP4 + "_V";
                        InGameController.P4_F = "J" + controlesP4 + "_F";
                        InGameController.P4_P = "J" + controlesP4 + "_P";

                        P4_nombreControl.text = controlesList[i].ToString();
                        encontradoP4 = true;
                        contadorNcontrol += 1;
                        break;
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (contadorNcontrol == 5)
        {
            lleno = true;
        }

        if (encontradoP1)
        {
            if (!readyP1)
            {
                P1_delay -= (Time.unscaledDeltaTime * 2);
                if (Input.GetAxisRaw("J" + controlesP1 + "_V") < -0.25f)
                {
                    P1_selectModeloImg.SetActive(true);
                    P1_selectColorImg.SetActive(false);
                    selecP1 = 1;
                }
                if (Input.GetAxisRaw("J" + controlesP1 + "_V") > 0.25f)
                {
                    P1_selectModeloImg.SetActive(false);
                    P1_selectColorImg.SetActive(true);
                    selecP1 = 2;
                }

                if (Input.GetAxisRaw("J" + controlesP1 + "_H") > 0.25f && P1_delay < 0.25f)
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
                    }
                }
                if (Input.GetAxisRaw("J" + controlesP1 + "_H") < -0.25f && P1_delay < 0.25f)
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
                    }
                }

                if (Input.GetButtonDown("J" + controlesP1 + "_F") && P1_delay < 0.25f)
                {
                    readyP1 = true;
                    Debug.Log("Player 1 Ready");
                }
            }
            else
            {
                if (Input.GetButtonDown("J" + controlesP1 + "_P"))
                {
                    readyP1 = false;
                    Debug.Log("Player 1 NO-Ready");
                }
            }
        }
        if (encontradoP2)
        {
            if (!readyP2)
            {
                P2_delay -= (Time.unscaledDeltaTime * 2);
                if (Input.GetAxisRaw("J" + controlesP2 + "_V") < -0.25f)
                {
                    P2_selectModeloImg.SetActive(true);
                    P2_selectColorImg.SetActive(false);
                    selecP2 = 1;
                }
                if (Input.GetAxisRaw("J" + controlesP2 + "_V") > 0.25f)
                {
                    P2_selectModeloImg.SetActive(false);
                    P2_selectColorImg.SetActive(true);
                    selecP2 = 2;
                }

                if (Input.GetAxisRaw("J" + controlesP2 + "_H") > 0.25f && P2_delay < 0.25f)
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
                    }
                }
                if (Input.GetAxisRaw("J" + controlesP2 + "_H") < -0.25f && P2_delay < 0.25f)
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
                    }
                }

                if (Input.GetButtonDown("J" + controlesP2 + "_F") && P2_delay < 0.25f)
                {
                    readyP2 = true;
                    Debug.Log("Player 2 Ready");
                }
            }
            else
            {
                if (Input.GetButtonDown("J" + controlesP2 + "_P"))
                {
                    readyP2 = false;
                    Debug.Log("Player 2 NO-Ready");
                }
            }
        }
        if (encontradoP3)
        {
            if (!readyP3)
            {
                P3_delay -= (Time.unscaledDeltaTime * 2);
                if (Input.GetAxisRaw("J" + controlesP3 + "_V") < -0.25f)
                {
                    P3_selectModeloImg.SetActive(true);
                    P3_selectColorImg.SetActive(false);
                    selecP3 = 1;
                }
                if (Input.GetAxisRaw("J" + controlesP3 + "_V") > 0.25f)
                {
                    P3_selectModeloImg.SetActive(false);
                    P3_selectColorImg.SetActive(true);
                    selecP3 = 2;
                }

                if (Input.GetAxisRaw("J" + controlesP3 + "_H") > 0.25f && P3_delay < 0.25f)
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
                    }
                }
                if (Input.GetAxisRaw("J" + controlesP3 + "_H") < -0.25f && P3_delay < 0.25f)
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
                    }
                }

                if (Input.GetButtonDown("J" + controlesP3 + "_F") && P3_delay < 0.25f)
                {
                    readyP3 = true;
                    Debug.Log("Player 3 Ready");
                }
            }
            else
            {
                if (Input.GetButtonDown("J" + controlesP3 + "_P"))
                {
                    readyP3 = false;
                    Debug.Log("Player 3 NO-Ready");
                }
            }
        }
        if (encontradoP4)
        {
            if (!readyP4)
            {
                P4_delay -= (Time.unscaledDeltaTime * 2);
                if (Input.GetAxisRaw("J" + controlesP4 + "_V") < -0.25f)
                {
                    P4_selectModeloImg.SetActive(true);
                    P4_selectColorImg.SetActive(false);
                    selecP4 = 1;
                }
                if (Input.GetAxisRaw("J" + controlesP4 + "_V") > 0.25f)
                {
                    P4_selectModeloImg.SetActive(false);
                    P4_selectColorImg.SetActive(true);
                    selecP4 = 2;
                }

                if (Input.GetAxisRaw("J" + controlesP4 + "_H") > 0.25f && P4_delay < 0.25f)
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
                    }
                }
                if (Input.GetAxisRaw("J" + controlesP4 + "_H") < -0.25f && P4_delay < 0.25f)
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
                    }
                }

                if (Input.GetButtonDown("J" + controlesP4 + "_F") && P3_delay < 0.25f)
                {
                    readyP4 = true;
                    Debug.Log("Player 4 Ready");
                }
            }
            else
            {
                if (Input.GetButtonDown("J" + controlesP4 + "_P"))
                {
                    readyP4 = false;
                    Debug.Log("Player 4 NO-Ready");
                }
            }
        }
    }
}
