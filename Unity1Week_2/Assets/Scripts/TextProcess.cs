using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextProcess
{
    public class TextSystem
    {
        /// <summary>
        /// 攻撃開始時.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="actorName"></param>
        /// <param name="targetName"></param>
        public static void AtackStartText(Text text, string actorName)
        {
            text.text = actorName + " の こうげき !";
        }

        /// <summary>
        /// 防御テキスト.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isPlayer"></param>
        public static void DefenceText(Text text,string actorName)
        {
            text.text = actorName + " は " + "まもりを かためた !!";
        }

        /// <summary>
        /// ダメージを受けたテキスト表示.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="targetName"></param>
        /// <param name="damege"></param>
        public static void PlayerAtackText(Text text,string targetName,int damege)
        {
            text.text += "\n" + targetName + " は " + damege + "の ダメージ をうけた !!";            
        }

        /// <summary>
        /// 敵を倒した.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="targetName"></param>
        public static void BeatEnemyText(Text text,string targetName)
        {
            text.text += "\n" + targetName + " を やっつけた !!";
        }

        /// <summary>
        /// テキスト処理.
        /// </summary>
        /// <param name="text"></param>
        public static void MissText(Text text,bool isPlayer)
        {
            string textData;
            if (isPlayer)
            {
                textData = "あなたたちは うまく れんけいが とれず\n こうげきに うつることが できなかった...";
            }else
            {
                textData = "まもの は あなたたちの \n ようすをみて こうげきをやめた !!";
            }
            text.text = textData;
        }

        /// <summary>
        /// プレイヤー側が倒された.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="targetName"></param>
        public static void DeadPlayerText(Text text)
        {
            text.text = "なんということだ...\nあなたたちは たおされてしまった... ";
        }

        public static void NextTurnText(Text text)
        {
            text.text = "さあ たたかいは まだ つづいているぞ !!";
        }

        /// <summary>
        /// 勝利処理
        /// </summary>
        /// <param name="text"></param>
        public static void WinText(Text text)
        {
            text.text = "みごと おそいくる てき を しりぞけた !!";
        }

        /// <summary>
        /// スキルによる攻撃.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="actorName"></param>
        /// <param name="skillName"></param>
        public static void SkillActiveText(Text text, string actorName,string skillName,bool isMagic)
        {
            string textData;
            if (isMagic)
            {
                textData = actorName + " は " + skillName + "を となえた !!";
            }else {
                textData = actorName + " の " + skillName + "を はなった !!";
            }
            text.text = textData;
        }

        /// <summary>
        /// 回復テキスト処理.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="healValue"></param>
        /// <param name="isPlayer"></param>
        public static void HealText(Text text,int healValue,bool isPlayer,string actorName)
        {
            string textData;
            if (isPlayer)
            {
                textData = "\n" + "あなたたちの たいりょくが" + healValue + "かいふくした !!";
            }else
            {
                textData = "\n" + "まもの の たいりょくが" + healValue + "かいふくした !!";
            }
            text.text += textData;
        }

        /// <summary>
        /// ステータスの増加,減少.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="inUpDownPoint"></param>
        /// <param name="statesName"></param>
        /// <param name="targetName"></param>
        /// <param name="isUp"></param>
        /// <param name="isPlayer"></param>
        public static void StatesUpDownText(Text text, int inUpDownPoint, string statesName, string targetName, bool isUp)
        {
            string textData;
            if (isUp)
            {
                textData = "\n" + targetName + " の " + statesName + " が" + inUpDownPoint + "あがった !!";
            }else
            {
                textData = "\n" + targetName + " の " + statesName + " が" + inUpDownPoint + "さがった !!";
            }
            text.text += textData;
        }

        /// <summary>
        /// 状態異常を受けた時のテキスト.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="actorName"></param>
        /// <param name="targetName"></param>
        /// <param name="badStatesName"></param>
        public static void BadStatesText(Text text,string actorName,string targetName,string badStatesName)
        {
            text.text += "\n" + targetName + " は " + badStatesName + " を うけた !!";
        }

        /// <summary>
        /// ステータスが元に戻る.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="targetName"></param>
        /// <param name="statesName"></param>
        public static void StatesReset(Text text,string targetName,string statesName)
        {
            string textData;
            textData = "\n" + targetName + " の " + statesName + " が" + "もとにもどった。";
            text.text += textData;
        }
        

    }
}
