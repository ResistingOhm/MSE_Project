using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public string sceneToLoad = "GameScene";
    public float minLoadingTime = 1f;

    void Start()
    {
        StartCoroutine(LoadSceneWithMinTime());
    }

    IEnumerator LoadSceneWithMinTime()
    {
        float timer = 0f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;

            if (operation.progress >= 0.9f && timer >= minLoadingTime)
            {
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
