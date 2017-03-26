using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void CallBack();
public delegate void CallBack<T>(T t);
public delegate void CallBack<T,K>(T t,K k);

public enum GameStatus
{
    Playing,Over
}

public class GameController : MonoBehaviour {

    private static GameController instance;

    private static GameController CreateInstance()
    {
        var go = GameObject.Find("GameController");
        if (null == go)
        {
            go = new GameObject("GameController");
        }
        return go.AddComponent<GameController>();
    }

    public static GameController Instance
    {
        get
        {
            if(null == instance)
            {
                instance = CreateInstance();
            }
            return instance;
        }
    }

    public GameStatus gameStaus = GameStatus.Playing;

    public CallBack MouseDown;
    public CallBack MouseUp;
    public CallBack<string> MagicGesture;
    public CallBack AddScore;
    public CallBack GameOver;

    public Sprite[] balloonSprites;
    public Dictionary<string, Color> balloonColorDict = new Dictionary<string, Color>()
    {
        {"Dark1",new Color(232f / 255f, 129f / 255f, 255f / 255f)},
        {"Dark2",new Color(232f / 255f, 129f / 255f, 255f / 255f)},
        {"Dark3",new Color(232f / 255f, 129f / 255f, 255f / 255f)},
        {"Delta",new Color(146f/255f,246f/255f,255f/255f)},
        {"Gamma",new Color(255f/255f,211f/255f,66f/255f)},
        {"Horizline",new Color(0f/255f,191f/255f,160f/255f)},
        {"Loop",new Color(174f/255f,164f/255f,141f/255f)},
        {"V",new Color(255f/255f,211f/255f,66f/255f)},
        {"Vertline", new Color(255f/255f,88f/255f,168f/255f)},
        {"Z",new Color(236f/255f,54f/255f,54f/255f)}
    };

    void Awake()
    {
        balloonSprites = Resources.LoadAll<Sprite>("balloons");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(null != MouseDown)
            {
                MouseDown();
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(null != MouseUp)
            {
                MouseUp();
            }
        }
    }
}
