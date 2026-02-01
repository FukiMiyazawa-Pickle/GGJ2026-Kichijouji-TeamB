using UnityEngine;
using UnityEngine.InputSystem;

public class ResultCtrl : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBGM("Result");
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        if (FadeManager.Instance.IsFading)
        {
            return;
        }

        FadeManager.Instance.LoadSceneWithFade("Title");
    }
}
