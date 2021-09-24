using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1f;
        int carentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++carentScene);
    }
}
