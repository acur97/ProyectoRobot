using UnityEngine;

[CreateAssetMenu(fileName = "PlayerColorsCustom", menuName = "ScriptableObjects/Player Colors", order = 0)]
public class PlayerCustoms : ScriptableObject
{
    public Elementos[] Colores;

    [System.Serializable]
    public class Elementos
    {
        public string nombre;
        public Color32 colorUI;

        [ColorUsage(false)]
        public Color colorAlbedo;

        [ColorUsage(false, true)]
        public Color colorEmission;

        [ColorUsage(true)]
        public Color colorRespaw;

        [ColorUsage(true)]
        public Color colorMiraPiso;

        [ColorUsage(true, true)]
        public Color colorBala;
    }
}