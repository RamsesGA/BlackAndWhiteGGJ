using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    private Rigidbody2D g_rb;

    private void 
    Start() {
        g_rb = GetComponent<Rigidbody2D>();
    }

    private void
    OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Trap")) {
          RestartLevel();
        }
    }

    private void
    Die() {
        //g_rb.bodyType = RigidbodyType2D.Static;
    }

    private void
    RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
