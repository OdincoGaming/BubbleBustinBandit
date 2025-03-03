using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleFactory", menuName = "Factory/Bubble Factory")]
public class BubbleFactorySO : FactorySO<BaseBubbleBehaviour>
{
	public BaseBubbleBehaviour Bubble;
	public override BaseBubbleBehaviour Create()
	{
		return Instantiate(Bubble);
	}
}

