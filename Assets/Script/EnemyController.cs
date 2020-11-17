using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.5f;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    public int isFixed {get{ return isfixed; } }

    Rigidbody2D rigidbody2D;

    float timerWalk = 0.4f;
    float timerWalktime;

    public static int isfixed = 5;
    float timer;
    bool broken=true;
    Vector2 moveDirec = new Vector2(0, 0);
    Vector2 lookDirec = new Vector2(0, -1);

    Animator animator;

    AudioSource audioSource;
    public AudioClip walk;
    public AudioClip fix;
    public AudioClip hit;
    

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timerWalktime = 0.0f;
    }
    void Update()
    {
        if (!broken) return;
        if (!Mathf.Approximately(moveDirec.x, 0) || !Mathf.Approximately(moveDirec.y, 0)){
            animator.SetFloat("lookX", moveDirec.x);
            animator.SetFloat("lookY", moveDirec.y);
            lookDirec = moveDirec;
        }
        animator.SetFloat("speed", moveDirec.magnitude);

        if (moveDirec.x != 0 && moveDirec.y != 0)
        {
            
            timerWalktime -= Time.deltaTime;
            
            if (timerWalktime < 0)
            {
                audioSource.PlayOneShot(walk);
                timerWalktime = timerWalk;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!broken) return;
        Vector2 position = rigidbody2D.position;
        GameObject ob = GameObject.Find("Ruby");
        PlayerController ruby = ob.GetComponent<PlayerController>();
        if ((ruby.po - position).magnitude<4)
        {
            if (ruby.mx > position.x) moveDirec.x = 1;
            else if (ruby.mx < position.x) moveDirec.x = -1;
            else moveDirec.x = 0;
            if (ruby.my > position.y) moveDirec.y = 1;
            else if (ruby.my < position.y) moveDirec.y = -1;
            else moveDirec.y = 0;
        }
        else { moveDirec.x = 0;moveDirec.y = 0; }
        
        position = position + Time.deltaTime * speed * moveDirec;
        animator.SetFloat("Move X", moveDirec.x);
        animator.SetFloat("Move Y", moveDirec.y);
        rigidbody2D.MovePosition(position);
    }
    void OnCollisionStay2D(Collision2D other)
    {
        PlayerController ruby = other.gameObject.GetComponent<PlayerController>();
        if (ruby)
        {
            ruby.ChangeHealth(-1);
        }
    }
    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("fixed");
        smokeEffect.Stop();
        isfixed--;
        audioSource.PlayOneShot(fix);
    }
}
