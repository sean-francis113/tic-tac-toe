namespace TicTacToe.Enums
{

    /// <summary>
    /// Enumerator Representing Tags for Each UI Element.
    /// </summary>
    public enum UITag
    {

        /// <summary>
        /// Default Value.
        /// </summary>
        None,
        
        /// <summary>
        /// Main Menu.
        /// </summary>
        MainMenu,

        /// <summary>
        /// Settings.
        /// </summary>
        Settings,

        /// <summary>
        /// Gameplay.
        /// </summary>
        Gameplay,

        /// <summary>
        /// In Game Ribbon Menu.
        /// </summary>
        Ribbon,

        /// <summary>
        /// Loading Screen.
        /// </summary>
        LoadingScreen,

        /// <summary>
        /// End Game Screen.
        /// </summary>
        EndGame

    }

    public enum GridType
    {

        /// <summary>
        /// Default Value
        /// </summary>
        None,

        /// <summary>
        /// A 3x3 Grid
        /// </summary>
        ThreeByThree,

        /// <summary>
        /// A 4x4 Grid
        /// </summary>
        FourByFour

    }

}
