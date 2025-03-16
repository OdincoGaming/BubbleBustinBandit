using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that have one rect transform argument.
/// Example: a ui element that needs to be moved.
/// </summary>

[CreateAssetMenu(menuName = "Events/RectTransform Event Channel")]
public class RectTransformEventChannelSO : SerializableScriptableObject
{
    public UnityAction<RectTransform> OnEventRaised;

    public void RaiseEvent(RectTransform value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
