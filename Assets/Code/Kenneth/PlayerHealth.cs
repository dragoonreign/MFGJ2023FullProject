using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int _health = 3;

    [SerializeField]
    private int _maxHealth = 3;

    public UIManagerScript _uiManager;

    public GameManager _gameManager;
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
        _health--;

        _uiManager.UpdateHealth(_health);

        if (_health < 1)
        {
            //_health = 0;
            Destroy(gameObject);
            //_gameManager.GameOver();
        }
    }

    public void PlayerRespawn()
    {

    }
}
