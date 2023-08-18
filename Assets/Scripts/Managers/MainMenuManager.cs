using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
 

    //In start if first time user enters default playerpref are set
    //in the GameConstantsPlayerPref class
    public void Start()
    {
        if(GameConstantsPlayerPref.GetFirstTimeGameOpenSet().Equals(0))
        {
            GameConstantsPlayerPref.SetDefaultGameValues();
        }
    }


}
