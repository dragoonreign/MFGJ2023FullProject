using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject checkPoint;
    public string sceneName;
    [SerializeField] private MyDefaultInputAction myDefaultInputAction;
    [SerializeField] private InputAction m_reset;
    public PlayerInput m_PlayerInput;
    public GameObject _player;

    public bool _isGameOver;

    private void Awake() {
        instance = this;
        myDefaultInputAction = new MyDefaultInputAction();
    }

    private void OnEnable() {
        m_reset = myDefaultInputAction.Player.Reset;
        m_reset.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_reset.WasPerformedThisFrame() && _isGameOver)
        {
            DoResetLevel();
        }
    }

    public void OnSceneUpdate()
    {
        SceneManager.LoadScene(sceneName);
    }

    
    public void DoGameOver()
    {
        _isGameOver = true;
        DoDisablePlayerInput();
    }

    public void DoResetLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void DoDisablePlayerInput()
    {
        _player.gameObject.GetComponent<PlayerController>().enabled = false;
    }
}
