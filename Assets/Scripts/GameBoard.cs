using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <Comment Start>  Class Purpose
/// Class for managing state of cards on board as well as checking cards interation
/// </Class Purpose>
public class GameBoard : MonoBehaviour
{
    [Header("Time User Is Showed Cards In Start")]
    [SerializeField]
    private float StartGameAfter = 1f; // Time to show the cards before game start
    [SerializeField]
    private float _cellSpacing = 5f; //Space Between Cards in containor
    private List<Transform> _spawnCards = new List<Transform>(); // Cards Created by Game Board are stored for keeping track
    [SerializeField]
    private Transform _cardPrefab = default, _boardWidgetHolder = default; // Card prefab object which will be spawned on board and Board widget will hold the spawn cards
    [Header("Pass Score System Object")]
    [SerializeField]
    private ScoreSystem _scoreSystem;
    private Transform _tempCard = default; // For Card Creation to avoid garbage collection
    private Card _previousCard; // for keeping track of selected card comparing
    private int state = 0; // To maintain cards selected state
    private int _currentLevel = default;
    private Action _callbackGameController;
    public void Start()
    {
        _currentLevel = GameConstantsPlayerPref.GetGameLevel();
        SetCurrentLevelText(_currentLevel);
    }
    /// Takes face card sprites and pass it to card creation  
    public void SetBoard(List<Sprite> selectedCardFace, Action callbackGameController)
    {
        _callbackGameController = callbackGameController;
        ScaleCardToFitContainor(selectedCardFace[0],(float) selectedCardFace.Count / 2);
        for (int i = 0; i < selectedCardFace.Count; i++)
        {
            CreateCard(i, selectedCardFace[i]);
            CreateCard(i, selectedCardFace[i]);
        }
        ShuffleAndSetToBoard();
        StartCoroutine(StartGame());
    }
    // Shuffles the created card and sets the Cards on to the UI Canvas containor
    void ShuffleAndSetToBoard()
    {
        GameController.Shuffle(ref _spawnCards);
        foreach (Transform card in _spawnCards)
        {
            card.SetParent(_boardWidgetHolder);
            card.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
    // Spawns the card with the face card sprite
    void CreateCard(int id, Sprite cardFront)
    {
        _tempCard = Instantiate(_cardPrefab.gameObject).transform;
        _tempCard.gameObject.SetActive(true);
        _tempCard.GetComponent<Card>().Init(id, cardFront, ValidateCardCombination);
        _spawnCards.Add(_tempCard);
    }
    // validates cards matching and mismatching condition
    public void ValidateCardCombination(Card currentCard)
    {
        switch (state)
        {
            case 0:
                {
                    _previousCard = currentCard;
                    state = 1;
                    break;
                }
            case 1:
                {
                    if (_previousCard.CardID.Equals(currentCard.CardID))
                    {
                        StartCoroutine(DeactivateCards(_previousCard, currentCard));
                        _scoreSystem.CardsMatched_Score();
                        GameSoundManager.Instance.PlaySoundOneShot(GameSoundManager.SoundType.Match);
                    }
                    else
                    {
                        _scoreSystem.CardsMisMatchedScore();
                        GameSoundManager.Instance.PlaySoundOneShot(GameSoundManager.SoundType.MisMatch);
                        StartCoroutine(ResetCards(_previousCard, currentCard));
                    }
                    state = 0;
                    break;
                }
        }
    }
    // set cards to Back side if when cards selected mismatch
    IEnumerator ResetCards(Card card1, Card card2)
    {
        yield return new WaitForSeconds(1f);
        card1.ResetCard();
        card2.ResetCard();
    }
    // Deactivates card selected
    IEnumerator DeactivateCards(Card card1, Card card2)
    {
        yield return new WaitForSeconds(1f);
        card1.DeactivateCardAnimated();
        card2.DeactivateCardAnimated();
        _spawnCards.Remove(card1.transform);
        _spawnCards.Remove(card2.transform);
        ValidateGameEnd();
    }
    /// Check if spawncard array is empty, as matching cards are removed from the list,
    /// if list is empty then no more cards are left to match
    void ValidateGameEnd()
    {
        if (_spawnCards.Count <= 0)
        {
            _callbackGameController.Invoke();
            SetLevelLabel();
        }
    }
    void SetLevelLabel()
    {
        _currentLevel++;
        GameConstantsPlayerPref.SetGameLevel(_currentLevel);
        SetCurrentLevelText(_currentLevel);
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(StartGameAfter);
        foreach (Transform card in _spawnCards)
        {
            card.GetComponent<Card>().ShowCardSide(Card.CardSides.Back);
        }
    }
    public void ResetBoardElements()
    {
        foreach (Transform card in _spawnCards)
        {
            card.GetComponent<Card>().DeactivateCard();
        }
        _spawnCards.Clear();
    }
    public void ResetBoard(List<Sprite> selectedCardFace)
    {
        ResetBoardElements();
        _scoreSystem.ResetScoreForNewLevel();
        Transform[] exixtingCards = _boardWidgetHolder.GetComponentsInChildren<Transform>();
        for (int i = 1, j = 0; i < exixtingCards.Length; i += 2, j++)
        {
            exixtingCards[i].GetComponent<Card>().ResetCard(j, selectedCardFace[j]);
            exixtingCards[i].SetParent(null);
            _spawnCards.Add(exixtingCards[i]);
            exixtingCards[i + 1].GetComponent<Card>().ResetCard(j, selectedCardFace[j]);
            exixtingCards[i + 1].SetParent(null);
            _spawnCards.Add(exixtingCards[i + 1]);
        }
        ShuffleAndSetToBoard();
        StartCoroutine(StartGame());
    }
    //scalling cards according to containor widget
    void ScaleCardToFitContainor(Sprite referenceCardSize,float gridSize)
    {
        float containorWidth;
        float containorHeight;
        GridLayoutGroup boardContainor = _boardWidgetHolder.GetComponent<GridLayoutGroup>();
        containorWidth = _boardWidgetHolder.GetComponent<RectTransform>().rect.width;
        containorHeight = _boardWidgetHolder.GetComponent<RectTransform>().rect.height;
        float spriteWidth = referenceCardSize.rect.width;
        float spriteHeight = referenceCardSize.rect.height;
        float forcastCellWidth = (containorWidth - ( _cellSpacing * gridSize )) / gridSize;
        float forcastCellHeight = (containorHeight - ( _cellSpacing * gridSize )) / gridSize;
        float ratioWidth = forcastCellWidth / spriteWidth;
        float ratioHeight = forcastCellHeight / spriteHeight;
        if (ratioHeight > 1 && ratioWidth > 1)
        {
            boardContainor.cellSize = new Vector2(spriteWidth, spriteHeight);
        }
        else if (ratioWidth < ratioHeight)
        {
            boardContainor.cellSize = new Vector2(spriteWidth * ratioWidth, spriteHeight * ratioWidth);
        }
        else
        {
            boardContainor.cellSize = new Vector2(spriteWidth * ratioHeight, spriteHeight * ratioHeight);
        }

    }
    void SetCurrentLevelText(int value)
    {
        GameUIMnager.Instance.SetGameLevelText("" + value);
    }
}
