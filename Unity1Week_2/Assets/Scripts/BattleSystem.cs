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

    public DataSetting dataSetting;

    public Text text;
    public bool isPlayerTurn;

	// Use this for initialization
	void Start () {
        GameLevelManager.IniGameLevel();
        dataSetting.IniDatas();
        GetSummonEnemysData();
        PlayerStatesSet();
        SpownEnemy("Gobrin");
    }

    public void TestBattleStart()
    {
        isPlayerTurn = true;
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

    /// <summary>
    /// 戦闘処理の開始.
    /// </summary>
    /// <param name="actor"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IEnumerator BattleProcessStart(States actor,States target)
    {
        int actionCount = 0;
        bool battleEnd = false;
        while (actionCount < actor.diceRoll.Count)
        {
            battleEnd = BattleProcess(actor, target);
            actionCount++;
            yield return StartCoroutine("TextTimer");
            if (battleEnd)
            {
                CommandList.WinPlayer(text);           
            }else if(isPlayerTurn == false)
            {
                CommandList.NextTurn(text);
            }
        }
        if (isPlayerTurn && battleEnd != true)
        {
            BattlleDiceRollStart(target, actor);
            isPlayerTurn = false;
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
    public bool BattleProcess(States actor,States target)
    {
        bool battleEnd = false;
        Debug.Log("呼び出しました");
        for (int diceNum=0;diceNum < actor.diceRoll.Count;diceNum++)
        {
            int diceSurfaceNumber = actor.diceRoll[diceNum].diceSurfaceInfo;
            ActionList.ACTIONTYPE actionType = target.diceSurfaceAction[diceSurfaceNumber];
            switch (actionType)
            {
                case ActionList.ACTIONTYPE.ATACK:
                    battleEnd = CommandList.Atack(text, actor.name, target.name, ref target.nowLifePoint, actor.nowAtackPower, target.nowDefencePower, target.isGuard, actor.isPlayer);
                    break;
                case ActionList.ACTIONTYPE.MISS:
                    CommandList.MissText(text, actor.isPlayer);
                    break;
            }
        }
        return battleEnd;
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
        foreach (DataSetting.CharactorData enemyData in dataSetting.enemyDatas)
        {
            if(enemyData.nestNumber == dungeonNest.GetHashCode()){
                States states = dataSetting.enemyStates[enemyData.number-1];
                battleJoinedEnemyStates.Add(states);
            }
        }
    }

    /// <summary>
    /// 味方のデータを格納する.
    /// </summary>
    public void PlayerStatesSet()
    {
        playerStates = dataSetting.playerStates[0];
        playerStates.PlayerStatesSet();
        playerStates.NowStatesSetting();
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
