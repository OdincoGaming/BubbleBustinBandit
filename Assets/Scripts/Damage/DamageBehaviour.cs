using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageBehaviour : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float effectTimer = 2f;
    [SerializeField] private List<Image> images;
    private Color startColor;
    private Color endColor;
    private Timer timer;
    private void OnTriggerEnter(Collider other)
    {
        slider.value -= .1f;
        TriggerEffect();
    }
    private void Awake()
    {
        startColor = images[0].color;
        endColor = new(images[0].color.r, images[0].color.g, images[0].color.b, 0);
    }

    private void TriggerEffect()
    {
        ToggleImageActive(true);
        if(timer != null)
        {
            timer.Cancel();
        }
        timer = Timer.Register
        (
            duration: effectTimer,
            onComplete: () => EndEffect(startColor),
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / effectTimer;
                progress = progress * progress * (3f - 2f * progress);
                Color newColor = Color.Lerp(startColor, endColor, progress);
                foreach (Image i in images)
                {
                    i.color = newColor;
                }
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }
    private void EndEffect(Color startColor)
    {
        ToggleImageActive(false);
        foreach (Image i in images)
        {
            i.color = startColor;
        }
    }
    private void ToggleImageActive(bool isActive)
    {
        foreach(Image i in images)
        {
            i.gameObject.SetActive(isActive);
        }
    }
}
