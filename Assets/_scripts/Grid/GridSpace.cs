using UnityEngine;

using TicTacToe.Managers;
using TicTacToe.Recorders;

namespace TicTacToe.Grid
{

    /// <summary>
    /// Class Handling Each Space on the Tic-Tac-Toe Grid.
    /// </summary>
    public class GridSpace : MonoBehaviour
    {

        /// <summary>
        /// The Row Letter This Space Represents
        /// </summary>
        public char Row;

        /// <summary>
        /// The Row Number This Space Represents
        /// </summary>
        public char Column;

        /// <summary>
        /// The String of the Row/Column That is Displayed to the Player
        /// </summary>
        public string DisplayStr;

        /// <summary>
        /// Has This Space Been Filled With Either Player's Symbol?
        /// </summary>
        public bool IsFilled;

        /// <summary>
        /// The Player Who Has Filled This Space, If Any.
        /// </summary>
        public int PlayerIndex;

        /// <summary>
        /// The SpriteRenderer Component Attached to This Object.
        /// </summary>
        private SpriteRenderer SR;

        /// <summary>
        /// The Collider Component Attached to This Object.
        /// </summary>
        private BoxCollider2D Collider;

        // Use this for initialization
        void Start()
        {

            IsFilled = false;
            PlayerIndex = 0;

            SR = GetComponent<SpriteRenderer>();
            //Only Set Collider Reference if Platform is Mobile
            #if (UNITY_ANDROID || UNITY_IOS)
                Collider = GetComponent<BoxCollider2D>();
            #endif

        }

        // Update is called once per frame
        void Update()
        {

            //Only Run Touch Check if Platform is Mobile
            #if (UNITY_ANDROID || UNITY_IOS)

                //If A Touch Was Detected
                if (Input.touchCount == 1)
                {
                
                    //Get Touch Position
                    Vector3 WP = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector2 TouchPos = new Vector2(WP.x, WP.y);

                    //If The Touch Was On One of the Spaces
                    if (Collider == Physics2D.OverlapPoint(TouchPos))
                    {

                        FillSpace(TurnManager.instance.CurrentPlayer, TurnManager.instance.CurrentRound);
 
                    }
                }

            #endif

        }

        //Only Do Things With Mouse Down if On Standalone or Editor
        #if (UNITY_STANDALONE || UNITY_EDITOR)

            public void OnMouseDown()
            {

                FillSpace(TurnManager.instance.CurrentPlayer, TurnManager.instance.CurrentRound);

            }

        #endif

        /// <summary>
        /// Fill the Space 
        /// </summary>
        /// <param name="CurrentPlayer"></param>
        /// <param name="CurrentRound"></param>
        public void FillSpace(int CurrentPlayer, int CurrentRound)
        {

            if(IsFilled)
            {

                //Ideally, Tell Player That Space is Already Taken
                Debug.LogWarning(string.Format("{0} is Already Taken!", DisplayStr));
                return;

            }

            IsFilled = true;
            PlayerIndex = CurrentPlayer;

            AudioManager.instance.PlaySound(AudioManager.instance.SymbolPlaced);

            //When Making Animations, This Will Instead Be Called By An Animation Event (At the Last Frame)
            if (CurrentPlayer == 1)
            {

                SR.sprite = TurnManager.instance.GetPlayerOneSymbol();


            }
            else if (CurrentPlayer == 2)
            {

                SR.sprite = TurnManager.instance.GetPlayerTwoSymbol();

            }

            MoveRecorder.instance.RecordMove(this, CurrentPlayer, CurrentRound, SR.sprite);
            GameManager.instance.CheckVictory(this);

        }

        /// <summary>
        /// Resets This Space for the Next Game.
        /// </summary>
        public void ResetSpace()
        {

            IsFilled = false;
            PlayerIndex = 0;
            SR.sprite = null;

        }

    }

}
