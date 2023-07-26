using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


    // Start is called before the first frame update
    void Start()
    {
        
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
        //_gameManager.GameOver();

        _gameOverText.gameObject.SetActive(true);

        _restart.gameObject.SetActive(true);

        //How the game OVer will pop up
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
}
