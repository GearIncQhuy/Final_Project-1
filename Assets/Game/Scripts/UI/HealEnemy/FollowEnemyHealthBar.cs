using UnityEngine;

public class FollowEnemyHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject enemyTransform;
    public Vector3 offset = new Vector3(0, 1, 0);
    private Vector3 enemyPosition;
    void LateUpdate()
    {
        if (enemyTransform != null)
        {
            enemyPosition = enemyTransform.transform.position;
            enemyPosition.y += 3f;
            Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(enemyPosition);

            transform.position = enemyScreenPos + offset;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}