using UnityEngine;

public class QuitOnClick : MonoBehaviour
{
	public void OnClick()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
	}
}

