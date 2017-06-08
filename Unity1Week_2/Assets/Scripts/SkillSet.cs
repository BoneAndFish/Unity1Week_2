﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkillSet{

    public string skillName;
    public int skillNumber;
    public string skillType;
    public string effectType;
    public int effectValue;
    public float timerTime;
    public string badStates;

    /// <summary>
    /// 使用するスキル情報をセットする.
    /// </summary>
    /// <param name="_skillName"></param>
    /// <param name="_skillNumber"></param>
    /// <param name="_skillType"></param>
    /// <param name="_effectType"></param>
    /// <param name="_effectValue"></param>
    /// <param name="_timerTime"></param>
    /// <param name="_resource"></param>
    public SkillSet(string _skillName,string _skillNumber,string _skillType,string _effectType,string _effectValue,string _timerTime,string _badStates)
    {
        skillName = _skillName;
        skillNumber = int.Parse(_skillNumber);
        skillType = _skillType;
        effectType = _effectType;
        effectValue = int.Parse(_effectValue);
        timerTime = float.Parse(_timerTime);
        badStates = _badStates;
    }
	
}
