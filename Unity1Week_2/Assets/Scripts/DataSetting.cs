using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;

public class DataSetting : MonoBehaviour {

    public string enemyDataPath;
    public string enemyActionDataPath;
    public string playerActionDataPath;
    public string playerDataPath;
    public string skillDataPath;
    public string diceDataPath;
    //public string[] enemyTextData;
    public string text;
    public CharactorActionData[] charactorActionDatas;

    public string[] line;

    public GameObject enemyDicePrefab;
    public GameObject playerDicePrefab;

    [System.Serializable]
    public struct CharactorData{
        public int number;
        public int nestNumber;
        public string dataName;
        public string type;
        public string gameName;
    }

    [System.Serializable]
    public struct CharactorActionData
    {
        public string actionCharaName;
        public string[] actionDataName;
    }

    public List<States> enemyStates;
    public List<States> playerStates;
    public List<SkillSet> skillDatas;
    public List<States.DiceActions> diceActions = new List<States.DiceActions>();

	// Use this for initialization
	void Start () {
        IniDatas();
    }

    /// <summary>
    /// 色々なデータの初期化処理.
    /// </summary>
    public void IniDatas()
    {
        SkillDataSetting();
        DiceDataSetting();
        //SetEnemyDatas();
        SetPlayerDatas();
        //SetEnemyDatas();
        //Invoke("TestDiceInstance", 1f);
    }
	
    /// <summary>
    /// テスト用.
    /// </summary>
    public void TestDiceInstance()
    {
        DiceCreateInstance("Gobrin",0);
        PlayerDiceCreateInstance("Dicelot",0);
    }

    /// <summary>
    /// 敵のデータをセッティング.
    /// </summary>
    public void SetEnemyDatas()
    {
        LoadTextData(enemyDataPath,ref enemyStates);
        LoadActionDatas(enemyActionDataPath,ref charactorActionDatas);
        EnemyDiceDataSetting();
    }

    /// <summary>
    /// プレイヤーのデータをセッティング.
    /// </summary>
    public void SetPlayerDatas()
    {
        LoadTextData(playerDataPath,ref playerStates);
        LoadActionDatas(playerActionDataPath, ref charactorActionDatas);
        playerStates[0].isPlayer = true;
        playerStates[0] = DiceActionDataSDetting(playerStates[0]);
    }

    /// <summary>
    /// スキルデータのセッティング.
    /// </summary>
    public void SkillDataSetting()
    {
        LoadTextData(skillDataPath,ref skillDatas);
    }

    /// <summary>
    /// ダイスの情報をセットする.
    /// </summary>
    public void DiceDataSetting()
    {
        LoadDiceData(diceDataPath,ref diceActions);        
    }

    public void EnemyDiceDataSetting()
    {
        for (int num = 0;num<enemyStates.Count;num++)
        {
            Debug.Log(enemyStates[num].name);
            enemyStates[num] = DiceActionDataSDetting(enemyStates[num]);
        }
    }

    /// <summary>
    /// 各キャラ毎にアクションダイスの設定を行う.
    /// </summary>
    /// <param name="states"></param>
    States DiceActionDataSDetting(States states)
    {
        CharactorActionData datas = SerchData_CharactorActionData(states.dataName);
        for (int num = 0; num < states.actionCount; num++)
        {
            Debug.Log("aaaa");
            states.diceActions.Add(SearchData_DiceActionData(datas.actionDataName[num]));
        }
        return states;
    }

    /// <summary>
    /// ダイスアクションデータを拾ってくる.
    /// </summary>
    /// <param name="dataName"></param>
    /// <returns></returns>
    States.DiceActions SearchData_DiceActionData(string dataName)
    {
        States.DiceActions returnDiceAction = new States.DiceActions("","","","","","");

        foreach (States.DiceActions diceAction in diceActions)
        {
            if (diceAction.diceName == dataName)
            {
                returnDiceAction = diceAction;
            }
        }
        return returnDiceAction;
    }

    /// <summary>
    /// 行動ダイスデータを名前から引っ張ってくる.
    /// </summary>
    /// <param name="dataName"></param>
    CharactorActionData SerchData_CharactorActionData(string dataName)
    {
        CharactorActionData returnDatas = new CharactorActionData();
        foreach (CharactorActionData datas  in charactorActionDatas)
        {
            if (datas.actionCharaName == dataName)
            {
                returnDatas = datas;
            }
        }
        return returnDatas;
    }

    /// <summary>
    /// 敵のバトルダイスの生成.
    /// </summary>
    /// <param name="charaName"></param>
    public void DiceCreateInstance(string gameDataName,int diceNumber)
    {
        int number = 0;
        foreach (States states in enemyStates)
        {
            if (gameDataName == states.dataName)
            {
                number = states.dataNumber;
            }
        }
        for (int num = 0; num < enemyStates[number].actionCount; num++)
        {
            enemyStates[number].DiceSurfaceSetting(enemyDicePrefab, num);
            GameObject obj = GameObject.Instantiate(enemyDicePrefab, transform.position, transform.rotation) as GameObject;
            enemyStates[number].diceRoll.Add(obj.GetComponent<DiceRoll>());
        }
    }

    /// <summary>
    /// プレイヤーのダイスの生成.　ダイスの名前と番号が必要からダイスのデータを取得.
    /// </summary>
    /// <param name="playerNum"></param>
    public void PlayerDiceCreateInstance(string diceName,int diceNumber)
    {
        int number = 0;
        foreach (States states in playerStates)
        {
            if (diceName == states.dataName)
            {
                number = states.dataNumber;
            }
        }
        for (int num = 0;num < playerStates[number].actionCount;num++)
        {
            playerStates[number].DiceSurfaceSetting(playerDicePrefab,num);
            GameObject obj = GameObject.Instantiate(playerDicePrefab, transform.position, transform.rotation) as GameObject;
            playerStates[number].diceRoll.Add(obj.GetComponent<DiceRoll>());
        }
        
    }

    /// <summary>
    /// サイコロのデータを取得する.
    /// </summary>
    /// <param name="dataPath"></param>
    /// <param name="diceActions"></param>
    void LoadDiceData(string dataPath,ref List<States.DiceActions> diceActions)
    {
        TextAsset textAsset = Resources.Load(dataPath) as TextAsset;
        string dataText = textAsset.text;
        text = dataText;
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
        string[] lines = dataText.Split(new string[] { "\r", "\n" }, option);
        line = lines;
        string[] lineWidth = lines[0].Split(new string[] { "," }, option);
        int dataWidth = lineWidth.Length;
        int dataLength = lines.Length;
        int dataCount = 0;

        for (int lengthNum = 1; lengthNum < dataLength; lengthNum++)
        {
            Debug.Log("aaaa");
            DataSplitAndInsert(dataCount, lines[lengthNum], option, ref diceActions);
            DiceSurfaceDataSet(diceActions[dataCount]);
            dataCount++;
        }
    }

    /// <summary>
    /// 全てのサイコロの情報設定を行う.
    /// </summary>
    /// <param name="dataCount"></param>
    /// <param name="dataLine"></param>
    /// <param name="option"></param>
    /// <param name="charaDatas"></param>
    void DataSplitAndInsert(int dataCount, string dataLine, System.StringSplitOptions option, ref List<States.DiceActions> diceActions)
    {
        string[] datas = dataLine.Split(new string[] { "," }, option);
        States.DiceActions setDiceAction = new States.DiceActions("","","","","","");
        setDiceAction.diceName = datas[0];
        for (int dataNum = 0;dataNum < 6;dataNum++)
        {
            setDiceAction.actions[dataNum] = datas[dataNum + 1];
        }
        Debug.Log("aaaaaaa");
        diceActions.Add(setDiceAction);
    }
    
    void DiceSurfaceDataSet(States.DiceActions diceAction)
    {
        for (int diceFaceNum = 0; diceFaceNum < 6; diceFaceNum++)
        {
            switch (diceAction.actions[diceFaceNum])
            {
                case "戦う":
                    diceAction.diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.ATACK;
                    break;
                case "防御":
                    diceAction.diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.GUARD;
                    break;
                case "回避":
                    diceAction.diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.DODGE;
                    break;
                case "ミス":
                    diceAction.diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.MISS;
                    break;
                case "必殺の一撃":
                    diceAction.diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.CRITICAL;
                    break;
            }
            if (diceAction.actions[diceFaceNum].Contains("魔法："))
            {
                SkillNameResetting("魔法：", diceFaceNum, ActionList.ACTIONTYPE.MAGICSKILL,ref diceAction);
            }
            if (diceAction.actions[diceFaceNum].Contains("特技："))
            {
                SkillNameResetting("特技：", diceFaceNum, ActionList.ACTIONTYPE.ATACKSKILL,ref diceAction);
            }
            if (diceAction.actions[diceFaceNum].Contains("強化："))
            {
                SkillNameResetting("強化：", diceFaceNum, ActionList.ACTIONTYPE.SUPPORTSKILL,ref diceAction);
            }
            if (diceAction.actions[diceFaceNum].Contains("弱体："))
            {
                SkillNameResetting("弱体：", diceFaceNum, ActionList.ACTIONTYPE.SUPPORTSKILL,ref diceAction);
            }
        }
    }

    /// <summary>
    /// スキルデータの名前の再設定.
    /// </summary>
    /// <param name="outText"></param>
    /// <param name="diceFaceNum"></param>
    /// <param name="actionType"></param>
    void SkillNameResetting(string outText, int diceFaceNum, ActionList.ACTIONTYPE actionType,ref States.DiceActions diceAction)
    {
        diceAction.actions[diceFaceNum] = diceAction.actions[diceFaceNum].Substring(outText.Length);
        diceAction.diceSurfaceAction[diceFaceNum] = actionType;
        //skillDatas.Add(DataSetting.SkillDataSetToStates(diceAction.actions[diceFaceNum]));
    }

    /// <summary>
    /// キャラ用のテキストデータのロード.
    /// </summary>
    void LoadTextData(string dataPath,ref List<States> datas)
    {
        TextAsset textAsset = Resources.Load(dataPath)as TextAsset;
        string dataText = textAsset.text;
        text = dataText;
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
        string[] lines = dataText.Split(new string[] {"\r","\n" },option);
        line = lines;
        string[] lineWidth = lines[0].Split(new string[] { ","},option);
        int dataWidth = lineWidth.Length;
        int dataLength = lines.Length;
        int dataCount = 0;

        for (int lengthNum = 1; lengthNum < dataLength; lengthNum++)
        {
            DataSplitAndInsert(dataCount++,lines[lengthNum],option,ref datas);
        }
    }

    /// <summary>
    /// スキルデータのロード
    /// </summary>
    void LoadTextData(string dataPath, ref List<SkillSet> datas)
    {
        TextAsset textAsset = Resources.Load(dataPath) as TextAsset;
        string dataText = textAsset.text;
        text = dataText;
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
        string[] lines = dataText.Split(new string[] { "\r", "\n" }, option);
        //line = lines;
        string[] lineWidth = lines[0].Split(new string[] { "," }, option);
        int dataLength = lines.Length;
        int dataCount = 0;

        for (int lengthNum = 1; lengthNum < dataLength; lengthNum++)
        {
            DataSplitAndInsert(dataCount, lines[lengthNum], option, ref datas);
            dataCount++;
        }
    }

    /// <summary>
    /// キャラクターデータをセット.
    /// </summary>
    void DataSplitAndInsert(int dataCount,string dataLine,System.StringSplitOptions option,ref List<States> charaDatas)
    {
        string[] datas = dataLine.Split(new string[] { "," }, option);
        int count = 0;
        //基本ステータスの初期化
        charaDatas.Add(new States(datas[count++],
                                  datas[count++],
                                  datas[count++],
                                  datas[count++],
                                  datas[count++],
                                  datas[count++],
                                  datas[count++],
                                  datas[count++],
                                  datas[count++],
                                  datas[count++]));
        //レベルアップ用データの初期化.
        charaDatas[dataCount].IniLevelUpData(datas[count++],
                                             datas[count++],
                                             datas[count++],
                                             datas[count++],
                                             datas[count++]);
        //アクションダイスのデータをセット.
        charaDatas[dataCount].ActionDiceData(datas[count++],
                                             datas[count++]);
        //ドロップアイテムのセット.
        charaDatas[dataCount].DropItemSetting(datas[count++],
                                              datas[count  ]);

    }

    /// <summary>
    /// スキルデータのセット.
    /// </summary>
    /// <param name="dataCount"></param>
    /// <param name="dataLine"></param>
    /// <param name="option"></param>
    /// <param name="skillDatas"></param>
    void DataSplitAndInsert(int dataCount, string dataLine, System.StringSplitOptions option, ref List<SkillSet> skillDatas)
    {
        string[] datas = dataLine.Split(new string[] { "," }, option);
        int count = 0;
        skillDatas.Add(new SkillSet(datas[count++], datas[count++], datas[count++], datas[count++], datas[count++], datas[count++], datas[count++]));
    }

    /// <summary>
    /// スキルデータのセッティング.
    /// </summary>
    /// <param name="skillName"></param>
    /// <returns></returns>
    public SkillSet SkillDataSetToStates(string skillName)
    {
        SkillSet skillData = null;
        foreach (SkillSet skill in skillDatas)
        {
            if (skillName == skill.skillName)
            {
                return skillData = skill;
            }
        }
        return skillData;
    }

    /// <summary>
    /// 行動ダイスのデータを読み込む.
    /// </summary>
    void LoadActionDatas(string dataPath, ref CharactorActionData[] datas)
    {
        TextAsset textAsset = Resources.Load(dataPath) as TextAsset;
        string dataText = textAsset.text;
        text = dataText;
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
        string[] lines = dataText.Split(new string[] { "\r", "\n" }, option);
        int dataLength = lines.Length;
        datas = new CharactorActionData[dataLength - 1];
        int dataCount = 0;

        for (int lengthNum = 1; lengthNum < dataLength; lengthNum++)
        {
            DataSplitAndInsert(dataCount,lines[lengthNum],option,ref datas);
            dataCount++;
        }
    }

    /// <summary>
    /// 行動ダイスの情報をセッティング.
    /// </summary>
    void DataSplitAndInsert(int dataCount, string dataLine, System.StringSplitOptions option, ref CharactorActionData[] charactorActionDatas)
    {
        string[] datas = dataLine.Split(new string[] { "," }, option);
        charactorActionDatas[dataCount].actionDataName = new string[datas.Length-1];
        charactorActionDatas[dataCount].actionCharaName = datas[0];
        for (int num = 0;num < charactorActionDatas[dataCount].actionDataName.Length;num ++)
        {
            charactorActionDatas[dataCount].actionDataName[num] = datas[num+1];
            Debug.Log(charactorActionDatas[dataCount].actionDataName[num]);
        }
    }

}
