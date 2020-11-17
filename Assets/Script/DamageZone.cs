using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController ruby = other.GetComponent<PlayerController>();
        if (ruby) ruby.ChangeHealth(-1);
    }
}
