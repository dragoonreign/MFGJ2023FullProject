using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public int health = 3;

    [SerializeField]
    private int _maxHealth = 3;

    public UIManagerScript _uiManager;

    public GameManager _gameManager;

    private bool _gameIsOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        health--;

        _uiManager.UpdateHealth(health);

        if (health < 1)
        {
            Destroy(gameObject);
        }

    }
    
}
