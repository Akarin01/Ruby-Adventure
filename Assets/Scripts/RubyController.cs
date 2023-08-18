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

    public GameObject projectilePrefab;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        // �޸ĸ����position
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    void Update()
    {
        // ��ȡ������
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        // ���x,y��ȫΪ0
        if(!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            // ����lookDirection
            lookDirection.Set(move.x, move.y);
            // ��һ��
            lookDirection.Normalize();
        }

        // ���ö���������
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // �޵�ʱ�䵹��ʱ
        // ��������޵�״̬
        if (isInvincible)
        {
            // ����ʱ
            invincibleTimer -= Time.deltaTime;
            // ����ʱ����ʱ
            if (invincibleTimer < 0f)
            {
                // ȡ���޵�״̬
                isInvincible = false;
            }
        }

        // ������C��
        if (Input.GetKeyDown(KeyCode.C))
        {
            // ����Launch����
            Launch();
        }
    }

    void Launch()
    {
        // ʵ����projectileProfab
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        // ��ȡ���������ϵ�projectile���
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        // ���ø������Launch����
        projectile.Launch(lookDirection, 300f);

        // ���ö���������
        animator.SetTrigger("Launch");
    }

    /// <summary>
    /// �޸�����ֵ
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth(int amount)
    {
        // ����������
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            // ��������޵�״̬
            if (isInvincible)
                return;

            // ���򣬽����޵�״̬�����õ���ʱ
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        // �޸��������ֵ
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        // ����UIHealthBar��ֵ
        UIHealthBar.Instance.SetValue((float)currentHealth / maxHealth);
    }
}
