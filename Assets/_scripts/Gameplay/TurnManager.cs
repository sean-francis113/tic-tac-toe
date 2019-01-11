using System.Collections.Generic;
using UnityEngine;
using System;

namespace TicTacToe.Managers
{

    /// <summary>
    /// Handles Switching Turns Between Both Players
    /// </summary>
    public class TurnManager : MonoBehaviour
    {

        /// <summary>
        /// The Single Instance of the TurnManager
        /// </summary>
        public static TurnManager instance;

        /// <summary>
        /// The List of Symbols That the Players Can Possibly Use
        /// </summary>
        [Tooltip("The List of Symbols That the Players Can Possibly Use")]
        public List<Sprite> PossiblePlayerSymbols;

        /// <summary>
        /// Which Player's Turn it is
        /// </summary>
        [Tooltip("Which Player's Turn it is")]
        public int CurrentPlayer;

        /// <summary>
        /// Which Round it is
        /// </summary>
        [Tooltip("Which Round it is")]
        public int CurrentRound;

        /// <summary>
        /// Have the Game Move to the Next Turn
        /// </summary>
        [Tooltip("Have the Game Move to the Next Turn")]
        public Action MoveToNextTurn;

        /// <summary>
        /// Have the Game Move to the Next Round
        /// </summary>
        [Tooltip("Have the Game Move to the Next Round")]
        public Action MoveToNextRound;

        /// <summary>
        /// The Instantiated List of Possible Player Symbols
        /// </summary>
        private List<Sprite> i_PossiblePlayerSymbols;

        /// <summary>
        /// The Symbol That Player One is Using
        /// </summary>
        private Sprite PlayerOneSymbol;

        /// <summary>
        /// The Symbol That Player Two is Using
        /// </summary>
        private Sprite PlayerTwoSymbol;

        private void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            i_PossiblePlayerSymbols = new List<Sprite>();

        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Move On to the Next Turn
        /// </summary>
        public void NextTurn()
        {

            if(CurrentPlayer == 1)
            {

                //Go to Player Two
                CurrentPlayer++;

            }
            else if(CurrentPlayer == 2)
            {

                //Go to Player One
                CurrentPlayer--;

                //Move to Next Round
                CurrentRound++;
                if(MoveToNextRound != null)
                {

                    MoveToNextRound();

                }

            }

            //Go to Next Turn
            if(MoveToNextTurn != null)
            {

                MoveToNextTurn();

            }

        }

        /// <summary>
        /// Loads All Possible Player Symbols
        /// </summary>
        public void LoadSymbols()
        {

            Debug.Log("Loading Symbols.");

            foreach (Sprite symbol in PossiblePlayerSymbols)
            {

                Debug.Log("Loaded Symbol.");
                i_PossiblePlayerSymbols.Add(Instantiate(symbol, Vector3.zero, Quaternion.identity));

            }

            Debug.Log("Symbols Load Done.");

        }

        /// <summary>
        /// Randomly Choose the Player Symbols Out of the Possible Choices
        /// </summary>
        public void SelectPlayerSymbols()
        {

            PlayerOneSymbol = i_PossiblePlayerSymbols[UnityEngine.Random.Range(0, i_PossiblePlayerSymbols.Count - 1)];
            PlayerTwoSymbol = i_PossiblePlayerSymbols[UnityEngine.Random.Range(0, i_PossiblePlayerSymbols.Count - 1)];

            if(PlayerOneSymbol == PlayerTwoSymbol)
            {

                SelectPlayerSymbols();

            }

        }

        /// <summary>
        /// Get Player One's Symbol
        /// </summary>
        /// <returns>The Sprite for Player One's Symbol</returns>
        public Sprite GetPlayerOneSymbol()
        {

            return PlayerOneSymbol;

        }

        /// <summary>
        /// Get Player Two's Symbol
        /// </summary>
        /// <returns>The Sprite for Player Two's Symbol</returns>
        public Sprite GetPlayerTwoSymbol()
        {

            return PlayerTwoSymbol;

        }

    }

}