using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleProcess {

    public class DamegeProcess {

        /// <summary>
        /// ライフ減少処理.
        /// </summary>
        /// <param name="targetLifePoint">対象のライフ</param>
        /// <param name="processPoint">減らす量</param>
        /// <returns></returns>
        public static int LifePointDown(ref int targetLifePoint,int inPoint)
        {
            targetLifePoint -= inPoint;
            if (targetLifePoint <= 0)
            {
                targetLifePoint = 0;
            }
            return targetLifePoint;
        }

        /// <summary>
        /// ライフ増加処理.
        /// </summary>
        /// <param name="targetLifePoint">対象のライフ</param>
        /// <param name="processPoint">増やす量</param>
        /// <returns></returns>
        public static int LifePointUp(ref int targetLifePoint,int maxLife, int inPoint)
        {
            targetLifePoint += inPoint;
            if (targetLifePoint >= maxLife)
            {
                targetLifePoint = maxLife;
            }
            return targetLifePoint;
        }

        /// <summary>
        /// ダメージ計算式.物理.
        /// </summary>
        /// <param name="inDamegePoint">受けるダメージ量</param>
        /// <param name="inDefencePoint">受ける側の防御力</param>
        /// <returns></returns>
        public static int AtackDamegeProcess(int inDamegePoint,int inDefencePoint)
        {
            int damege = inDamegePoint;
            int baseDamage = (inDamegePoint - inDefencePoint);
            damege = baseDamage + Random.Range((int)(baseDamage * -0.5),(int)(baseDamage*0.5)+1);
            if (damege <= 0)
            {
                damege = Random.Range(0,2);
            }
            return damege;
        }

        /// <summary>
        /// ダメージ計算式.魔法攻撃.
        /// </summary>
        /// <param name="inDamegePoint">与えるダメージ</param>
        /// <param name="inDefencePoint">魔法防御力</param>
        /// <returns></returns>
        public static int MagicDamegeProcess(int inDamegePoint, int inDefencePoint)
        {
            int damege = inDamegePoint;
            int baseDamage = inDamegePoint - (int)(inDefencePoint * 1.65);
            damege = baseDamage + Random.Range((int)(baseDamage * -0.25), (int)(baseDamage * 0.75)+1);
            if (damege <= 0)
            {
                damege = Random.Range(0, 2);
            }
            return damege;
        }

        /// <summary>
        /// 防御によるダメージ軽減.
        /// </summary>
        /// <param name="inDamegePoint"></param>
        /// <returns></returns>
        public static int DefenceProcess(int inDamegePoint,float cutValue)
        {            
            return (int)(inDamegePoint * cutValue);
        }

        /// <summary>
        /// 回復処理.
        /// </summary>
        /// <param name="inTargetLifePoint">回復対象のライフ</param>
        /// <param name="inHealPoint">回復量</param>
        /// <returns></returns>
        public static int HealProcess(int inHealPoint)
        {
            inHealPoint += Random.Range((int)(inHealPoint*-0.1),(int)(inHealPoint*1.25));
            return inHealPoint;
        }

        /// <summary>
        /// 反撃プロセス.
        /// </summary>
        /// <param name="inDamegePoint"></param>
        /// <returns></returns>
        public static int CounterProcess(int inDamegePoint)
        {
            int returnDamege = 0;
            returnDamege = (int)(inDamegePoint * Random.Range(1.25f,1.75f));
            return returnDamege;
        }

        /// <summary>
        /// パラメータの上昇とか.
        /// </summary>
        /// <param name="inStates"></param>
        /// <param name="nowStates"></param>
        /// <param name="downUpPoint"></param>
        /// <param name="isUp"></param>
        /// <returns></returns>
        public static void ParamateUp(ref int nowStates,int downUpPoint,bool isUp)
        {
            if (isUp)
            {
                nowStates += downUpPoint;
            }else
            {
                nowStates -= downUpPoint;
            }
        }


    }

}