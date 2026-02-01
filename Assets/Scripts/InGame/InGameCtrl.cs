using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameCtrl : MonoBehaviour
{
    [SerializeField] private TimerCtrl _timerCtrl;

    private List<CharacterCtrl> _charaList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 一旦2人分取得して先頭のプレイヤーを鬼にしておく
        _charaList = FindObjectsByType<CharacterCtrl>(FindObjectsSortMode.None).ToList();

        _charaList[0].ChangePlayerType(CharacterCtrl.CharacterType.Seek);

        for (int i = 1; i < _charaList.Count; i++)
        {
            _charaList[i].ChangePlayerType(CharacterCtrl.CharacterType.Hide);
        }

        _timerCtrl.IsCountdown = true;

        SoundManager.Instance.PlayBGM("InGame");
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (FadeManager.Instance.IsFading)
        {
            return;
        }

        if (IsGameFinish())
        {
            foreach (CharacterCtrl c in _charaList)
            {
                Destroy(c.gameObject);
            }

            FadeManager.Instance.LoadSceneWithFade("Result");
        }
    }

    private bool IsGameFinish()
    {
        var allCharaDisable = true;
        foreach (CharacterCtrl c in _charaList)
        {
            if(c.GetPlayerType() == CharacterCtrl.CharacterType.Seek)
            {
                continue;
            }
            allCharaDisable &= !c.gameObject.activeSelf;
        }

        return _timerCtrl.CurrentTime <= 0.0f || allCharaDisable;
    }
}
