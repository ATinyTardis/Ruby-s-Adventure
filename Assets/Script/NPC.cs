using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject dialogFinish;
    float timerDisplay;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        dialogFinish.SetActive(false);
        timerDisplay = -1.0f;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                
            }
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        this.animator.SetTrigger("Talk");
    }

    public void DisplayDialogFinish()
    {
        dialogBox.SetActive(false);
        dialogFinish.SetActive(true);
    }
}
