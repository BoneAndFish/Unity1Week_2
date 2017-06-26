using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    public static int actionCount;
    public int turnCount;

    public static States actor;
    public static States target;

    public static DataSetting dataSetting;

    [System.Serializable]
    public class CharactorAction
    {
        public int insertNum;
        public int surfaceNum;
        public States actorStates;
        public States targetStates;
        public int actionTurn;
        public States.DiceActions diceActions;
        public SkillSet skillSet;
        public CharactorAction(int _num,int _surfaceNum, States _actor, States _target, States.DiceActions _action)
        {
            insertNum = _num;
            surfaceNum = _surfaceNum;
            actorStates = _actor;
            targetStates = _target;
            diceActions = _action;
        }
        public CharactorAction(){}
        
        public void GetSkillData()
        {
            foreach (SkillSet _skillSet in dataSetting.alldatas.skillDatas) {
                if (diceActions.actions[surfaceNum] == skillSet.skillName)
                {
                    skillSet = _skillSet;
                    actionTurn = skillSet.activeTurnCount;
                }
            }
        }

    }

    public List<DiceRoll> actorBattleDiceRoll = new List<DiceRoll>();

    public List<CharactorAction> charactorAction = new List<CharactorAction>();
    public static List<CharactorAction> static_charactorAction = new List<CharactorAction>();

    public static States.DiceActions addDiceActions;

    // Use this for initialization
    void Start () {
        Invoke("Test",1.5f);
	}

    public void Test()
    {
        actor = dataSetting.alldatas.playerStates[0];
        target = dataSetting.alldatas.playerStates[0];
        actorBattleDiceRoll.Add(dataSetting.alldatas.playerStates[0].diceRoll[0]);
    }

    /// <summary>
    /// 行動者のステータスをセット.ボタンとかに情報を送る.
    /// </summary>
    /// <param name="actorName">行動者の名前</param>
    public void ActorStatesSet(string actorName)
    {
        foreach (States states in dataSetting.alldatas.playerStates)
        {
            if (actorName == states.dataName)
            {
                actor = states;//行動者のステータスをセット.
            }
        }
    }

    /// <summary>
    /// 行動者のdicerollスクリプトを取得する.dicerollは出現時のみ追加される.
    /// </summary>
    public void ActorDiceRollSet(string diceName)
    {
        foreach (DiceRoll diceRoll in actor.diceRoll)
        {
            actorBattleDiceRoll.Add(diceRoll);            
        }
    }

    /// <summary>
    /// サイコロを転がす.ボタンで処理.
    /// </summary>
    /// <param name="actor"></param>
    /// <param name="target"></param>
    public void BattlleDiceRollStart()
    {
        for (int diceNum = 0; diceNum < actorBattleDiceRoll.Count; diceNum++)
        {
            actorBattleDiceRoll[diceNum].RollDice();
        }
        StartCoroutine(DiceValueSet());//サイコロの値をセットする.
    }

    /// <summary>
    /// サイコロの値セット.
    /// </summary>
    /// <param name="actor"></param>
    /// <returns></returns>
    IEnumerator DiceValueSet()
    {
        bool isSetOk = false;
        while (isSetOk == false)
        {
            isSetOk = IsAtackPrepareComplete(actor);
            yield return null;
        }
        charactorAction = static_charactorAction;
    }

    /// <summary>
    /// 攻撃準備完了かどうか.サイコロを全て振る.
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
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 行動するまでの時間.ターン数減少.ターン開始時に処理.
    /// </summary>
    void ActionTurnDecrease()
    {
        foreach (CharactorAction charactorAction in static_charactorAction)
        {
            charactorAction.actionTurn--;
        }
    }

    /// <summary>
    /// 行動ターン数が0になったダイスから行動を処理していく.
    /// </summary>
    void ActionProcess()
    {
        foreach (CharactorAction _charactorAction in static_charactorAction)
        {
            if (_charactorAction.actionTurn == 0)
            {
                Action(_charactorAction);
            }
        }
        static_charactorAction = charactorAction;
    }

    /// <summary>
    /// 実行動部分
    /// </summary>
    void Action(CharactorAction charactorAction)
    {
        
    }

    /// <summary>
    /// アクションの削除.サイコロが飛ばされたときとかに行動をキャンセルする.
    /// </summary>
    /// <param name="actionNum"></param>
    public static void RemoveAction(int actionNum)
    {
        CharactorAction removeActionData = new CharactorAction();
        foreach (CharactorAction action in static_charactorAction)
        {
            if (action.insertNum == actionNum)
            {
                removeActionData = action;
                break;
            }
        }
        static_charactorAction.Remove(removeActionData);
    }

    /// <summary>
    /// アクションの追加.サイコロが完全に停止したときに実行する.
    /// </summary>
    /// <param name="diceAction"></param>
    public static void AddAction(States.DiceActions diceAction,int surfaceNum)
    {
        CharactorAction addActionData = new CharactorAction(actionCount,
                                                            surfaceNum,
                                                            actor,
                                                            target,
                                                            diceAction);
        addActionData.GetSkillData();
        static_charactorAction.Add(addActionData);
        Debug.Log("追加しました");
        actionCount++;
    }

}
