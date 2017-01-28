using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TestTween : MonoBehaviour
{
    public Transform target;
    public PathType pathType = PathType.CatmullRom;
    public Vector3[] waypoints = new[] {
        new Vector3(-20, 0f, 0),
        new Vector3(-15, 0f, 0),
        new Vector3(-10, 0f, 0),
        new Vector3(-5, 0f, 0),
        new Vector3(0, 0, 0),
        new Vector3(5.9f, -2f, 3f),
        new Vector3(10.3f, -4f, 6f),
        new Vector3(15.5f, -6f, 9f),
        new Vector3(20.5f, -8f, 12),
        new Vector3(25.5f, -6f, 15),
        new Vector3(30.5f, -4f, 18),
        new Vector3(35.5f, -2f, 21),
        new Vector3(40.5f, 0f, 30),
        new Vector3(45.5f, -2f, 40),
        new Vector3(47f, -4f, 40),
        new Vector3(55.5f, -6f, 30),
        new Vector3(60.5f, -8f, 30),
        new Vector3(65.5f, -6f, 15),
        new Vector3(70.5f, -4f, 0),
        new Vector3(80.1721f, -19.8289f, 15),
        new Vector3(94.5753f, -91.8645f, 30),
        new Vector3(103.816f, 0.0f, 45),
        new Vector3(116.448f, 0.0f, 70),
        new Vector3(129.919f, 0.0f, 85),
        new Vector3(146.244f, 0.0f, 100),
        new Vector3(163.287f, 0.0f, 100.069f),
        new Vector3(191.751f, 0.0f, 107),
        new Vector3(216.974f, 0.0f, 125),
        new Vector3(234.099f, 0.0f, 169),
        new Vector3(254.964f, 0.0f, 189),
        new Vector3(403.15f, 0.0f, 231.987f),
        new Vector3(403.15f, 0.0f, 231.987f),
        new Vector3(403.15f, 0.0f, 231.987f)
	};

    void Start()
    {
        // Create a path tween using the given pathType, Linear or CatmullRom (curved).
        // Use SetOptions to close the path
        // and SetLookAt to make the target orient to the path itself
        Tween t = target.DOPath(waypoints, 70, pathType, PathMode.Full3D, 10, Color.cyan)
            .SetOptions(true)
            .SetLookAt(0.001f);
        // Then set the ease to Linear and use infinite loops
        t.SetEase(Ease.Linear).SetLoops(-1);
    }
}