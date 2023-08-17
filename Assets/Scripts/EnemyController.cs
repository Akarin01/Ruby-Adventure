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
        // �޸ĸ���λ��
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
        // ����ʱ
        timer -= Time.deltaTime;
        // ������ʱ����
        if (timer < 0f)
        {
            // �޸��ƶ�����
            direction = -direction;
            // ���õ���ʱ
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
