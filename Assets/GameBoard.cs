using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("Time User Is Showed Cards In Start")]

    public float StartGameAfter = 1f; // Time to show the cards before game start

    private List<Transform> _spawnCards = new List<Transform>(); // Cards Created by Game Board are stored for keeping track
    [SerializeField]
    private Transform _cardPrefab = default, _boardWidgetHolder = default; // Card prefab object which will be spawned on board and Board widget will hold the spawn cards
    private Transform _tempCard = default; // For Card Creation to avoid garbage collection
    /// Takes face card sprites and pass it to card creation  
    public void SetBoard(List<Sprite> selectedCardFace)
    {
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
        }
    }
    // Spawns the card with the face card sprite
    void CreateCard(int id, Sprite cardFront)
    {
        _tempCard = Instantiate(_cardPrefab.gameObject).transform;
        _tempCard.gameObject.SetActive(true);
        _spawnCards.Add(_tempCard);
    }
   
}
