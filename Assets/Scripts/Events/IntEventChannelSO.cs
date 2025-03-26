using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: number of bubbles popped at once.
/// </summary>

[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannelSO : SerializableScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
