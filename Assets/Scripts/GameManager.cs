using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    public enum GameStates 
    {
        IDLE,
        MOVE,
        ADD,
        DELETE,
        PREVIEW,
        PLAY,
        STOP
    };
    
    GameStates currState;

    public List<GameObject> waypoints;

    public Material roadMat;
    public Material underRoadMat;

    [SerializeField]
    GameObject ARSessionOrigin;

    public GameObject raceCarPrefab;
    GameObject raceCar;

    // Start is called before the first frame update
    void Start()
    {
        currState = GameStates.IDLE;
        waypoints = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < waypoints.Count; ++i)
        {
            WaypointObject iterWO = waypoints[i].GetComponent<WaypointObject>();
            iterWO.Index = i + 1;
        }
    }

    public void GenerateZePathAndRoad()
    {
        // ------------------------------------------------------ IF GOT PREVIEW, UNCOMMENT THE FOLLOWING CODE
        //if (!ChangeGameState(GameStates.PREVIEW))
        //{
        //    return;
        //}

        gameObject.AddComponent<PathCreation.Examples.GeneratePathExample>();
        GetComponent<PathCreation.Examples.GeneratePathExample>().roadMat = roadMat;
        GetComponent<PathCreation.Examples.GeneratePathExample>().underMat = underRoadMat;

        Transform[] transforms = new Transform[waypoints.Count];
        for (int i = 0; i < waypoints.Count; ++i)
        {
            transforms[i] = waypoints[i].transform;
        }

        GetComponent<PathCreation.Examples.GeneratePathExample>().waypoints = transforms;        
    }

    public void PlayButtonPressed()
    {
        if (!ChangeGameState(GameStates.PLAY))
        {
            return;
        }

        GenerateZePathAndRoad();
        raceCar = Instantiate(raceCarPrefab);
    }

    public void StopButtonPressed()
    {
        if (!ChangeGameState(GameStates.STOP))
        {
            return;
        }

        if (raceCar != null)
            Destroy(raceCar);

        if (GetComponent<PathCreation.Examples.GeneratePathExample>() != null)
            Destroy(GetComponent<PathCreation.Examples.GeneratePathExample>());

        if (GetComponent<PathCreation.PathCreator>() != null)
            Destroy(GetComponent<PathCreation.PathCreator>());

        GameObject[] roadMesh = GameObject.FindGameObjectsWithTag("RoadMesh");
        foreach (GameObject rm in roadMesh)
        {
            Destroy(rm);
        }
    }

    // Adds a waypoint to the list
    // The first added waypoint will be set as the starting point
    public void AddWaypoint(GameObject instance)
    {
        waypoints.Add(instance);
    }

    // Deletes all waypoints in the list
    public void ClearWaypoints()
    {
        foreach (GameObject waypoint in waypoints)
        {
            Destroy(waypoint);
        }
        waypoints.Clear();
    }

    // Deletes a waypoint in the scene
    public void DeleteWaypoint(GameObject waypoint)
    {
        waypoints.Remove(waypoint);
        Destroy(waypoint);
    }

    // Returns the number of waypoints currently placed in scene
    public int GetWaypointCount()
    {
        return waypoints.Count;
    }

    // Returns the current game state
    public GameStates GetGameState()
    {
        return currState;
    }

    // Changes the game state to the passed in state
    public bool ChangeGameState(GameStates state)
    {
        if (currState == GameStates.IDLE)
        {
            return IdleState(state);
        }
        else if (currState == GameStates.MOVE)
        {
            return MoveState(state);
        }
        else if (currState == GameStates.ADD)
        {
            return AddState(state);
        }
        else if (currState == GameStates.DELETE)
        {
            return DeleteState(state);
        }
        else if (currState == GameStates.PREVIEW)
        {
            return PreviewState(state);
        }
        else if (currState == GameStates.PLAY)
        {
            return PlayState(state);
        }
        else if (currState == GameStates.STOP)
        {
            return StopState(state);
        }

        return false;
    }

    // Idle game state
    bool IdleState(GameStates state)
    {
        if (state == GameStates.MOVE ||
            state == GameStates.ADD || 
            state == GameStates.DELETE)
        {
            
        }
        else if (state == GameStates.PLAY)
        {
            if (waypoints.Count < 2)
            {
                // Cannot start when have less than 2 waypoints
                return false;
            }
        }
        else
        {
            return false;
        }

        currState = state;
        return true;
    }

    // Move game state
    bool MoveState(GameStates state)
    {
        if (state == GameStates.ADD)
        {
            ARSessionOrigin.GetComponent<PlaceObjectOnPlane>().ResetPointers();
        }
        else if (state == GameStates.PLAY)
        {
            if (waypoints.Count < 2)
            {
                // Cannot start when have less than 2 waypoints
                return false;
            }            
        }
        else
        {
            return false;
        }

        currState = state;
        return true;
    }

    // Add game state
    bool AddState(GameStates state)
    {
        if (state == GameStates.STOP ||
            state == GameStates.PREVIEW)
        {
            return false;
        }

        currState = state;
        return true;
    }

    // Delete game state
    bool DeleteState(GameStates state)
    {
        if (state == GameStates.STOP ||
            state == GameStates.PREVIEW)
        {
            return false;
        }

        currState = state;
        return true;
    }

    // Preview game state
    bool PreviewState(GameStates state)
    {
        if (state != GameStates.IDLE)
        {
            return false;
        }

        currState = state;
        return true;
    }

    // Play game state
    bool PlayState(GameStates state)
    {
        if (state != GameStates.STOP)
        {
            return false;
        }

        currState = GameStates.IDLE;
        return true;
    }

    // Stop game state
    bool StopState(GameStates state)
    {
        if (state != GameStates.IDLE)
        {
            return false;
        }

        currState = state;
        return true;
    }
}
