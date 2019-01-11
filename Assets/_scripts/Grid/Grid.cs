using System.Collections.Generic;
using UnityEngine;

using TicTacToe.Enums;

namespace TicTacToe.Grid
{

    /// <summary>
    /// The Class for the Grid That 
    /// Handles GridSpace Lists, Finding Spaces, and Checking For Player Win.
    /// </summary>
    public class Grid : MonoBehaviour
    {

        /// <summary>
        /// The Size of This Grid
        /// </summary>
        [Tooltip("The Size of This Grid")]
        public GridType Size;

        /// <summary>
        /// A Two Dimensional List of the Grid's Grid Spaces. 
        /// First Dimension Represents Each Row. 
        /// Second Dimension Represents Each Column In the Row.
        /// </summary>
        [Tooltip("A Two Dimensional List of the Grid's Grid Spacess")]
        public List<List<GridSpace>> GridSpaces;

        // Use this for initialization
        void Start()
        {

        }

        /// <summary>
        /// Generates the GridSpace Lists At Runtime
        /// </summary>
        public void GenerateGridSpacesList()
        {

            Debug.Log("Creating GridSpaces.");
            GridSpaces = new List<List<GridSpace>>();

            //Need to Build the Grid's List At Grid Initialization
            switch (Size)
            {

                //If the Grid is 3x3
                case GridType.ThreeByThree:
                    for (int i = 0; i < 3; i++)
                    {

                        char RowChar = ' ';

                        //Set Which Row We Are Setting
                        switch (i)
                        {

                            case 0:
                                RowChar = 'A';
                                break;
                            case 1:
                                RowChar = 'B';
                                break;
                            case 2:
                                RowChar = 'C';
                                break;

                        }

                        Debug.Log(string.Format("Currently Generating Row: {0}", RowChar));

                        List<GridSpace> Row = new List<GridSpace>();
                        GridSpace One = null;
                        GridSpace Two = null;
                        GridSpace Three = null;

                        //For Each GridSpace in Grid, Set the GridSpaces in the List
                        //In the Correct Order
                        foreach (Transform child in transform)
                        {

                            Debug.Log(string.Format("Looking At {0}", child.name));

                            GridSpace Component = null;

                            Component = child.GetComponentInChildren<GridSpace>();

                            //If We Found the Component
                            if (Component != null)
                            {

                                Debug.Log(string.Format("Currently Generating Column: {0}", Component.Column));

                                //Set The Component Appropriately
                                if (Component.Row == RowChar && 
                                    Component.Column == '1'
                                    && One == null)
                                {

                                    Debug.Log("Set One.");
                                    One = Component;

                                }
                                else if (Component.Row == RowChar && 
                                    Component.Column == '2'
                                    && Two == null)
                                {

                                    Debug.Log("Set Two.");
                                    Two = Component;

                                }
                                else if (Component.Row == RowChar && 
                                    Component.Column == '3'
                                    && Three == null)
                                {

                                    Debug.Log("Set Three.");
                                    Three = Component;

                                }

                            }

                            //Reset Component Variable
                            Component = null;

                            //If We Found the Three Columns
                            if (One != null &&
                                Two != null &&
                                Three != null)
                            {

                                Debug.Log("Adding Columns To Row");
                                Row.Add(One);
                                Row.Add(Two);
                                Row.Add(Three);

                                One = null;
                                Two = null;
                                Three = null;

                                break;

                            }

                        }

                        //Add the Whole Row Into The List
                        GridSpaces.Add(Row);

                    }
                    break;
                //If the Grid is 4x4
                case GridType.FourByFour:
                    for (int i = 0; i < 4; i++)
                    {

                        char RowChar = ' ';

                        //Set Which Row We Are Setting
                        switch (i)
                        {

                            case 0:
                                RowChar = 'A';
                                break;
                            case 1:
                                RowChar = 'B';
                                break;
                            case 2:
                                RowChar = 'C';
                                break;
                            case 3:
                                RowChar = 'D';
                                break;

                        }

                        Debug.Log(string.Format("Currently Generating Row: {0}", RowChar));

                        List<GridSpace> Row = new List<GridSpace>();
                        GridSpace One = null;
                        GridSpace Two = null;
                        GridSpace Three = null;
                        GridSpace Four = null;

                        //For Each GridSpace in Grid, Set the GridSpaces in the List
                        //In the Correct Order
                        foreach (Transform child in transform)
                        {

                            Debug.Log(string.Format("Looking At {0}", child.name));

                            GridSpace Component = null;

                            Component = child.GetComponentInChildren<GridSpace>();

                            //If We Found the Component
                            if (Component != null)
                            {

                                Debug.Log(string.Format("Currently Generating Column: {0}", Component.Column));

                                //Set the Column Appropriately
                                if (Component.Row == RowChar &&
                                    Component.Column == '1'
                                    && One == null)
                                {

                                    Debug.Log("Set One.");
                                    One = Component;

                                }
                                else if (Component.Row == RowChar &&
                                    Component.Column == '2'
                                    && Two == null)
                                {

                                    Debug.Log("Set Two.");
                                    Two = Component;

                                }
                                else if (Component.Row == RowChar &&
                                    Component.Column == '3'
                                    && Three == null)
                                {

                                    Debug.Log("Set Three.");
                                    Three = Component;

                                }
                                else if (Component.Row == RowChar &&
                                    Component.Column == '4'
                                    && Four == null)
                                {

                                    Debug.Log("Set Four.");
                                    Four = Component;

                                }

                            }

                            //Reset Component Variable
                            Component = null;

                            //If We Found the Three Columns
                            if (One != null &&
                                Two != null &&
                                Three != null &&
                                Four != null)
                            {

                                Debug.Log("Adding Columns To Row");
                                Row.Add(One);
                                Row.Add(Two);
                                Row.Add(Three);
                                Row.Add(Four);

                                One = null;
                                Two = null;
                                Three = null;
                                Four = null;

                                break;

                            }

                        }

                        //Add the Whole Row Into the List
                        GridSpaces.Add(Row);

                    }
                    break;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Check the Row of the Grid Space That Was Last Filled to See if the Player Won.
        /// </summary>
        /// <param name="LastPlayed">The Grid Space That Was Last Played.</param>
        /// <returns>True if the Player That Last Played Has Completed the Row, False Otherwise</returns>
        public bool CheckRow(GridSpace LastPlayed)
        {

            Debug.Log("Finding Space.");
            int[] SpaceIndex = FindSpace(LastPlayed);

            Debug.Log(string.Format("Space Index: {0}-{1}", SpaceIndex[0], SpaceIndex[1]));

            //Check Through the Row to See if All Spaces Are Valid
            //Checks Are Seperated In Case Different Messages/Effects Are Needed in Future Development
            for(int i = 0; i < GridSpaces[SpaceIndex[0]].Count; i++)
            {

                //If the Space is Empty
                if(!GridSpaces[SpaceIndex[0]][i].IsFilled)
                {

                    return false;

                }
                //If the Space Is Not Filled By the Current Player
                else if(GridSpaces[SpaceIndex[0]][i].IsFilled &&
                    GridSpaces[SpaceIndex[0]][i].PlayerIndex != LastPlayed.PlayerIndex)
                {

                    return false;

                }

            }

            //If Code Reaches This Point, All Spaces in Row Are Valid Spaces
            //So Player Wins
            return true;

        }

        /// <summary>
        /// Check the Column of the Grid Space That Was Last Filled to See if the Player Won.
        /// </summary>
        /// <param name="LastPlayed">The Grid Space That Was Last Played.</param>
        /// <returns>True if the Player That Last Played Has Completed the Column, False Otherwise</returns>
        public bool CheckColumn(GridSpace LastPlayed)
        {

            int[] SpaceIndex = FindSpace(LastPlayed);

            //Check Through the Column to See if All Spaces Are Valid
            //Checks Are Seperated In Case Different Messages/Effects Are Needed in Future Development
            for (int i = 0; i < GridSpaces.Count; i++)
            {

                //If the Space is Empty
                if (!GridSpaces[i][SpaceIndex[1]].IsFilled)
                {

                    return false;

                }
                //If the Space is Not Filled By the Current Player
                else if (GridSpaces[i][SpaceIndex[1]].IsFilled &&
                    GridSpaces[i][SpaceIndex[1]].PlayerIndex != LastPlayed.PlayerIndex)
                {

                    return false;

                }

            }

            //If Code Reaches This Point, All Spaces in Row Are Valid Spaces
            //So Player Wins
            return true;

        }

        /// <summary>
        /// Check the Diagonal of the Grid Space That Was Last Filled to See if the Player Won.
        /// </summary>
        /// <param name="LastPlayed">The Grid Space That Was Last Played.</param>
        /// <returns>True if the Player That Last Played Has Completed the Diagonal, False Otherwise</returns>
        public bool CheckDiagonal(GridSpace LastPlayed)
        {

            //Need This As Not All Diagonals Are The Correct Length
            //This is Already Counting the Space the Player Played On
            int ValidSpacesCount = 1;

            //Need These To Know If We Need To Continue Looking Along Diagonal Or Not
            //Diagonal Moving From Left Down to Right
            bool DownwardDiagonalFailed = false;
            //Diagonal Moving From Left Up to Right
            bool UpwardDiagonalFailed = false;

            int[] SpaceIndex = FindSpace(LastPlayed);

            //First, Move Up and To Left of Last Played
            int Row = SpaceIndex[0];
            int Column = SpaceIndex[1];

            Debug.Log("Checking Up and To Left Diagonal.");
            //While The Space We Are Searching is Within the List Bounds
            while ((Row - 1) > -1 && (Column - 1) > -1)
            {

                //If the Space is Empty
                if(!GridSpaces[Row-1][Column-1].IsFilled)
                {

                    DownwardDiagonalFailed = true;
                    ValidSpacesCount = 1;
                    break;

                }
                //If the Space Is Not Filled By the Current Player
                else if(GridSpaces[Row - 1][Column - 1].IsFilled && 
                    GridSpaces[Row - 1][Column - 1].PlayerIndex != LastPlayed.PlayerIndex)
                {

                    DownwardDiagonalFailed = true;
                    ValidSpacesCount = 1;
                    break;

                }
                //If the Space is Filled By the Current Player
                else if(GridSpaces[Row - 1][Column - 1].IsFilled &&
                    GridSpaces[Row - 1][Column - 1].PlayerIndex == LastPlayed.PlayerIndex)
                {

                    ValidSpacesCount++;

                }

                Row -= 1;
                Column -= 1;

            }

            //Now, If Needed, Move Down and To Right of Last Played
            //If Downward Diagonal Did Not Fail
            if (!DownwardDiagonalFailed)
            {

                Debug.Log("Continuing Downward Diagonal");

                //Reset Starting Position
                Row = SpaceIndex[0];
                Column = SpaceIndex[1];

                Debug.Log("Checking Down and To Right Diagonal.");
                //While The Space We Are Searching is Within the List Bounds
                while ((Row + 1) < GridSpaces.Count && (Column + 1) < GridSpaces.Count)
                {

                    //If the Space is Empty
                    if (!GridSpaces[Row + 1][Column + 1].IsFilled)
                    {

                        DownwardDiagonalFailed = true;
                        ValidSpacesCount = 1;
                        break;

                    }
                    //If the Space is Filled Not By the Current Player
                    else if (GridSpaces[Row + 1][Column + 1].IsFilled &&
                        GridSpaces[Row + 1][Column + 1].PlayerIndex != LastPlayed.PlayerIndex)
                    {

                        DownwardDiagonalFailed = true;
                        ValidSpacesCount = 1;
                        break;

                    }
                    //If the Space is Filled By the Current Player
                    else if (GridSpaces[Row + 1][Column + 1].IsFilled &&
                        GridSpaces[Row + 1][Column + 1].PlayerIndex == LastPlayed.PlayerIndex)
                    {

                        ValidSpacesCount++;

                    }

                    Row += 1;
                    Column += 1;

                }

            }

            //If the Player Has Filled Up All of the Valid Spaces They Win
            if(ValidSpacesCount == GridSpaces.Count)
            {

                return true;

            }

            //If Player Has Not Won By One Diagonal, Check the Other
            //First, Move Up and To Right of Last Played
            Row = SpaceIndex[0];
            Column = SpaceIndex[1];
            Debug.Log("Checking Up and To Right Diagonal");
            //While the Space We Are Checking is Within the List Bounds
            while ((Row - 1) > -1 && (Column + 1) < GridSpaces[SpaceIndex[0]].Count)
            {

                //If The Space Is Empty
                if (!GridSpaces[Row - 1][Column + 1].IsFilled)
                {

                    UpwardDiagonalFailed = true;
                    ValidSpacesCount = 1;
                    break;

                }
                //If the Space Is Not Filled By the Current Player
                else if (GridSpaces[Row - 1][Column + 1].IsFilled &&
                    GridSpaces[Row - 1][Column + 1].PlayerIndex != LastPlayed.PlayerIndex)
                {

                    UpwardDiagonalFailed = true;
                    ValidSpacesCount = 1;
                    break;

                }
                //If the Space is Filled By the Current Player
                else if (GridSpaces[Row - 1][Column + 1].IsFilled &&
                    GridSpaces[Row - 1][Column + 1].PlayerIndex == LastPlayed.PlayerIndex)
                {

                    ValidSpacesCount++;

                }

                Row -= 1;
                Column += 1;

            }

            //Now, If Needed, Move Down and To Left of Last Played
            //If Upward Diagonal Did Not Fail
            if (!UpwardDiagonalFailed)
            {

                Debug.Log("Continuing Upward Diagonal");

                //Reset Starting Position
                Row = SpaceIndex[0];
                Column = SpaceIndex[1];
                Debug.Log("Checking Down and To Left Diagonal");
                //If the Space Is Within the List Bounds
                while ((Row + 1) < GridSpaces.Count && (Column - 1) > GridSpaces.Count)
                {

                    //If the Space is Empty
                    if (!GridSpaces[Row + 1][Column - 1].IsFilled)
                    {

                        UpwardDiagonalFailed = true;
                        ValidSpacesCount = 1;
                        break;

                    }
                    //If the Space is Not Filled By the Current Player
                    else if (GridSpaces[Row + 1][Column - 1].IsFilled &&
                        GridSpaces[Row + 1][Column - 1].PlayerIndex != LastPlayed.PlayerIndex)
                    {

                        UpwardDiagonalFailed = true;
                        ValidSpacesCount = 1;
                        break;

                    }
                    //If the Space is Filled By The Current Player
                    else if (GridSpaces[Row + 1][Column - 1].IsFilled &&
                        GridSpaces[Row + 1][Column - 1].PlayerIndex == LastPlayed.PlayerIndex)
                    {

                        ValidSpacesCount++;

                    }

                    Row += 1;
                    Column -= 1;

                }

            }

            Debug.Log(string.Format("Valid Spaces: {0}", ValidSpacesCount));

            //If the Player Has Filled Up All of the Valid Spaces
            if (ValidSpacesCount == GridSpaces.Count)
            {

                return true;

            }

            //If We Have Not Returned True By This Point, Then It Won't Happen.
            Debug.Log("Diagonal Check Complete and Failed.");
            return false;

        }

        /// <summary>
        /// Finds Where in the Grid List the Provided GridSpace is Located.
        /// </summary>
        /// <param name="Space"></param>
        /// <returns>A Two Lengthed Array, First Number is the Row Index, the Second is the Column Index</returns>
        public int[] FindSpace(GridSpace Space)
        {
            Debug.Log("In FindSpace()");
            Debug.Log(string.Format("Grid Spaces Count: {0}", GridSpaces.Count));
            
            //Search Through Rows for Row Index
            for(int i = 0; i < GridSpaces.Count; i++)
            {

                Debug.Log(string.Format("Row: {0}", GridSpaces[i][0].Row));

                //If We Found the Correct Row
                if(GridSpaces[i][0].Row == Space.Row)
                {

                    Debug.Log("Found Correct Row.");

                    //Search Through Columns for Column Index
                    for(int k = 0; k < GridSpaces[i].Count; k++)
                    {

                        //If We Found the Correct Column
                        if(GridSpaces[i][k].Column == Space.Column)
                        {

                            Debug.Log("Found Correct Column.");

                            //Create Array
                            int[] Array = { i, k };
                            Debug.Log("Found Space in Grid.");
                            //Return Array
                            return Array;

                        }

                    }

                }

            }

            Debug.Log("Unable to Find Space in Grid.");
            return null;

        }

        /// <summary>
        /// Reset the Grid for the Next Game
        /// </summary>
        public void ResetGrid()
        {

            //Go Through Every Single Row
            for(int i = 0; i < GridSpaces.Count; i++)
            {
                
                //For Each Row, Go Through Every Single Column
                for(int k = 0; k < GridSpaces[i].Count; k++)
                {

                    //Reset the Space
                    GridSpaces[i][k].ResetSpace();

                }

            }

            gameObject.SetActive(false);            

        }

    }

}