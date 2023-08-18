using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <Comment Start>  Class Purpose
/// Class for managing state of cards on board as well as checking cards interation
/// </Class Purpose>
public class GameBoard : MonoBehaviour
{
    [Header("Time User Is Showed Cards In Start")]

    public float StartGameAfter = 1f; // Time to show the cards before game start

    private List<Transform> _spawnCards = new List<Transform>(); // Cards Created by Game Board are stored for keeping track
    [SerializeField]
    private Transform _cardPrefab = default, _boardWidgetHolder = default; // Card prefab object which will be spawned on board and Board widget will hold the spawn cards
    private Transform _tempCard = default; // For Card Creation to avoid garbage collection
    [SerializeField]
    private float _cellSpacing = 5f; //Space Between Cards in containor
    /// Takes face card sprites and pass it to card creation  
    public void SetBoard(List<Sprite> selectedCardFace)
    {
        ScaleCardToFitContainor(selectedCardFace[0],(float) selectedCardFace.Count / 2);
        for (int i = 0; i < selectedCardFace.Count; i++)
        {
            CreateCard(i, selectedCardFace[i]);
            CreateCard(i, selectedCardFace[i]);
        }
        ShuffleAndSetToBoard();
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
        _tempCard.GetComponent<Card>().Init(id, cardFront);
        _spawnCards.Add(_tempCard);
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
}
