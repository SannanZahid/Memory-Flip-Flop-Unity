using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <Comment Start>  Class Purpose
//   Class for creating game board with required card sprites and providing basic shuffling functionality,
//    class also keep tack of the game state.
/// </Comment End>
public class GameController : MonoBehaviour
{
    [Header("Check if you want to load from resource")]
    public bool LoadFromResources = false;
    [Header("Place Path to the sprite or atlas in resources directory")]
    public string PathToResourceSprite;
    [Header("Pass Sprites Of Facing Cards")]
    [SerializeField]
    private List<Sprite> _cardFace = new List<Sprite>(); // Conatains Face Card Sprites which are passed from Editor.
    [Header("Pass GameBoard Object Reference")]
    [SerializeField]
    private GameBoard _gameBoard = default; // Reference to game board.
    public int CardToPlaceOnBoard = 8; // How many unique cards the board will have.
    bool resetFailScreenflag = true;
    void Start()
    {
        if(LoadFromResources)
        {
            _cardFace.Clear();
            _cardFace = Resources.LoadAll<Sprite>(PathToResourceSprite).ToList();
        }
        InitializeBoard();
    }
    //Function Shuffles the sprites so everytime new face card are spawn on to the board.
    void InitializeBoard()
    {
        Shuffle(ref _cardFace);
        _gameBoard.SetBoard(GetShuffleFaceCards(), LevelComplete);
    }
    // Returns the number of cards to be placed on board from sprite list.
    List<Sprite> GetShuffleFaceCards()
    {
        return _cardFace.Take(CardToPlaceOnBoard).ToList();
    }
    //For shuffling objects provide through List.
    public static void Shuffle<T>(ref List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public void ResetLevelOnFail()
    {
        if(resetFailScreenflag)
        {
            resetFailScreenflag = false;
            StartCoroutine(ResetGame());
        }
    }
    public void StartNextLevel()
    {
        GameUIMnager.Instance.ToggleActivateLevelCompleteScreen(false);
        StartCoroutine(StartGame());
    }
    public void LevelComplete()
    {
        GameSoundManager.Instance.PlaySoundOneShot(GameSoundManager.SoundType.GameComplete);
        GameUIMnager.Instance.ToggleActivateLevelCompleteScreen(true);
    }
    void ShuffleAndResetBoard()
    {
        Shuffle(ref _cardFace);
        _gameBoard.ResetBoard(GetShuffleFaceCards());
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.2f);
        ShuffleAndResetBoard();
    }
    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1f);
        resetFailScreenflag = true;
        GameUIMnager.Instance.ToggleActivateLevelFailScreen(false);
        ShuffleAndResetBoard();
    }
}
