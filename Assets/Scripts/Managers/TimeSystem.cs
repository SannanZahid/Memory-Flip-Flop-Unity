using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    private float _defaultTime = default;
    public float TimeRemaining = 10;
    private bool _timerIsRunning = false;
    // In start original time is stored 
    // later if you want to reset time on level fail / complete
    public void Start()
    {
        _defaultTime = TimeRemaining;
    }
    // funcation is called when games starts to keep playtime record
    public void StartGameTimer()
    {
        _timerIsRunning = true;
    }
    //reset time one level is complete or fail
    public void ResetTimer()
    {
        TimeRemaining = _defaultTime;
        _timerIsRunning = true;
    }
    //stops timer in case of level complete so level fail is not called
    public void StopLevelTimer()
    {
        _timerIsRunning = false;
    }
    void Update()
    {
        if (_timerIsRunning)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                DisplayTime(TimeRemaining);
            }
            else
            {
                GameUIMnager.Instance.ToggleActivateLevelFailScreen(true);
                GameSoundManager.Instance.PlaySoundOneShot(GameSoundManager.SoundType.GameFail);
                TimeRemaining = 0;
                _timerIsRunning = false;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        GameUIMnager.Instance.SetGameTimerText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
