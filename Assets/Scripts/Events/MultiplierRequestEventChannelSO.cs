using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/MultiplierRequest Event Channel")]
public class MultiplierRequestEventChannelSO : SerializableScriptableObject
{
    public UnityAction<MultiplierRequest> OnEventRaised;

    public void RaiseEvent(MultiplierRequest value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
