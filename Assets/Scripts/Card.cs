using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [Header("Front and Back Side Card Images")]
    [SerializeField]
    private Transform _cardFront, _cardBack;//Reference to Front and Back card image of Object
    public int CardID { private set; get; } // Card id to uniquely a card
    private Button _cardBtn; //Refernce to button for card interation toggle
    public enum CardSides { Front, Back } //State of the card
    //Initialize the card front with sprite, adds button and binds interation listener to button
    public void Init(int cardId, Sprite cardFace)
    {
        CardID = cardId;
        _cardFront.GetComponent<Image>().sprite = cardFace;
        _cardBtn = transform.gameObject.AddComponent<Button>();
        _cardBtn.onClick.AddListener(CardInteraction);
        ShowCardSide(CardSides.Front);
    }
    // for calling card side functionality
    public void ShowCardSide(CardSides cardSide)
    {
        switch (cardSide)
        {
            case CardSides.Front:
                {
                    break;
                }
            case CardSides.Back:
                {
                    break;
                }
        }
    }
    // Capturing user input through button
    public void CardInteraction()
    {
        _cardBtn.interactable = false;
        ShowCardSide(CardSides.Front);
    }
}
