using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSetting : MonoBehaviour {

    public string enemyDataPath;
    public string playerDataPath;
    public string skillDataPath;
    private string dataPath;
    //public string[] enemyTextData;
    public string text;
    public CharactorData[] enemyDatas;
    public CharactorData[] playerDatas;

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
        public string action1;
        public string action2;
        public string action3;
        public string action4;
        public string action5;
        public string action6;
        public string dropItemName;
        public string rareDropName;
    }

    public States[] enemyStates;
    public States[] playerStates;
    public static SkillSet[] skillDatas;

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
        SetEnemyDatas();
        SetPlayerDatas();
        SetEnemyDatas();
        //DiceCreateInstance("Gobrin"); 
        Invoke("TestDiceInstance", 1f);
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
        LoadTextData(enemyDataPath,ref enemyDatas);
        StatesSet(ref enemyStates,ref enemyDatas);
        //EnemyStatesSet();
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
    /// プレイヤーのダイスの生成.
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
            states[dataNum].IniActionDatas(charactorData[dataNum].action1, charactorData[dataNum].action2, charactorData[dataNum].action3, charactorData[dataNum].action4, charactorData[dataNum].action5, charactorData[dataNum].action6);
        }
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
    /// スキルデータのロードとセーブ.
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
        charaDatas[dataCount].action1 = datas[count++];
        charaDatas[dataCount].action2 = datas[count++];
        charaDatas[dataCount].action3 = datas[count++];
        charaDatas[dataCount].action4 = datas[count++];
        charaDatas[dataCount].action5 = datas[count++];
        charaDatas[dataCount].action6 = datas[count++];
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

}
