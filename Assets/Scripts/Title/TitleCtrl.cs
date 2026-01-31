using UnityEngine;
using UnityEngine.InputSystem;

public class TitleCtrl : MonoBehaviour
{
    public void OnStart(InputAction.CallbackContext context)
    {
		if (FadeManager.Instance.IsFading)
		{
			return;
		}

		FadeManager.Instance.LoadSceneWithFade("InGame");
	}
}
