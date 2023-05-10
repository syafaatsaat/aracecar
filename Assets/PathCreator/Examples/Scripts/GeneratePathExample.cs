using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = true;
        public Transform[] waypoints;
        public Material roadMat;
        public Material underMat;

        void Start () 
        {
            if (waypoints.Length > 0) 
            {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);

                PathCreator pc = GetComponent<PathCreator>();
                if (pc != null)
                    pc.bezierPath = bezierPath;

                PathSceneTool patool = FindObjectOfType<PathSceneTool>().GetComponent<PathSceneTool>();
                if (patool != null)
                {
                    patool.pathCreator = pc;
                    patool.TriggerUpdate();
                }
            }
        }
    }
}