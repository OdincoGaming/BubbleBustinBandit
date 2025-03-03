using UnityEngine;
using TMPro;
public class FPS : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private int limiter = 0;
    private void Update()
    {
        limiter++;
        if(limiter % 15 == 0)
        {
            textMeshPro.text = (1 / Time.deltaTime).ToString();
            limiter = 0;
        }
    }
}
