using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class PulseColor : MonoBehaviour
{
	public Gradient gradient;
	public float speedMultiplier = 1f;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		//Assert.IsNotNull()
	}

	private void Update()
	{
		spriteRenderer.color = gradient.Evaluate(Mathf.PingPong(Time.time * speedMultiplier, 1f));
	}
}

