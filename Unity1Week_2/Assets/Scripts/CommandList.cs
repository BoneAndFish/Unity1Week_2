using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleProcess;
using TextProcess;

namespace BattleCommand
{
    public class CommandList
    {
        /// <summary>
        /// 通常攻撃処理
        /// </summary>
        public static bool Atack(Text text, string actorName, string targetName,ref int targetLifePoint, int atackerAtack, int targetDefence, bool isGuard,bool isPlayer)
        {
            //if (MissText(text, isMiss, isPlayer)) return;

            Defence(text,isGuard,isPlayer);
            TextSystem.AtackStartText(text, actorName);
            int damege = DamegeProcess.AtackDamegeProcess(atackerAtack, targetDefence);
            if (isGuard == true)
            {
                damege = DamegeProcess.DefenceProcess(damege, 0.5f);
            }
            DamegeProcess.LifePointDown(ref targetLifePoint, damege);
            TextSystem.PlayerAtackText(text, targetName, damege);
            return IsGameEnd(text, targetLifePoint, isPlayer, targetName);           
        }

        /// <summary>
        /// 特殊な攻撃処理
        /// </summary>
        /// <param name="targetLifePoint"></param>
        /// <param name="atackerMagic"></param>
        /// <param name="targetMind"></param>
        public static bool SpecialAtack(Text text, string actorName, string skillName, string targetName,ref int targetLifePoint, int atackePoint, int targetPoint, bool isMagic, bool isGuard,bool isPlayer)
        {
            //if (MissText(text, isMiss, isPlayer)) return;
            
            Defence(text, isGuard, isPlayer);
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
            TextSystem.SkillActiveText(text, actorName, skillName, isMagic);//スキル発動.
            DamegeProcess.LifePointDown(ref targetLifePoint, damege);//ライフ減少.
            TextSystem.PlayerAtackText(text, targetName, damege);//敵に攻撃.
            return IsGameEnd(text,targetLifePoint,isPlayer, targetName);//どちらが死んだか.
        }

        /// <summary>
        /// 防御処理
        /// </summary>
        static void Defence(Text text,bool isGuard,bool isPlayer)
        {
            if (isGuard)
            {
                TextSystem.DefenceText(text,isPlayer);
            }
        }

        /// <summary>
        /// 回復処理.
        /// </summary>
        public static void Heal(Text text, string actorName, string targetName,ref int targetLifePoint,int targetMaxLife , int healPoint,bool isPlayer)
        {
            int healValue = DamegeProcess.HealProcess(healPoint);
            TextSystem.HealText(text, healValue,isPlayer);
            DamegeProcess.LifePointUp(ref targetLifePoint,targetMaxLife,healValue);
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
