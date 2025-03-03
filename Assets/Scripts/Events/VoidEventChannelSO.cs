using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that have no arguments (Example: Exit game event)
/// </summary>

[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : SerializableScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}