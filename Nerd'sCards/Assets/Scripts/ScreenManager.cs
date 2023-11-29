using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    void Awake()
    {
        if(Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToControls()
    {
        SceneManager.LoadScene(1);
    }

    public void StartPlaying()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayerWin()
    {
        SceneManager.LoadScene(3);
    }

    public void PlayerLose()
    {
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
