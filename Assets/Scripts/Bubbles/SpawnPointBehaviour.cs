using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPointBehaviour : MonoBehaviour
{
    [SerializeField] private BubbleRequestEventChannelSO channel;
    [SerializeField] private List<TimerTier> timerTiers = new List<TimerTier>();
    private List<TimerTier> _tmrTrs = new();

    private void OnEnable()
    {
        PopulateTiers();
        SpawnAtRandomIntervals(0);
        TierShift();
    }

    private void PopulateTiers()
    {
        _tmrTrs.Clear();
        foreach(TimerTier t in timerTiers)
        {
            _tmrTrs.Add(t);
        }
    }

    private void SpawnAtRandomIntervals(float previousInflateTime)
    {
        float randomInterval = Random.Range(_tmrTrs[0].intervalRange.x + previousInflateTime, _tmrTrs[0].intervalRange.y);
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
        if (_tmrTrs.Count > 0)
        {
            int ind = Random.Range(0, _tmrTrs[0].bubbles.Count);
            channel.RaiseEvent(new(transform, _tmrTrs[0].bubbles[ind]));
            SpawnAtRandomIntervals(_tmrTrs[0].bubbles[ind].inflationTime);
        }
    }
    private void TierShift()
    {
        float startFloat = _tmrTrs[0].intervalRange.y;
        float dur = _tmrTrs[0].timeUntilNextTier;
        Timer timer = Timer.Register
        (
            duration: dur,
            onComplete: () => LoopTS(),
            isLooped: false,
            useRealTime: true
        );
    }

    private void LoopTS()
    {
        _tmrTrs.RemoveAt(0);
        if (_tmrTrs.Count > 0)
        {
            TierShift();
        }
    }
}

[System.Serializable]
public class TimerTier
{
    public Vector2 intervalRange = new(1, 20);
    public float timeUntilNextTier = 300f;
    public List<BaseBubbleSO> bubbles;
    public UnityEvent onEnd;
}
