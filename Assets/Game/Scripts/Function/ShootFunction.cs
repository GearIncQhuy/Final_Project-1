using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFunction : MonoBehaviour
{
    /**
     * 
     */
    public void Shoot(GameObject bulletPreFab, float speed, GameObject obj)
    {
        GameObject bullet = Instantiate(bulletPreFab, obj.transform.position, Quaternion.identity);
        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        if (bulletRigid != null)
        {
            bulletRigid.velocity = obj.transform.forward * speed;
            Destroy(bullet, 2.0f);
        }
    }
}