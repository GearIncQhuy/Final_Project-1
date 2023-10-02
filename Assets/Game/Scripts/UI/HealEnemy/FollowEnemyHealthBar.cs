using UnityEngine;

public class FollowEnemyHealthBar : MonoBehaviour
{
    public GameObject enemyTransform;
    public Vector3 offset = new Vector3(0, 1, 0);

    void Update()
    {
        if (enemyTransform != null)
        {
            Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(enemyTransform.transform.position);

            transform.position = enemyScreenPos + offset;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}