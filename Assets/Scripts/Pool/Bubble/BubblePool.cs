using System.Collections.Generic;
using UnityEngine;

public class BubblePool : MonoBehaviour
{
    [SerializeField] private List<BaseBubbleBehaviour> available;
    [SerializeField] private BubbleFactorySO factory;
    [SerializeField] private BubbleRequestEventChannelSO _requestChannel;
    [SerializeField] private BubbleEventChannelSO _returnChannel;

    private void Start()
    {
        Prewarm();
    }

    private void OnEnable()
    {
        _requestChannel.OnEventRaised += Request;
        _returnChannel.OnEventRaised += Return;
    }

    private void OnDisable()
    {
        _requestChannel.OnEventRaised -= Request;
        _returnChannel.OnEventRaised -= Return;
    }

    private void Prewarm()
    {
        for (int i = 0; i < 10; i++)
        {
            available.Add(Push());
        }
    }

    private BaseBubbleBehaviour Push()
    {
        return factory.Create();
    }
    private BaseBubbleBehaviour Pop()
    {
        BaseBubbleBehaviour result = available[0];
        available.Remove(result);
        return result;
    }

    private void Request(BubbleRequest request)
    {
        BaseBubbleBehaviour bubble = available.Count > 0 ? Pop() : Push();
        bubble.SetBubbleData(request.bubbleData);
        bubble.transform.position = request.t.position;
        bubble.transform.localScale = Vector3.zero;
        bubble.rb.linearVelocity = Vector3.zero;
        bubble.rb.angularVelocity = Vector3.zero;
        bubble.rb.WakeUp();
        bubble.gameObject.SetActive(true);
    }

    private void Return(BaseBubbleBehaviour bubble)
    {
        bubble.gameObject.SetActive(false);
        bubble.rb.Sleep();
        available.Add(bubble);
    }
}

public class BubbleRequest
{
    public Transform t;
    public BaseBubbleSO bubbleData;

    public BubbleRequest(Transform t, BaseBubbleSO bubbleData)
    {
        this.t = t;
        this.bubbleData = bubbleData;
    }
}