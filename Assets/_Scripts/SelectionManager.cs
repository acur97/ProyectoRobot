using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectionManager : MonoBehaviour
{
    public GameObject P1_canvasSelect;
    public GameObject P1_canvasIniciado;
    public Text P1_nombreControl;
    public GameObject P1_selectModeloImg;
    public GameObject P1_selectColorImg;
    [Space]
    public GameObject P2_canvasSelect;
    public GameObject P2_canvasIniciado;
    public Text P2_nombreControl;
    public GameObject P2_selectModeloImg;
    public GameObject P2_selectColorImg;
    [Space]
    public GameObject P3_canvasSelect;
    public GameObject P3_canvasIniciado;
    public Text P3_nombreControl;
    public GameObject P3_selectModeloImg;
    public GameObject P3_selectColorImg;
    [Space]
    public GameObject P4_canvasSelect;
    public GameObject P4_canvasIniciado;
    public Text P4_nombreControl;
    public GameObject P4_selectModeloImg;
    public GameObject P4_selectColorImg;
    [Space]
    public string[] controlesList;

    private int contadorNcontrol = 1;
    private bool puedeComenzar;
    private bool lleno;

    private int controlesP1;
    private bool encontradoP1;
    private int selecP1 = 1;
    private int controlesP2;
    private bool encontradoP2;
    private int selecP2 = 1;
    private int controlesP3;
    private bool encontradoP3;
    private int selecP3 = 1;
    private int controlesP4;
    private bool encontradoP4;
    private int selecP4 = 1;

    private string excepcion1;
    private string excepcion2;
    private string excepcion3;

    private float countWait;

    private void Awake()
    {
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
                        excepcion2 = controlesList[i];

                        controlesP2 = (Array.IndexOf(controlesList, excepcion2) + 1);
                        InGameController.P2_H = "J" + controlesP2 + "_H";
                        InGameController.P2_V = "J" + controlesP2 + "_V";
                        InGameController.P2_F = "J" + controlesP2 + "_F";
                        InGameController.P2_P = "J" + controlesP2 + "_P";

                        P2_nombreControl.text = controlesList[i].ToString();
                        puedeComenzar = true;
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

        if (contadorNcontrol == 5)
        {
            lleno = true;
        }

        if (encontradoP1)
        {
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

            if (Input.GetAxisRaw("J" + controlesP1 + "_H") > 0.25f)
            {
                if (selecP1 == 1)
                {
                    Debug.Log("derecha 1");
                }
                else
                {
                    Debug.Log("derecha 2");
                }
            }
            if (Input.GetAxisRaw("J" + controlesP1 + "_H") < -0.25f)
            {
                if (selecP1 == 1)
                {
                    Debug.Log("izquierda 1");
                }
                else
                {
                    Debug.Log("izquierda 2");
                }
            }
        }
        if (encontradoP2)
        {
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

            if (Input.GetAxisRaw("J" + controlesP2 + "_H") > 0.25f)
            {
                if (selecP2 == 1)
                {
                    Debug.Log("derecha 1");
                }
                else
                {
                    Debug.Log("derecha 2");
                }
            }
            if (Input.GetAxisRaw("J" + controlesP2 + "_H") < -0.25f)
            {
                if (selecP2 == 1)
                {
                    Debug.Log("izquierda 1");
                }
                else
                {
                    Debug.Log("izquierda 2");
                }
            }
        }
        if (encontradoP3)
        {
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

            if (Input.GetAxisRaw("J" + controlesP3 + "_H") > 0.25f)
            {
                if (selecP3 == 1)
                {
                    Debug.Log("derecha 1");
                }
                else
                {
                    Debug.Log("derecha 2");
                }
            }
            if (Input.GetAxisRaw("J" + controlesP3 + "_H") < -0.25f)
            {
                if (selecP3 == 1)
                {
                    Debug.Log("izquierda 1");
                }
                else
                {
                    Debug.Log("izquierda 2");
                }
            }
        }
        if (encontradoP4)
        {
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

            if (Input.GetAxisRaw("J" + controlesP4 + "_H") > 0.25f)
            {
                if (selecP4 == 1)
                {
                    Debug.Log("derecha 1");
                }
                else
                {
                    Debug.Log("derecha 2");
                }
            }
            if (Input.GetAxisRaw("J" + controlesP4 + "_H") < -0.25f)
            {
                if (selecP4 == 1)
                {
                    Debug.Log("izquierda 1");
                }
                else
                {
                    Debug.Log("izquierda 2");
                }
            }
        }
    }
}
