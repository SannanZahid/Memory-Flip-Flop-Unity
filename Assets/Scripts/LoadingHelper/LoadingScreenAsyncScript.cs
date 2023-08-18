using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenAsyncScript : MonoBehaviour
{
    [SerializeField]
    private Image _loadSlide;
    // to start loading functionality function is called from script 
    public void StartLoading(string sceneName)
    {
        StartCoroutine(AsyncLoading(sceneName));
    }
    // coroutine to start async loading for scene switch
    private IEnumerator AsyncLoading(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            _loadSlide.fillAmount = operation.progress;
            if(operation.progress>=0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

    }
}