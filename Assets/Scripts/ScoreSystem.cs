using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int _matchScore = 0, _turnsScore = 0, _comboScore = 0, _totalMatchScore = 0, _totalTurnsScore = 0, _totalCombo = 0;
    bool _match = false;

    public void ResetScoreForNewLevel()
    {
        _matchScore = 0;
        SetMatchScoreDashboard(0);
        _turnsScore = 0;
        SetTurnScoreDashboard(0);
        _comboScore = 0;
        SetComboScoreDashboard(0);
    }
    public void CardsMatched_Score()
    {
        if (_match)
        {
            CardsComboScore();
        }
        else
        {
            _match = true;
        }
        IncrementMatch();
        IncrementTurn();
    }
    public void CardsMisMatchedScore()
    {
        _match = false;
        IncrementTurn();
        _comboScore = 0;
        SetComboScoreDashboard(_comboScore);
    }
    void IncrementMatch()
    {
        _matchScore++;
        SetMatchScoreDashboard(_matchScore);
        SetCardsTotalMatchScore();
    }
    void IncrementTurn()
    {
        _turnsScore++;
        SetTurnScoreDashboard(_turnsScore);
        SetCardsTotalTurnsScore();
    }
    void CardsComboScore()
    {
        _comboScore++;
        SetComboScoreDashboard(_comboScore);
        SetCardsTotalComboScore();
    }
    void SetCardsTotalMatchScore()
    {
        _totalMatchScore++;
        SetTotalMatchScoreDashboard(_totalMatchScore);
    }
    void SetCardsTotalTurnsScore()
    {
        _totalTurnsScore++;
        SetTotalTurnScoreDashboard(_totalTurnsScore);
    }
    void SetCardsTotalComboScore()
    {
        _totalCombo++;
        SetTotalComboScoreDashboard(_totalCombo);
    }
    void SetComboScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetComboText("" + value);
    }
    void SetMatchScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetMatchesText("" + value);
    }
    void SetTurnScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetTurnsText("" + value);
    }
    void SetTotalComboScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetTotalComboText("" + value);
    }
    void SetTotalMatchScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetTotalMatchesText("" + value);
    }
    void SetTotalTurnScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetTotalTurnsText("" + value);
    }

}
