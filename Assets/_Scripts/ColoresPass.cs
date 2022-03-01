using UnityEngine;

public class ColoresPass : MonoBehaviour
{
    public Color32 colorUI;

    [ColorUsage(true)]
    public Color RespawColor;

    [ColorUsage(true)]
    public Color MiraPisoColor;

    [ColorUsage(true, true)]
    public Color BalaColor;
}