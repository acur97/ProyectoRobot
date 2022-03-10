using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ControlsUpdate : MonoBehaviour
{
    public static ControlsUpdate Instance { get; private set; }

    public static UnityAction<string, bool, int> OnConnect;
    public static UnityAction<string, bool, int> OnDisconnect;
    public static UnityAction<string, int> OnReconnect;

    public bool countKeyboard = false;

    [Space]
    public string[] controlesList = new string[0];

    private string[] lastControlesList = new string[0];

    [Space]
    public int clearControlesCount = 0;
    public List<string> clearControlesList = new List<string>();

    [Space]
    public List<string> allControlesList = new List<string>();

    private string allEmpty = string.Empty;

    private const string _TyM = "Teclado y Mouse";

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        controlesList = Input.GetJoystickNames();
        ClearControls();
        //lastControlesList = controlesList;
    }

    private void Update()
    {
        ControlesUpdate();
    }

    private void ClearControls()
    {
        clearControlesList.Clear();
        if (countKeyboard)
        {
            clearControlesCount = 1;
            clearControlesList.Add(_TyM);
            if (!allControlesList.Contains(_TyM))
            {
                allControlesList.Add(_TyM);
            }
        }
        else
        {
            clearControlesCount = 0;
        }
        for (int i = 0; i < controlesList.Length; i++)
        {
            if (controlesList[i] != string.Empty)
            {
                clearControlesCount++;
                clearControlesList.Add(controlesList[i]);
                if (!allControlesList.Contains(controlesList[i]))
                {
                    allControlesList.Add(controlesList[i]);
                }
            }
        }
    }


    private void ControlesUpdate()
    {
        controlesList = Input.GetJoystickNames();
        ClearControls();

        if (lastControlesList.Length != controlesList.Length)
        {
            for (int i = 0; i < clearControlesList.Count; i++)
            {
                OnControlesChange(clearControlesList[i], true, i + 1);
            }
        }
        else
        {
            allEmpty = string.Empty;
            for (int i = 0; i < controlesList.Length; i++)
            {
                allEmpty += controlesList[i];
                if (lastControlesList[i] != controlesList[i])
                {
                    if (controlesList[i] == string.Empty)
                    {
                        OnControlesChange(lastControlesList[i], false, i + 1);
                    }
                    else
                    {
                        OnControlesChange(controlesList[i], true, i + 1);
                        if (allControlesList.Contains(controlesList[i]))
                        {
                            OnReconnect?.Invoke(controlesList[i], i + 1);
                            Debug.LogWarning("Se REconecto: " + controlesList[i] + ", Joystick #" + (i + 1));
                        }
                    }
                }
            }
            if (allEmpty == string.Empty)
            {
                Debug.LogWarning("vacio");
            }
        }

        lastControlesList = controlesList;
    }

    private void OnControlesChange(string controlName, bool conectado, int joystickNum)
    {
        if (conectado)
        {
            OnConnect?.Invoke(controlName, conectado, joystickNum);
            Debug.LogWarning("Se Conecto: " + controlName + ", Joystick #" + joystickNum);
        }
        else
        {
            OnDisconnect?.Invoke(controlName, conectado, joystickNum);
            Debug.LogWarning("Se DESconecto: " + controlName + ", Joystick #" + joystickNum);
        }
    }
}