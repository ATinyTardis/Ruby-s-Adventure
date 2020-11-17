using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController ruby = other.GetComponent<PlayerController>();
        if (ruby != null&&ruby.health<ruby.maxHealth)
        {
            ruby.ChangeHealth(1);
            Destroy(gameObject);
            ruby.PlaySound(collectedClip);
        }
    }
}
