using UnityEngine;

public class BubbleDamagerBehaviour : MonoBehaviour
{
    public DamageTypeEnum DamageType;
    public int limiter = 0;
    private void Update()
    {
        limiter++;
        if(limiter > 4) 
        {
            limiter = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseBubbleBehaviour bubble = other.gameObject.GetComponent<BaseBubbleBehaviour>();
        if (bubble != null) 
        {
            bubble.TakeDamage(DamageType);
        }
    }
}
