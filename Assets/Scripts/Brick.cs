using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
	[Tooltip("Should we cause the camera to shake, if the ball hits this brick?")] //Om du håller muspekaren över texten i editor så får du den här texten
	public bool causeCameraShake = false;
	public bool isBreakable = true;

	[Tooltip("Numbers of sprites  = numbers of hits the specific brick can take")]
	public List<Sprite> sprites = new List<Sprite>();

	private SpriteRenderer spriteRenderer;

	public static int bricksDestroyed = 0;

	public void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		Assert.IsNotNull(spriteRenderer, "Failed to find sprite rendewre component");
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(causeCameraShake)
		{
			GameCamera.instance.cameraShake.Shake();
		}

		if(!isBreakable)
		{ return; }

		if(sprites.Count > 0)
		{
			sprites.RemoveAt(0);
			if(sprites.Count > 0)
			{
				spriteRenderer.sprite = sprites[0];
			}
			else
			{
				bricksDestroyed++; //keeps count of how many bricks are destroyed
				if(bricksDestroyed % Gamemode.instance.spawnBallForEveryBrickDestoryed == 0) //in every third ball this will execute
				{
					Instantiate(Gamemode.instance.ballPrefab, transform.position, Quaternion.identity);
				}
				Destroy(gameObject);
			}
		}
	}
}

