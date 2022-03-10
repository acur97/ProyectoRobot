using UnityEngine;
using UnityEngine.Events;

public class AnimInvoke : MonoBehaviour
{
    public UnityEvent Action1;

    public void OnAction1()
    {
        Action1.Invoke();
    }
}