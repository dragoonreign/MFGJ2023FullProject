using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject GunPoint;
    bool bCanShootBullet = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bCanShootBullet)
        {
            StartCoroutine(ExampleCoroutine());
        }
    }

    private void DoShoot()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(ObjectPool.SharedInstance.pipeBulletObjects); 
        if (!bullet) return;
		if (bullet != null) {
			bullet.transform.position = GunPoint.transform.position;
			bullet.transform.rotation = GunPoint.transform.rotation;
			bullet.SetActive(true);
		}
		bullet.GetComponent<BulletBehavior>().DoShootBullet();
		// var bullet = Instantiate(BulletPrefab, GunPoint.transform.position, aimDirection.transform.rotation);
		// bullet.GetComponent<BulletBehavior>().OnBulletEnabled();
		// bulletCount--;
    }

    IEnumerator ExampleCoroutine()
    {
        bCanShootBullet = false;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        DoShoot();
        bCanShootBullet = true;
    }
}
