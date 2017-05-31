using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSetting : MonoBehaviour {

    public string enemyDataPath;
    public string playerDataPath;
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

	// Use this for initialization
	void Start () {     
    }

    public void IniDatas()
    {
        SetEnemyDatas();
        SetPlayerDatas();
        SetEnemyDatas();
        //DiceCreateInstance("Gobrin"); 
        Invoke("TestDiceInstance", 1f);
    }
	
    public void TestDiceInstance()
    {
        DiceCreateInstance("Gobrin");
        PlayerDiceCreateInstance("Dicelot");
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
    }

    /// <summary>
    /// バトルダイスの生成.
    /// </summary>
    /// <param name="charaName"></param>
    public void DiceCreateInstance(string gameDataName)
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
        Instantiate(enemyDicePrefab,transform.position,transform.rotation);
    }

    /// <summary>
    /// プレイヤーのダイスの生成.
    /// </summary>
    /// <param name="playerNum"></param>
    public void PlayerDiceCreateInstance(string diceName)
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
        Instantiate(playerDicePrefab, transform.position, transform.rotation);
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
    /// テキストデータのロード.
    /// </summary>
    void LoadTextData(string dataPath,ref CharactorData[] datas)
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
        datas = new CharactorData[dataLength-1];
        int dataCount = 0;

        for (int lengthNum = 1; lengthNum < dataLength; lengthNum++)
        {
            DataSplitAndInsert(dataCount,lines[lengthNum],option,ref datas);
            dataCount++;
        }
    }

    /// <summary>
    /// 敵のデータを数値にセット.
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

}
