using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFlow : MonoBehaviour
{
    public bool activado = true;
    public float velocidad;
    public Color col;
    private float m_Hue = 0;
    private float m_Saturation = 1;
    private float m_Value = 1;

    public Material matPiso;

    private void Update()
    {
        if (activado)
        {
            m_Hue += Time.unscaledDeltaTime * velocidad;
            if (m_Hue > 1)
            {
                m_Hue = 0;
            }
            col = Color.HSVToRGB(m_Hue, m_Saturation, m_Value, false);
            matPiso.SetColor("_BaseColor", col);
        }
    }
}