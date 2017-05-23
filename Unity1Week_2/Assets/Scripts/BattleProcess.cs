using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattlePricess {

    public enum PROCESSTYPE
    {
        NONE,
        DAMEGE,//攻撃計算
        HEAL,//回復計算
        DODGE,//回避
        DEFENCE,//防御計算
        COUNTER,//反撃計算
        POWERHIT,//会心の一撃
        MISS,//失敗処理
    }

    public class DamegeProcess {

        /// <summary>
        /// ライフの増減処理を全て詰め込んだ.processTypeで指定する.
        /// </summary>
        /// <param name="targetLigePoint">対象のライフ</param>
        /// <param name="inActorPoint">行動側のポイント</param>
        /// <param name="inTargetPoint">目標のポイント</param>
        /// <param name="processType">どの処理か</param>
        /// <returns></returns>
        public int LifePointProcess(int targetLigePoint,int inActorPoint,int inTargetPoint,PROCESSTYPE processType)
        {
            int processedValue = targetLigePoint;//処理後の値
            switch (processType)
            {
                case PROCESSTYPE.DAMEGE:
                    int damegeValue = AtackDamegeProcess(inActorPoint, inTargetPoint);
                    processedValue =  LifePointDown(targetLigePoint,damegeValue);
                    break;

                case PROCESSTYPE.HEAL:
                    processedValue = HealProcess(targetLigePoint,inActorPoint);

                    break;
                case PROCESSTYPE.DODGE:

                    break;

                case PROCESSTYPE.DEFENCE:
                    DefenceProcess(inActorPoint,0.5f);
                    break;

                case PROCESSTYPE.COUNTER:
                    int counterDamege = CounterProcess(inActorPoint);
                    processedValue = LifePointDown(targetLigePoint, counterDamege);
                    break;

                case PROCESSTYPE.POWERHIT:

                    break;

                case PROCESSTYPE.MISS:

                    break;
            }
            return processedValue;
        }

        /// <summary>
        /// ライフ減少処理.
        /// </summary>
        /// <param name="targetLifePoint">対象のライフ</param>
        /// <param name="processPoint">減らす量</param>
        /// <returns></returns>
        int LifePointDown(int targetLifePoint,int inPoint)
        {
            targetLifePoint -= inPoint;
            return targetLifePoint;
        }

        /// <summary>
        /// ライフ増加処理.
        /// </summary>
        /// <param name="targetLifePoint">対象のライフ</param>
        /// <param name="processPoint">増やす量</param>
        /// <returns></returns>
        int LifePointUp(int targetLifePoint, int inPoint)
        {
            targetLifePoint += inPoint;
            return targetLifePoint;
        }

        /// <summary>
        /// ダメージ計算式.
        /// </summary>
        /// <param name="inDamegePoint">受けるダメージ量</param>
        /// <param name="inDefencePoint">受ける側の防御力</param>
        /// <returns></returns>
        int AtackDamegeProcess(int inDamegePoint,int inDefencePoint)
        {
            int damege = inDamegePoint;
            int baseDamage = (inDamegePoint - inDefencePoint);
            damege = baseDamage + Random.Range((int)(baseDamage * -0.5),(int)(baseDamage*0.5+1));
            if (damege <= 0)
            {
                damege = Random.Range(0,2);
            }
            return damege;
        }

        /// <summary>
        /// 防御によるダメージ軽減.
        /// </summary>
        /// <param name="inDamegePoint"></param>
        /// <returns></returns>
        int DefenceProcess(int inDamegePoint,float cutValue)
        {            
            return (int)(inDamegePoint * cutValue);
        }

        /// <summary>
        /// 回復処理.
        /// </summary>
        /// <param name="inTargetLifePoint">回復対象のライフ</param>
        /// <param name="inHealPoint">回復量</param>
        /// <returns></returns>
        int HealProcess(int inTargetLifePoint,int inHealPoint)
        {
            int lifePoint = inTargetLifePoint;
            lifePoint += inHealPoint + Random.Range((int)(inHealPoint*-0.1),(int)(inHealPoint*1.25));
            return lifePoint;
        }

        /// <summary>
        /// 反撃プロセス.
        /// </summary>
        /// <param name="inDamegePoint"></param>
        /// <returns></returns>
        int CounterProcess(int inDamegePoint)
        {
            int returnDamege = 0;
            returnDamege = (int)(inDamegePoint * Random.Range(1.25f,1.75f));
            return returnDamege;
        }


    }

}