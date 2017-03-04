using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float speedTime;
    private Vector3 targetPos;
    private Camera uicamera;
    void Awake()
    {
        uicamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            targetPos = uicamera.ScreenToWorldPoint(Input.mousePosition);
            targetPos.y = transform.position.y;
            targetPos.z = transform.position.z;
            PublicGameData.playerMoveDir = transform.position.x < targetPos.x ? 1 : -1;
            StartCoroutine(StartMove());
        }
    }


    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(0.1f);
        while(Mathf.Abs(transform.position.x - targetPos.x) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position,targetPos,speedTime * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
    }
}
