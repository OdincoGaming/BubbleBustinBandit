using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that have one transform argument.
/// Example: a spawner where the spawn point is the transform that is raised.
/// </summary>

[CreateAssetMenu(menuName = "Events/Transform Event Channel")]
public class TransformEventChannelSO : SerializableScriptableObject
{
    public UnityAction<Transform> OnEventRaised;

    public void RaiseEvent(Transform value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
