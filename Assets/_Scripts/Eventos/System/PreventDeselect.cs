using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventSystem))]
public class PreventDeselect : MonoBehaviour
{
    private EventSystem evt;
    private GameObject sel;

    private void Awake()
    {
        evt = EventSystem.current;
    }

    private void Update()
    {
        if (evt.currentSelectedGameObject != null && evt.currentSelectedGameObject != sel)
        {
            sel = evt.currentSelectedGameObject;
        }
        else if (sel != null && evt.currentSelectedGameObject == null)
        {
            evt.SetSelectedGameObject(sel);
        }
    }
}