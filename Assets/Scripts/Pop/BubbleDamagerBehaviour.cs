using Unity.VisualScripting;
using UnityEngine;

public class BubbleDamagerBehaviour : MonoBehaviour
{
    public DamageTypeEnum DamageType;
    public int limiter = 0;
    [SerializeField] private BubbleEventChannelSO popChannel;
    [SerializeField] private MultiplierRequestEventChannelSO requestChannel;
    private int numHits = 0;
    private bool hasBubbleBeenPoppedThisClick = false;
    private void OnEnable()
    {
        popChannel.OnEventRaised += Respond;
    }
    private void OnDisable()
    {
        popChannel.OnEventRaised -= Respond;
    }
    private void Update()
    {
        limiter++;
        if(limiter > 4) 
        {
            if (hasBubbleBeenPoppedThisClick)
            {
                if (numHits > 1)
                {
                    MultiplierRequest mr = new(numHits, new(Input.mousePosition.x, Input.mousePosition.y));
                    requestChannel.RaiseEvent(mr);
                }
            }
            limiter = 0;
            numHits = 0;
            hasBubbleBeenPoppedThisClick = false;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseBubbleBehaviour bubble = other.gameObject.GetComponent<BaseBubbleBehaviour>();
        if (bubble != null) 
        {
            numHits++;
            bubble.TakeDamage(DamageType);
        }
    }

    private void Respond(BaseBubbleBehaviour bbb)
    {
        hasBubbleBeenPoppedThisClick = true;
    }
}
