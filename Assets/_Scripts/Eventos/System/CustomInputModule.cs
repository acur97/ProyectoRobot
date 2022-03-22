using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomInputModule : MonoBehaviour
{
    public static CustomInputModule Instance;

    public string horizontalAxis = "";
    public string verticalAxis = "";
    public string submitButton = "";
    //public string CancelButton = "";

    [Space]
    //public float InputActionsPerSecond = 10;
    public float RepeatDelay = 0.5f;

    private Button _btn;
    private bool _pressedH;
    private bool _pressedV;
    private WaitForSeconds wait;
    private IEnumerator waitH;
    private IEnumerator waitV;

    private void Awake()
    {
        Instance = this;
        wait = new WaitForSeconds(RepeatDelay);
        waitH = DelayH();
        waitV = DelayV();
    }

    public void SetSelected(GameObject btn)
    {
        //Debug.LogWarning(btn.name, btn);
        _btn = btn.GetComponent<Button>();
        _btn.Select();
    }
    public void DesSetSelected()
    {
        _btn = null;
    }

    private void Update()
    {
        if (_btn != null && _btn.enabled)
        {
            if (Input.GetKeyDown(submitButton))
            {
                _btn.onClick.Invoke();
            }

            if (_btn.navigation.selectOnUp != null && !_pressedH && Input.GetAxisRaw(verticalAxis) > 0.25f)
            {
                SetSelected(_btn.navigation.selectOnUp.gameObject);
                _pressedH = true;
                StartCoroutine(waitH);
            }
            else if (_btn.navigation.selectOnDown != null && !_pressedH && Input.GetAxisRaw(verticalAxis) < -0.25f)
            {
                SetSelected(_btn.navigation.selectOnDown.gameObject);
                _pressedH = true;
                StartCoroutine(waitH);
            }
            else if (Input.GetAxisRaw(verticalAxis) < 0.25f && Input.GetAxisRaw(verticalAxis) > -0.25f)
            {
                _pressedH = false;
                StopCoroutine(waitH);
            }

            if (_btn.navigation.selectOnRight != null && !_pressedV && Input.GetAxisRaw(horizontalAxis) > 0.25f)
            {
                SetSelected(_btn.navigation.selectOnRight.gameObject);
                _pressedV = true;
                StartCoroutine(waitV);
            }
            else if (_btn.navigation.selectOnLeft != null && !_pressedV && Input.GetAxisRaw(horizontalAxis) < -0.25f)
            {
                SetSelected(_btn.navigation.selectOnLeft.gameObject);
                _pressedV = true;
                StartCoroutine(waitV);
            }
            else if (Input.GetAxisRaw(horizontalAxis) < 0.25f && Input.GetAxisRaw(horizontalAxis) > -0.25f)
            {
                _pressedV = false;
                StopCoroutine(waitV);
            }
        }
    }

    private IEnumerator DelayH()
    {
        Debug.LogWarning("1");
        yield return wait;
        _pressedH = false;
        Debug.LogWarning("2");
    }
    private IEnumerator DelayV()
    {
        Debug.LogWarning("1");
        yield return wait;
        _pressedV = false;
        Debug.LogWarning("2");
    }
}