using UnityEngine;
using System.Collections;

public class EnemyBuilder : MonoBehaviour {

    private const int MAXENEMYCOUNT = 4;
    private const int NORMALENEMY_MAXBALLOONCOUNT = 3;
    private const int BIGENEMY_CREATESCORE = 35;
    private const int DARKENEMY_CREATESCORE = 20;

    private int enemyCount;//当前存在的enemy数量
    private Transform enemyList;

    public float offestX;
    public GameObject[] normalEnemys;//普通enemy 3
    public GameObject bigEnemy;
    public GameObject darkEnemy;

    private bool hasCreate;//该回合是否生成了enemy
    private bool hasDarkEnemyAlive;
    private bool hasBigEnemyAlive;

    void Awake()
    {
        hasCreate = false;
        hasDarkEnemyAlive = false;
        hasBigEnemyAlive = false;
        enemyList = GameObject.FindGameObjectWithTag("EnemyList").transform;
        StartCoroutine(CreateEnemy());
    }

    IEnumerator CreateEnemy()
    {
        yield return new WaitForSeconds(1f);
        while(true)
        {
            if (enemyCount < MAXENEMYCOUNT)
            {
                while (!hasCreate)
                {
                    int index = Random.Range(0, 9);
                    if (index < 3)
                    {
                        CreateNormalEnemy(index);
                    }
                    else if (index < 6)
                    {
                        CreateDarkEnemy();
                    }
                    else
                    {
                        CreateBigEnemy();
                    }
                }
                hasCreate = false;
                yield return new WaitForSeconds(3f);
            }else
            {
                yield return null;
            }
        }
    }

    private void CreateNormalEnemy(int index)
    {
        int balloonCount = PublicGameData.gameCurrentScore / 20 + 1;
        balloonCount = Mathf.Min(balloonCount, NORMALENEMY_MAXBALLOONCOUNT);
        balloonCount = Random.Range(1, balloonCount + 1);

        float offest = Random.Range(-offestX,offestX);
        GameObject go = Instantiate(normalEnemys[index], transform.localPosition + Vector3.right*offest, Quaternion.identity) as GameObject;
        go.GetComponent<EnemyController>().CreateBalloon(balloonCount);
        go.transform.parent = enemyList;
        hasCreate = true;
        enemyCount++;

    }

    private void CreateDarkEnemy()
    {
        if(PublicGameData.gameCurrentScore >= DARKENEMY_CREATESCORE && !hasDarkEnemyAlive)
        {
            float offest = Random.Range(-offestX, offestX);
            GameObject go = Instantiate(darkEnemy, transform.localPosition + Vector3.right * offest, Quaternion.identity) as GameObject;
            go.GetComponent<EnemyController>().CreateBalloon(1);
            go.transform.parent = enemyList;
            hasCreate = true;
            enemyCount++;
            hasDarkEnemyAlive = true;
        }
    }

    private void CreateBigEnemy()
    {
        if (PublicGameData.gameCurrentScore >= BIGENEMY_CREATESCORE && !hasBigEnemyAlive)
        {
            int balloonCount = Random.Range(5, 7);
            float offest = Random.Range(-offestX, offestX);
            GameObject go = Instantiate(bigEnemy, transform.localPosition + Vector3.right * offest, Quaternion.identity) as GameObject;
            go.GetComponent<EnemyController>().CreateBalloon(balloonCount);
            go.transform.parent = enemyList;
            hasCreate = true;
            enemyCount++;
            hasBigEnemyAlive = true;
        }
    }



}
