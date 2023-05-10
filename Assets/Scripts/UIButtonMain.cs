using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIButtonMain : MonoBehaviour
{
    public CanvasGroup cgMain;
    public CanvasGroup cgStart;

    public GameObject gm;

    public GameObject editButton;
    public GameObject stopButton;
    public GameObject playButton;
    public GameObject editPanel;

    public Animator announceAnimator;
    public GameObject announcer;

    public GameObject sessionOrigin;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("PathCreator");
    }

    void Update()
    {
       if(cgMain.alpha == 1)
       {
            cgStart.gameObject.SetActive(false);
       }
    }


    public void Play()
    {
        editButton.GetComponent<Button>().interactable = true;

        // pathGenerator.GetComponent<ARPathGenerator>().GeneratePath();

        playButton.GetComponent<Button>().interactable = false;
        playButton.gameObject.SetActive(false);

        stopButton.gameObject.SetActive(true);
        stopButton.GetComponent<Button>().interactable = true;

        editPanel.gameObject.SetActive(false);

        gm.GetComponent<GameManager>().PlayButtonPressed();
       
        
    }

    public void Info()
    {
            
    }

    public void Stop()
    {
        stopButton.gameObject.SetActive(false);
        stopButton.GetComponent<Button>().interactable = false;

        playButton.GetComponent<Button>().interactable = true;
        playButton.gameObject.SetActive(true);

        editPanel.gameObject.SetActive(true);

        gm.GetComponent<GameManager>().StopButtonPressed();
    }

    /*
    public void Edit()
    {
        //SetAnnouncer("Edit");

        // adding onclick listener event
        addBtn = addButton.GetComponent<Button>();
        addBtn.onClick.AddListener(AddButton);

        // adding onclick listener event
        removeBtn = removeButton.GetComponent<Button>();
        removeBtn.onClick.AddListener(RemoveButton);
        editButton.GetComponent<Button>().interactable = false;
        cgEdit.alpha = 1;
        cEdit.sortingOrder = 999;
        cgEdit.interactable = true;
        cgMain.alpha = 0;
        
        gm.GetComponent<GameManager>().ChangeGameState(GameManager.GameStates.EDIT);

    }
    */
    public void SetAnnouncer(string name)
    {
        announcer.GetComponent<Text>().text = name;
        announceAnimator.SetTrigger("announce");
    }
}