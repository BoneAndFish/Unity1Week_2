using Action;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class States {

    /// <summary>
    /// 基礎ステ.
    /// </summary>
    public string name;
    public string dataName;
    public int dataNumber;//データ番号
    public int nestNumber;//ダンジョンの深さ出現
    public string type;//このキャラクターのタイプ.ボスかどうかとか.
    public int level;//レベル.
    public int maxLifePoint;//最大ＨＰ.
    public int nowLifePoint;//今のＨＰ.
    public int atackPower;//攻撃力.
    public int nowAtackPower;//現在攻撃力.
    public int defencePower;//防御力.
    public int nowDefencePower;//現在防御力.
    public int magicPower;//魔法力.
    public int nowMagicPower;
    public int mindPower;//精神力.
    public int nowMindPower;
    public bool isPlayer;//プレイヤー側か否か.
    public bool isGuard;//ガードしてるかどうか.

    /// <summary>
    /// 初期の値.
    /// </summary>
    public int iniLife;
    public int iniAtack;
    public int iniDefence;
    public int iniMagic;
    public int iniMind;

    /// <summary>
    /// レベルアップ時の上昇幅.
    /// </summary>
    public int levelUpLife;
    public int levelUpAtack;
    public int levelUpDefence;
    public int levelUpMagic;
    public int levelUpMind;

    public string normalDropItemName;
    public string rareDropItemName;

    public int actionCount;//行動回数.
    public string actionDicePatternName;//行動ダイスの種類.

    public List<DiceActions> diceActions = new List<DiceActions>();
    public List<DiceRoll> diceRoll = new List<DiceRoll>();
    public List<SkillSet> skillSet = new List<SkillSet>();
    public BadStates badStates = new BadStates();

    /// <summary>
    /// サイコロのアクション設定.キャラによってサイコロの数が違う.→サイコロの大きさとかも保存することに.
    /// </summary>
    [System.Serializable]
    public class DiceActions
    {
        public ActionList.ACTIONTYPE[] diceSurfaceAction = new ActionList.ACTIONTYPE[6];
        public string diceName = "None";
        public string[] actions = new string[6];
        public float size;//大きさ
        public float weight;//重さ
        public DiceActions(string actionName_1, string actionName_2, string actionName_3, string actionName_4, string actionName_5, string actionName_6)
        {
            actions[0] = actionName_1;
            actions[1] = actionName_2;
            actions[2] = actionName_3;
            actions[3] = actionName_4;
            actions[4] = actionName_5;
            actions[5] = actionName_6;
        }
        
    }

    /// <summary>
    /// 状態異常の設定.
    /// </summary>
    public class BadStates{
        public int poizonTrun;
        public int paralysisTurn;
        public int stunTrun;
        public int blindTrun;
        public int sleepTrun;
        public int silenceTrun;

        /// <summary>
        /// 初期化.
        /// </summary>
        public BadStates()
        {
            poizonTrun = 0;
            paralysisTurn = 0;
            stunTrun = 0;
            blindTrun = 0;
            sleepTrun = 0;
            silenceTrun = 0;
        }

        /// <summary>
        /// 状態異常ターンの設定.
        /// </summary>
        /// <param name="turn"></param>
        /// <param name="effectName"></param>
        public void SetbadStatesTrun(int turn,string effectName)
        {
            switch (effectName)
            {
                case "毒":
                    poizonTrun = turn;
                    break;
                case "麻痺":
                    paralysisTurn = turn;
                    break;
                case "スタン":
                    stunTrun = turn;
                    break;
                case "暗闇":
                    blindTrun = turn;
                    break;
                case "睡眠":
                    sleepTrun = turn;
                    break;
                case "沈黙":
                    silenceTrun = turn;
                    break;
            }
        }

        /// <summary>
        /// ターン終了時に状態異常の持続カウントを減らす.
        /// </summary>
        public void TurnEndBadStatesCountDown()
        {
            BadStatesTurnCountMinus(ref poizonTrun);
            BadStatesTurnCountMinus(ref paralysisTurn);
            BadStatesTurnCountMinus(ref stunTrun);
            BadStatesTurnCountMinus(ref blindTrun);
            BadStatesTurnCountMinus(ref sleepTrun);
            BadStatesTurnCountMinus(ref silenceTrun);
        }

        /// <summary>
        /// 状態異常を即座に回復する.
        /// </summary>
        /// <param name="effectName"></param>
        public void BadStatesClear(string effectName)
        {
            switch (effectName)
            {
                case "毒":
                    poizonTrun = 0;
                    break;
                case "麻痺":
                    paralysisTurn = 0;
                    break;
                case "スタン":
                    stunTrun = 0;
                    break;
                case "暗闇":
                    blindTrun = 0;
                    break;
                case "睡眠":
                    sleepTrun = 0;
                    break;
                case "沈黙":
                    silenceTrun = 0;
                    break;
            }
        }

        /// <summary>
        /// 特定の状態異常を受けているかどうかの判定.
        /// </summary>
        /// <param name="effectName"></param>
        /// <returns></returns>
        public bool BadStatesEffeted(string effectName)
        {
            bool isEffected = false;
            switch (effectName)
            {
                case "毒":
                    isEffected =  poizonTrun > 0 ? isEffected = true : isEffected = false;
                    break;
                case "麻痺":
                    isEffected = paralysisTurn > 0 ? isEffected = true : isEffected = false;
                    break;
                case "スタン":
                    isEffected = stunTrun > 0 ? isEffected = true : isEffected = false;
                    break;
                case "暗闇":
                    isEffected = blindTrun > 0 ? isEffected = true : isEffected = false;
                    break;
                case "睡眠":
                    isEffected = sleepTrun > 0 ? isEffected = true : isEffected = false;
                    break;
                case "沈黙":
                    isEffected = silenceTrun > 0 ? isEffected = true : isEffected = false;
                    break;
            }
            return isEffected;
        }

        /// <summary>
        /// カウント減少処理.
        /// </summary>
        /// <param name="badStatesTurn"></param>
        void BadStatesTurnCountMinus(ref int badStatesTurn)
        {
            badStatesTurn = badStatesTurn > 0 ? badStatesTurn - 1 : 0;
        }

    }

    /// <summary>
    /// ステータスの増加処理.
    /// </summary>
    public class StatesUpDown{
        public int atackUpTurn;
        public int defenceUpTurn;
        public int speedUpTurn;

        public StatesUpDown()
        {
            atackUpTurn = 0;
            defenceUpTurn = 0;
            speedUpTurn = 0;
        }

    }

    /// <summary>
    /// 基本情報の初期化
    /// </summary>
    public States(string _dataNum, string _nestNum, string loadDataName, string charaType, string charaName, string life, string atack, string defence, string magic, string mind)
    {
        dataNumber = int.Parse(_dataNum);
        nestNumber = int.Parse(_nestNum);
        dataName = loadDataName;
        type = charaType;
        name = charaName;
        iniLife = int.Parse(life);
        iniAtack = int.Parse(atack);
        iniDefence = int.Parse(defence);
        iniMagic = int.Parse(magic);
        iniMind = int.Parse(mind);
    }

    /// <summary>
    /// レベルアップ用数値を設定する.
    /// </summary>
    /// <param name="life"></param>
    /// <param name="atack"></param>
    /// <param name="defence"></param>
    /// <param name="magic"></param>
    /// <param name="mind"></param>
    public void IniLevelUpData(string life, string atack, string defence, string magic, string mind)
    {
        levelUpLife = int.Parse(life);
        levelUpAtack = int.Parse(atack);
        levelUpDefence = int.Parse(defence);
        levelUpMagic = int.Parse(magic);
        levelUpMind = int.Parse(mind);
    }

    /// <summary>
    /// ドロップアイテムのセット.
    /// </summary>
    public void DropItemSetting(string normal, string rare)
    {
        normalDropItemName = normal;
        rareDropItemName = rare;
    }
    
    /// <summary>
    /// 行動ダイスのセッティング.
    /// </summary>
    /// <param name="_actionCount"></param>
    /// <param name="iniActionPatternData"></param>
    public void ActionDiceData(string _actionCount,string iniActionPatternData)
    {
        actionCount = int.Parse(_actionCount);
        actionDicePatternName = iniActionPatternData;
    }

    /// <summary>
    /// 行動ダイスの設定.
    /// </summary>
    void IniDiceSurfaces(int diceNum, string action1, string action2, string action3, string action4, string action5, string action6)
    {
        diceActions[diceNum].actions[0] = action1;
        diceActions[diceNum].actions[1] = action2;
        diceActions[diceNum].actions[2] = action3;
        diceActions[diceNum].actions[3] = action4;
        diceActions[diceNum].actions[4] = action5;
        diceActions[diceNum].actions[5] = action6;
    }
  
    /// <summary>
    /// ダイスの表面とサイコロの情報の紐づけ設定.召喚するたびに呼び出して初期化する.
    /// </summary>
    /// <param name="dicePrefab"></param>
    public void DiceSurfaceSetting(GameObject dicePrefab,int diceNum)
    {
        DiceSetting diceSetting = dicePrefab.GetComponent<DiceSetting>();
        diceSetting.diceAction = diceActions[diceNum];
        diceSetting.IniDiceSet(diceActions[diceNum].diceSurfaceAction);
    }

    /// <summary>
    /// プレイヤーのステータスセット.
    /// /// </summary>
    public void PlayerStatesSet()
    {
        level = GameLevelManager.nowDungeonHierarchy;
        StatesSet(level);
    }

    /// <summary>
    /// レベルを使ってステータスセット.
    /// </summary>
    public void EnemyStatesSet()
    {
        GameLevelManager.EnemyLevelSetting();
        level = GameLevelManager.enemyLevel;
        StatesSet(level);
    }

    /// <summary>
    /// レベルによるステータスの設定.
    /// </summary>
    /// <param name="level"></param>
    void StatesSet(int level)
    {
        maxLifePoint = iniLife + levelUpLife * level;
        atackPower = iniAtack + levelUpAtack * level;
        defencePower = iniDefence + levelUpDefence * level;
        magicPower = iniMagic + levelUpMagic * level;
        mindPower = iniMind + levelUpMind *level;        
    }

    /// <summary>
    /// 出現時およびレベルアップ時のステータスを設定する.
    /// </summary>
    public void NowStatesSetting()
    {
        nowLifePoint = maxLifePoint;
        nowAtackPower = atackPower;
        nowDefencePower = defencePower;
        nowMagicPower = magicPower;
        nowMindPower = mindPower;
    }

}
