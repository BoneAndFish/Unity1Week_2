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

        public static void DefenceText(Text text,bool isPlayer)
        {
            string textData;
            if (isPlayer)
            {
                textData = "あなたたちは まもり を かためた !";
            }else
            {
                textData = "まもの は まもり を かためた !";
            }
            text.text = textData;
        }

        /// <summary>
        /// ダメージを受けたテキスト表示.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="targetName"></param>
        /// <param name="damege"></param>
        public static void PlayerAtackText(Text text,string targetName,int damege)
        {
            text.text = targetName + " は " + damege + "の ダメージ をうけた !!";            
        }

        /// <summary>
        /// 敵を倒した.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="targetName"></param>
        public static void BeatEnemyText(Text text,string targetName)
        {
            text.text = targetName + " を やっつけた !!";
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
                textData = "まもの の こうげきは \n しっぱいに おわった !!";
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

        /// <summary>
        /// 勝利処理
        /// </summary>
        /// <param name="text"></param>
        public static void WinText(Text text)
        {
            text.text = "みごと おそいくる てき を しりぞけた !";
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
        public static void HealText(Text text,int healValue,bool isPlayer)
        {
            string textData;
            if (isPlayer)
            {
                textData = "あなたたちの たいりょくが" + healValue + "かいふくした !!";
            }else
            {
                textData = "まもの の たいりょくが" + healValue + "かいふくした !!";
            }
            text.text = textData;
        }

    }
}
