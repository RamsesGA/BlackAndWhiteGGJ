using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
  private Rigidbody2D g_rb;

  private void
  Start()
  {
    g_rb = GetComponent<Rigidbody2D>();
  }

  private void
  OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Trap"))
    {
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      gameObject.GetComponent<Rigidbody2D>().simulated = false;
      Invoke("RestartLevel", 1.0f);
      AudioManager.Instance.PlayEffect("Dead");
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Trap"))
    {
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      gameObject.GetComponent<Rigidbody2D>().simulated = false;
      Invoke("RestartLevel", 1.0f);
      AudioManager.Instance.PlayEffect("Dead");
    }
  }


  private void
    Die()
  {
    //g_rb.bodyType = RigidbodyType2D.Static;
  }

  private void
  RestartLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
