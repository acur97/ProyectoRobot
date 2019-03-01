using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMat : MonoBehaviour
{
    public Material mat;
    public float velocidad;
    public float reaparecer;
    public SphereCollider coll;
    public MeshRenderer mesh;

    private void Update()
    {
        mat.mainTextureOffset = new Vector2(velocidad * Time.unscaledTime * 2, velocidad * Time.unscaledTime / 2);
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
