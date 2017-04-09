using UnityEngine;
using System.Collections;

public class PublicGameData {

    private static int _gameCurrentScore;//游戏当前分数
    public static int gameCurrentScore
    {
        get{return _gameCurrentScore;}
        set
        {
            _gameCurrentScore = value;
            if(null != GameController.Instance.AddScore)
            {
                GameController.Instance.AddScore();
            }
        }
    }

    public static int playerMoveDir;//角色移动方向 1 向右 -1 向左

}
