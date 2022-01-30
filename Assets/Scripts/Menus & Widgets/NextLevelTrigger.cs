using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
      var maxIndex = SceneManager.sceneCountInBuildSettings;
      var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        AudioManager.Instance.PlayEffect("Win");
    if (nextIndex >= maxIndex)
    {
      SceneManager.LoadScene(0);
    }
    else
    {
        SceneManager.LoadScene(nextIndex);
    }
    }
}
