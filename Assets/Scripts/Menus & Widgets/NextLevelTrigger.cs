using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    AudioManager.Instance.PlayEffect("Win");
    var player = FindObjectOfType<PlayerMovement>();
    if (player)
    {
      player.enabled = false;
    }
    Invoke("changeLevel", 1.0f);
  }
  void changeLevel()
  {
    var maxIndex = SceneManager.sceneCountInBuildSettings;
    var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
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
