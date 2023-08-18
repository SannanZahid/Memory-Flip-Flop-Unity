using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int _matchScore = 0, _turnsScore = 0;

    public void CardsMatched_Score()
    {
        IncrementMatch();
        IncrementTurn();
    }
    public void CardsMisMatchedScore()
    {
        IncrementTurn();
    }
    void IncrementMatch()
    {
        _matchScore++;
    }
    void IncrementTurn()
    {
        _turnsScore++;
    }

}
