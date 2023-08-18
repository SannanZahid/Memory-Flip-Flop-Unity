using UnityEngine;

public class GameSoundManager : Singleton<GameSoundManager>
{
    [SerializeField]
    private int _soundPercentage;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _matchClip, _misMatchClip, _gameCompleteClip, _BtnSoundClip, _cardFlipSoundClip, _gameFailClip;
    public enum SoundType { Match, MisMatch, GameComplete, ButtonSound, CardFlip, GameFail }

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }
    public int GetSoundVolume()
    {
        return _soundPercentage;
    }
    public void SetSoundVolume(int volume)
    {
        _soundPercentage = volume;
        GameConstantsPlayerPref.SetSound(volume);
    }
    public void PlaySoundOneShot(SoundType playSound)
    {
        switch(playSound)
        {
        case SoundType.Match:
            {
                _audioSource.PlayOneShot(_matchClip);
               break;
            }
        case SoundType.MisMatch:
            {
                _audioSource.PlayOneShot(_misMatchClip);
               break;
            }
        case SoundType.GameComplete:
            {
                 _audioSource.PlayOneShot(_gameCompleteClip);
               break;
            }
        case SoundType.ButtonSound:
            {
                _audioSource.PlayOneShot(_BtnSoundClip);
               break;
            }
            case SoundType.CardFlip:
            {
                 _audioSource.PlayOneShot(_cardFlipSoundClip);
                break;
            }
            case SoundType.GameFail:
                {
                    _audioSource.PlayOneShot(_gameFailClip);
                    break;
                }
        }
    }
}
