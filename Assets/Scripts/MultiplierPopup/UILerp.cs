using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class UILerp : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private float lerpDuration;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        DoLerp();
    }

    private void DoLerp()
    {
        Vector3 startPos = rectTransform.position;
        this.AttachTimer
        (
            duration: lerpDuration,
            onComplete: () => { rectTransform.position = target.position; },
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / lerpDuration;
                progress = progress * progress * (3f - 2f * progress);
                rectTransform.position = Vector3.Lerp(startPos, target.position, progress);
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }
}
