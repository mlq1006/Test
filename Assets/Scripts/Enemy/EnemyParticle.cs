using UnityEngine;
using System.Collections;

public class EnemyParticle : MonoBehaviour {

    private Rigidbody2D rigid;

    public float minangle;
    public float maxangle;
    public float velocity;
    public float destoryPosY;

    void Awake()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        Init();
    }

    void Init()
    {
        float angle = Random.Range(minangle, maxangle);
        StartCoroutine(TranRotate(angle));
        //transform.Rotate(Vector3.forward, angle);
        int dir = Random.Range(0,2);
        if(0 == dir)
        {
            dir = -1;
        }
        float velocityX = dir * Random.Range(0,0.5f) * velocity;
        float velocityY = Random.Range(0.85f,1f);
        rigid.velocity = new Vector2(velocityX,velocityY);
        rigid.gravityScale = 0.3f;
    }

    void Update()
    {
        if (transform.position.y < destoryPosY)
        {
            Destroy(gameObject);
        }
        
    }

    IEnumerator TranRotate(float angle)
    {
        while(true)
        {
            transform.Rotate(Vector3.forward, angle * Time.deltaTime);
            yield return null;
        }
    }

}
