using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeRandom : MonoBehaviour
{
    private ManagerScript manager;
    [SerializeField] private GameObject boomPreFab;
    // Start is called before the first frame update
    void Start()
    {
        manager = ManagerScript.Ins;
    }

    // Update is called once per frame
    void Update()
    {
        GlobeMove();
    }

    /**
     * Quả cầu rơi dần xuống
     */
    private void GlobeMove()
    {
        Vector3 target = transform.position;
        target.y = 0;
        Vector3 move =  target - transform.position;
        transform.Translate(move.normalized * 20f * Time.deltaTime);

        // Kiểm tra xem đối tượng đã đến vị trí mới chưa
        float distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget < 1f)
        {
            // tạo vụ nổ
            GameObject boom = Instantiate(boomPreFab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
