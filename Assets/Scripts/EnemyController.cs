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

        // 修改刚体位置
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * Time.deltaTime * direction;

            // 设置动画机参数
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

    /// <summary>
    /// 修复敌人
    /// </summary>
    public void Fix()
    {
        isBroken = false;
        // 关闭物理模拟，碰撞体不再起作用
        rigidbody2d.simulated = false;
        // 设置状态机参数
        animator.SetTrigger("Fixed");
        // 停止烟雾特效
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
