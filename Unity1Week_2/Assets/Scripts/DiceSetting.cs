using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;

public class DiceSetting : MonoBehaviour {

    [SerializeField]
    private ActionList.ACTIONTYPE[] actionType = new ActionList.ACTIONTYPE[6];

    [SerializeField]
    private SpriteRenderer[] spriteRenderer = new SpriteRenderer[6];

    private string textureFilePath = "Textures/Actions/";

    /// <summary>
    /// ダイスの初期化
    /// </summary>
	public void IniDiceSet(ActionList.ACTIONTYPE[] actionTypes)
    {
        actionType = actionTypes;
        for (int num = 0;num < 6;num++)
        {
           spriteRenderer[num].sprite = Resources.Load<Sprite>(textureFilePath + ActionList.textureName[actionType[num].GetHashCode()]);
        }
    }
    /// <summary>
    /// 特定のダイスの値の入れ替え
    /// </summary>
    public void DiceReset(int diceNum,ActionList.ACTIONTYPE newActionType)
    {
        spriteRenderer[diceNum].sprite = Resources.Load(textureFilePath + "/" + ActionList.textureName[newActionType.GetHashCode()] )as Sprite;
    }

}
