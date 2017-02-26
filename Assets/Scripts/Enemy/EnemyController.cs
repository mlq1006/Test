using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D rigidBody2D;
    private float speed; //移动速度
    private float drag;//气球的阻力

    public GameObject balloonPrefab; //气球的预设

    List<GameObject> balloonList = new List<GameObject>();

    void Awake()
    {
        speed = 1f;
        drag = 0.9f;
        rigidBody2D = this.GetComponent<Rigidbody2D>();
        CreateBalloon();
    }

    void FixedUpdate()
    {
        float currentSpeed = speed;
        if (balloonList.Count > 0)
        {
            currentSpeed -= drag;
        }

        rigidBody2D.AddForce(Vector2.down * currentSpeed, ForceMode2D.Force);
    }

    public void CreateBalloon()
    {
        balloonList.Clear();
        GameObject go = Instantiate(balloonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.parent = this.transform;
        go.transform.localPosition = Vector3.zero;
        balloonList.Add(go);
    }

}
