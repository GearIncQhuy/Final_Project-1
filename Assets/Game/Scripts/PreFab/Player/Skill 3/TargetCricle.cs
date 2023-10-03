using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCricle : MonoBehaviour
{
    private ManagerScript manager;
    [SerializeField] private GameObject globePreFab;

    private List<GameObject> globes = new List<GameObject>();
    Vector3 globalPosition;
    private int dem;
    // Start is called before the first frame update
    void Start()
    {
        manager = ManagerScript.Ins;
        globalPosition = transform.position;
        globalPosition.x += 50f;
        globalPosition.y += 20f;
        dem = 0;
    }

    private float timeStart = 1f;
    private float timeEnd;

    private bool creat = true;
    
    private void Update()
    {
        if (creat)
        {
            CreatePreFab();
            creat = false;
        }
    }

    private void LateUpdate()
    {
        timeStart += Time.deltaTime;
        timeEnd += Time.deltaTime;

        // Sau 1s active true 1 quả cầu
        if(globes.Count > 0)
        {
            if (timeStart >= 1f)
            {
                globes[dem].SetActive(true);
                dem++;
                timeStart = 0f;
            }
        }

        // Sau 10s kết thúc chiêu
        if(timeEnd >= 10f)
        {
            Destroy(this.gameObject);
        }
    }

    /**
     * Random vị trí rơi của Quả cầu
     */
    public Vector3 RandomPosition()
    {
        float angle = Random.Range(0f, 360f); // Góc ngẫu nhiên
        float distance = Random.Range(0f, 20f); // bán kính 

        Vector3 position = transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
        position.y += 60f;
        return position;
    }
    /**
     * Tạo ra các PreFab -> false
     */
    private void CreatePreFab()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject globe = Instantiate(globePreFab, RandomPosition(), Quaternion.identity);
            globes.Add(globe);
            globe.SetActive(false);
        }
    }
}
