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

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

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
            // ��������޵�״̬
            if (isInvincible)
                return;

            // ���򣬽����޵�״̬�����õ���ʱ
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        // �޸��������ֵ
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"{currentHealth}/{maxHealth}");
    }
}
