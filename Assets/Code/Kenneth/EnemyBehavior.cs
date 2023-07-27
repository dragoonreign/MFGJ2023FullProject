using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBehavior : MonoBehaviour
{
    //Waypoint Variables
    //public List<Transform> wayPoints;

    //private NavMeshAgent _enemyAgent;

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

    [SerializeField]
    private EnemyAI enemyAI;

    public float sSpeed = 1;

    void Start()
    {
        
        _player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        
        //_enemyAgent = GetComponent<NavMeshAgent>();


        ////Waypoint Error Check & First Waypoint
        //if (wayPoints[0] != null)
        //{
        //    _enemyAgent.SetDestination(wayPoints[0].position);
        //}
    }


    void Update()
    {
        EnemyPursuit();
        RotateAIModelToWaypoint();
    }

    //Enemy Pursues the Player when in Range

    private void EnemyPursuit()
    {
        if (_player != null)
        {
            _distance = Vector3.Distance(_player.transform.position, transform.position);
            StartEnemyWayPoint();
        }

        if (_distance <= _pursuitRange && _player != null)
        {
            Vector3 direction = transform.position - _player.transform.position;
            direction = direction.normalized;

            //Pursues the Player
            transform.position -= direction * Time.deltaTime * _pursuitSpeed;

            //Faces the Player
            transform.forward -= direction * Time.deltaTime * _rotateSpeed;

            //stops waypoint movement behavior
            StopEnemyWayPoint();
        }
    }

    //push comment
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (_player != null)
            {
                _player.Damage();
                EnemyRecoil();
            }

        }
    }

    private void EnemyRecoil()
    {
        transform.position += (Vector3.back * 75) * Time.deltaTime;
    }

    void StopEnemyWayPoint()
    {
        enemyAI.enabled = false;
    }

    void StartEnemyWayPoint()
    {
        enemyAI.enabled = true;
    }

    public void RotateAIModelToWaypoint()
    {
        Vector3 lookDirection = enemyAI.target.transform.position - transform.position;
        lookDirection.Normalize();

        if (lookDirection == Vector3.zero) return;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), sSpeed * Time.deltaTime);
    }
}
