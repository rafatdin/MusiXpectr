using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


    public class Spectrum : MonoBehaviour
    {
        public GameObject car;
        public GameObject spectrObject, reflection;
        public int numSamples;
        public int channel;
        public float sizeOfCubes;

        GameObject[] children = new GameObject[1024];
        public float[] spectrum;
        float[] volume;
       
        AudioSource audioSource = new AudioSource();


        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            if (GlobalData.samples != null)
            {
                Debug.Log(GlobalData.samples.Length);
                audioSource.clip.SetData(GlobalData.samples, 0);
                Debug.Log(audioSource.timeSamples);

            }

            audioSource.Play();


            volume = new float[numSamples];
            spectrum = new float[numSamples];

            GameObject sClone = Instantiate(spectrObject, spectrObject.transform.position + new Vector3(0, 0, sizeOfCubes), transform.rotation) as GameObject;
            children[0] = sClone;
            for (int i = 2; i < 128; i++)
            {
                sClone = Instantiate(spectrObject, sClone.transform.position + new Vector3(0, 0, sizeOfCubes), transform.rotation) as GameObject;
                sClone.name = "sp" + i;
                sClone.transform.parent = transform;
                children[i - 1] = sClone;
            }

            GameObject rClone = Instantiate(reflection, reflection.transform.position + new Vector3(0, 0, sizeOfCubes), transform.rotation) as GameObject;
            children[128] = rClone;

            for (int i = 130; i < 257; i++)
            {
                rClone = Instantiate(reflection, rClone.transform.position + new Vector3(0, 0, sizeOfCubes), transform.rotation) as GameObject;
                rClone.name = "sp" + i;
                rClone.transform.parent = transform;
                children[i - 1] = rClone;
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 tempVector = car.transform.position;
            tempVector.x = this.transform.position.x;
            this.transform.position = tempVector;


            audioSource.GetOutputData(volume, channel);
            audioSource.GetSpectrumData(spectrum, channel, FFTWindow.BlackmanHarris);
            for (int i = 0; i < 127; i++)
            {
                children[i].transform.localScale = new Vector3(sizeOfCubes, 20 * spectrum[i * 2], sizeOfCubes);
                children[i].GetComponent<MeshRenderer>().material.color = new Color(200 * spectrum[i*2], 1f / (200 * spectrum[i*2]), 1f / (200 * spectrum[i]), 255);
                children[i + 128].transform.localScale = new Vector3(sizeOfCubes, 20 * spectrum[(i + 1) * 2], sizeOfCubes);
                children[i + 128].GetComponent<MeshRenderer>().material.color = new Color(200 * spectrum[i * 2], 1f / (200 * spectrum[i * 2]), 1f / (200 * spectrum[i]), 155);
            }
        }
    }
