using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TicTacToe.Enums;

namespace TicTacToe.Managers
{

    /// <summary>
    /// The Manager Which Controlls Opening, Closing, and Altering UI Elements.
    /// </summary>
    public class UIManager : MonoBehaviour
    {

        /// <summary>
        /// The Single Instance of the Manager
        /// </summary>
        public static UIManager instance;

        [Header("UI Objects")]
        /// <summary>
        /// The GameObject for the Main Menu UI
        /// </summary>
        [Tooltip("The GameObject for the Main Menu UI")]
        public GameObject MainMenuUI;

        /// <summary>
        /// The GameObject for the Settings UI to be Loaded In
        /// </summary>
        [Tooltip("The Game Object for the Settings UI to be Loaded In")]
        public GameObject SettingsUI;

        /// <summary>
        /// The Game Object for the Gameplay UI to be Loaded In
        /// </summary>
        [Tooltip("The Game Object for the Gameplay UI to be Loaded In")]
        public GameObject GameplayUI;

        /// <summary>
        /// The Game Object for the Ribbon UI to be Loaded In
        /// </summary>
        [Tooltip("The Game Object for the Ribbon UI to be Loaded In")]
        public GameObject RibbonUI;

        /// <summary>
        /// The Game Object for the Loading Screen to be Loaded In
        /// </summary>
        [Tooltip("The Game Object for the Loading Screen to be Loaded In")]
        public GameObject LoadingScreenUI;

        /// <summary>
        /// The Game Object for the End Game Screen to be Loaded In
        /// </summary>
        [Tooltip("The Game Object for the End Game Screen to be Loaded In")]
        public GameObject EndGameUI;

        [Header("Text Labels")]
        /// <summary>
        /// The Text Component of the Player Turn Label
        /// </summary>
        [Tooltip("The Text Component of the Player Turn Label")]
        public Text PlayerTurnLabel;

        /// <summary>
        /// The Text Component of the Round Label
        /// </summary>
        [Tooltip("The Text Component of the Round Label")]
        public Text RoundLabel;

        /// <summary>
        /// The Text Component of the End Game Label
        /// </summary>
        [Tooltip("The Text Component of the End Game Label")]
        public Text EndGameLabel;

        [Header("Sliders")]
        /// <summary>
        /// The Master Volume Slider
        /// </summary>
        [Tooltip("The Master Volume Slider")]
        public Slider MasterVolumeSlider;

        /// <summary>
        /// The Music Volume Slider
        /// </summary>
        [Tooltip("The Music Volume Slider")]
        public Slider MusicVolumeSlider;

        /// <summary>
        /// The SFX Volume Slider
        /// </summary>
        [Tooltip("The SFX Volume Slider")]
        public Slider SFXVolumeSlider;

        /// <summary>
        /// The List of All Initialized UI
        /// </summary>
        private List<GameObject> UIList;

        private void Awake()
        {
            
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

        }

        // Use this for initialization
        void Start()
        {

            TurnManager.instance.MoveToNextRound += ChangeRoundLabel;
            TurnManager.instance.MoveToNextTurn += ChangePlayerLabel;

            //Create the UI List
            UIList = new List<GameObject>();

            SetUpUI();
            
            ShowUI(LoadingScreenUI);

            GameManager.instance.LoadGrid();
            TurnManager.instance.LoadSymbols();

            ShowUI(MainMenuUI);

        }

        /// <summary>
        /// Set Up the Initial UI State.
        /// </summary>
        void SetUpUI()
        {

            Debug.Log("Setting Up UI");

            UIList.Add(LoadingScreenUI);
            UIList.Add(MainMenuUI);
            UIList.Add(SettingsUI);
            UIList.Add(RibbonUI);
            UIList.Add(GameplayUI);
            UIList.Add(EndGameUI);

            foreach (GameObject UI in UIList)
            {

                UI.SetActive(false);

            }

            Debug.Log("UI Set Up Done.");

        }

        /// <summary>
        /// Close All UI Windows
        /// </summary>
        void CloseAll()
        {

            Debug.Log("Closing All UI Elements.");

            foreach (GameObject UI in UIList)
            {

                if (UI.activeSelf)
                {

                    UI.SetActive(false);

                }

            }

        }

        /// <summary>
        /// Opens the Passed In UI, Closing All Others if Told To
        /// </summary>
        /// <param name="UI">The UI To Open</param>
        /// <param name="CloseOthers">If All of the Other UI Should Be Closed First (Default = true)</param>
        void ShowUI(GameObject UI, bool CloseOthers = true)
        {

            if (CloseOthers)
            {

                CloseAll();

            }

            UI.SetActive(true);
            Debug.Log(string.Format("Showing {0}", UI.name));

        }

        /// <summary>
        /// Opens the Passed In UI, Closing All Others if Told To
        /// </summary>
        /// <param name="Tag">The UI To Open</param>
        /// <param name="CloseOthers">If All of the Other UI Should Be Closed First (Default = true)</param>
        public void ShowUI(UITag Tag, bool CloseOthers = true)
        {

            switch (Tag)
            {

                case UITag.LoadingScreen:
                    ShowUI(LoadingScreenUI, CloseOthers);
                    break;
                case UITag.MainMenu:
                    ShowUI(MainMenuUI, CloseOthers);
                    break;
                case UITag.Settings:
                    ShowUI(SettingsUI, CloseOthers);
                    break;
                case UITag.Gameplay:
                    ShowUI(GameplayUI, CloseOthers);
                    break;
                case UITag.Ribbon:
                    ShowUI(RibbonUI, CloseOthers);
                    break;
                case UITag.EndGame:
                    ShowUI(EndGameUI, CloseOthers);
                    break;

            }

        }

        /// <summary>
        /// Returns the Player to the Main Menu, Resetting Gameplay.
        /// </summary>
        public void ReturnToMainMenu()
        {

            GameManager.instance.ResetGame();
            AudioManager.instance.ChangeMusicImmediately(AudioManager.instance.MainMenuMusic);
            ShowUI(UITag.MainMenu);

        }

        public void ShowSettings()
        {

            float MasterVol = 0.0f;
            float MusicVol = 0.0f;
            float SFXVol = 0.0f;

            AudioManager.instance.MainMixer.GetFloat("MasterVolume", out MasterVol);
            AudioManager.instance.MainMixer.GetFloat("MusicVolume", out MusicVol);
            AudioManager.instance.MainMixer.GetFloat("SFXVolume", out SFXVol);

            MasterVolumeSlider.value = MasterVol;
            MusicVolumeSlider.value = MusicVol;
            SFXVolumeSlider.value = SFXVol;

            ShowUI(SettingsUI, false);

        }

        public void RemoveSettings()
        {

            SettingsUI.SetActive(false);

        }

        /// <summary>
        /// Toggle On and Off the Gameplay Ribbon UI.
        /// </summary>
        public void ToggleRibbon()
        {

            if(RibbonUI.activeSelf)
            {

                RibbonUI.SetActive(false);

            }
            else
            {

                ShowUI(RibbonUI, false);

            }

        }

        /// <summary>
        /// Alter the Player Turn Label
        /// </summary>
        void ChangePlayerLabel()
        {

            PlayerTurnLabel.text = string.Format("Player {0}'s Turn", (TurnManager.instance.CurrentPlayer == 1 ? "One" : "Two"));

        }

        /// <summary>
        /// Alter the Round Label
        /// </summary>
        void ChangeRoundLabel()
        {

            RoundLabel.text = string.Format("Round {0}", TurnManager.instance.CurrentRound);

        }

        /// <summary>
        /// Update the Label For the End Game Text
        /// </summary>
        /// <param name="Message">The Message to Change the End Game Text To</param>
        public void ChangeEndGameMessage(string Message)
        {

            EndGameLabel.text = Message;

        }

        /// <summary>
        /// Start the Game Based On Grid Type
        /// </summary>
        /// <param name="Grid">The Type of Grid to Play On</param>
        public void StartGame(GridType Grid)
        {

            ShowUI(LoadingScreenUI);

            GameManager.instance.StartGame(Grid);

            ShowUI(GameplayUI);

        }

        /// <summary>
        /// Start the Game Based On Grid Type
        /// </summary>
        /// <param name="GridTag">The Type of Grid to Play On</param>
        public void StartGame(string GridTag)
        {

            if (GridTag == "4x4")
            {

                StartGame(GridType.FourByFour);

            }
            else if(GridTag == "3x3")
            {

                StartGame(GridType.ThreeByThree);

            }

        }

        /// <summary>
        /// Close Out of the Game.
        /// </summary>
        public void ExitGame()
        {

            Application.Quit();

        }

    }

}