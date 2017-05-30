using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action {

    public class ActionList
    {
        /// <summary>
        /// 行動タイプ.
        /// </summary>
        public enum ACTIONTYPE
        {
            NONE,
            ATACK,
            MAGICSKILL,
            ATACKSKILL,
            SUPPORTSKILL,
            HEAL,
            DODGE,
            GUARD,
            CRITICAL,
            MISS,
        }

        /// <summary>
        /// 行動タイプが読み込むテクスチャ.
        /// </summary>
        public static string[] textureName =
        {
            "None",
            "Atack",
            "MagicSkill",
            "AtackSkill",
            "SupportSkill",
            "Heal",
            "Dodge",
            "Guard",
            "Critical",
            "Miss",
        };
    }
}

