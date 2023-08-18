using UnityEngine;
using TMPro;
public class GameUIMnager : Singleton<GameUIMnager>
{
    [Header("Pass Text References")]
    [SerializeField]
    private TMP_Text _matchesTxt, _turnsTxt, _comboTxt, _totalMatchesTxt, _totalTurnsTxt, _totalComboTxt, _gameLevelTxt,_gameTimerTxt;
    public Transform _LevelCompleteScreen;
    public void SetMatchesText(string messageText)
    {
        _matchesTxt.text = messageText;
    }
    public void SetTurnsText(string messageText)
    {
        _turnsTxt.text = messageText;
    }
    public void SetComboText(string messageText)
    {
        _comboTxt.text = messageText;
    }
    public void SetTotalComboText(string messageText)
    {
        _totalComboTxt.text = messageText;
    }
    public void SetTotalMatchesText(string messageText)
    {
        _totalMatchesTxt.text = messageText;
    }
    public void SetTotalTurnsText(string messageText)
    {
        _totalTurnsTxt.text = messageText;
    }
    public void SetGameLevelText(string messageText)
    {
        _gameLevelTxt.text ="LEVEL "+ messageText;
    }
    public void SetGameTimerText(string messageText)
    {
        _gameTimerTxt.text = "TIME " + messageText;
    }
    public void ToggleActivateLevelCompleteScreen(bool flag)
    {
        _LevelCompleteScreen.gameObject.SetActive(flag);
    }

}
