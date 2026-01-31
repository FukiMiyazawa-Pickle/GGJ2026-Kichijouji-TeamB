using UnityEngine;

public class InGameCtrl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 一旦2人分取得して先頭のプレイヤーを鬼にしておく
        var players = FindObjectsByType<CharacterCtrl>(FindObjectsSortMode.None);

        players[0].ChangePlayerType(CharacterCtrl.CharacterType.Seek);

        for (int i = 1; i < players.Length; i++)
        {
            players[i].ChangePlayerType(CharacterCtrl.CharacterType.Hide);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
