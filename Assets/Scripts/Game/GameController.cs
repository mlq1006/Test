using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public delegate void EventHandler();
    public EventHandler MouseDown;
    public EventHandler MouseUp;


    void Awake()
    {
        instance = this;
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
