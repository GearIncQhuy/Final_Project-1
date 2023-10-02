using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Enemy enemy;
    
    public Slider slider;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    /**
     * Hàm update lại thanh máu cho Enemy
     * @param: healCurrentEnemy : máu hiện tại của Enemy
     * @param: dame : dame nhận vào từ Player hoặc tác nhân bên ngoài
     * note: heal slider = (healCurrentEnemy - dame) / data.healMax
     */
    public void UpdateHealEnemy(float healCurrentEnemy, float dame)
    {
        float healCurrent = (healCurrentEnemy - dame) / enemy.data.healMax;
        // update thanh máu
        slider.value = healCurrent;
        // update máu hiện tại của Enemy
        enemy.heal = healCurrentEnemy - dame;
        //
        enemy.checkActive = true;
        enemy.sliderObj.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm viên đạn -> hiển thị thanh máu và cập nhật máu
        if (other.gameObject.CompareTag(Constants.Tag_Bullet))
        {
            UpdateHealEnemy(enemy.heal, enemy.manager.player.dame);
        }
    }
}
