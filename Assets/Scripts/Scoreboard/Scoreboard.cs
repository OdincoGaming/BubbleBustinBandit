using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private BubbleEventChannelSO bubbleChannel;
    [SerializeField] private IntEventChannelSO valueChannel;
    [SerializeField] private List<TextMeshProUGUI> scoreTexts;
    [SerializeField] private List<TextMeshProUGUI> multTexts;
    [SerializeField] private Color multDefaultColor;
    [SerializeField] private Color multGlowColor;
    private float multDefaultFontSize;
    private float multBigFontSize;
    private float multiplier = 1f;
    private float scoreDefaultFontSize;
    private float scoreBigFontSize;
    private float score = 0f;

    private void OnEnable()
    {
        bubbleChannel.OnEventRaised += Respond;
        valueChannel.OnEventRaised += MultRespond;

        multDefaultFontSize = multTexts[1].fontSize;
        multBigFontSize = multDefaultFontSize * 1.2f;

        scoreDefaultFontSize = scoreTexts[1].fontSize;
        scoreBigFontSize = scoreTexts[1].fontSize * 1.2f;
    }

    private void OnDisable()
    {
        bubbleChannel.OnEventRaised += Respond;
        valueChannel.OnEventRaised -= MultRespond;
    }

    private void Respond(BaseBubbleBehaviour bubble)
    {
        if (!bubble.IsLost())
        {
            score += bubble.GetBubbleData().score * multiplier;

            foreach (TextMeshProUGUI textbox in scoreTexts)
            {
                textbox.text = score.ToString();
            }
            LerpscoreTextcolor();
        }
    }

    private void MultRespond(int value)
    {
        LerpMultTextcolor();
        multiplier += value;
        foreach(TextMeshProUGUI textbox in multTexts)
        {
            textbox.text = "x" + multiplier.ToString();
        }
    }

    private void LerpMultTextcolor()
    {
        float effectTimer = 0.25f;
        Timer timer = Timer.Register
        (
            duration: effectTimer,
            onComplete: () => LerpBackMultTextcolor(),
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / effectTimer;
                progress = progress * progress * (3f - 2f * progress);
                Color newColor = Color.Lerp(multDefaultColor, multGlowColor, progress);
                foreach(TextMeshProUGUI textbox in multTexts)
                {
                    textbox.fontSize = Mathf.Lerp(multDefaultFontSize, multBigFontSize, progress);
                }
                multTexts[1].color = newColor;
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }

    private void LerpBackMultTextcolor()
    {
        float effectTimer = 0.25f;
        Timer timer = Timer.Register
        (
            duration: effectTimer,
            onComplete: () => SetMultTextColor(multDefaultColor),
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / effectTimer;
                progress = progress * progress * (3f - 2f * progress);
                Color newColor = Color.Lerp(multGlowColor, multDefaultColor, progress);
                foreach (TextMeshProUGUI textbox in multTexts)
                {
                    textbox.fontSize = Mathf.Lerp(multBigFontSize, multDefaultFontSize, progress);
                }
                multTexts[1].color = newColor;
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }


    private void SetMultTextColor(Color newColor)
    {
        multTexts[0].color = newColor;
        multTexts[1].fontSize = multDefaultFontSize;
    }

    public void ResetMultiplier()
    {
        multiplier = 1.0f;
        foreach (TextMeshProUGUI textbox in multTexts)
        {
            textbox.text = "x" + multiplier.ToString();
        }
    }

    private void LerpscoreTextcolor()
    {
        float effectTimer = 0.15f;
        Timer timer = Timer.Register
        (
            duration: effectTimer,
            onComplete: () => LerpBackScoreTextcolor(),
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / effectTimer;
                progress = progress * progress * (3f - 2f * progress);
                Color newColor = Color.Lerp(multDefaultColor, multGlowColor, progress);
                foreach (TextMeshProUGUI textbox in scoreTexts)
                {
                    textbox.fontSize = Mathf.Lerp(scoreDefaultFontSize, scoreBigFontSize, progress);
                }
                scoreTexts[1].color = newColor;
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }

    private void LerpBackScoreTextcolor()
    {
        float effectTimer = 0.15f;
        Timer timer = Timer.Register
        (
            duration: effectTimer,
            onComplete: () => SetMultTextColor(multDefaultColor),
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / effectTimer;
                progress = progress * progress * (3f - 2f * progress);
                Color newColor = Color.Lerp(multGlowColor, multDefaultColor, progress);
                foreach (TextMeshProUGUI textbox in scoreTexts)
                {
                    textbox.fontSize = Mathf.Lerp(scoreBigFontSize, scoreDefaultFontSize, progress);
                }
                scoreTexts[1].color = newColor;
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }

    public float GetScore()
    {
        return score;
    }
}
