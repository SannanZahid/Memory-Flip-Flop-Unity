using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pass Text References")]
    [SerializeField]
    private TMP_Text _totalMatches, _totalTurns, _totalCombo, _gameLevel;

    //In start if first time user enters default playerpref are set
    //in the GameConstantsPlayerPref class
    public void Start()
    {
        if(GameConstantsPlayerPref.GetFirstTimeGameOpenSet().Equals(0))
        {
            GameConstantsPlayerPref.SetDefaultGameValues();
        }
        SetMenuScoreBoard();
    }
    void SetMenuScoreBoard()
    {
        SetTotalComboText("" + GameConstantsPlayerPref.GetTotalCombo());
        SetTotalMatchesText("" + GameConstantsPlayerPref.GetTotalMatches());
        SetTotalTurnsText("" + GameConstantsPlayerPref.GetTotalTurns());
        SetGameLevelText("LEVEL " + GameConstantsPlayerPref.GetGameLevel());
    }
    public void SetTotalComboText(string messageText)
    {
        _totalCombo.text = messageText;
    }
    public void SetTotalMatchesText(string messageText)
    {
        _totalMatches.text = messageText;
    }
    public void SetTotalTurnsText(string messageText)
    {
        _totalTurns.text = messageText;
    }
    public void SetGameLevelText(string messageText)
    {
        _gameLevel.text = messageText;
    }

}
