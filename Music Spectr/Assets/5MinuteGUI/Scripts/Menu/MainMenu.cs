using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace FMG
{
	public class MainMenu : MonoBehaviour {
		public GameObject mainMenu;
		public GameObject browseMusicMenu;
		public GameObject optionsMenu;
		public GameObject creditsMenu;

		public bool useLevelSelect = true;
		public bool useExitButton = true;

		public GameObject exitButton;


		public void Awake()
		{
			if(useExitButton==false)
			{
				exitButton.SetActive(false);
			}
		}

		public void onCommand(string str)
		{
            if (str.Equals("BrowseMusic"))
			{
				if(useLevelSelect)
				{
					Constants.fadeInFadeOut(browseMusicMenu,mainMenu);
				}else{
					Application.LoadLevel(1);
				}
			}

			if(str.Equals("BrowseBack"))
			{
				Constants.fadeInFadeOut(mainMenu,browseMusicMenu);

			}
			if(str.Equals("Exit"))
			{
				Application.Quit();
			}
			if(str.Equals("Credits"))
			{
				Constants.fadeInFadeOut(creditsMenu,mainMenu);

			}
			if(str.Equals("CreditsBack"))
			{
				Constants.fadeInFadeOut(mainMenu,creditsMenu);
			}

			
			if(str.Equals("OptionsBack"))
			{
				Constants.fadeInFadeOut(mainMenu,optionsMenu);

			}
			if(str.Equals("Options"))
			{
				Constants.fadeInFadeOut(optionsMenu,mainMenu);
			}


		}
	}
}
