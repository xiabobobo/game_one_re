using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_platform : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D 变量
    public Transform leftPoint, rightPoint;
    public float speed;
    private bool faceLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren(); // 获取 Rigidbody2D 组件
        rb.gravityScale = 0f; // 将重力设置为零
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (faceLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x < leftPoint.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightPoint.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }  // 当物体进入触发器时
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 如果是玩家物体
        {
            other.transform.SetParent(transform); // 将玩家物体设为平台的子物体
        }
    }

    // 当物体离开触发器时
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 如果是玩家物体
        {
            other.transform.SetParent(null); // 取消玩家物体的父物体关系
        }
    }
}

