using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void UnPause()
    {
        LeevelManager.Instance.UnPause();
    }

    public void QuitMain()
    {
        FindObjectOfType<LeevelManager>().UnPause();
        SceneManager.LoadScene(0);
    }
}
