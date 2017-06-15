using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllDatas {

    //呼び出し用データパス群.
    public string enemyDataPath;
    public string enemyActionDataPath;
    public string playerActionDataPath;
    public string playerDataPath;
    public string skillDataPath;
    public string diceDataPath;
    public string enemyDiceDataPath = "Prefabs/EnemyDice";
    public string playerDiceDataPath = "Prefabs/PlayerDice";

    //public string[] enemyTextData;
    //public string text;
    public CharactorActionData[] charactorActionDatas;

    public string[] line;

    //各種ダイスのプレハブ.
    public GameObject enemyDicePrefab;
    public GameObject playerDicePrefab;

    [System.Serializable]
    public struct CharactorActionData
    {
        public string actionCharaName;
        public string[] actionDataName;
    }

    public List<States> enemyStates = new List<States>();
    public List<States> playerStates = new List<States>();
    public List<SkillSet> skillDatas = new List<SkillSet>();
    public List<States.DiceActions> diceActions = new List<States.DiceActions>();

    public AllDatas()
    {
        enemyDataPath = "StatesData/EnemyDaya";
        enemyActionDataPath = "StatesData/EnemyActionData";
        playerActionDataPath = "StatesData/PlayerActionData";
        playerDataPath = "StatesData/PlayerData";
        skillDataPath = "StatesData/SkillData";
        diceDataPath = "StatesData/DiceData";
    }
}
