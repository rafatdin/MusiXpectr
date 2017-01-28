using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MoveTween : MonoBehaviour {

    public Transform target;
    public PathType pathType = PathType.CatmullRom;
    public Vector3[] waypoints = new Vector3[GlobalData.samples.Length/88400];

	// Use this for initialization
	void Start () {
        GenerateWaypoints();

        Tween t = target.DOPath(waypoints, GlobalData.samples.Length/88400, pathType, PathMode.Full3D, 10, Color.cyan)
            .SetOptions(false)
            .SetLookAt(0.001f);
        // Then set the ease to Linear and use infinite loops
        t.SetEase(Ease.Linear).SetLoops(-1);
	}


    void GenerateWaypoints()
    {
        float[] averages = GlobalData.processedSamples;
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = new Vector3(i * 15, averages[i] * 100, 0);
        }
    }
}
