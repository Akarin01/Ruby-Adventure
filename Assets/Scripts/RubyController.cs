using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed;

    public int maxHealth;
    public int health { get { return currentHealth; } }
    private int currentHealth;

    public float timeInvincible;
    private float invincibleTimer;
    private bool isInvincible;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    private Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // 修改刚体的position
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    void Update()
    {
        // 获取轴输入
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        // 如果x,y不全为0
        if(!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            // 设置lookDirection
            lookDirection.Set(move.x, move.y);
            // 归一化
            lookDirection.Normalize();
        }

        // 设置动画机参数
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // 无敌时间倒计时
        // 如果处于无敌状态
        if (isInvincible)
        {
            // 倒计时
            invincibleTimer -= Time.deltaTime;
            // 倒计时结束时
            if (invincibleTimer < 0f)
            {
                // 取消无敌状态
                isInvincible = false;
            }
        }
    }

    /// <summary>
    /// 修改生命值
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth(int amount)
    {
        // 如果玩家受伤
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            // 如果处于无敌状态
            if (isInvincible)
                return;

            // 否则，进入无敌状态，重置倒计时
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        // 修改玩家生命值
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"{currentHealth}/{maxHealth}");
    }
}
