using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <Comment Start>  Class Purpose
/// Class for managing state of cards as well as handeling card functionality
/// </Class Purpose>
public class Card : MonoBehaviour
{
    [Header("Front and Back Side Card Images")]
    [SerializeField]
    private Transform _cardFront, _cardBack;//Reference to Front and Back card image of Object
    public int CardID { private set; get; } // Card id to uniquely a card
    private Button _cardBtn; //Refernce to button for card interation toggle
    public enum CardSides { Front, Back } //State of the card
    Action<Card> _callbackGameBoard; //Callback for card selected
    float _cardRotationVelocity;// Used for SmoothDampAngle to achieve card flipping animation functionality
    bool flipAnimFlag = true; // Flag to wait for flipping animation to complete

    //Initialize the card front with sprite, adds button and binds interation listener to button
    public void Init(int cardId, Sprite cardFace, Action<Card> callback)
    {
        _callbackGameBoard = callback;
        CardID = cardId;
        _cardFront.GetComponent<Image>().sprite = cardFace;
        _cardBtn = transform.gameObject.AddComponent<Button>();
        _cardBtn.onClick.AddListener(CardInteraction);
        ShowCardSide(CardSides.Front);
    }
    // for calling card side functionality
    public void ShowCardSide(CardSides cardSide)
    {
        if (!flipAnimFlag)
            return;
        switch (cardSide)
        {
            case CardSides.Front:
                {
                    StartCoroutine(CardAnimationRotationCoroutine(_cardBack, _cardFront));
                    break;
                }
            case CardSides.Back:
                {
                    StartCoroutine(CardAnimationRotationCoroutine(_cardFront, _cardBack));
                    break;
                }
        }
        GameSoundManager.Instance.PlaySoundOneShot(GameSoundManager.SoundType.CardFlip);
    }
    // Capturing user input through button
    public void CardInteraction()
    {
        _cardBtn.interactable = false;
        _callbackGameBoard.Invoke(this);
        ShowCardSide(CardSides.Front);
    }
    // sets card to back side
    public void ResetCard()
    {
        ShowCardSide(CardSides.Back);
        _cardBtn.interactable = true;
    }
    //reset the cards on level fail or level complete
    public void ResetCard(int cardId, Sprite cardFace)
    {
        CardID = cardId;
        _cardFront.GetComponent<Image>().sprite = cardFace;
        ShowCardSide(CardSides.Front);
        _cardBtn.interactable = true;
    }
    public void DeactivateCard()
    {
        _cardFront.gameObject.SetActive(false);
        _cardBack.gameObject.SetActive(false);
        _cardBtn.interactable = false;
        StopAllCoroutines();
    }

    // set card match animation
    public void DeactivateCardAnimated()
    {
        AnimateMatchCard();
        _cardBtn.interactable = false;
    }
    //For animation of flipping cards.
    //Rotates card to 90 degree before swaping to card desired face either front or back
    IEnumerator CardAnimationRotationCoroutine(Transform cardF, Transform cardB)
    {
        flipAnimFlag = false;
        cardB.gameObject.SetActive(false);
        cardF.gameObject.SetActive(true);
        cardF.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        cardB.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));
        while (true)
        {
            float Angle = Mathf.SmoothDampAngle(cardF.eulerAngles.y, 90f, ref _cardRotationVelocity, 0.05f);
            cardF.rotation = Quaternion.Euler(0, Angle, 0);
            if (cardF.eulerAngles.y >= 89.0f)
            {
                cardF.gameObject.SetActive(false);
                cardB.gameObject.SetActive(true);
                break;
            }
            else
                yield return null;
        }
        while (true)
        {
            float Angle = Mathf.SmoothDampAngle(cardB.eulerAngles.y, 0f, ref _cardRotationVelocity, 0.05f);
            cardB.rotation = Quaternion.Euler(0, Angle, 0);
            if (cardB.eulerAngles.y <= 0.1f)
            {
                break;
            }
            else
                yield return null;
        }
        cardF.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        cardB.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        flipAnimFlag = true;
    }
    //play animation of scale on matching cards
    public void AnimateMatchCard()
    {
        StartCoroutine(scaleOverTime(_cardFront, new Vector3(0, 0, 0), 0.25f));
        StartCoroutine(scaleOverTime(_cardBack, new Vector3(0, 0, 0), 0.25f));
    }
    // coroutine for scaling animation
    IEnumerator scaleOverTime(Transform objectToScale, Vector3 toScale, float duration)
    {
        float counter = 0;
        Vector3 startScaleSize = objectToScale.localScale;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            yield return null;
        }
        objectToScale.gameObject.SetActive(false);
        objectToScale.localScale = Vector3.one;
    }
}
