using UnityEngine;

[System.Serializable]
public enum DamageTypeEnum
{
    basic,
    blue,
    red,
    green,
    yellow,
    pink,
    orange,
    cyan
}

[CreateAssetMenu(menuName = "Config/DamageTypeColors")]
public class DamageTypeColorsSO: SerializableScriptableObject
{
    public Color basic;
    public Color blue;
    public Color red;
    public Color green;
    public Color yellow;
    public Color pink;
    public Color orange;
    public Color cyan;
}
