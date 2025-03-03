using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseBubbleSO", menuName = "Scriptable Objects/BaseBubbleSO")]
public class BaseBubbleSO : ScriptableObject
{
    [Header("Appearance")]
    public Material material;
    public Vector2 sizeRange = new(0.5f,1.5f);
    public float inflationTime = 0.25f;
    public GameObject spawnParticle;
    public GameObject hitParticle;
    public GameObject deathParticle;

    [Header("Movement")]
    public Vector2 turbulenceRange = new(-0.5f, 0.5f);
    public float upSpeed = 0.5f;

    [Header("Functionality")]
    public int health = 4;
    public List<Resistance> resistances = new();
    public int score = 1;
}

[System.Serializable]
public class Resistance
{
    public DamageTypeEnum dmgType;
    [Range(0,4)] public int resistance;
}
