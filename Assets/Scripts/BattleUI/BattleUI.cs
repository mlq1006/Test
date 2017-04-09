using UnityEngine;
using System.Collections;

public class BattleUI : MonoBehaviour {

    public MySpriteNum currentScore;
    public GameObject battleMenu;

    void Awake()
    {
        currentScore.Num = 0;
        GameController.Instance.AddScore += ChangeScore;
        battleMenu.gameObject.SetActive(false);
    }

    void ChangeScore()
    {
        currentScore.Num = PublicGameData.gameCurrentScore;
    }

    public void ShowBattleMenu()
    {
        Time.timeScale = 0;
        battleMenu.gameObject.SetActive(true);
    }


}
