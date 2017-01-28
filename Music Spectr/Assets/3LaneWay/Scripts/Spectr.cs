using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public GameObject[] list;
    public int numSamples;
    public int channel;
    public float sizeOfCubes;


    public AudioController(int size)
    {
        list = new GameObject[size];
        GameObject[] children = new GameObject[size];
    }


    public void InitSpectr(GameObject object1, GameObject object2, Transform transform, float sizeOfCube)
    {
        for (int i = 0; i < list.Length / 2; i++)
        {
            GameObject clone = Instantiate(object1, object1.transform.position + new Vector3(0, 0, i*sizeOfCube), transform.rotation) as GameObject;
            clone.name = "sp" + i;
            clone.transform.parent = transform;
            list[i] = clone;
        }
        for (int i = 0; i < list.Length / 2; i++)
        {
            GameObject clone = Instantiate(object2, object2.transform.position + new Vector3(0, 0, i*sizeOfCube), transform.rotation) as GameObject;
            clone.name = "ssp" + i;
            clone.transform.parent = transform;
            list[i+list.Length/2] = clone;
        }
    }

}
