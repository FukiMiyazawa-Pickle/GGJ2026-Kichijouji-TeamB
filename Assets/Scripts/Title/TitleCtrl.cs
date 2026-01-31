using UnityEngine;
using UnityEngine.InputSystem;

public class TitleCtrl : MonoBehaviour
{
	private void Start()
	{
		SoundManager.Instance.PlayBGM("Title");
	}

	public void OnStart(InputAction.CallbackContext context)
    {
		if (FadeManager.Instance.IsFading)
		{
			return;
		}

		FadeManager.Instance.LoadSceneWithFade("InGame");
	}
}
