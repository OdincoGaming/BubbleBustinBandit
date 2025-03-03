using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/BubbleRequest Event Channel")]
public class BubbleRequestEventChannelSO : SerializableScriptableObject
{
    public UnityAction<BubbleRequest> OnEventRaised;

    public void RaiseEvent(BubbleRequest value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
