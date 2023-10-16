using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    // Tag Object
    public const string Tag_Player = "Player";
    public const string Tag_Enemy = "Enemy";
    public const string Tag_Boss = "Boss";
    public const string Tag_Map = "Map";

    // Tag Pool and Tag PreFab
    // - Tag Diamon
    public const string Diamon = "Diamon";

    // - Tag Enemy
    public const string EnemyRun = "EnemyRun";
    public const string EnemyFly = "EnemyFly";
    //   + Default attack
    public const string Tag_BulletEnemy = "BulletEnemy";
    public const string Tag_Bullet_Enemy = "Bullet_Enemy";
    // - Animation
    public const string Enemy_Run_Ani = "isRun";
    public const string Enemy_Attack_Ani = "isAttack";
    public const string Enemy_Die_Ani = "isDie";

    // - Tag skill Player
    //   + Default attack
    public const string Tag_Bullet   = "Bullet";
    //   + Skill 1
    public const string Tag_Skill1   = "PlayerSkill1";
    public const string Tag_Skill1_2 = "PlayerSkill1_2";
    //   + Skill 2
    public const string Tag_Skill2   = "PlayerSkill2";
    //   + Skill 3
    public const string Tag_Skill3   = "PlayerSkill3";
    public const string Tag_Skill3_2 = "PlayerSkill3_2";
    public const string Tag_Skill3_3 = "PlayerSkill3_3";
    //   + Animation
    public const string Tag_Player_Run = "isRun";

    // - Tag Boss
    //   + Animation
    public const string BossMap1_Idle = "Idle";
    public const string BossMap1_Move = "Crawl Forward Slow In Place";
    public const string BossMap1_Attack1 = "Bite Attack";
    public const string BossMap1_SpawnEnemy = "Cast Spell";
    public const string BossMap1_Die = "Die";

    // - Tag UI
    //   + End Game UI
    public const string Tag_EndGameUI = "EndGameUI";


    // Scene
    public const string Scene_GamePlay = "GamePlay";
}
