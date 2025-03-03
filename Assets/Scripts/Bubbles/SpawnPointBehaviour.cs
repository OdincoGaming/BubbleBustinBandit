using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour
{
    [SerializeField] private BubbleRequestEventChannelSO channel;
    [SerializeField] private List<BaseBubbleSO> bubbles;
    [SerializeField] private Vector2 intervalRange = new(1, 20);
    [SerializeField] private List<TimerTier> timerTiers = new List<TimerTier>();

    private void OnEnable()
    {
        SpawnAtRandomIntervals(0);
        DifficultyShift();
    }

    private void SpawnAtRandomIntervals(float previousInflateTime)
    {
        float randomInterval = Random.Range(intervalRange.x + previousInflateTime, intervalRange.y);
        Timer timer = Timer.Register
        (
            duration: randomInterval,
            onComplete: () => LoopSARI(),
            isLooped: false,
            useRealTime: true
        );
    }
    private void LoopSARI()
    {
        int ind = Random.Range(0, bubbles.Count);
        channel.RaiseEvent(new(transform, bubbles[ind]));
        SpawnAtRandomIntervals(bubbles[ind].inflationTime);
    }
    private void DifficultyShift()
    {
        float startFloat = intervalRange.y;
        float dur = timerTiers[0].timeUntilNextTier;
        Timer timer = Timer.Register
        (
            duration: dur,
            onComplete: () => LoopDS(),
            onUpdate: secondsElapsed =>
            {
                float progress = secondsElapsed / dur;
                progress = progress * progress * (3f - 2f * progress);
                intervalRange.y = Mathf.Lerp(startFloat, timerTiers[0].maxTimeBetweenSpawns, progress);
                secondsElapsed += Time.deltaTime;
            },
            isLooped: false,
            useRealTime: true
        );
    }

    private void LoopDS()
    {
        intervalRange.y = timerTiers[0].maxTimeBetweenSpawns;

        
        if (timerTiers[0].bubblesToAdd.Count > 0)
        {
            foreach(BaseBubbleSO b in timerTiers[0].bubblesToAdd)
            {
                bubbles.Add(b);
            }
        }


        timerTiers.RemoveAt(0);
        if (timerTiers.Count > 0)
        {
            DifficultyShift();
        }
    }
}

[System.Serializable]
public class TimerTier
{
    public float maxTimeBetweenSpawns = 20;
    public float timeUntilNextTier = 300f;
    public List<BaseBubbleSO> bubblesToAdd;
}
