using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointObject : MonoBehaviour
{
    [SerializeField]
    public Camera ARCamera;

    private Material mat;

    private bool isSelected;
    private bool isPressedHold;

    private int index;

    public bool Selected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
        }
    }

    public bool PressedHold
    {
        get
        {
            return isPressedHold;
        }
        set
        {
            isPressedHold = value;
        }
    }

    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject gm = GameObject.Find("GameManagerEnt");
        GameManager gameManager = gm.GetComponent<GameManager>();

        GameObject childText = transform.GetChild(0).gameObject;
        childText.GetComponent<TextMesh>().text = Index.ToString();

        Vector3 desired_normal_y = transform.position - ARCamera.transform.position;
        childText.transform.rotation = Quaternion.LookRotation(new Vector3(desired_normal_y.x, 0.0f, desired_normal_y.z), Vector3.up);

        if (isSelected && gameManager.GetGameState() == GameManager.GameStates.MOVE)
        {
            GetComponent<cakeslice.Outline>().enabled = true;
        }
        else
        {
            GetComponent<cakeslice.Outline>().enabled = false;
        }
    }
}
