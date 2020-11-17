using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2D;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f) Destroy(gameObject);
    }
    public void Launch(Vector2 direction,float force)
    {
        rigidbody2D.AddForce(direction * force);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemy = other.collider.GetComponent<EnemyController>();
        if (enemy) enemy.Fix();
        Destroy(gameObject);
    }
}
