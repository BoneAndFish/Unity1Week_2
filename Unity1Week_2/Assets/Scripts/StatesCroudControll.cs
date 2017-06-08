using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesCroudControll{

    public enum BADSTATESLIST
    {
        NONE,
        Poizon,//毒.毎ターンダメージを受ける.
        Paralysis,//麻痺.サイコロが1個減る.
        Stun,//スタン.受けたターンは行動できない.
        Blind,//暗闇.通常攻撃が失敗する.
        Sleep,//睡眠.継続的にランダムで行動できない.
        Silence,//沈黙.スキルが使えない.
    }




}
