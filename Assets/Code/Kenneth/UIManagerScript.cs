using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

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

    private GameManager _gameManager;

    private bool _restartOptionActive;


    [SerializeField]
    public Button playButton;

    [SerializeField]
    public Button quitButton;

    [SerializeField]
    public Button springButton;

    [SerializeField]
    public Button summerButton;

    [SerializeField]
    public Button fallButton;

    [SerializeField]
    public Button winterButton;

    


    // Start is called before the first frame update
    void Start()
    {
        //-------Main Menu Buttons-----------

        //Play Button
        Button playBtn = playButton.GetComponent<Button>();
        playBtn.onClick.AddListener(PlayButton);

        //Exit Button
        Button quitBtn = quitButton.GetComponent<Button>();
        quitBtn.onClick.AddListener(QuitButton);

        //---------Level Select Buttons--------

        //Spring Level
        Button springBtn = springButton.GetComponent<Button>();
        springBtn.onClick.AddListener(SpringLevel);

        //SummerLevel
        Button summerBtn = summerButton.GetComponent<Button>();
        summerBtn.onClick.AddListener(SummerLevel);

        //Fall Level
        Button fallBtn = fallButton.GetComponent<Button>();
        fallBtn.onClick.AddListener(FallLevel);

        //Winter Level
        Button winterBtn = winterButton.GetComponent<Button>();
        winterBtn.onClick.AddListener(WinterLevel);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Subtracts Health from the UI
    public void UpdateHealth(int currentHealth)
    {
        _healthImage.sprite = _healthSprites[currentHealth];

        if (currentHealth <= 0)
        {
            GameOverSequence();
        }
    }


    //Sequence of events when the player dies
    private void GameOverSequence()
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

    private void PlayButton()
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene(0);
    }

    private void QuitButton()
    {
        Debug.Log("Button clicked");
        Application.Quit();
    }


    //-----Level Select------

    private void SpringLevel()
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene(1);
    }

    public void SummerLevel()
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene(2);
    }

    public void FallLevel()
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene(3);
    }

    public void WinterLevel()
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene(4);
    }
}
