using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NewMultiplierFactory", menuName = "Factory/Multiplier Factory")]
public class MultiplierFactorySO : FactorySO<MultiplierPopupBehaviour>
{
    public MultiplierPopupBehaviour multiplier;
    public override MultiplierPopupBehaviour Create()
    {
        return Instantiate(multiplier);
    }
}