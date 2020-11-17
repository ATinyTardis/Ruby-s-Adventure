using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCollect : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip audioClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController Ruby = other.GetComponent < PlayerController>();
        if (Ruby!=null)
        {
            Debug.Log("cog");
            Ruby.collectCog();
            Ruby.PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
