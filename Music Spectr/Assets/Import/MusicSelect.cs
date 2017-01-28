using UnityEngine;
using System.Collections;
using System;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.AddOn.Fx;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BassImporter))]
public class MusicSelect : MonoBehaviour
{

    public BassImporter importer;
    public static TAG_INFO tagInfo;
    int resolution = 60;
    float[] waveForm;
    float[] samples;
    int frequency;
    int channels;

    // Use this for initialization
    public void Start()
    {
        importer = GetComponent<BassImporter>();
        importer.OpenBrowser();
    }


    public BPMBEATPROC _beatProc;
    public void FileSelected(string path)
    {
        Debug.Log(path);
        GetComponent<AudioSource>().clip = importer.ImportFile(path);
        GetComponent<AudioSource>().Play();
    }

    public void ReceiveSamples(float[] localSamples)
    {
        samples = new float[localSamples.Length];
        samples = localSamples;
        GlobalData.samples = localSamples;
    }

    public void ReceiveInfo(BASS_SAMPLE localInfo)
    {
        frequency = localInfo.freq;
        channels = localInfo.chans;
        GlobalData.sinfo = localInfo;
    }

    public void ReceiveTagInfo(TAG_INFO localTagInfo)
    {
        tagInfo = localTagInfo;
        GlobalData.tagInfo = localTagInfo;
    }
}
