using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;
using TextProcess;
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

    /// <summary>
    /// テスト用.
    /// </summary>
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
        StartCoroutine(DiceValueSet(actor,target));
    }

    /// <summary>
    /// サイコロの値セット.
    /// </summary>
    /// <param name="actor"></param>
    /// <returns></returns>
    IEnumerator DiceValueSet(States actor,States target)
    {
        bool isSetOk = false;
        while (isSetOk == false)
        {
            isSetOk = IsAtackPrepareComplete(actor);
            Debug.Log(isSetOk);
            yield return null;
        }
        StartCoroutine(BattleProcessStart(actor, target));
    }

    /// <summary>
    /// 攻撃準備完了かどうか.
    /// </summary>
    /// <param name="actor"></param>
    /// <returns></returns>
    bool IsAtackPrepareComplete(States actor)
    {
        int num = 0;
        int rollDiceNum = actor.diceRoll.Count;
        foreach (DiceRoll diceRoll in actor.diceRoll)
        {
            if (diceRoll.diceRolled == true && diceRoll.diceSurfaceInfo != 0)
            {
                num++;
            }
        }
        if (num == rollDiceNum)
        {
            return true;
        }else
        {
            return false;
        }        
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
        while (actionCount < actor.diceRoll.Count)
        {
            yield return StartCoroutine(BattleProcess(actor, target));//戦闘処理.
            actionCount++;
            yield return BattleFinish(target,actor);
        }
    }

    /// <summary>
    /// 次のテキストを表示するまでに待機する時間.
    /// </summary>
    /// <param name="waitTimer"></param>
    /// <returns></returns>
    IEnumerator TextTimer(float waitTimer)
    {
        float time = 0;
        while (time < waitTimer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                time = waitTimer+1f;
            }
            time += Time.deltaTime;
            yield return Time.deltaTime;
        }
    }


    /// <summary>
    /// 行動決定からのターン処理.
    /// </summary>
    IEnumerator BattleProcess(States actor,States target)
    {
        for (int diceNum=0;diceNum < actor.diceRoll.Count;diceNum++)
        {
            int diceSurfaceNumber = actor.diceRoll[diceNum].diceSurfaceInfo-1;
            Debug.Log(diceSurfaceNumber);
            ActionList.ACTIONTYPE actionType = actor.diceSurfaceAction[diceSurfaceNumber];
            switch (actionType)
            {
                //攻撃処理.
                case ActionList.ACTIONTYPE.ATACK:
                    int damege = CommandList.Atack(text, actor.name, target.name,ref target.nowLifePoint, actor.nowAtackPower, target.nowDefencePower, target.isGuard, actor.isPlayer);
                    yield return TextTimer(1.0f);
                    TextSystem.PlayerAtackText(text,target.name, damege);
                    yield return TextTimer(1.0f);
                    break;
                case ActionList.ACTIONTYPE.MAGICSKILL:
                    string skillName = actor.actions[diceSurfaceNumber];
                    SkillSet selectSkill = GetSkillData(diceSurfaceNumber,actor,skillName);
                    TextSystem.SkillActiveText(text, actor.name, skillName, true);
                    if (selectSkill.effectType == "Damege")
                    {
                        int skillDamege = CommandList.SpecialAtack(text, actor.name, skillName, target.name, ref target.nowLifePoint, actor.nowMagicPower+selectSkill.effectValue, target.nowMindPower, true, target.isGuard, actor.isPlayer);
                        yield return TextTimer(selectSkill.timerTime);
                        TextSystem.PlayerAtackText(text, target.name, skillDamege);
                        yield return TextTimer(1.0f);

                    }else if (selectSkill.effectType == "Heal")
                    {
                        int healValue = CommandList.Heal(text,actor.name,actor.name,ref actor.nowLifePoint,actor.maxLifePoint, selectSkill.effectValue,actor.isPlayer);
                        yield return TextTimer(selectSkill.timerTime);
                        TextSystem.HealText(text,healValue,actor.isPlayer,actor.name);
                        yield return TextTimer(1.0f);
                    }
                    else if (selectSkill.effectType == "Support")
                    {

                    }else if (selectSkill.effectType == "BadStates")
                    {
                        switch (selectSkill.badStates)
                        {
                            case "毒":
                                CommandList.StatesControll(text, actor.name, target.name,ref target.badStates.poizonTrun , selectSkill.effectValue,selectSkill.badStates);
                                break;
                            case "麻痺":
                                CommandList.StatesControll(text, actor.name, target.name, ref target.badStates.poizonTrun, selectSkill.effectValue, selectSkill.badStates);
                                break;
                            case "スタン":
                                CommandList.StatesControll(text, actor.name, target.name, ref target.badStates.poizonTrun, selectSkill.effectValue, selectSkill.badStates);
                                break;
                            case "暗闇":
                                CommandList.StatesControll(text, actor.name, target.name, ref target.badStates.poizonTrun, selectSkill.effectValue, selectSkill.badStates);
                                break;
                            case "睡眠":
                                CommandList.StatesControll(text, actor.name, target.name, ref target.badStates.poizonTrun, selectSkill.effectValue, selectSkill.badStates);
                                break;
                            case "沈黙":
                                CommandList.StatesControll(text, actor.name, target.name, ref target.badStates.poizonTrun, selectSkill.effectValue, selectSkill.badStates);
                                break;
                        }
                        yield return TextTimer(selectSkill.timerTime);

                    }
                    break;
                //攻撃失敗処理.
                case ActionList.ACTIONTYPE.MISS:
                    CommandList.MissText(text, actor.isPlayer);
                    yield return TextTimer(1.0f);
                    break;
                //防御失敗処理.
                case ActionList.ACTIONTYPE.GUARD:
                    CommandList.Defence(text,ref actor.isGuard,actor.name);
                    yield return TextTimer(1.0f);
                    break;
            }
        }
    }

    SkillSet GetSkillData(int diceSurfaceNumber,States actor,string skillName)
    {
        SkillSet nowSelectSkill = null;
        foreach (SkillSet skillset in actor.skillSet)
        {
            if (skillset.skillName == skillName)
            {
                nowSelectSkill = skillset;
            }
        }
        return nowSelectSkill;
    }

    /// <summary>
    /// 戦闘の終了処理.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    IEnumerator BattleFinish(States target,States actor)
    {
        //攻撃目標死亡処理.
        if (target.nowLifePoint <= 0)
        {
            if (target.isPlayer == true)
            {
                TextSystem.DeadPlayerText(text);
                yield return TextTimer(1.0f);
            }else if (target.isPlayer == false)
            {
                TextSystem.BeatEnemyText(text, target.name);
                battleJoinedEnemyStates.Remove(target);
                yield return TextTimer(1.0f);
                TextSystem.WinText(text);
                yield return TextTimer(1.0f);
            }
        }
        else if (isPlayerTurn == false && playerStates.nowLifePoint > 0)
        {
            isPlayerTurn = true;
            CommandList.ResetDefence(ref actor,ref target);
            TextSystem.NextTurnText(text);
        }else if (isPlayerTurn == true && target.nowLifePoint > 0)
        {
            isPlayerTurn = false;
            BattlleDiceRollStart(target, actor);
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
