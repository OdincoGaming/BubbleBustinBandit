using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/MultiplierPopupBehaviour Event Channel")]
public class MultiplierPopupBehaviourEventChannelSO : SerializableScriptableObject
{
    public UnityAction<MultiplierPopupBehaviour> OnEventRaised;

    public void RaiseEvent(MultiplierPopupBehaviour value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
