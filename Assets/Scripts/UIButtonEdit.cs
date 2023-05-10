using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIButtonEdit : MonoBehaviour
{
    //public GameObject startButton;
    public GameObject editButton;
    public Animator animator;

    public GameObject gm;
    public GameObject sessionOrigin;

    bool isShown = false;

    void Start()
    {
    }

    void Update()
    {
    }


    public void EditSlideUp()
    {
        animator.SetTrigger("clickedtest");
        editButton.GetComponent<Button>().interactable = true;
    }

    public void AddButton()
    {
        // do the adding stuff here
        // SetAnnouncer("Add");

        // the other function for adding waypoint
        gm.GetComponent<GameManager>().ChangeGameState(GameManager.GameStates.ADD);

    }

    public void RemoveButton()
    {
        // do the removing stuff here
        // SetAnnouncer("Remove");

        // the other function for removing waypoint
         gm.GetComponent<GameManager>().ChangeGameState(GameManager.GameStates.DELETE);
    }

    public void DestroyAll()
    {
        gm.GetComponent<GameManager>().ClearWaypoints();
    }
}
