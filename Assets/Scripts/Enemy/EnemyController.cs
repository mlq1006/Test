using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType
{
    Normal, Big, Dark, Item
}

public class EnemyController : MonoBehaviour {

    private Animator animator;

    private Rigidbody2D rigidBody2D;
    private float speed; //移动速度
    private float drag;//气球的阻力
    public float landPosY;

    private Transform linepos;

    private EnemyType enemyType = EnemyType.Normal;//enemy类型
    private int enemyIndex;//normal 

    public GameObject enemyParticlePrefab;//enemydestroy 身体部件
    public GameObject balloonPrefab; //气球的预设
    List<GameObject> balloonList = new List<GameObject>();

    void Awake()
    {
        animator = this.GetComponent<Animator>();
        linepos = transform.FindChild("linepos");

        speed = 1.5f;
        drag = 1.3f;
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
            rigidBody2D.velocity = Vector2.zero;
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

            rigidBody2D.velocity = new Vector2(0, -1 *currentSpeed);
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

    public void SetEnemyTypeAndIndex(EnemyType type, int index = 0)
    {
        enemyType = type;
        if (enemyType.Equals(EnemyType.Normal))
        {
            enemyIndex = index;
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
        Sprite[] bodyParticle = new Sprite[0];
        if (enemyType == EnemyType.Normal)
        {
            bodyParticle = Resources.LoadAll<Sprite>("enemyParticle/Normal/Other");
            Sprite[] headParticle = Resources.LoadAll<Sprite>("enemyParticle/Normal/Head");
            GameObject go = Instantiate(enemyParticlePrefab, transform.position, Quaternion.identity) as GameObject;
            go.GetComponent<SpriteRenderer>().sprite = headParticle[enemyIndex];
        }
        else if (enemyType == EnemyType.Big)
        {
            bodyParticle = Resources.LoadAll<Sprite>("enemyParticle/Big");
        }
        else if (enemyType == EnemyType.Dark)
        {
            bodyParticle = Resources.LoadAll<Sprite>("enemyParticle/Dark");
        }

        //分解enemy部件
        for (int i = 0; i < bodyParticle.Length; i++)
        {
            GameObject go = Instantiate(enemyParticlePrefab, transform.position, Quaternion.identity) as GameObject;
            go.GetComponent<SpriteRenderer>().sprite = bodyParticle[i];
        }

        PublicGameData.gameCurrentScore++;
        Destroy(gameObject);
    }
}
