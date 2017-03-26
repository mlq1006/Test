using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    private Animator animator;

    private Rigidbody2D rigidBody2D;
    private float speed; //移动速度
    private float drag;//气球的阻力
    public float landPosY;

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
        int count = balloonList.Count;
        if (transform.position.y <= landPosY)
        {
            if (count == 0)
            {
                EnemyDestroy();
            }
            else
            {
                PlayLand();
            }
            rigidBody2D.Sleep();
        }else
        {
            float currentSpeed = speed;

            if (count > 0)
            {
                currentSpeed -= drag;
            }
            else
            {
                PlayPop();
            }

            rigidBody2D.AddForce(Vector2.down * currentSpeed, ForceMode2D.Force);
        }

        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    //TODO 落体
        //    PlayLand();
        //}

        // if(Input.GetKeyDown(KeyCode.K))
        //{
        //    //TODO 落体
        //    PlayPop();
        //}
      
    }

    public void CreateBalloon(int balloonCount,bool isDark = false)
    {
        int minIndex = isDark ? 0 : 3;
        int maxIndex = isDark ? 3 : GameController.Instance.balloonSprites.Length;
        List<int> indexList = new List<int>();

        for (int i = 0; i < balloonCount; i++)
        {
            int count = balloonList.Count;
            int numDir = count % 2 == 0 ? 1 : -1;
            GameObject go = Instantiate(balloonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            int index = Random.Range(minIndex, maxIndex);
            while (indexList.Contains(index))
            {
                index = Random.Range(minIndex, maxIndex);
            }

            go.GetComponent<BalloonController>().Init(linepos, GameController.Instance.balloonSprites[index]);
            indexList.Add(index);
            go.transform.parent = this.transform;
            go.transform.localPosition = new Vector3(numDir * 0.15f, 0, 0);
            go.transform.localScale = Vector3.one;
            balloonList.Add(go);
        }
    }

    public void BalloonDestroy(GameObject go)
    {
        if (balloonList.Contains(go))
        {
            balloonList.Remove(go);
        }
    }

    public void PlayLand()
    {
        GameController.Instance.gameStaus = GameStatus.Over;
        animator.SetBool("land",true);
    }

    public void PlayPop()
    {
        animator.SetBool("pop",true);
    }

    private void EnemyDestroy()
    {

    }
}
