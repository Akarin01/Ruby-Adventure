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
    private Animator animator;

    private bool isBroken = true;

    public ParticleSystem smokeEffect;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timer = changeTime;
    }

    private void FixedUpdate()
    {
        if (!isBroken)
            return;

        // �޸ĸ���λ��
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * Time.deltaTime * direction;

            // ���ö���������
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * Time.deltaTime * direction;

            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    void Update()
    {
        if (!isBroken)
            return;

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

    /// <summary>
    /// �޸�����
    /// </summary>
    public void Fix()
    {
        isBroken = false;
        // �ر�����ģ�⣬��ײ�岻��������
        rigidbody2d.simulated = false;
        // ����״̬������
        animator.SetTrigger("Fixed");
        // ֹͣ������Ч
        smokeEffect.Stop();
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
