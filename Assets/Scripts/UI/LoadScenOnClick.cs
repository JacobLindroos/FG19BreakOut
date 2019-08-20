using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenOnClick : MonoBehaviour
{

	public void OnClick(int SceneToLoad)
	{
		SceneManager.LoadScene(SceneToLoad);
		Debug.Log($"Load scene {SceneToLoad}");
	}
}

