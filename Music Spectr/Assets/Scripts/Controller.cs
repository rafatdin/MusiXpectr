using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public GameObject road;
    public GameObject spectrum;


    //void Awake()
    //{
    //    setSpectrum(GlobalData.samples); //Import the samples from previous scene using GlobalData class
    //}

    void setSpectrum(float [] samples)
    {
        AudioClip clip = new AudioClip();
        clip.SetData(samples, 0);

        spectrum.GetComponent<AudioSource>().clip = clip;                
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
