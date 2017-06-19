using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    public static int actionCount;
    public int turnCount;

    public static States actor;
    public static States target;

    public DataSetting dataSetting;

    [System.Serializable]
    public class CharactorAction
    {
        public int insertNum;
        public States actorStates;
        public States targetStates;
        public States.DiceActions diceActions;
        public CharactorAction(int _num, States _actor, States _target, States.DiceActions _action)
        {
            insertNum = _num;
            actorStates = _actor;
            targetStates = _target;
            diceActions = _action;
        }
        public CharactorAction(){}        
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
    /// サイコロを転がす.
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
            //Debug.Log(isSetOk);
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

    void BattleStart()
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
    public static void AddAction(States.DiceActions diceAction)
    {
        CharactorAction addActionData = new CharactorAction(actionCount,
                                                            actor,
                                                            target,
                                                            diceAction);
        static_charactorAction.Add(addActionData);
        Debug.Log("追加しました");
        actionCount++;
    }

}
