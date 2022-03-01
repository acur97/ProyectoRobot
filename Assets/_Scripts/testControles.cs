using UnityEngine;

public class TestControles : MonoBehaviour
{
    private const string _joy = "es el joystick #";
    public string[] controles;

    void Update()
    {
        controles = Input.GetJoystickNames();

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (Input.GetJoystickNames()[i] != "")
            {
                Debug.Log(Input.GetJoystickNames()[i] + _joy + (i + 1));
            }
        }
        // requires you to set up axes "Joy0X" - "Joy3X" and "Joy0Y" - "Joy3Y" in the Input Manger
        /*for (int i = 0; i < 4; i++)
        {
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2 ||
                Mathf.Abs(Input.GetAxis("Joy" + i + "Y")) > 0.2)
            {
                Debug.Log(Input.GetJoystickNames()[i] + " is moved");
            }
        }*/
    }
}