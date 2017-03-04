using UnityEngine;
using System.Collections;

public class BalloonController : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rigidbody;
    private Transform target;

    public float moveSpeedY;
    public float moveSpeedX;
    public float lineLength;

    void Awake()
    {
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void Init(Transform targetPos)
    {
        target = targetPos;
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

            if(Mathf.Abs(transform.localPosition.x) > 0)
            {
                rigidbody.AddForce((transform.localPosition.x>0?Vector2.left:Vector2.right) * moveSpeedX);
            }else
            {
                rigidbody.Sleep();
            }
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            BalloonPop();
        }
    }

    void BalloonPop()
    {
        //animator.Play(Animator.StringToHash("Pop"),0);
        animator.enabled = true;
    }
}
