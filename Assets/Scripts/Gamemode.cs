using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameModeIntEvent : UnityEvent<int>
{ }

public class Gamemode : MonoBehaviour
{
	public static Gamemode instance;
	public UnityEngine.GameObject ballPrefab;
	public int spawnBallForEveryBrickDestoryed = 3;

	public int winSceneIndex;
	public int loseScenIndex;

	private int ballsInPlay;
	private int starsInPlay;

	public GameModeIntEvent onBallsChanged;
	public GameModeIntEvent onStarsChanged;

	private void Awake()
	{
		if (!instance)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void OnBallAdded()
	{
		ballsInPlay++;
		onBallsChanged.Invoke(ballsInPlay);
	}

	public void OnBallRemove()
	{
		ballsInPlay--;
		onBallsChanged.Invoke(ballsInPlay);
		if(ballsInPlay <= 0)
		{
			SceneManager.LoadScene(loseScenIndex);
		}
		
	}

	public void OnStarsAdded()
	{
		starsInPlay++;
		onStarsChanged.Invoke(starsInPlay);
	}

	public void OnStarsRemove()
	{
		starsInPlay--;
		onStarsChanged.Invoke(starsInPlay);
		if(starsInPlay <= 0)
		{
			SceneManager.LoadScene(winSceneIndex);
		}
	}
}


