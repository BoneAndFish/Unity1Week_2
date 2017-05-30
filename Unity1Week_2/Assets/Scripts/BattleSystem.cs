using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;
using BattleCommand;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour {
    
    public States playerStates;
    public List<States> battleJoinedEnemyStates = new List<States>();//登場させる敵のデータ.
    public List<States> battleEnemyStates = new List<States>();//実際に戦闘する敵のデータ.

    public DataSetting enemyDataSetting;

    public Text text;

	// Use this for initialization
	void Start () {
        GameLevelManager.IniGameLevel();
        GetSummonEnemysData();
        //SpownEnemy("Gobrin");
    }

    public void TestBattleStart()
    {
        BattlleDiceRollStart(playerStates,battleEnemyStates[0]);
    }

    /// <summary>
    /// 行動者のサイコロを振る.
    /// </summary>
    /// <param name="actor"></param>
    public void BattlleDiceRollStart(States actor,States target)
    {
        for (int diceNum = 0;diceNum < actor.diceRoll.Count;diceNum++)
        {
            actor.diceRoll[diceNum].RollDice();
        }
        StartCoroutine(BattleProcessStart(actor,target));
    }

    IEnumerator BattleProcessStart(States actor,States target)
    {
        int actionCount = 0;
        while (actionCount < actor.diceRoll.Count)
        {            
            BattleProcess(actor,target);
            actionCount++;
            yield return StartCoroutine("TextTimer");
        }
    }

    IEnumerator TextTimer()
    {
        float time = 0;
        while (time < 0.75f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                time = 1f;
            }
            time += Time.deltaTime;
            yield return Time.deltaTime;
        }
    }

    /// <summary>
    /// 交互にターンを行う.
    /// </summary>
    public void BattleProcess(States actor,States target)
    {
        Debug.Log("呼び出しました");
        for (int diceNum=0;diceNum < actor.diceRoll.Count;diceNum++)
        {
            int diceSurfaceNumber = actor.diceRoll[diceNum].diceSurfaceInfo;
            ActionList.ACTIONTYPE actionType = target.diceSurfaceAction[diceSurfaceNumber];
            switch (actionType)
            {
                case ActionList.ACTIONTYPE.ATACK:
                    CommandList.Atack(text, actor.name, target.name, ref target.nowLifePoint, actor.nowAtackPower, target.nowDefencePower, target.isGuard, actor.isPlayer);
                    break;
                case ActionList.ACTIONTYPE.MISS:
                    CommandList.MissText(text, actor.isPlayer);
                    break;
            }
        }
        
    }

    /// <summary>
    /// 敵の召喚データの確保.
    /// </summary>
    public void GetSummonEnemysData()
    {
        battleJoinedEnemyStates.Clear();
        EnemyStatesSet(GameLevelManager.dungeonNest);
    }

    /// <summary>
    /// 敵のデータを格納する.
    /// </summary>
    /// <param name="dungeonNest"></param>
    public void EnemyStatesSet(GameLevelManager.DUNGEONNEST dungeonNest )
    {
        foreach (DataSetting.CharactorData enemyData in enemyDataSetting.enemyDatas)
        {
            if(enemyData.nestNumber == dungeonNest.GetHashCode()){
                States states = enemyDataSetting.enemyStates[enemyData.number-1];
                battleJoinedEnemyStates.Add(states);
            }
        }
    }

    /// <summary>
    /// 戦闘する敵のデータを追加する.ステータスも初期化.
    /// </summary>
    /// <param name="dataName"></param>
    public void SpownEnemy(string dataName)
    {
        foreach (States states in battleJoinedEnemyStates)
        {
            if (states.dataName == dataName)
            {
                states.EnemyStatesSet();
                states.NowStatesSetting();
                battleEnemyStates.Add(states);
            }
        }
    }

}
