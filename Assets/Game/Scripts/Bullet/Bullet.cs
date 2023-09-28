using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    // Get data poperties
    public ScriptTableBullet data;
    private float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if(time > 2)
        {
            Destroy(this.gameObject);
        }
    }
}
