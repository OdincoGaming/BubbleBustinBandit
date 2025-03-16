using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class MultiplierPopupBehaviour : MonoBehaviour
{
    [SerializeField] private MultiplierPopupBehaviourEventChannelSO returnChannel;
    [SerializeField] private List<TextMeshProUGUI> textboxes;
    [SerializeField] private RectTransform target;
    [SerializeField] private float lerpDuration;
    [SerializeField] private AnimationCurve xCurve;
    [SerializeField] private AnimationCurve xCurveNeg;
    [SerializeField] private AnimationCurve yCurve;
    public RectTransform rectTransform;

    private void Awake()
    {
        if(rectTransform ==null)
            rectTransform = GetComponent<RectTransform>();
    }

    public void DoLerp()
    {
        float randChance = Random.Range(-11, 10);
        Vector3 startPos = rectTransform.position;
        this.AttachTimer
        (
            duration: lerpDuration,
            onComplete: () => 
            { 
                rectTransform.position = target.position;
                ClearText();
                returnChannel.RaiseEvent(this);
            },
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / lerpDuration;
                progress = progress * progress * (3f - 2f * progress);
                Vector3 newV3 = Vector3.Lerp(startPos, target.position, progress);
                if (randChance < 0)
                {
                    newV3.x = newV3.x * xCurve.Evaluate(progress);
                }
                else
                {
                    newV3.x = newV3.x * xCurveNeg.Evaluate(progress);
                }
                newV3.y = newV3.y * yCurve.Evaluate(progress);
                rectTransform.position = newV3;
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }

    public void SetTarget(RectTransform targ)
    {
        target = targ;
        DoLerp();
    }

    public void SetText(int mult)
    {
        foreach(TextMeshProUGUI t in textboxes)
        {
            t.text = "x" + mult.ToString();
        }
    }
    private void ClearText()
    {
        foreach (TextMeshProUGUI t in textboxes)
        {
            t.text = "";
        }
    }
}
