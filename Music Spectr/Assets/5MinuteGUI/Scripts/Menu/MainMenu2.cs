using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace FMG
{

[RequireComponent(typeof(BassImporter))]
	public class MainMenu2 : MonoBehaviour {
        public GameObject mainMenu;
		public GameObject languagesMenu;
        public GameObject optionsMenu;
        public GameObject creditsMenu;
        public GameObject numbersMenu;

        public BassImporter importer;

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

            if (str.Equals("Languages"))
            {
                if (useLevelSelect)
                {
                    Constants.fadeInFadeOut(languagesMenu, mainMenu);
                }
                else
                {
                    Application.LoadLevel(1);
                }
            }

			if(str.Equals("BrowseBack"))
			{
                Constants.fadeInFadeOut(mainMenu, languagesMenu);

			}

            if (str.Equals("Numbers"))
            {
                Constants.fadeInFadeOut(numbersMenu, languagesMenu);

            }
            if (str.Equals("NumbersBack"))
            {
                Constants.fadeInFadeOut(languagesMenu, numbersMenu);
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
