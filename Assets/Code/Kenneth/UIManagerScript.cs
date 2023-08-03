using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using UnityEditor.SceneManagement;

public class UIManagerScript : MonoBehaviour
{

    [SerializeField]
    private Sprite[] _healthSprites;

    [SerializeField]
    private Image _healthImage;

    [SerializeField]
    private TMP_Text _gameOverText;

    [SerializeField]
    private TMP_Text _restart;

    [SerializeField]
    private GameObject _player;

    private GameManager _gameManager;

    private bool _restartOptionActive;

    //----Timer Variables-----
    [SerializeField]
    private float _timeRemaining = 10;

    private bool _timerIsRunning;

    [SerializeField]
    public TMP_Text timeText;

    [SerializeField]
    private GameObject _directionalLight;

    private bool _playerHasDied;

    // Start is called before the first frame update
    void Start()
    {
        _timerIsRunning = true;
        _playerHasDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        RunningTimer();
    }

    //Subtracts Health from the UI
    public void UpdateHealth(int currentHealth)
    {
        _healthImage.sprite = _healthSprites[currentHealth];

        if (currentHealth <= 0)
        {
            _playerHasDied = true;
            GameOverSequence();
        }
    }

    //Sequence of events when the player dies
    public void GameOverSequence()
    {
        _restartOptionActive = true;

        _gameOverText.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);

        RestartLevel();

        //How the game over will pop up
        StartCoroutine(GameOverFlicker());
    }

    //Game Over Text
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);

            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    //Restart the Level after Game Over
    private void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R) && (_restartOptionActive == true))
        {
            _gameManager.OnSceneUpdate();
        }
    }

    //-----Main Menu-----

    public void Play()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }


    //-----Level Select------

    //public void SpringLevel()
    //{
    //    Debug.Log("Button clicked");
    //    SceneManager.LoadScene(1);
    //}

    //public void SummerLevel()
    //{
    //    Debug.Log("Button clicked");
    //    SceneManager.LoadScene(2);
    //}

    //public void FallLevel()
    //{
    //    Debug.Log("Button clicked");
    //    SceneManager.LoadScene(3);
    //}

    //public void WinterLevel()
    //{
    //    Debug.Log("Button clicked");
    //    SceneManager.LoadScene(4);
    //}

    public void DoLoadLevel(string sceneToLoad)
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene(sceneToLoad);
    }

    //----------Timer----------
    
    //References the Display of Minutes and Seconds
    private void DisplayTime(float timeToDisplay)
    {

        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    //Makes the Timer decrease every seconds
    public void RunningTimer()
    {
        if (_timerIsRunning == true)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;

                DisplayTime(_timeRemaining);
            }
            else
            {
                Debug.Log("Out of Time Sucka!");

                // _player.gameObject.SetActive(false);
                GameManager.instance.DoGameOver();
                _timeRemaining = 0;
                _timerIsRunning = false;
                GameOverSequence();
            }
        }

        if (_playerHasDied == true)
        {
            _timeRemaining += Time.deltaTime;
        }
    }

    
}
