using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBehavior : MonoBehaviour
{
    
    //GameObjects
    [SerializeField]
    private PlayerHealth _player;

    //Pursuit Variables
    private float _distance;

    [SerializeField]
    private float _pursuitRange = 3.0f;

    [SerializeField]
    private float _pursuitSpeed = 3.0f;

    [SerializeField]
    private float _rotateSpeed = 20;

    
    private float _canAttack = -1;

    private float _attackRate = 1.5f;

    private bool _enemyCollided;




    //Enemy AI
    [SerializeField]
    private EnemyAI enemyAI;

    public float sSpeed = 1;

    void Start()
    {
        if (enemyAI) return;
        _player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _enemyCollided = false;
    }

    void Update()
    {
        EnemyPursuit();

        if (_enemyCollided == false)
        {
            StopCoroutine(RecoilCooldown());
        }

        if (_distance > _pursuitRange)
        {
            RotateAIModelToWaypoint(enemyAI.target);
        }

    }

    //Enemy Pursues the Player when in Range

    private void EnemyPursuit()
    {
        //when the player is outside of pursuit distance
        if (_player != null && _enemyCollided == false)
        {
            _distance = Vector3.Distance(_player.transform.position, transform.position);

            // start or return to waypoint movement behavior
            StartEnemyWayPoint();
        }

        //when the player is within pursuit distance
        if (_distance <= _pursuitRange && _player != null && _enemyCollided == false)
        {
            Vector3 direction = transform.position - _player.transform.position;
            direction = direction.normalized;

            //Pursues the Player
            transform.position -= direction * Time.deltaTime * _pursuitSpeed;

            //Faces the Player
            // transform.forward -= direction * Time.deltaTime * _rotateSpeed;
            RotateAIModelToWaypoint(enemyAI.playerTarget);

            //stops waypoint movement behavior
            StopEnemyWayPoint();
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
                //EnemyRecoil();

                StartCoroutine(RecoilCooldown());
            }
        }
    }

    IEnumerator RecoilCooldown()
    {
        _enemyCollided = true;

        transform.position += (Vector3.back * 75 * Time.deltaTime);

        yield return new WaitForSeconds(0.5f);

        _enemyCollided = false;
    }
    
    void StopEnemyWayPoint()
    {
        enemyAI.enabled = false;
    }

    void StartEnemyWayPoint()
    {
        enemyAI.enabled = true;
    }

    public void RotateAIModelToWaypoint(Transform target)
    {
        Vector3 lookDirection = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
        lookDirection.Normalize();

        if (lookDirection == Vector3.zero) return;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), sSpeed * Time.deltaTime);
    }
}
