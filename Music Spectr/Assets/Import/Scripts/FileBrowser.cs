using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Un4seen.Bass.AddOn.Tags;


public enum FileBrowserType
{
	File,
	Directory
}

public class FileBrowser
{
	private string[] drives;
	private int selectedDrive = 0;
	private bool selectDrive = false;
	protected GUIContent[] drivesContent;

	// Called when the user clicks cancel or select
	public delegate void FinishedCallback (string path);
	// Defaults to working directory
	public string CurrentDirectory {
		get {
			return m_currentDirectory;
		}
		set {
			SetNewDirectory (value);
			SwitchDirectoryNow ();
		}
	}
	
	protected string m_currentDirectory;
	protected string m_filePattern;
	
	// Optional image for directories
	public Texture2D DirectoryImage {
		get {
			return m_directoryImage;
		}
		set {
			m_directoryImage = value;
			BuildContent ();
		}
	}
	
	protected Texture2D m_directoryImage;
	
	// Optional image for files
	public Texture2D FileImage {
		get {
			return m_fileImage;
		}
		set {
			m_fileImage = value;
			BuildContent ();
		}
	}
	
	protected Texture2D m_fileImage;
	
	// Browser type. Defaults to File, but can be set to Folder
	public FileBrowserType BrowserType {
		get {
			return m_browserType;
		}
		set {
			m_browserType = value;
			ReadDirectoryContents ();
		}
	}
	
	protected FileBrowserType m_browserType;
	protected string m_newDirectory;
	protected string[] m_currentDirectoryParts;
	protected string[] m_files;
	protected GUIContent[] m_filesWithImages;
	protected int m_selectedFile;
	protected string[] m_nonMatchingFiles;
	protected GUIContent[] m_nonMatchingFilesWithImages;
	protected int m_selectedNonMatchingDirectory;
	protected string[] m_directories;
	protected GUIContent[] m_directoriesWithImages;
	protected int m_selectedDirectory;
	protected string[] m_nonMatchingDirectories;
	protected GUIContent[] m_nonMatchingDirectoriesWithImages;
	protected bool m_currentDirectoryMatches;
	
	protected GUIStyle CentredText {
		get {
			if (m_centredText == null) {
				m_centredText = new GUIStyle (GUI.skin.label);
				m_centredText.alignment = TextAnchor.MiddleLeft;
				m_centredText.fixedHeight = GUI.skin.button.fixedHeight;
			}
			return m_centredText;
		}
	}
	
	protected GUIStyle m_centredText;
	protected string m_name;
	protected Rect m_screenRect;
	protected Vector2 m_scrollPosition;
	protected FinishedCallback m_callback;
	
	public FileBrowser (Rect screenRect, string name, FinishedCallback callback, string filePattern)
	{
		m_name = name;
		m_screenRect = screenRect;
        
		m_browserType = FileBrowserType.File;
		m_callback = callback;
		m_filePattern = filePattern;
		
		string lastD = LoadLastD();
		
		if (lastD != null && lastD != "" && Directory.Exists (lastD)) {
			SetNewDirectory (lastD);
		} else {
			selectDrive = true;
			SetNewDirectory ("/");
		}
		drives = Directory.GetLogicalDrives ();
		SwitchDirectoryNow ();
	}
	
	protected void SetNewDirectory (string directory)
	{
		m_newDirectory = directory;
	}
	
	protected void SwitchDirectoryNow ()
	{
		if (m_newDirectory == null || m_currentDirectory == m_newDirectory) {
			return;
		}
		
		m_currentDirectory = m_newDirectory;
		m_scrollPosition = Vector2.zero;
		m_selectedDirectory = m_selectedNonMatchingDirectory = m_selectedFile = -1;
		ReadDirectoryContents ();
	}
	
	protected void ReadDirectoryContents ()
	{
		char[] separators = new char[]{'/','\\'};
		
		if (m_currentDirectory == "/") {
			
			m_currentDirectoryParts = new string[] {""};
			m_currentDirectoryMatches = false;
			
		} else {
			
			m_currentDirectoryParts = m_currentDirectory.Split (separators);
			
			if (m_currentDirectoryParts [1] == "") {
				Array.Resize (ref m_currentDirectoryParts, 1);
			}
			
			string path = m_currentDirectory;
			
			string[] generation = Directory.GetDirectories (path, m_filePattern);
			m_currentDirectoryMatches = Array.IndexOf (generation, m_currentDirectory) >= 0;
		}
		
		if (BrowserType == FileBrowserType.File || m_filePattern == null) {
			m_directories = Directory.GetDirectories (m_currentDirectory);
			m_nonMatchingDirectories = new string[0];
		} else {
			m_directories = Directory.GetDirectories (m_currentDirectory, m_filePattern);
			var nonMatchingDirectories = new List<string> ();
			foreach (string directoryPath in Directory.GetDirectories(m_currentDirectory)) {
				if (Array.IndexOf (m_directories, directoryPath) < 0) {
					nonMatchingDirectories.Add (directoryPath);
				}
			}
			m_nonMatchingDirectories = nonMatchingDirectories.ToArray ();
			for (int i = 0; i < m_nonMatchingDirectories.Length; ++i) {
				int lastSeparator = m_nonMatchingDirectories [i].LastIndexOf (Path.DirectorySeparatorChar);
				m_nonMatchingDirectories [i] = m_nonMatchingDirectories [i].Substring (lastSeparator + 1);
			}
			Array.Sort (m_nonMatchingDirectories);
		}
		
		for (int i = 0; i < m_directories.Length; ++i) {
			m_directories [i] = m_directories [i].Substring (m_directories [i].LastIndexOf (Path.DirectorySeparatorChar) + 1);
		}
		
		if (BrowserType == FileBrowserType.Directory || m_filePattern == null) {
			m_files = Directory.GetFiles (m_currentDirectory);
			m_nonMatchingFiles = new string[0];
			
		} else {
			m_files = Directory.GetFiles (m_currentDirectory, m_filePattern);
			var nonMatchingFiles = new List<string> ();
			
			foreach (string filePath in Directory.GetFiles(m_currentDirectory)) {
				if (Array.IndexOf (m_files, filePath) < 0) {
					nonMatchingFiles.Add (filePath);
				}
			}
			
			m_nonMatchingFiles = nonMatchingFiles.ToArray ();
			for (int i = 0; i < m_nonMatchingFiles.Length; ++i) {
				m_nonMatchingFiles [i] = Path.GetFileName (m_nonMatchingFiles [i]);
			}
			Array.Sort (m_nonMatchingFiles);
		}
		
		for (int i = 0; i < m_files.Length; ++i) {
			m_files [i] = Path.GetFileName (m_files [i]);
		}
		
		Array.Sort (m_files);
		BuildContent ();
		m_newDirectory = null;
	}
	
	protected void BuildContent ()
	{
		m_directoriesWithImages = new GUIContent[m_directories.Length];
		for (int i = 0; i < m_directoriesWithImages.Length; ++i) {
			m_directoriesWithImages [i] = new GUIContent (m_directories [i], DirectoryImage);
		}
		m_nonMatchingDirectoriesWithImages = new GUIContent[m_nonMatchingDirectories.Length];
		for (int i = 0; i < m_nonMatchingDirectoriesWithImages.Length; ++i) {
			m_nonMatchingDirectoriesWithImages [i] = new GUIContent (m_nonMatchingDirectories [i], DirectoryImage);
		}
		m_filesWithImages = new GUIContent[m_files.Length];
		for (int i = 0; i < m_filesWithImages.Length; ++i) {
            m_filesWithImages[i] = new GUIContent(m_files[i], FileImage);
		}
		m_nonMatchingFilesWithImages = new GUIContent[m_nonMatchingFiles.Length];
		for (int i = 0; i < m_nonMatchingFilesWithImages.Length; ++i) {
			m_nonMatchingFilesWithImages [i] = new GUIContent (m_nonMatchingFiles [i], FileImage);
		}
		
		drivesContent = new GUIContent[drives.Length];
		for (int i = 0; i < drivesContent.Length; ++i) {
			drivesContent [i] = new GUIContent (drives [i], DirectoryImage);
		}
	}
	
	public void OnGUI ()
	{
		GUILayout.BeginArea (m_screenRect, "", GUI.skin.window);
		GUILayout.BeginVertical ();
		GUILayout.FlexibleSpace ();
		
		GUILayout.Label ("Select song", CentredText);
		GUILayout.EndVertical ();
		GUILayout.BeginHorizontal ();
		
		if (!selectDrive) {
			if (GUILayout.Button ("Computer"))
				selectDrive = true;
			
			for (int parentIndex = 0; parentIndex < m_currentDirectoryParts.Length; parentIndex++) {
				
				if (m_currentDirectoryParts.Length == 1) {
					GUI.enabled = false;
					GUILayout.Button (m_currentDirectoryParts [0]);
					GUI.enabled = true;
				} else {
					
					if (parentIndex == m_currentDirectoryParts.Length - 1) {
						GUI.enabled = false;							
						GUILayout.Button (m_currentDirectoryParts [parentIndex]);
						GUI.enabled = true;
					} else if (GUILayout.Button (m_currentDirectoryParts [parentIndex])) {
						
						string parentDirectoryName = m_currentDirectory;
						
						for (int i = m_currentDirectoryParts.Length - 1; i > parentIndex; --i) {
							parentDirectoryName = Path.GetDirectoryName (parentDirectoryName);
						}
						
						SetNewDirectory (parentDirectoryName);
					}
				}
			}
			
		} else {
			GUI.enabled = false;
			GUILayout.Button ("Computer");
			GUI.enabled = true;
		}
		
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		
		m_scrollPosition = GUILayout.BeginScrollView (m_scrollPosition, false, true, GUI.skin.horizontalScrollbar,
		                                              GUI.skin.verticalScrollbar, GUI.skin.box, GUILayout.Height (270));
		
		if (!selectDrive) {
			
			m_selectedDirectory = GUILayoutx.SelectionList (m_selectedDirectory, m_directoriesWithImages, 
			                                                DirectoryDoubleClickCallback);
			if (m_selectedDirectory > -1) {
				m_selectedFile = m_selectedNonMatchingDirectory = -1;
			}
			
			m_selectedNonMatchingDirectory = GUILayoutx.SelectionList (m_selectedNonMatchingDirectory, m_nonMatchingDirectoriesWithImages, 
			                                                           NonMatchingDirectoryDoubleClickCallback);
			if (m_selectedNonMatchingDirectory > -1) {
				m_selectedDirectory = m_selectedFile = -1;
			}			
			
			GUI.enabled = BrowserType == FileBrowserType.File;
			m_selectedFile = GUILayoutx.SelectionList (m_selectedFile, m_filesWithImages, 
			                                           FileDoubleClickCallback);
			GUI.enabled = true;
			if (m_selectedFile > -1) {
				m_selectedDirectory = m_selectedNonMatchingDirectory = -1;
			}
			
			if (m_filePattern == null) {
				GUI.enabled = false;
				GUILayoutx.SelectionList (-1, m_nonMatchingFilesWithImages);
				GUI.enabled = true;
			}
			
		} else {
			selectedDrive = GUILayoutx.SelectionList (selectedDrive, drivesContent, DrivesDoubleClickCallback);
			if (selectedDrive >= 0) {
				m_selectedFile = m_selectedNonMatchingDirectory = -1;
			}
		}
		
		GUILayout.EndScrollView ();
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		
		if (GUILayout.Button ("Cancel", GUILayout.Width (150))) {
			m_callback ("");
		}
		
		if (BrowserType == FileBrowserType.File) {
			GUI.enabled = m_selectedFile > -1;
		} else {
			if (m_filePattern == null) {
				GUI.enabled = m_selectedDirectory > -1;
			} else {
				GUI.enabled = m_selectedDirectory > -1 ||
					(m_currentDirectoryMatches && m_selectedNonMatchingDirectory == -1 && m_selectedFile == -1);
			}
		}
		
		if (GUILayout.Button ("Select", GUILayout.Width (150))) {
			if (BrowserType == FileBrowserType.File) {
				SaveLastD (CurrentDirectory);				
				m_callback (Path.Combine (m_currentDirectory, m_files [m_selectedFile]));
			} else {
				if (m_selectedDirectory > -1) {
					SaveLastD (CurrentDirectory);					
					m_callback (Path.Combine (m_currentDirectory, m_directories [m_selectedDirectory]));
				} else {
					SaveLastD (CurrentDirectory);					
					m_callback (m_currentDirectory);
				}
			}
		}
		
		GUI.enabled = true;
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		
		if (Event.current.type == EventType.Repaint) {
			SwitchDirectoryNow ();
		}
	}
	
	protected void SaveLastD (string l)
	{
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream("FileBrowser", FileMode.Create, FileAccess.Write, FileShare.None);
		formatter.Serialize(stream, l);
		
		stream.Close();	
	}
	
	protected string LoadLastD()
	{
		if (!File.Exists ("FileBrowser"))
			return "";
		
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream("FileBrowser", FileMode.Open, FileAccess.Read, FileShare.Read);
		string obj = (string) formatter.Deserialize(stream);
		stream.Close();
		
		return obj;
	}
	
	protected void FileDoubleClickCallback (int i)
	{
		if (BrowserType == FileBrowserType.File) {
			SaveLastD (CurrentDirectory);			
			m_callback (Path.Combine (m_currentDirectory, m_files [i]));
		}
	}
	
	protected void DirectoryDoubleClickCallback (int i)
	{
		SetNewDirectory (Path.Combine (m_currentDirectory, m_directories [i]));
	}
	
	protected void DrivesDoubleClickCallback (int i)
	{
		SetNewDirectory (drives [i]);
		selectDrive = false;
	}
	
	protected void NonMatchingDirectoryDoubleClickCallback (int i)
	{
		SetNewDirectory (Path.Combine (m_currentDirectory, m_nonMatchingDirectories [i]));
	}
}