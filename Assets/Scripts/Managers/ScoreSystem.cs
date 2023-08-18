using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int _matchScore = 0, _turnsScore = 0, _comboScore = 0, _totalMatchScore = 0, _totalTurnsScore = 0, _totalCombo = 0;
    bool _match = false;
    //set stored total match cards, total turn and total combo to scoreboard
    public void Start()
    {
        _totalMatchScore = GameConstantsPlayerPref.GetTotalMatches();
        SetTotalMatchScoreDashboard(_totalMatchScore);
        _totalTurnsScore = GameConstantsPlayerPref.GetTotalTurns();
        SetTotalTurnScoreDashboard(_totalTurnsScore);
        _totalCombo = GameConstantsPlayerPref.GetTotalCombo();
        SetTotalComboScoreDashboard(_totalCombo);
    }
    ///Reset Board to default on level complete and level fail
    public void ResetScoreForNewLevel()
    {
        _matchScore = 0;
        SetMatchScoreDashboard(0);
        _turnsScore = 0;
        SetTurnScoreDashboard(0);
        _comboScore = 0;
        SetComboScoreDashboard(0);
    }
    // To keep track of matching cards and sequelce of matching cards
    // for combo calculation called by GameBoard class 
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
    // To keep track of mismatching cards called by GameBoard class 
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
        GameConstantsPlayerPref.SetTotalMatches(_totalMatchScore);
    }
    void SetCardsTotalTurnsScore()
    {
        _totalTurnsScore++;
        SetTotalTurnScoreDashboard(_totalTurnsScore);
        GameConstantsPlayerPref.SetTotalTurns(_totalTurnsScore);
    }
    void SetCardsTotalComboScore()
    {
        _totalCombo++;
        SetTotalComboScoreDashboard(_totalCombo);
        GameConstantsPlayerPref.SetTotalCombo(_totalCombo);
    }
    /// <Comment Start>  Functions Purpose
    /// Score calculated is populated to scored board in game screen UI
    /// </Comment End>
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
