using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectOnPlane : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    [SerializeField]
    GameObject m_Waypoint;

    [SerializeField]
    GameObject m_GameManager;

    [SerializeField]
    Camera m_ARCamera;

    [SerializeField]
    GameObject DeleteButton;

    GameObject m_PressHoldObject;
    public GameObject m_LastClickedObject;

    Vector2 m_TouchPosition = default;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gm = m_GameManager.GetComponent<GameManager>();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            m_TouchPosition = touch.position;            

            if (touch.phase == TouchPhase.Began)
            {
                // If MOVE or DELETE mode
                if (gm.GetGameState() == GameManager.GameStates.MOVE || gm.GetGameState() == GameManager.GameStates.DELETE)
                {
                    Ray ray = m_ARCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;

                    // Detect object being tapped on
                    if (Physics.Raycast(ray, out hitObject))
                    {
                        m_PressHoldObject = hitObject.transform.gameObject;

                        if (m_PressHoldObject != null)
                        {
                            foreach (GameObject go in m_GameManager.GetComponent<GameManager>().waypoints)
                            {
                                // If object is a waypoint
                                if (go == m_PressHoldObject)
                                {
                                    if (m_LastClickedObject != null)
                                        m_LastClickedObject.GetComponent<WaypointObject>().Selected = false;

                                    m_LastClickedObject = m_PressHoldObject;
                                }

                                // Update all waypoints' selected state
                                go.GetComponent<WaypointObject>().Selected = go == m_LastClickedObject;
                                go.GetComponent<WaypointObject>().PressedHold = go == m_PressHoldObject;
                            }
                        }
                    }
                }

                // If EDIT mode, spawn waypoint at position of finger
                if (gm.GetGameState() == GameManager.GameStates.ADD)
                {
                    if (m_RaycastManager.Raycast(m_TouchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
                    {
                        if (gm.GetWaypointCount() < 10)
                        {
                            Pose hitPose = s_Hits[0].pose;
                            m_PressHoldObject = Instantiate(m_Waypoint, hitPose.position, hitPose.rotation);
                            m_PressHoldObject.GetComponent<WaypointObject>().ARCamera = m_ARCamera;
                            gm.AddWaypoint(m_PressHoldObject);
                        }
                    }
                }
            }

            // When finger is off the screen, object no longer being pressed and hold
            if (touch.phase == TouchPhase.Ended)
            {
                if (m_PressHoldObject != null)
                    m_PressHoldObject.GetComponent<WaypointObject>().PressedHold = false;
            }
        }

        // Check for position of finger on screen
        if (m_RaycastManager.Raycast(m_TouchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = s_Hits[0].pose;

            // Only for MOVE state
            if (gm.GetGameState() == GameManager.GameStates.MOVE)
            {
                if (m_PressHoldObject != null)
                {
                    // Update position to follow finger position on screen
                    if (m_PressHoldObject.GetComponent<WaypointObject>().PressedHold)
                    {
                        m_PressHoldObject.transform.position = hitPose.position;
                    }
                }
            }
        }

        if (gm.GetGameState() == GameManager.GameStates.DELETE)
        {
            if (m_LastClickedObject != null)
            {
                if (m_LastClickedObject.GetComponent<WaypointObject>().Selected)
                {
                    gm.GetComponent<GameManager>().DeleteWaypoint(m_LastClickedObject);
                }
            }
        }        
    }

    public void ResetPointers()
    {
        if (m_LastClickedObject != null)
        {
            if (m_LastClickedObject.GetComponent<WaypointObject>() != null)
            {
                m_LastClickedObject.GetComponent<WaypointObject>().Selected = false;
                m_LastClickedObject.GetComponent<WaypointObject>().PressedHold = false;
            }
        }        

        if (m_PressHoldObject != null)
        {
            if (m_PressHoldObject.GetComponent<WaypointObject>() != null)
            {
                m_PressHoldObject.GetComponent<WaypointObject>().Selected = false;
                m_PressHoldObject.GetComponent<WaypointObject>().PressedHold = false;
            }            
        }

        m_LastClickedObject = null;
        m_PressHoldObject = null;
    }
}
