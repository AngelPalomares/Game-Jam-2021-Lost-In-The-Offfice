using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public GameObject Options, PauseScreen;

    public string MainMenu;

    private bool isPaused;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseUnpause()
    {
        if(!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 1f;
        }
    }
    public void OpenOptions()
    {
        Options.SetActive(true);
    }

    public void CloseOptions()
    {
        Options.SetActive(false);
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(MainMenu);
    }
}
