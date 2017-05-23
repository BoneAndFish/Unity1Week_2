using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSurface : MonoBehaviour {

    [SerializeField]
    private int thisSurfaceNum;
    [SerializeField]
    private int diceNum;
    [SerializeField]
    private DiceRoll diceRoll;

    // Use this for initialization
    void Start () {
		
	}

    /// <summary>
    /// 一番上の面の値を取得する.
    /// </summary>
    void OnTriggerEnter()
    {
        int getSurfaceNum = (7 - thisSurfaceNum);
        Debug.Log("今一番上にあるのはコレ:"+getSurfaceNum);
        diceRoll.diceSurfaceInfo = getSurfaceNum;
    }
    /// <summary>
    /// 離れたら0を返して何もとれてないことを伝える.
    /// </summary>
    void OnTriggerExit()
    {
        diceRoll.diceSurfaceInfo = 0;
    }
}
