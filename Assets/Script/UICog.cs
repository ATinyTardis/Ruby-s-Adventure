using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICog : MonoBehaviour
{
    // Start is called before the first frame update
    public static UICog instance { get; private set; }
    public TMP_Text ob;
    void Awake()
    {
        instance = this;
    }
    public void changeText(int a)
    {
        ob.text = a.ToString();
    }
}
