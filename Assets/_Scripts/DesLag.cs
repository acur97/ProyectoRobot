using UnityEngine;

public class DesLag : MonoBehaviour
{
    public GameObject[] objetos;
    public Transform padre;

    private void Awake()
    {
        for (int i = 0; i < objetos.Length; i++)
        {
            Instantiate(objetos[i], padre.position, padre.rotation, padre);
        }
    }
}