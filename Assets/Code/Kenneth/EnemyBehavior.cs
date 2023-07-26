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
    }

    //Enemy Pursues the Player when in Range

    private void EnemyPursuit()
    {
        if (_player != null)
        {
            _distance = Vector3.Distance(_player.transform.position, transform.position);
        }

        if (_distance <= _pursuitRange && _player != null)
        {
            Vector3 direction = transform.position - _player.transform.position;
            direction = direction.normalized;
            transform.position -= direction * Time.deltaTime * _pursuitSpeed;
        }
    }

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
        //Vector3 randomRecoil = new Vector3(Random.Range(-5f, 5f),0, 0);

        int randomRecoil = Random.Range(0, 4);

        switch (randomRecoil)
        {
            case 0:
                transform.position += (Vector3.forward * 75) * Time.deltaTime;
                break;
            case 1:
                transform.position += (Vector3.back * 75) * Time.deltaTime;
                break;
            case 2:
                transform.position += (Vector3.right * 75) * Time.deltaTime;
                break;
            case 3:
                transform.position += (Vector3.left * 75) * Time.deltaTime;
                break;
            default:
                break;

        }

    }
}
