using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(Rigidbody))]
public class BaseBubbleBehaviour : MonoBehaviour
{
    [SerializeField] private BaseBubbleSO bubbleData;
    [SerializeField] private int health;

    public Rigidbody rb;

    [SerializeField] private BubbleEventChannelSO returnChannel;

    private float turbulenceX = 0f;
    private float turbulenceZ = 0f;

    public BaseBubbleSO GetBubbleData()
    {
        return bubbleData;
    }
    public void SetBubbleData(BaseBubbleSO data)
    {
        bubbleData = data;
    }

    private void OnEnable()
    {
        float size = Random.Range(bubbleData.sizeRange.x, bubbleData.sizeRange.y);
        InflateBubbleTimer(size, bubbleData.inflationTime);

        if(rb == null)
            rb = GetComponent<Rigidbody>();

        turbulenceX = Random.Range(bubbleData.turbulenceRange.x, bubbleData.turbulenceRange.y);
        turbulenceZ = Random.Range(bubbleData.turbulenceRange.x, bubbleData.turbulenceRange.y);

        Instantiate(bubbleData.spawnParticle, this.transform.position + bubbleData.spawnParticle.transform.position, bubbleData.spawnParticle.transform.rotation);
        health = bubbleData.health;;

        GetComponent<MeshRenderer>().material = bubbleData.material;
    }

    private void InflateBubbleTimer(float targetScale, float lerpDuration)
    {
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = new(targetScale, targetScale, targetScale);
        //Timer timer = Timer.Register
        this.AttachTimer
        (
            duration: lerpDuration,
            onComplete: () => { transform.localScale = endScale; },
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / lerpDuration;
                progress = progress * progress * (3f - 2f * progress);
                transform.localScale = Vector3.Lerp(startScale, endScale, progress);
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }

    public void TakeDamage(DamageTypeEnum dmgType)
    {
        Instantiate(bubbleData.hitParticle, this.transform.position + bubbleData.hitParticle.transform.position, bubbleData.hitParticle.transform.rotation);

        int baseDamage = 4;
        foreach (Resistance r in bubbleData.resistances)
        {
            if (r.dmgType == dmgType)
            {
                baseDamage -= r.resistance;
            }
        }

        health -= baseDamage;
        if (health <= 0)
        {
            Pop();
        }
    }

    private void Pop()
    {
        Instantiate(bubbleData.deathParticle, this.transform.position + bubbleData.deathParticle.transform.position, bubbleData.deathParticle.transform.rotation);
        returnChannel.RaiseEvent(this);
    }

    void FixedUpdate()
    {
        Vector3 moveVector = new(turbulenceX, bubbleData.upSpeed, turbulenceZ);
        rb.linearVelocity = moveVector;

        /*rb.AddRelativeForce(moveVector);

        Vector3 velocity = rb.linearVelocity;

        // Limit the y-axis velocity
        if (velocity.y > bubbleData.upSpeed)
        {
            velocity.y = Mathf.Sign(velocity.y) * bubbleData.upSpeed;
        }
        if(velocity.y < 0)
        {
            velocity.y = bubbleData.upSpeed;
        }

        if(Mathf.Abs(velocity.x) > bubbleData.turbulenceRange.y)
        {
            velocity.x = Mathf.Sign(velocity.x) * bubbleData.turbulenceRange.y;
        }

        if (Mathf.Abs(velocity.y) > bubbleData.turbulenceRange.y)
        {
            velocity.y = Mathf.Sign(velocity.y) * bubbleData.turbulenceRange.y;
        }

        rb.linearVelocity = velocity;*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        turbulenceX *= -1;
        turbulenceZ *= -1;
    }
}
