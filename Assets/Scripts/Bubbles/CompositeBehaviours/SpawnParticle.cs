using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
