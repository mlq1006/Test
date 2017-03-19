using UnityEngine;
using System.Collections;

public delegate void CallBack();
public delegate void CallBack<T>(T t);
public delegate void CallBack<T,K>(T t,K k);

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

    public CallBack MouseDown;
    public CallBack MouseUp;
    public CallBack<string> MagicGesture;

    private Sprite[] balloonSprites;

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
