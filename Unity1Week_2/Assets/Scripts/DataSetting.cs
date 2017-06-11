using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;

public class DataSetting : MonoBehaviour {

    public string enemyDataPath;
    public string enemyActionDataPath;
    public string playerDataPath;
    public string skillDataPath;
    public string diceDataPath;
    private string dataPath;
    //public string[] enemyTextData;
    public string text;
    public CharactorData[] enemyDatas;
    public CharactorData[] playerDatas;
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
        public int life;
        public int atack;
        public int defence;
        public int magic;
        public int mind;
        public int levelUpLife;
        public int levelUpAtack;
        public int levelUpDefence;
        public int levelUpMagic;
        public int levelUpMind;
        public int actionCount;
        public string dropItemName;
        public string rareDropName;
    }

    [System.Serializable]
    public struct CharactorActionData
    {
        public string enemyDataName;
        public string[] actionDataName;
    }

    public States[] enemyStates;
    public States[] playerStates;
    public static SkillSet[] skillDatas;
    public static List<States.DiceActions> diceActions = new List<States.DiceActions>();

    private int diceNumber;

	// Use this for initialization
	void Start () {     
    }

    /// <summary>
    /// ステータスの初期化.
    /// </summary>
    public void IniDatas()
    {
        SkillDataSetting();
        DiceDataSetting();
        SetEnemyDatas();
        SetPlayerDatas();
        SetEnemyDatas();
        //Invoke("TestDiceInstance", 1f);
    }
	
    /// <summary>
    /// テスト用.
    /// </summary>
    public void TestDiceInstance()
    {
        //DiceCreateInstance("Gobrin",0);
        //PlayerDiceCreateInstance("Dicelot",0);
    }

    /// <summary>
    /// 敵のデータをセッティング.
    /// </summary>
    public void SetEnemyDatas()
    {
        LoadTextData(enemyDataPath,ref enemyDatas);
        LoadEnemyActionDatas(enemyActionDataPath,ref charactorActionDatas);
        StatesSet(ref enemyStates,ref enemyDatas);
    }

    /// <summary>
    /// プレイヤーのデータをセッティング.
    /// </summary>
    public void SetPlayerDatas()
    {
        LoadTextData(playerDataPath,ref playerDatas);
        StatesSet(ref playerStates, ref playerDatas);
        playerStates[0].isPlayer = true;
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

    /// <summary>
    /// 各キャラ毎にアクションダイスの設定を行う.敵用?
    /// </summary>
    /// <param name="states"></param>
    void DiceActionDataSDetting(States states)
    {
        CharactorActionData datas = SerchData_CharactorActionData(states.dataName);
        for (int num = 0; num < states.actionCount; num++)
        {
            states.diceActions.Add(SerchFData_DiceActionData(datas.actionDataName[num]));
        }        
    }

    /// <summary>
    /// ダイスアクションデータを拾ってくる.
    /// </summary>
    /// <param name="dataName"></param>
    /// <returns></returns>
    States.DiceActions SerchFData_DiceActionData(string dataName)
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
    /// 敵の行動ダイスデータを名前から引っ張ってくる.
    /// </summary>
    /// <param name="dataName"></param>
    CharactorActionData SerchData_CharactorActionData(string dataName)
    {
        CharactorActionData returnDatas = new CharactorActionData();
        foreach (CharactorActionData datas  in charactorActionDatas)
        {
            if (datas.enemyDataName == dataName)
            {
                returnDatas = datas;
            }
        }
        return returnDatas;
    }

    /// <summary>
    /// バトルダイスの生成.
    /// </summary>
    /// <param name="charaName"></param>
    public void DiceCreateInstance(string gameDataName,int diceNumber)
    {
        int number = 0;
        foreach (CharactorData states in enemyDatas)
        {
            if (gameDataName == states.gameName)
            {
                number = states.number;
            }
        }
        enemyStates[number].DiceSurfaceSetting(enemyDicePrefab);
        GameObject obj =  GameObject.Instantiate(enemyDicePrefab, transform.position, transform.rotation)as GameObject;
        enemyStates[number].diceRoll.Add(obj.GetComponent<DiceRoll>());
    }

    /// <summary>
    /// プレイヤーのダイスの生成.　ダイスの名前と番号が必要からダイスのデータを取得.
    /// </summary>
    /// <param name="playerNum"></param>
    public void PlayerDiceCreateInstance(string diceName,int diceNumber)
    {
        int number = 0;
        foreach (CharactorData states in playerDatas)
        {
            if (diceName == states.gameName)
            {
                number = states.number;
            }
        }
        playerStates[number].DiceSurfaceSetting(playerDicePrefab);
        GameObject obj = GameObject.Instantiate(playerDicePrefab, transform.position, transform.rotation) as GameObject;
        playerStates[number].diceRoll.Add(obj.GetComponent<DiceRoll>());
    }

    /// <summary>
    /// 能力値の設定.
    /// </summary>
    /// <param name="states"></param>
    /// <param name="charactorData"></param>
    void StatesSet(ref States[] states,ref CharactorData[] charactorData)
    {
        states = new States[charactorData.Length];
        for (int dataNum = 0; dataNum < states.Length; dataNum++)
        {
            states[dataNum] = new States();
            states[dataNum].IniStates(charactorData[dataNum].gameName, charactorData[dataNum].dataName, charactorData[dataNum].life, charactorData[dataNum].atack, charactorData[dataNum].defence, charactorData[dataNum].magic, charactorData[dataNum].mind);
            states[dataNum].IniLevelUpData(charactorData[dataNum].levelUpLife, charactorData[dataNum].levelUpAtack, charactorData[dataNum].levelUpDefence, charactorData[dataNum].levelUpMagic, charactorData[dataNum].levelUpMind);
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
        diceActions.Add(setDiceAction);
    }

    public void DiceSurfaceDataSet(States.DiceActions diceAction)
    {
        for (int diceFaceNum = 0; diceFaceNum < 6; diceFaceNum++)
        {
            Debug.Log(diceAction.actions[diceFaceNum]);
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
    void LoadTextData(string dataPath,ref CharactorData[] datas)
    {
        TextAsset textAsset = Resources.Load(dataPath)as TextAsset;
        string dataText = textAsset.text;
        text = dataText;
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
        string[] lines = dataText.Split(new string[] {"\r","\n" },option);
        //line = lines;
        string[] lineWidth = lines[0].Split(new string[] { ","},option);
        int dataWidth = lineWidth.Length;
        int dataLength = lines.Length;
        datas = new CharactorData[dataLength-1];
        int dataCount = 0;

        for (int lengthNum = 1; lengthNum < dataLength; lengthNum++)
        {
            DataSplitAndInsert(dataCount,lines[lengthNum],option,ref datas);
            dataCount++;
        }
    }

    /// <summary>
    /// スキルデータのロード
    /// </summary>
    void LoadTextData(string dataPath, ref SkillSet[] datas)
    {
        TextAsset textAsset = Resources.Load(dataPath) as TextAsset;
        string dataText = textAsset.text;
        text = dataText;
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
        string[] lines = dataText.Split(new string[] { "\r", "\n" }, option);
        //line = lines;
        string[] lineWidth = lines[0].Split(new string[] { "," }, option);
        int dataWidth = lineWidth.Length;
        int dataLength = lines.Length;
        datas = new SkillSet[dataLength - 1];
        int dataCount = 0;

        for (int lengthNum = 1; lengthNum < dataLength; lengthNum++)
        {
            DataSplitAndInsert(dataCount, lines[lengthNum], option, ref datas);
            dataCount++;
        }
    }

    /// <summary>
    /// キャラクターデータを数値にセット.
    /// </summary>
    void DataSplitAndInsert(int dataCount,string dataLine,System.StringSplitOptions option,ref CharactorData[] charaDatas)
    {
        string[] datas = dataLine.Split(new string[] { "," }, option);
        int count = 0;
        charaDatas[dataCount].number = int.Parse(datas[count++]);
        charaDatas[dataCount].nestNumber = int.Parse(datas[count++]);
        charaDatas[dataCount].dataName = datas[count++];
        charaDatas[dataCount].type = datas[count++];
        charaDatas[dataCount].gameName = datas[count++];
        charaDatas[dataCount].life = int.Parse(datas[count++]);
        charaDatas[dataCount].atack = int.Parse(datas[count++]);
        charaDatas[dataCount].defence = int.Parse(datas[count++]);
        charaDatas[dataCount].magic = int.Parse(datas[count++]);
        charaDatas[dataCount].mind = int.Parse(datas[count++]);
        charaDatas[dataCount].levelUpLife = int.Parse(datas[count++]);
        charaDatas[dataCount].levelUpAtack = int.Parse(datas[count++]);
        charaDatas[dataCount].levelUpDefence = int.Parse(datas[count++]);
        charaDatas[dataCount].levelUpMagic = int.Parse(datas[count++]);
        charaDatas[dataCount].levelUpMind = int.Parse(datas[count++]);
        /*
        charaDatas[dataCount].action1 = datas[count++];
        charaDatas[dataCount].action2 = datas[count++];
        charaDatas[dataCount].action3 = datas[count++];
        charaDatas[dataCount].action4 = datas[count++];
        charaDatas[dataCount].action5 = datas[count++];
        charaDatas[dataCount].action6 = datas[count++];
        */
        charaDatas[dataCount].dropItemName = datas[count++];
        charaDatas[dataCount].rareDropName = datas[count];
    }

    /// <summary>
    /// スキルデータのセット.
    /// </summary>
    /// <param name="dataCount"></param>
    /// <param name="dataLine"></param>
    /// <param name="option"></param>
    /// <param name="skillDatas"></param>
    void DataSplitAndInsert(int dataCount, string dataLine, System.StringSplitOptions option, ref SkillSet[] skillDatas)
    {
        string[] datas = dataLine.Split(new string[] { "," }, option);
        int count = 0;
        skillDatas[dataCount] = new SkillSet(datas[count++], datas[count++], datas[count++], datas[count++], datas[count++], datas[count++], datas[count++]);
    }

    /// <summary>
    /// スキルデータのセッティング.
    /// </summary>
    /// <param name="skillName"></param>
    /// <returns></returns>
    public static SkillSet SkillDataSetToStates(string skillName)
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
    /// 敵の行動ダイスのデータを読み込む.
    /// </summary>
    void LoadEnemyActionDatas(string dataPath, ref CharactorActionData[] datas)
    {
        TextAsset textAsset = Resources.Load(dataPath) as TextAsset;
        string dataText = textAsset.text;
        text = dataText;
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
        string[] lines = dataText.Split(new string[] { "\r", "\n" }, option);
        string[] lineWidth = lines[0].Split(new string[] { "," }, option);
        int dataWidth = lineWidth.Length;
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
    /// 敵の行動ダイスの情報をセッティング.
    /// </summary>
    void DataSplitAndInsert(int dataCount, string dataLine, System.StringSplitOptions option, ref CharactorActionData[] charactorActionDatas)
    {
        string[] datas = dataLine.Split(new string[] { "," }, option);
        charactorActionDatas[dataCount].enemyDataName = datas[0];
        for (int num = 0;num < datas.Length;num ++)
        {
            charactorActionDatas[dataCount].actionDataName[num] = datas[num + 1];
        }
    }

}
