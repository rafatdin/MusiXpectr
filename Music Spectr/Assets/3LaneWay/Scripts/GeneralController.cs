using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneralController : MonoBehaviour {

    public GameObject transport, roadStraight, coin, roadLeft;
    public GameObject spectrum1, spectrum2;
    public AudioSource audioSource;
    public List<Material> materials;
    public int sampleSize;
    public int channel;
    public float cubeSize;
    public int numberOfCubes;

    private RoadBuilder road;
    private AudioController audio;
    private float[] spectrum;
    private float[] volume;

    void Awake()
    {
        road = new RoadBuilder(roadStraight,coin,materials,transform);
        audio = new AudioController(numberOfCubes);
        SetSamples();
    }

	// Use this for initialization
	void Start () {
        audioSource.Play();
        audio.InitSpectr(spectrum1, spectrum2, transform, cubeSize);
	}

    float temp;
	// Update is called once per frame
	void Update () {
        road.MoveObject(transport);
        road.AddRoad(transform);
        road.ChangePosition();

        MoveAlong();

        foreach (GameObject o in road.CurrentCoins)
        {
            temp = 150 * spectrum[Random.Range(0, 6)];
            if(1f >= temp)
                o.transform.localScale = new Vector3(1f, 0.5f, 0.5f);
            else
                o.transform.localScale = new Vector3(1.1f, 0.7f, 0.7f);
        }

	}

    void SetSamples()
    {
        if (GlobalData.samples != null)
        {
            Debug.Log(GlobalData.samples.Length);
            audioSource.clip = AudioClip.Create("MyFile", GlobalData.samples.Length, GlobalData.sinfo.chans, GlobalData.sinfo.freq, false); //public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream);
            audioSource.clip.SetData(GlobalData.samples, 0);
            Debug.Log(audioSource.timeSamples);

        }
        volume= new float[sampleSize];
        spectrum = new float[sampleSize];
    }

    void MoveAlong()
    {
        Vector3 tempVector = transport.transform.position;
        tempVector.x = this.transform.position.x;
        this.transform.position = tempVector;

        audioSource.GetOutputData(volume, channel);
        audioSource.GetSpectrumData(spectrum, channel, FFTWindow.BlackmanHarris);
        for (int i = 0; i < numberOfCubes/2; i++)
        {
            audio.list[i].transform.localScale = new Vector3(cubeSize, 20 * spectrum[i * 2], cubeSize);
            audio.list[i].GetComponent<MeshRenderer>().material.color = new Color(200 * spectrum[i * 2], 1f / (200 * spectrum[i * 2]), 1f / (200 * spectrum[i]), 255);
            audio.list[i + numberOfCubes / 2].transform.localScale = new Vector3(cubeSize, 20 * spectrum[(i + 1) * 2], cubeSize);
            audio.list[i + numberOfCubes / 2].GetComponent<MeshRenderer>().material.color = new Color(200 * spectrum[i * 2], 1f / (200 * spectrum[i * 2]), 1f / (200 * spectrum[i]), 155);
        }

        temp = 0;
        for (int i = 0; i < volume.Length; i++)
        {
            temp += Mathf.Abs(volume[i]);
        }
        temp/=volume.Length;
        road.speed = temp * 40;

    }

}
