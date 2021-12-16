using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;





public static class Loader
{

    private class LoadingMonoBehaviour : MonoBehaviour
    {
    }


    private static Action onLoaderCallback;
    private static AsyncOperation asyncOperation;

    public static void LoadScene(string name)
    {
        asyncOperation = null;

        onLoaderCallback += () =>
        {
            GameObject newGameObject = new GameObject("Whatever this is");
            newGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneWithAysnc(name));
        };

        SceneManager.LoadScene("Loading");
    }

    private static IEnumerator LoadSceneWithAysnc(string name)
    {
        yield return null;

        asyncOperation = SceneManager.LoadSceneAsync(name);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (asyncOperation != null)
        {
            float returnValue = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            return returnValue;
        }
        else
        {
            return 0f;
        }
    }

    public static void LoadSceneCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}