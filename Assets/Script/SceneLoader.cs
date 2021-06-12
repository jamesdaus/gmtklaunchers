using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MrushumeMan.SeanManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] int sceneLoadTime = 2;

        void Start()
        {
            
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadSettings()
        {

        }

        public void LoadCredits()
        {

        }

        public void LoadNextScean()
        {
            StartCoroutine(GetNextScene());
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
}

