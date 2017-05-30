using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager{

    public enum DUNGEONNEST
    {
        NONE = 0,
        NEST1,
        NEST2,
        NEST3,
        NEST4,
        NEST5,
        NEST6,
        NEST7,
        NEST8,
        NEST9,
        NEST10,
    }

    public static DUNGEONNEST dungeonNest;
    public static int enemyLevel;
    public static int nowDungeonHierarchy;

    /// <summary>
    /// レベル初期化.
    /// </summary>
    public static void IniGameLevel()
    {
        dungeonNest = DUNGEONNEST.NEST1;
        enemyLevel = 1;
        nowDungeonHierarchy = 1;
    }

    /// <summary>
    /// 敵のレベル設定.
    /// </summary>
    public static void EnemyLevelSetting()
    {
        enemyLevel = Random.Range((nowDungeonHierarchy -3 ),nowDungeonHierarchy+1);
        if (enemyLevel <= 0)
        {
            enemyLevel = 1;
        }
    }

}
