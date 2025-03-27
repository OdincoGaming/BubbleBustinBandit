using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DamageBehaviour : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float effectTimer = 2f;
    [SerializeField] private List<Image> images;
    [SerializeField] private Scoreboard scoreboard;
    public UnityEvent gameOverEvent;
    private Color startColor;
    private Color endColor;
    private void OnTriggerEnter(Collider other)
    {
        slider.value -= .1f;
        TriggerEffect();
        BaseBubbleBehaviour bubble = other.GetComponent<BaseBubbleBehaviour>();
        if (bubble != null)
        {
            bubble.SetLoss(true);
            bubble.TakeDamage(DamageTypeEnum.basic, 10000);
            scoreboard.ResetMultiplier();
        }

        if(slider.value <= 0)
        {
            gameOverEvent.Invoke();
        }
    }

    private void Awake()
    {
        startColor = images[0].color;
        endColor = new(images[0].color.r, images[0].color.g, images[0].color.b, 0);
    }

    private void TriggerEffect()
    {
        Image i = images[Random.Range(0, images.Count)];
        Image[] childImages = i.GetComponentsInChildren<Image>();

        ToggleImageActive(true, i);
        Timer timer = Timer.Register
        (
            duration: effectTimer,
            onComplete: () => EndEffect(startColor, childImages, i),
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / effectTimer;
                progress = progress * progress * (3f - 2f * progress);
                Color newColor = Color.Lerp(startColor, endColor, progress);
                foreach (Image j in childImages)
                {
                    j.color = newColor;
                }
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }
    private void EndEffect(Color startColor, Image[] childImages, Image parentImage)
    {
        ToggleImageActive(false, parentImage);
        foreach (Image i in childImages)
        {
            i.color = startColor;
        }
        parentImage.color = startColor;
    }
    private void ToggleImageActive(bool isActive, Image parentImage)
    {
        parentImage.gameObject.SetActive(isActive);
    }
}
