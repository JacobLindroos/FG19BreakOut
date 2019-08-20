using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]

public class Star : MonoBehaviour
{
	[SerializeField] private Sprite normalMouth = null;
	[SerializeField] private Sprite worriedMouth = null;
	public LayerMask reactToLayer;

	private CircleCollider2D awarness;
	private SpriteRenderer mouth;
	private LookAtTarget leftEye;
	private LookAtTarget rightEye;

	private List<GameObject> activeScaryObjects = new List<GameObject>();

	private void Awake()
	{
		awarness = GetComponent<CircleCollider2D>();

		Transform go = transform.Find("LeftEye/Pupil");
		Assert.IsNotNull(go, "Failed to locate child \"LeftEye/Pupil\".");
		//Assert.IsNotNull(go, @"\"); @-sign = you will type out one backslash otherwise you need to have two backslashes
		leftEye = go.GetComponent<LookAtTarget>();
		Assert.IsNotNull(leftEye, "Failed to locate Look at mouse component");

		go = transform.Find("RightEye/Pupil");
		Assert.IsNotNull(go, "Failed to locate child \"RightEye/Pupil\".");
		//Assert.IsNotNull(go, @"\"); @-sign = you will type out one backslash otherwise you need to have two backslashes
		rightEye = go.GetComponent<LookAtTarget>();
		Assert.IsNotNull(rightEye, "Failed to locate Look at mouse component");

		go = transform.Find("Mouth");
		Assert.IsNotNull(go, "Failed to locate child \"Mouth\".");
		//Assert.IsNotNull(go, @"\"); @-sign = you will type out one backslash otherwise you need to have two backslashes
		mouth = go.GetComponent<SpriteRenderer>();
		Assert.IsNotNull(mouth, "Failed to locate SpriteRenderer component on mouth");
	}

	private void Start()
	{
		Gamemode.instance.OnStarsAdded();
	}

	private void OnDestroy()
	{
		Gamemode.instance.OnStarsRemove();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		activeScaryObjects.Add(collision.gameObject);
		UpdateTarget();

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		activeScaryObjects.Remove(collision.gameObject);
		UpdateTarget();
	}

	private void UpdateTarget()
	{
		if(activeScaryObjects.Count > 0)
		{
			Transform target = transform.GetClosestObject(ref activeScaryObjects);
			SetWorried(target);
			
		}
		else
		{
			//Todo: Set scared.
		}
	}

	public void SetWorried(Transform target)
	{
		mouth.sprite = worriedMouth;
		leftEye.target = target;
		rightEye.target = target;
	}

	public void SetCool()
	{
		mouth.sprite = normalMouth;
		leftEye.target = null;
		rightEye.target = null;
		leftEye.transform.localPosition = Vector3.zero;
		rightEye.transform.localPosition = Vector3.zero;
	}
}

