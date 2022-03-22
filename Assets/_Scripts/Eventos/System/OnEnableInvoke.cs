using UnityEngine;
using UnityEngine.Events;

public class OnEnableInvoke : MonoBehaviour
{
    [SerializeField] private bool autoSelectObject = false;
    [SerializeField] private GameObject ObjectSelect;

    public enum Type { Gameobject, Canvas }
    [Space]
    [SerializeField] private Type Tipo;

    [Space]
    [SerializeField] private UnityEvent ifOn;
    [SerializeField] private UnityEvent ifOff;

    private Canvas canvas;
    private bool isOn = false;
    private CustomInputModule cInputModule;

    private void Awake()
    {
        if (Tipo == Type.Canvas)
        {
            canvas = GetComponent<Canvas>();
        }

        cInputModule = CustomInputModule.Instance;
    }

    private void Update()
    {
        if (Tipo == Type.Canvas)
        {
            if (!isOn && canvas.enabled == false)
            {
                ifOff.Invoke();
                isOn = true;
            }
            else if (isOn && canvas.enabled == true)
            {
                ifOn.Invoke();
                isOn = false;

                if (autoSelectObject)
                {
                    cInputModule.SetSelected(ObjectSelect);
                }
            }
        }
    }

    private void OnEnable()
    {
        if (Tipo == Type.Gameobject)
        {
            ifOn.Invoke();

            if (autoSelectObject)
            {
                cInputModule.SetSelected(ObjectSelect);
            }
        }
    }

    private void OnDisable()
    {
        if (Tipo == Type.Gameobject)
        {
            ifOff.Invoke();
        }
    }
}