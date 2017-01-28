using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.AddOn.Fx;

[AddComponentMenu("Audio/BassImporter")]
public class BassImporter : MonoBehaviour
{
	private bool browserActive = false;
	private FileBrowser fileBrowser;

	private int sample;
	private int channel;
	private AudioClip audioClip;

	/// <summary>
	/// Imports an mp3 file. Only the start of a file is actually imported.
	/// The remaining part of the file will be imported bit by bit to speed things up. 
	/// </summary>
	/// <returns>
	/// Audioclip containing the song.
	/// </returns>
	/// <param name='filePath'>
	/// Path to mp3 file.
	/// </param>
    /// 


	public AudioClip ImportFile (string filePath)
	{
		//get license from http://bass.radio42.com/bass_purchase.html
		//Un4seen.Bass.BassNet.Registration ("email", "key");

        if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
        {

            sample = Bass.BASS_SampleLoad(filePath, 0, 0, 1, BASSFlag.BASS_SAMPLE_FLOAT);

            BASS_SAMPLE info = Bass.BASS_SampleGetInfo(sample);

            int lengthSamples = (int)(info.length / sizeof(float));

            audioClip = AudioClip.Create(Path.GetFileNameWithoutExtension(filePath), lengthSamples / info.chans, info.chans, info.freq, false, false);
            float[] data = new float[lengthSamples];
            Bass.BASS_SampleGetData(sample, data);
            //Debug.Log("Sample Length: " + lengthSamples);

            TAG_INFO tagInfo = new TAG_INFO(filePath);
            //SendMessage("ReceiveTagInfo", tagInfo, SendMessageOptions.RequireReceiver);
            //SendMessage("ReceiveSamples", data, SendMessageOptions.RequireReceiver);
            //SendMessage("ReceiveInfo", info, SendMessageOptions.RequireReceiver);

            GlobalData.samples = data;
            GlobalData.tagInfo = tagInfo;
            GlobalData.sinfo = info;

            audioClip.SetData(data, 0);
            
            /*-------------------------------------!-!-!-!------------------------------------------*/
            Debug.Log("Data Length: " + data.Length);
            string tempS=" ";
            
            //for (int i = 0; i < data.Length; i += 2048)
            //{
            //    tempS += data[i].ToString() + Environment.NewLine;
            //}
            //System.IO.File.WriteAllText(@"C:\Users\Rafatdin\Desktop\Bass\Samples.txt", tempS);



            float[] arrayOfAverages = new float[data.Length / 88200 + 1];
            for (int i = 1; i < data.Length / 88200; i++)
            {
                float average = 0;
                for (int ii = i * 88200; ii < i * 88200 + 88200; ii++)
                {
                    average += data[ii];
                }
                average /= 44100;
                arrayOfAverages[i] = average*10;
                tempS += average.ToString() + Environment.NewLine;
            }

            GlobalData.processedSamples = arrayOfAverages;

            //System.IO.File.WriteAllText(@"C:\Users\Rafatdin\Desktop\Bass\Samples.txt", tempS);

            /*-------------------------------------!-!-!-!------------------------------------------*/

            // free the Sample
            Bass.BASS_SampleFree(sample);
            // free BASS
            Bass.BASS_Free();
            
            Application.LoadLevel("3LaneWay");

        }
        else{
            Debug.Log(Bass.BASS_ErrorGetCode());
            if (Bass.BASS_ErrorGetCode().ToString().Equals("BASS_ERROR_ALREADY"))
            {
                Bass.BASS_Free();
                ImportFile(filePath);
            }
        }
		return audioClip;
	}

	
	void OnGUI ()
	{
		if (browserActive) {
            GUI.skin = (GUISkin)Resources.Load("SciFi", typeof(GUISkin));
			
			if (fileBrowser == null) {
				
				float windowWidth = 512;
				float windowHeight = 512f;

                Rect browserRect = new Rect(Screen.width - windowWidth, Screen.height / 2 - (windowHeight / 2), windowWidth, windowHeight);
				//fileBrowser = new FileBrowser (browserRect, "Choose song", FileSelectedCallback, "*.mp3");
                fileBrowser = new FileBrowser(browserRect, "Choose song", FileSelectedCallback, "*.mp3");
				
				fileBrowser.DirectoryImage = (Texture2D)Resources.Load ("folder", typeof(Texture2D));
				fileBrowser.FileImage = (Texture2D)Resources.Load ("file", typeof(Texture2D));
				
				fileBrowser.OnGUI ();
			} else {				
				fileBrowser.OnGUI ();				
			}			
		}
	}
	
	public void FileSelectedCallback (string path)
	{
        SendMessage("onCommand", "BrowseBack", SendMessageOptions.RequireReceiver);
		fileBrowser = null;
		browserActive = false;
		if(path.Length>1)
			SendMessage("FileSelected",path,SendMessageOptions.DontRequireReceiver);
	}
	
	/// <summary>
	/// Opens a browser for song selection. Imports a (new) song. (re) initializes analyses and variables.
	/// </summary>
	/// <param name='c'>
	/// When a file is selected c is called.
	/// </param>
	public void OpenBrowser ()
	{
		browserActive = true;
	}


	/// <summary>
	/// Close the browser without doing anything else.
	/// </summary>
	public void CloseBrowser ()
	{
		fileBrowser = null;
		browserActive = false;
	}


}
