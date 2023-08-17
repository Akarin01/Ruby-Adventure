using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    // Instantiate后会立刻执行Awake
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position.magnitude > 100f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 发射飞弹
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="force"></param>
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 命中Enemy时
        EnemyController enmey = collision.gameObject.GetComponent<EnemyController>();
        // 调用其Fix方法
        enmey?.Fix();

        Destroy(gameObject);
    }
}
