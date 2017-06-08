using Action;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class States {

    /// <summary>
    /// 基礎ステ.
    /// </summary>
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

    public string name;
    public string dataName;
    public string[] actions;
    public ActionList.ACTIONTYPE[] diceSurfaceAction;
    public List<DiceRoll> diceRoll = new List<DiceRoll>();
    public List<SkillSet> skillSet = new List<SkillSet>();
    public BadStates badStates = new BadStates();

    //public DiceSurface[] diceSurface;
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
    /// 初期化しないと怒られることに気付いた.
    /// </summary>
    public States()
    {
        level = 0;
        maxLifePoint = 0;
        nowLifePoint = 0;
        nowLifePoint = 0;
        atackPower = 0;
        nowAtackPower = 0;
        defencePower = 0;
        nowDefencePower = 0;
        magicPower = 0;
        nowMagicPower=0;
        mindPower = 0;
        nowMindPower = 0;
        isPlayer = false;
        isGuard = false;
        iniLife = 0;
        iniAtack = 0;
        iniDefence = 0;
        iniMagic = 0;
        iniMind = 0;
        levelUpLife = 0;
        levelUpAtack = 0;
        levelUpDefence = 0;
        levelUpMagic = 0;
        levelUpMind = 0;
        name = "";
        actions = new string[6];
        diceSurfaceAction = new ActionList.ACTIONTYPE[6];
    }

    /// <summary>
    /// ステータスの初期化.
    /// </summary>
    public void IniStates(string charaName,string loadDataName,int life,int atack,int defence,int magic,int mind)
    {
        name = charaName;
        dataName = loadDataName;
        iniLife = life;
        iniAtack = atack;
        iniDefence = defence;
        iniMagic = magic;
        iniMind = mind;
    }

    /// <summary>
    /// レベルアップ用数値を設定する.
    /// </summary>
    /// <param name="life"></param>
    /// <param name="atack"></param>
    /// <param name="defence"></param>
    /// <param name="magic"></param>
    /// <param name="mind"></param>
    public void IniLevelUpData(int life, int atack, int defence, int magic, int mind)
    {
        levelUpLife = life;
        levelUpAtack = atack;
        levelUpDefence = defence;
        levelUpMagic = magic;
        levelUpMind = mind;
    }
    
    /// <summary>
    /// 行動ダイスの設定.
    /// </summary>
    /// <param name="action1"></param>
    /// <param name="action2"></param>
    /// <param name="action3"></param>
    /// <param name="action4"></param>
    /// <param name="action5"></param>
    /// <param name="action6"></param>
    public void IniActionDatas(string action1, string action2, string action3, string action4, string action5, string action6)
    {
        actions[0] = action1;
        actions[1] = action2;
        actions[2] = action3;
        actions[3] = action4;
        actions[4] = action5;
        actions[5] = action6;
        DiceSurfaceDataSet();
    }

    /// <summary>
    /// アクションサイコロの行動設定.
    /// </summary>
    public void DiceSurfaceDataSet()
    {
        for (int diceFaceNum = 0;diceFaceNum < 6;diceFaceNum++)
        {
            switch (actions[diceFaceNum])
            {
                case "戦う":
                    diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.ATACK;
                    break;
                case "防御":
                    diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.GUARD;
                    break;
                case "回避":
                    diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.DODGE;
                    break;
                case "ミス":
                    diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.MISS;
                    break;
                case "必殺の一撃":
                    diceSurfaceAction[diceFaceNum] = ActionList.ACTIONTYPE.CRITICAL;
                    break;
            }
            if (actions[diceFaceNum].Contains("魔法："))
            {
                SkillNameResetting("魔法：", diceFaceNum, ActionList.ACTIONTYPE.MAGICSKILL);
            }
            if (actions[diceFaceNum].Contains("特技："))
            {
                SkillNameResetting("特技：", diceFaceNum, ActionList.ACTIONTYPE.ATACKSKILL);
            }
            if (actions[diceFaceNum].Contains("強化："))
            {
                SkillNameResetting("強化：", diceFaceNum, ActionList.ACTIONTYPE.SUPPORTSKILL);
            }
            if (actions[diceFaceNum].Contains("弱体："))
            {
                SkillNameResetting("弱体：",diceFaceNum, ActionList.ACTIONTYPE.SUPPORTSKILL);
            }
        }
    }

    /// <summary>
    /// スキルデータの名前の再設定.
    /// </summary>
    /// <param name="outText"></param>
    /// <param name="diceFaceNum"></param>
    /// <param name="actionType"></param>
    void SkillNameResetting(string outText,int diceFaceNum,ActionList.ACTIONTYPE actionType)
    {
        actions[diceFaceNum] = actions[diceFaceNum].Substring(outText.Length);
        diceSurfaceAction[diceFaceNum] = actionType;
        skillSet.Add(DataSetting.SkillDataSetToStates(actions[diceFaceNum]));
    }

    /// <summary>
    /// ダイスの表面とサイコロの情報の紐づけ設定.召喚するたびに呼び出して初期化する.
    /// </summary>
    /// <param name="dicePrefab"></param>
    public void DiceSurfaceSetting(GameObject dicePrefab)
    {
        DiceSetting diceSetting = dicePrefab.GetComponent<DiceSetting>();
        diceSetting.IniDiceSet(diceSurfaceAction);
        //diceRoll.Add(dicePrefab.GetComponent<DiceRoll>());//ここを変えないとサイコロ回ってくれない.
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
