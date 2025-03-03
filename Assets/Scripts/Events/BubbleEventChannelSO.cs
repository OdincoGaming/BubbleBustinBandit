using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that have one transform argument.
/// Example: a spawner where the spawn point is the transform that is raised.
/// </summary>

[CreateAssetMenu(menuName = "Events/Bubble Event Channel")]
public class BubbleEventChannelSO : SerializableScriptableObject
{
    public UnityAction<BaseBubbleBehaviour> OnEventRaised;

    public void RaiseEvent(BaseBubbleBehaviour value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
