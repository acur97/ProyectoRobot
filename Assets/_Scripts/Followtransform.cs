using UnityEngine;

public class Followtransform : MonoBehaviour
{
    public Transform quienMuevo;

    private void Update()
    {
        quienMuevo.position = transform.position;
        quienMuevo.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}