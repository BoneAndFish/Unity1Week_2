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

    void Start()
    {
        IniDiceSet();
    }

    /// <summary>
    /// ダイスの初期化
    /// </summary>
	void IniDiceSet()
    {
        for (int num = 0;num < 6;num++)
        {
            Debug.Log(ActionList.textureName[actionType[num].GetHashCode()]);
            Debug.Log(textureFilePath + ActionList.textureName[actionType[num].GetHashCode()]);
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
