using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSurface : MonoBehaviour {

    [SerializeField]
    public int thisSurfaceNum;
    [SerializeField]
    private int diceNum;
    [SerializeField]
    private DiceRoll diceRoll;

    public int actionCount;
    public DiceSetting diceSetting;

    /// <summary>
    /// 一番上の面の値を取得する.
    /// </summary>
    void OnTriggerEnter()
    {
        if (diceRoll.diceSurfaceInfo == 0)
        {
            int getSurfaceNum = (7 - thisSurfaceNum);
            Debug.Log("今一番上にあるのはコレ:"+getSurfaceNum);
            diceRoll.diceSurfaceInfo = getSurfaceNum;
            diceRoll.diceRolled = true;
            actionCount = BattleManager.actionCount;
            BattleManager.AddAction(diceSetting.diceAction);
        }
    }
    /// <summary>
    /// 地面から離れたら0を返して何もとれてないことを伝える.
    /// </summary>
    void OnTriggerExit()
    {
        if (diceRoll.diceRolled != true) {
            diceRoll.diceSurfaceInfo = 0;
            BattleManager.RemoveAction(actionCount);
        }
    }


}
