using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2.5f;
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;
    public int cogNum;
    public GameObject helpDialog;

    Rigidbody2D rigidbody2D;
    float moveX;
    public float mx { get { return position.x; } }
    float moveY;
    public float my { get { return position.y; } }
    Vector2 move;
    Vector2 position;
    public Vector2 po { get { return position; } }

    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    bool robotFixed=false;
    public bool isInvincible;
    float invincibleTimer;
    float time=2.0f;

    Animator animator;
    Vector2 lookDirection=new Vector2(1,0);
    
    AudioSource audioSource;
    public AudioClip hit;
    public AudioClip throwCog;
    public AudioClip allFixed;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        helpDialog.SetActive(true);
        cogNum = 5;
    }

    // Update is called once per frame
 
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        move = new Vector2(moveX, moveY);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (Input.GetKeyDown(KeyCode.J)&&cogNum>0) Launch();
        if (Input.GetKeyDown(KeyCode.T))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f,
                LayerMask.GetMask("npc"));
            if (hit.collider != null)
            {
                NPC character = hit.collider.GetComponent<NPC>();
                if (character != null)
                {
                    if(!robotFixed)character.DisplayDialog();
                    if (robotFixed) character.DisplayDialogFinish();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            if (Mathf.Approximately(Time.timeScale, 0.0f)) Time.timeScale = 1;
            else Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            helpDialog.SetActive(true);
            time = 2.0f;
        }
        time -= Time.deltaTime;
        if (time < 0)
        {
            time = 2.0f;
            helpDialog.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.R) || currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
            EnemyController.isfixed = 5;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        GameObject ob = GameObject.Find("Robot");
        EnemyController robot = ob.GetComponent<EnemyController>();
        if (robot.isFixed<=0)
        {
            
            robotFixed = true;
            audioSource.PlayOneShot(allFixed);
        }
    }
    void FixedUpdate() {
        position = rigidbody2D.position;
        position.x = position.x + speed * moveX * Time.deltaTime;
        position.y = position.y + speed * moveY * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }
    public void ChangeHealth(int amount){
        if (amount < 0)
        {
            if (isInvincible) return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(hit);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
        audioSource.PlayOneShot(throwCog);
        cogNum--;
        UICog.instance.changeText(cogNum);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void collectCog()
    {
        cogNum += 3;
        UICog.instance.changeText(cogNum);
    }
}