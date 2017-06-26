using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSurface : MonoBehaviour {

    //[SerializeField]
    public int thisSurfaceNum;
    [SerializeField]
    private int diceNum;
    [SerializeField]
    private DiceRoll diceRoll;

    public int actionCount;
    public DiceSetting diceSetting;

    
    void FixedUpdate()
    {
        if (diceRoll.rigidBody.velocity.magnitude == 0 && diceRoll.diceRolled != true)
        {
            diceRoll.DecideDiceNum();
        }
    }
    
    /// <summary>
    /// 一番上の面の値を取得する.
    /// </summary>
    void OnTriggerStay()
    {        
        if (diceRoll.rigidBody.velocity.magnitude == 0 && diceRoll.diceRolled != true)
        {
            int getSurfaceNum = (7 - thisSurfaceNum);
            Debug.Log("今一番上にあるのはコレ:"+getSurfaceNum);
            diceRoll.diceSurfaceInfo = getSurfaceNum;
            actionCount = BattleManager.actionCount;
            BattleManager.addDiceActions = diceSetting.diceAction;
            BattleManager.AddAction(diceSetting.diceAction,getSurfaceNum);
            diceRoll.diceRolled = true;
            diceRoll.DecideDiceNum();
        }
    }

    /// <summary>
    /// 地面から離れたら0を返して何もとれてないことを伝える.
    /// </summary>
    void OnTriggerExit()
    {
        if (diceRoll.diceRolled != true) {
            diceRoll.diceSurfaceInfo = 0;
            Debug.Log("aaa");
            BattleManager.RemoveAction(actionCount);
        }
    }
}
