using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class UIButtonStart : MonoBehaviour
{
    public CanvasGroup cgStart;
    public CanvasGroup cgMain;

    public Animator animator;
    public Animator animator2;

    void Start()
    {
        cgMain.alpha = 0.0f;
        cgMain.interactable = false;
    }

    void Update()
    {
        // to allow the main menu buttons to fade in
        if(cgStart.alpha == 0)
            animator2.SetTrigger("cancomein");
    }


    public void StartMenu()
    {

        // to allow the intro button to fade out
        animator.SetTrigger("clickedliao");
        cgStart.interactable = false;
        cgMain.interactable = true;
    }
    
}
