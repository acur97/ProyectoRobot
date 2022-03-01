﻿using System.Collections;
using UnityEngine;

public class RespawnMat : MonoBehaviour
{
    private Material mat;

    public float velocidad;
    public float reaparecer;
    public SphereCollider coll;
    public MeshRenderer mesh;

    private WaitForSeconds wait;

    private readonly int _BaseMap = Shader.PropertyToID("_BaseMap");

    private void Awake()
    {
        wait = new WaitForSeconds(reaparecer);
        mat = mesh.material;
    }

    private void Update()
    {
        if (mesh.enabled)
        {
            mat.SetTextureOffset(_BaseMap, (new Vector2(velocidad * Time.time * 2, velocidad * Time.time / 2)));
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
        yield return wait;
        coll.enabled = true;
        mesh.enabled = true;
    }
}