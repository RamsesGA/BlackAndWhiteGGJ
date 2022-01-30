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
        SceneManager.LoadScene(0);
    }
}
