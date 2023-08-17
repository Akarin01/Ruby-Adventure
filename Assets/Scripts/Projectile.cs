using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    // Instantiate�������ִ��Awake
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
    /// ����ɵ�
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="force"></param>
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����Enemyʱ
        EnemyController enmey = collision.gameObject.GetComponent<EnemyController>();
        // ������Fix����
        enmey?.Fix();

        Destroy(gameObject);
    }
}
