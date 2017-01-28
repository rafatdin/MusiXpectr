using UnityEngine;
using System.Collections;
using System;
using Un4seen.Bass;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BassImporter))]
public class Test : MonoBehaviour
{

    public BassImporter importer;
    int resolution = 60;

    float[] waveForm;
    float[] samples;
    int frequency;
    int channels;

    // Use this for initialization
    public void StartImport()
    {
        importer = GetComponent<BassImporter>();
        importer.OpenBrowser();
    }

    public void FileSelected(string path)
    {
        GetComponent<AudioSource>().clip = importer.ImportFile(path);
        GetComponent<AudioSource>().Play();
    }

    public void ReceiveSamples(float[] localSamples)
    {
        samples = new float[localSamples.Length];
        samples = localSamples;
    }

    public void ReceiveInfo(BASS_SAMPLE localInfo)
    {
        frequency = localInfo.freq;
        channels = localInfo.chans;
        Debug.Log("freq = " + frequency + "| channels = " + channels);
        Draw();
    }


    void SetWaveForms()
    {
        resolution = frequency / resolution;
        
        waveForm = new float[(samples.Length / resolution)];

        for (int i = 0; i < waveForm.Length; i++)
        {
            waveForm[i] = 0;

            for (int ii = 0; ii < resolution; ii++)
            {
                waveForm[i] += Mathf.Abs(samples[(i * resolution) + ii]);
            }

            waveForm[i] /= resolution;
        }
    }

    public void Draw()
    {
        SetWaveForms();
        for (int i = 0; i < waveForm.Length - 1; i++)
        {
            Vector3 sv = new Vector3(i * .01f, waveForm[i] * 10, 0);
            Vector3 ev = new Vector3(i * .01f, -waveForm[i] * 10, 0);

            Debug.DrawLine(sv, ev, Color.yellow);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < waveForm.Length - 1; i++)
        //{
        //    Vector3 sv = new Vector3(i * .01f, waveForm[i] * 10, 0);
        //    Vector3 ev = new Vector3(i * .01f, -waveForm[i] * 10, 0);

        //    Debug.DrawLine(sv, ev, Color.yellow);
        //}

        //int current = GetComponent<AudioSource>().timeSamples / resolution;
        //current *= 2;

        //Vector3 c = new Vector3(current * .01f, 0, 0);

        //Debug.DrawLine(c, c + Vector3.up * 10, Color.white);
    }
}
