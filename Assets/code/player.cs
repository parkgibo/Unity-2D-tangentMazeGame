using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private float speed = 4f;
    Vector3 mousePos, transPos, targetPos, dist;

    void Start()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

    }
    void Update()
    {

        /*float inputX = Input.GetAxisRaw("Horizontal"); 키보드로 움직이는 코드
        float inputY = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime*speed);*/
        if (Input.GetMouseButton(0))
            CalTargetPos();
        RotateMove();

    }
    void CalTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
    }
    void RotateMove()
    {
        dist = targetPos - transform.position;
        transform.position += dist * speed * Time.deltaTime;
        float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent(out item item))
        {
            
        }
        else if (collision.TryGetComponent(out mazewall mazewall))
        {
            
        }
        else if (collision.TryGetComponent(out Wall wall))
        {

        }
    }
}
