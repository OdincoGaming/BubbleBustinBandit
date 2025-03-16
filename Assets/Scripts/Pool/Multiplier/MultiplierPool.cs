using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MultiplierPool : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private List<MultiplierPopupBehaviour> available;
    [SerializeField] private MultiplierFactorySO factory;
    [SerializeField] private MultiplierRequestEventChannelSO _requestChannel;
    [SerializeField] private MultiplierPopupBehaviourEventChannelSO _returnChannel;
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
            MultiplierPopupBehaviour mpb = Push();
            mpb.gameObject.SetActive(false);
            mpb.rectTransform.SetParent(transform, false);
            available.Add(mpb);
        }
    }

    private MultiplierPopupBehaviour Push()
    {
        return factory.Create();
    }
    private MultiplierPopupBehaviour Pop() 
    {
        MultiplierPopupBehaviour result = available[0];
        available.Remove(result);
        return result;
    }

    private void Request(MultiplierRequest request)
    {
        MultiplierPopupBehaviour multiplier = available.Count > 0 ? Pop() : Push();
        multiplier.rectTransform.position = request.mousePos;
        multiplier.gameObject.SetActive(true);
        multiplier.SetText(request.mult);
        multiplier.SetTarget(target);
    }
    private void Return(MultiplierPopupBehaviour multiplier)
    {
        multiplier.gameObject.SetActive(false);
        available.Add(multiplier);
    }
}

public class MultiplierRequest
{
    public int mult;
    public Vector2 mousePos;

    public MultiplierRequest(int mult, Vector2 mousePos)
    {
        this.mult = mult;
        this.mousePos = mousePos;
    }
}