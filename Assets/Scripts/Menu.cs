using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class Menu : MonoBehaviour
{
    public GameObject Collectibles;

    public GameObject OptionsScreen;

    public string LevelSelect;

    public Text CountingDowm;

    public float TimeRemaining = 0f;


    public GameObject LoadingScreen;
    public Text LoadingText;


    private int _highScore = 0;
    public int HighScore
    {
        get => _highScore;
        set
        {
            _highScore = value;
            highScore.text = value.ToString();
        }
    }
    
    
    [FormerlySerializedAs("HighScore")] [SerializeField]
    private Text highScore;
    public static Menu instance;

    public int MusicToPlays;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        highScore.text = HighScore.ToString();
        AudioManager.instance.MainThemePlay();
    }

    // Update is called once per frame
    void Update()
    {

        if (TimeRemaining >= 0)
        {
            TimeRemaining += 1 * Time.deltaTime;
            CountingDowm.text = TimeRemaining.ToString("0");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(LevelSelect);
        StartCoroutine(LoadingLoading());
    }
    public void OpenOptions()
    {
        OptionsScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        OptionsScreen.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadingLoading()
    {
        LoadingScreen.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelSelect);

        asyncLoad.allowSceneActivation = false;
        while(!asyncLoad.isDone)
        {
            if(asyncLoad.progress >= .9f)
            {
                asyncLoad.allowSceneActivation = true;
                Time.timeScale = 1f;
            }

            yield return null;
        }
    }


}
