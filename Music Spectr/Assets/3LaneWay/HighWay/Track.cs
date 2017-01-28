using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour
{
    // Road
    public float width;
    public float height;

    // Rail
    public float railheight;
    public float railwidth;

    // public to help debug in editor
    public int n;
    public Vector3[] points;
    public Vector3[] vs;
    public int[] tris;

    // Start and end to be used by anyone
    public Vector3 start;
    public Vector3 end;

    void Awake()
    {
        // Set the points for track in this function you create
        GenTrackPoints();

        // Create mesh filter
        //---------------MeshFilter meshfilter = this.gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;


        // Create and set the mesh
        Mesh mesh = new Mesh();
        
        

        // Blocks
        n = points.Length - 1;

        // Start and end points
        start = points[0];
        end = points[n - 1];

        // 18 + x8 vertices
        vs = new Vector3[18 + (n - 1) * 8];

        // iterator
        int run = 0;

        float w = 0;
        float h = 0;
        for (int s = 0; s < 8; s++)
        {
            // For the given sequence, set w and h offset from the center
            switch (s)
            {
                case 0:
                    w = -width / 2f;
                    h = height / 2f;
                    break;

                case 1:
                    w = width / 2f;
                    h = height / 2f;
                    break;

                case 2:
                    w = width / 2f;
                    h = height / 2f + railheight;
                    break;

                case 3:
                    w = width / 2f + railwidth;
                    h = height / 2f + railheight;
                    break;

                case 4:
                    w = width / 2f + railwidth;
                    h = -height / 2f;
                    break;

                case 5:
                    w = -width / 2f - railwidth;
                    h = -height / 2f;
                    break;

                case 6:
                    w = -width / 2f - railwidth;
                    h = height / 2f + railheight;
                    break;

                case 7:
                    w = -width / 2f;
                    h = height / 2f + railheight;
                    break;

                default:
                    break;
            }

            // Default initialize - cribbing compiler
            Vector3 fwd = Vector3.forward, left = Vector3.left;

            for (int i = 0; i <= n; i++)
            {
                // Except for the last point
                if (i != n)
                {
                    // Direction of track
                    fwd = points[i + 1] - points[i];

                    // Now assume no banking
                    fwd.y = 0;
                    fwd.Normalize();

                    // Get left
                    left = Vector3.Cross(Vector3.up, fwd);
                }

                vs[run++] = points[i] + left * w + Vector3.up * h;
            }
        }

        mesh.vertices = vs;

        //TODO: Do your UV mapping here
        

        // Triangle n x 16 x 3
        tris = new int[n * 16 * 3];

        // reset iterator
        run = 0;
        for (int s = 0; s < 8; s++)
        {
            for (int i = 0; i < n; i++)
            {
                // 1st Tri
                tris[run + 0] = s * (n + 1) + i;
                tris[run + 1] = s * (n + 1) + i + 1;
                tris[run + 2] = ((s + 1) % 8) * (n + 1) + i;

                // 2nd Tri
                tris[run + 3] = s * (n + 1) + i + 1;
                tris[run + 4] = ((s + 1) % 8) * (n + 1) + i + 1;
                tris[run + 5] = ((s + 1) % 8) * (n + 1) + i;

                run += 6;
            }
        }

        mesh.triangles = tris;
        mesh.RecalculateNormals();

        //? mesh.Optimize();

        // Add collider
        this.gameObject.AddComponent(typeof(MeshCollider));
        GetComponent<MeshFilter>().mesh = mesh;
    }



    // Sample
    void GenTrackPoints()
    {
        points = new Vector3[]{
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
        new Vector3(403.15f, 0.0f, 231.987f),
        
        };

    }

}
