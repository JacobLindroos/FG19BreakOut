using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	public float speed = 1f;

	private Rigidbody2D body;
	private AudioSource hitAudio;

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull(body, "Failed to find rigidbody 2D component");

		hitAudio = GetComponent<AudioSource>();
		Assert.IsNotNull(hitAudio, "Failed to find audiosource");
	}

	private void Start()
	{
		Gamemode.instance.OnBallAdded();
	}

	private void OnDestroy()
	{
		Gamemode.instance.OnBallRemove();
	}

	private void FixedUpdate()
	{
		Vector3 velocity = body.velocity.normalized;
		velocity *= speed;
		body.velocity = velocity;
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(hitAudio.isPlaying)
		{
			hitAudio.Stop();
		}
		hitAudio.Play();
	}
}

