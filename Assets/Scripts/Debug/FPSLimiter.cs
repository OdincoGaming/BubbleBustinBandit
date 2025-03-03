using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    private void OnEnable()
    {
        Application.runInBackground = false;
        Application.targetFrameRate = 30;
    }
}
