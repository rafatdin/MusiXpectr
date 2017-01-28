using UnityEngine;
using System.Collections;
namespace FMG
{
	public class LevelButton : MonoBehaviour {
		public int levelIndex=0;
        public string language="";
		public void onClick()
		{
            Debug.Log(language + "" + levelIndex.ToString() + ".wav");
            AudioClip source = Resources.Load<AudioClip>(language + "" + levelIndex.ToString());
            GameObject music = GameObject.Find("Music");
            AudioSource aSource = music.AddComponent<AudioSource>();
            aSource.clip = source;
            aSource.Play();
		}
	}
}