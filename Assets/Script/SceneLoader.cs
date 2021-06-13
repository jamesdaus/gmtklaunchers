using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int sceneLoadTime = 2;
    [SerializeField] int loadMenu = 1;

    public void LoadNextScean()
    {
        StartCoroutine(GetNextScene());
    }
    
    public IEnumerable LoadMenu()
    {
        yield return new WaitForSeconds(loadMenu);
        SceneManager.LoadSceneAsync(0);
        yield return new WaitForSeconds(loadMenu);
    }

    private IEnumerator GetNextScene()
    {
        DontDestroyOnLoad(gameObject);
        yield return new WaitForSeconds(sceneLoadTime);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(sceneLoadTime);
        Destroy(gameObject);
    }
}

