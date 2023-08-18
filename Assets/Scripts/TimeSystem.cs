using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    private float _defaultTime = default;
    public float TimeRemaining = 10;
    private bool _timerIsRunning = false;
    public void Start()
    {
        _defaultTime = TimeRemaining;
    }
    public void StartGameTimer()
    {
        _timerIsRunning = true;
    }
    public void ResetTimer()
    {
        TimeRemaining = _defaultTime;
        _timerIsRunning = true;
    }
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
