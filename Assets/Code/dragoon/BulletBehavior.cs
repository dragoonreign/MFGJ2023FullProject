using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public float cooldown;
    public GameObject bulletTrail;
    public UIManagerScript _UIManagerScript;

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

        if (transform.gameObject.name == "Bullet(Clone)")
        {
            if (other.transform.gameObject.tag == "Enemy")
            {
                other.transform.gameObject.SetActive(false);
            }
        }

        if (transform.gameObject.name == "EnemyBullet(Clone)")
        {
            if (other.transform.gameObject.tag == "Player")
            {
                _UIManagerScript.UpdateHealth(1);
                other.transform.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        //change tag names to something else.

        if (transform.gameObject.name == "Bullet(Clone)")
        {
            if (other.transform.gameObject.tag == "Enemy")
            {
                other.transform.gameObject.SetActive(false);
            }
        }
    }
}
