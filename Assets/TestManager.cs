using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public Transform[] waypoints;
    public Material roadMat;
    public Material underMat;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<PathCreation.Examples.GeneratePathExample>();
        GetComponent<PathCreation.Examples.GeneratePathExample>().roadMat = roadMat;
        GetComponent<PathCreation.Examples.GeneratePathExample>().underMat = underMat;

        GetComponent<PathCreation.Examples.GeneratePathExample>().waypoints = waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
