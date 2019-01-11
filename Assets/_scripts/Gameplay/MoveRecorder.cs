using System.Collections.Generic;
using UnityEngine;

using TicTacToe.Grid;

namespace TicTacToe.Recorders
{

    /// <summary>
    /// Handles Recording Each Move That A Player Makes
    /// </summary>
    public class MoveRecorder : MonoBehaviour
    {

        /// <summary>
        /// Class Representing a Player's Move.
        /// </summary>
        public class Move
        {

            /// <summary>
            /// The Grid Space That Was Just Played On.
            /// </summary>
            [Tooltip("The Grid Space That Was Just Played On")]
            public GridSpace SpacePlaced;

            /// <summary>
            /// The Index of the Player That Played.
            /// </summary>
            [Tooltip("The Index of the Player That Played")]
            public int PlayerIndex;

            /// <summary>
            /// The Round When the Move Was Made.
            /// </summary>
            [Tooltip("The Round When the Move Was Made")]
            public int RoundNumber;

            /// <summary>
            /// The Symbol the Player Used.
            /// </summary>
            [Tooltip("The Symbol the Player Used")]
            public Sprite PlayerSymbol;

            public Move()
            {

                SpacePlaced = null;
                PlayerIndex = 0;
                RoundNumber = 0;
                PlayerSymbol = null;

            }

            public Move(GridSpace Space, int Player, int Round, Sprite Symbol)
            {

                SpacePlaced = Space;
                PlayerIndex = Player;
                RoundNumber = Round;
                PlayerSymbol = Symbol;

            }

        }

        /// <summary>
        /// The Single Instance of the Move Recorder
        /// </summary>
        [Tooltip("The Single Instance of the Move Recorder")]
        public static MoveRecorder instance;

        /// <summary>
        /// The List of Moves Made In the Game
        /// </summary>
        private List<Move> MoveList;

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

        }

        // Use this for initialization
        void Start()
        {

            MoveList = new List<Move>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Stores the Last Move Played Into the List of Moves.
        /// </summary>
        /// <param name="SpacePlayed">The Space Played in the Move.</param>
        /// <param name="PlayerIndex">Which Player Played.</param>
        /// <param name="RoundNumber">The Round That the Player Played.</param>
        public void RecordMove(GridSpace SpacePlayed, int PlayerIndex, int RoundNumber, Sprite PlayerSymbol)
        {

            MoveList.Add(new Move(SpacePlayed, PlayerIndex, RoundNumber, PlayerSymbol));
            Debug.Log("Move Recorded.");

        }

        /// <summary>
        /// Get the Entire Move List.
        /// </summary>
        /// <returns>The List of Moves.</returns>
        public List<Move> GetMoveList()
        {

            return MoveList;

        }

        /// <summary>
        /// Get A Specific Turn Based on Round Number and the Player That Played.
        /// </summary>
        /// <param name="RoundNumber">The Round Number of the Move.</param>
        /// <param name="PlayerIndex">The Player That Played the Move.</param>
        /// <returns></returns>
        public Move GetTurn(int RoundNumber, int PlayerIndex)
        {

            foreach(Move Turn in MoveList)
            {

                if(Turn.RoundNumber == RoundNumber && 
                    Turn.PlayerIndex == PlayerIndex)
                {

                    return Turn;

                }

            }

            Debug.Log("Cannot Find Turn!");
            return null;

        }

    }

}