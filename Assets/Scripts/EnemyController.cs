using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;

    public float changeTime;
    private float timer;
    private int direction = 1;

    private Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        timer = changeTime;
    }

    private void FixedUpdate()
    {
        // 修改刚体位置
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * Time.deltaTime * direction;
        }
        else
        {
            position.x = position.x + speed * Time.deltaTime * direction;
        }

        rigidbody2d.MovePosition(position);
    }

    void Update()
    {
        // 倒计时
        timer -= Time.deltaTime;
        // 当倒计时结束
        if (timer < 0f)
        {
            // 修改移动方向
            direction = -direction;
            // 重置倒计时
            timer = changeTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController player = collision.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
