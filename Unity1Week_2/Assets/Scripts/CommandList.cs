﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BattleProcess;
using TextProcess;

namespace BattleCommand
{
    public class CommandList
    {

        /// <summary>
        /// 通常攻撃処理.
        /// </summary>
        public static int Atack(Text text, string actorName, string targetName,ref int targetLifePoint, int atackerAtack, int targetDefence, bool isGuard,bool isPlayer)
        {
            TextSystem.AtackStartText(text, actorName);
            int damege = DamegeProcess.AtackDamegeProcess(atackerAtack, targetDefence);
            if (isGuard == true)
            {
                damege = DamegeProcess.DefenceProcess(damege, 0.5f);
            }
            DamegeProcess.LifePointDown(ref targetLifePoint, damege);
            return damege;
        }

        /// <summary>
        /// 特殊な攻撃処理.ダメージ技.
        /// </summary>
        /// <param name="targetLifePoint"></param>
        /// <param name="atackerMagic"></param>
        /// <param name="targetMind"></param>
        public static int SpecialAtack(Text text, string actorName, string skillName, string targetName,ref int targetLifePoint, int atackePoint, int targetPoint, bool isMagic, bool isGuard,bool isPlayer)
        {
            int damege = 0;
            if (isMagic)
            {
                damege = DamegeProcess.MagicDamegeProcess(atackePoint, targetPoint);
            }
            else
            {
                damege = DamegeProcess.AtackDamegeProcess(atackePoint, targetPoint);
                if (isGuard == true)
                {
                    damege = DamegeProcess.DefenceProcess(damege, 0.5f);
                }
            }
            DamegeProcess.LifePointDown(ref targetLifePoint, damege);//ライフ減少.
            return damege;
        }

        /// <summary>
        /// 防御処理.
        /// </summary>
        public static void Defence(Text text,ref bool isGuard,string actorName)
        {
            isGuard = true;
            TextSystem.DefenceText(text,actorName);
        }

        /// <summary>
        /// 防御リセット処理.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="target"></param>
        public static void ResetDefence(ref States actor,ref States target)
        {
            actor.isGuard = false;
            target.isGuard = false;
        }

        /// <summary>
        /// 回復処理.
        /// </summary>
        public static int Heal(Text text, string actorName, string targetName,ref int targetLifePoint,int targetMaxLife , int healPoint,bool isPlayer)
        {
            int healValue = DamegeProcess.HealProcess(healPoint);
            DamegeProcess.LifePointUp(ref targetLifePoint,targetMaxLife,healValue);
            return healValue;
        }

        /// <summary>
        /// 状態異常を受けた処理.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="actorName"></param>
        /// <param name="targetName"></param>
        /// <param name="targetStatePoint"></param>
        /// <param name="badStatesValue"></param>
        public static void BadStates(Text text,string actorName,string targetName,ref int targetStatePoint,int badStatesValue,string badStatesName)
        {
            targetStatePoint = badStatesValue;
            TextSystem.BadStatesText(text,actorName,targetName,badStatesName);
        }
        

        public static void StatesControll(Text text,string actorName,string targetName,ref int targetStatesPoint,int controllPoint)
        {
            targetStatesPoint += controllPoint;
        }

        /// <summary>
        /// 死んだか死んでないか.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="targetLifePoint"></param>
        /// <param name="isPlayer"></param>
        public static bool IsGameEnd(Text text,int targetLifePoint,bool isPlayer,string targetName)
        {            
            if (targetLifePoint <= 0)
            {
                switch (isPlayer)
                {
                    case true://敗北時.
                        TextSystem.DeadPlayerText(text);
                        break;

                    case false://勝利時.
                        TextSystem.BeatEnemyText(text, targetName);
                        break;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 勝利処理.
        /// </summary>
        /// <param name="text"></param>
        public static void WinPlayer(Text text)
        {
            TextSystem.WinText(text);
        }

        /// <summary>
        /// 次のターン開始処理.
        /// </summary>
        /// <param name="text"></param>
        public static void NextTurn(Text text)
        {
            TextSystem.NextTurnText(text);
        }

        /// <summary>
        /// 攻撃が失敗に終わるときの処理.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isMiss"></param>
        /// <param name="isPlayer"></param>
        public static void MissText(Text text,bool isPlayer)
        {
            TextSystem.MissText(text, isPlayer);
        }

    }
}
