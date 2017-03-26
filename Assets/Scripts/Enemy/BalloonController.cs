using UnityEngine;
using System.Collections;

public class BalloonController : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidbody;
    private Transform target;
    private string balloonName;

    public float moveSpeedY;
    public float moveSpeedX;
    public float lineLength;

    void Awake()
    {
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        GameController.Instance.MagicGesture += BalloonPop;
    }

    public void Init(Transform targetPos,Sprite sprite)
    {
        target = targetPos;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        balloonName = sprite.name;
    }

    void Update()
    {
        if (null != target)
        {
            float distance = Vector3.Distance(target.localPosition,transform.localPosition);
            if (distance < lineLength)
            {
                rigidbody.AddForce(Vector2.up * moveSpeedY);
            }
            else
            {
                rigidbody.Sleep();
            }

            //if (Mathf.Abs(transform.localPosition.x) > 0.1f)
            //{
            //    rigidbody.AddForce((transform.localPosition.x > 0.1f ? Vector2.left : Vector2.right) * moveSpeedX);
            //}
            //else
            //{
            //    rigidbody.Sleep();
            //}
        }

        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    BalloonPop();
        //}
    }

    void BalloonPop(string name)
    {
        Debug.Log("magicName:" + name);
        if (name.Equals(balloonName))
        {
            StartCoroutine(DestroySelf());
        }
    }

    IEnumerator DestroySelf()
    {
        //animator.Play(Animator.StringToHash("Pop"),0);
        animator.enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = GameController.Instance.balloonColorDict[balloonName];
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        this.transform.parent.GetComponent<EnemyController>().BalloonDestroy(gameObject);
        GameController.Instance.MagicGesture -= BalloonPop;
        Destroy(gameObject);
    }
}
