using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BassImporter))]
public class BassExample : MonoBehaviour {

	public BassImporter importer;
	
	// Use this for initialization
	void Start () {

		importer = GetComponent<BassImporter>();

		importer.OpenBrowser();
	}
	
	public void FileSelected(string path)
	{
		GetComponent<AudioSource>().clip=importer.ImportFile(path);
		GetComponent<AudioSource>().Play();
	}

}
