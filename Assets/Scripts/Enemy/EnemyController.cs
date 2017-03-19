using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    private Animator animator;

    private Rigidbody2D rigidBody2D;
    private float speed; //移动速度
    private float drag;//气球的阻力

    private Transform linepos;

    public GameObject balloonPrefab; //气球的预设

    List<GameObject> balloonList = new List<GameObject>();

    void Awake()
    {
        animator = this.GetComponent<Animator>();
        linepos = transform.FindChild("linepos");

        speed = 1f;
        drag = 0.9f;
        rigidBody2D = this.GetComponent<Rigidbody2D>();
        balloonList.Clear();
    }

    void FixedUpdate()
    {
        float currentSpeed = speed;
        if (balloonList.Count > 0)
        {
            currentSpeed -= drag;
        }

        rigidBody2D.AddForce(Vector2.down * currentSpeed, ForceMode2D.Force);

        if(Input.GetKeyDown(KeyCode.J))
        {
            //TODO 落体
            PlayLand();
        }

         if(Input.GetKeyDown(KeyCode.K))
        {
            //TODO 落体
            PlayPop();
        }
      
    }

    public void CreateBalloon(int balloonCount,bool isDark = false)
    {
        int count = balloonList.Count;
        int numDir = count % 2 == 0 ? 1 : -1;
        GameObject go = Instantiate(balloonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        go.GetComponent<BalloonController>().Init(linepos);
        go.transform.parent = this.transform;
        go.transform.localPosition = new Vector3(numDir * 0.15f, 0, 0);
        go.transform.localScale = Vector3.one;
        balloonList.Add(go);
    }

    public void PlayLand()
    {
        animator.SetBool("land",true);
    }

    public void PlayPop()
    {
        animator.SetBool("pop",true);
    }
}
