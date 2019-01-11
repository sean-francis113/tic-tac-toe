using UnityEngine;

using TicTacToe.Enums;
using TicTacToe.Grid;

namespace TicTacToe.Managers
{

    /// <summary>
    /// Handles Managing General Gameplay
    /// </summary>
    public class GameManager : MonoBehaviour
    {

        /// <summary>
        /// The Single Instance of GameManager
        /// </summary>
        public static GameManager instance;

        [Header("Grid Objects")]
        /// <summary>
        /// The 3x3 Grid Object
        /// </summary>
        [Tooltip("The 3x3 Grid Object")]
        public GameObject ThreeByThreeGrid;

        /// <summary>
        /// The 4x4 Grid Object
        /// </summary>
        [Tooltip("The 4x4 Grid Object")]
        public GameObject FourByFourGrid;

        /// <summary>
        /// What Grid is Currently Being Played On
        /// </summary>
        private GameObject CurrentGrid;

        /// <summary>
        /// The Instantiated 3x3 Grid
        /// </summary>
        private GameObject i_ThreeByThreeGrid;

        /// <summary>
        /// The Instantiated 4x4 Grid
        /// </summary>
        private GameObject i_FourByFourGrid;

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



        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Get the Grid Currently Being Played On
        /// </summary>
        /// <returns>The Grid Object</returns>
        GameObject GetCurrentGrid()
        {

            return CurrentGrid;

        }

        /// <summary>
        /// Instantiate and Generate the GridSpace Lists for Each Grid
        /// </summary>
        public void LoadGrid()
        {

            Debug.Log("Loading Grid.");

            i_ThreeByThreeGrid = Instantiate(ThreeByThreeGrid, Vector3.zero, Quaternion.identity);
            i_ThreeByThreeGrid.GetComponent<Grid.Grid>().GenerateGridSpacesList();
            i_ThreeByThreeGrid.SetActive(false);
            i_FourByFourGrid = Instantiate(FourByFourGrid, Vector3.zero, Quaternion.identity);
            i_FourByFourGrid.GetComponent<Grid.Grid>().GenerateGridSpacesList();    
            i_FourByFourGrid.SetActive(false);

            Debug.Log("Grid Done Loading.");

        }

        /// <summary>
        /// Start Setting Up the Game.
        /// </summary>
        /// <param name="Grid">Which Type of Grid to Use</param>
        public void StartGame(GridType Grid)
        {

            switch(Grid)
            {

                case GridType.ThreeByThree:
                    CurrentGrid = i_ThreeByThreeGrid;
                    break;
                case GridType.FourByFour:
                    CurrentGrid = i_FourByFourGrid;
                    break;

            }

            TurnManager.instance.SelectPlayerSymbols();
            TurnManager.instance.CurrentPlayer = 1;
            TurnManager.instance.CurrentRound = 1;

            if (TurnManager.instance.MoveToNextTurn != null)
            {
                TurnManager.instance.MoveToNextTurn();
            }

            if (TurnManager.instance.MoveToNextRound != null)
            {

                TurnManager.instance.MoveToNextRound();

            }

            CurrentGrid.transform.position = new Vector3(0, 0, 0);
            CurrentGrid.SetActive(true);

        }

        /// <summary>
        /// Checks All Conditions to See if Player Who Played Last Has Won
        /// </summary>
        /// <param name="LastPlayed">The Space Last Played.</param>
        public void CheckVictory(GridSpace LastPlayed)
        {

            Debug.Log("Checking Victory");

            //Get Grid Parent For Future Checks
            Grid.Grid Parent = LastPlayed.GetComponentInParent<Grid.Grid>();

            Debug.Log(string.Format("Grid to Check: {0}", Parent.name));
            Debug.Log(string.Format("Space to Check: {0}", LastPlayed.DisplayStr));

            bool PlayerWin = Parent.CheckRow(LastPlayed);

            if (!PlayerWin)
            {

                PlayerWin = Parent.CheckColumn(LastPlayed);

                if (!PlayerWin)
                {

                    PlayerWin = Parent.CheckDiagonal(LastPlayed);

                }

            }

            //After All Needed Checks, If Player Won
            if (PlayerWin)
            {

                //End Game
                EndGame(TurnManager.instance.CurrentPlayer);

            }
            else
            {

                //Move to the Next Turn
                TurnManager.instance.NextTurn();

            }

        }

        /// <summary>
        /// Resets the Game.
        /// </summary>
        public void ResetGame()
        {

            CurrentGrid.GetComponent<Grid.Grid>().ResetGrid();

        }

        /// <summary>
        /// Ends the Game and Shows the End Game Screen
        /// </summary>
        /// <param name="PlayerWon">Which Player Won</param>
        public void EndGame(int PlayerWon)
        {

            string EndGameMessage = string.Format("Player {0} Won!", (PlayerWon == 1) ? "One" : "Two");

            UIManager.instance.ChangeEndGameMessage(EndGameMessage);
            UIManager.instance.ShowUI(UITag.EndGame);
            AudioManager.instance.HaltAllMusic();
            AudioManager.instance.PlaySound(AudioManager.instance.EndGameFanfare);

        }

        /// <summary>
        /// Sets the Current Grid to a New Value.
        /// </summary>
        /// <param name="Grid"></param>
        public void SetCurrentGrid(GameObject Grid)
        {

            CurrentGrid = Grid;

        }

    }

}
