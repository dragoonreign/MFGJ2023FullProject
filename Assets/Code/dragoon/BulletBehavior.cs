using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public float cooldown;
    public GameObject bulletTrail;

    public void DoShootBullet()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * speed /*- rb.velocity*/, ForceMode.Impulse);
        StartCoroutine(BulletCooldown(cooldown));
    }

    public void OnBulletEnabled()
    {
        bulletTrail.SetActive(true);
    }

    IEnumerator BulletCooldown(float cooldown)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(cooldown);

        transform.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        //change tag names to something else.


        // if  (other.transform.gameObject.tag == "Zombie")
        // {
        //     Debug.Log("Zombie");
        //     other.transform.gameObject.SetActive(false);
        //     GameManager.instance.DoDefeatedEnemiesUIUpdate();
        // }
        // if  (other.transform.gameObject.tag == "ZombieSpecial")
        // {
        //     Debug.Log("ZombieSpecial");
        //     other.transform.gameObject.SetActive(false);
        //     GameManager.instance.DoDefeatedEnemiesUIUpdate();
        // }
        // if  (other.transform.gameObject.tag == "Wall")
        // {
        //     Debug.Log("Wall");
        //     wallCounter++;
        //     GetComponent<AudioSource>().Play();
        //     if (wallCounter >= 3)
        //     {
        //         transform.gameObject.SetActive(false);
        //         bulletTrail.SetActive(false);
        //         wallCounter = 0;
        //     }
        //     GameManager.instance.cinemachineShake.ShakeCamera(0.5f, 0.05f);
        // }
    }
}
