using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMat : MonoBehaviour
{
    private Material mat;
    public float velocidad;
    public float reaparecer;
    public SphereCollider coll;
    public MeshRenderer mesh;

    private void Start()
    {
        mat = mesh.material;
    }

    private void Update()
    {
        if (mesh.enabled)
        {
            mat.SetTextureOffset("_BaseMap", (new Vector2(velocidad * Time.unscaledTime * 2, velocidad * Time.unscaledTime / 2)));
        }
    }

    public void Apagar()
    {
        coll.enabled = false;
        mesh.enabled = false;
        StartCoroutine(DelayPrender());
    }

    IEnumerator DelayPrender()
    {
        yield return new WaitForSecondsRealtime(reaparecer);
        coll.enabled = true;
        mesh.enabled = true;
    }
}
