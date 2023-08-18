using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pass Text References")]
    [SerializeField]
    private TMP_Text _totalMatches, _totalTurns, _totalCombo, _gameLevel;
    [Header("Pass Loading Screen Reference")]
    [SerializeField]
    private Transform _loagingScreen;

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
    //Update score board on main menu
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
    //button click to start gamescene from main menu
    public void StartGameScene()
    {
        _loagingScreen.gameObject.SetActive(true);
        _loagingScreen.gameObject.GetComponent<LoadingScreenAsyncScript>().StartLoading("GameScene");
    }
}
