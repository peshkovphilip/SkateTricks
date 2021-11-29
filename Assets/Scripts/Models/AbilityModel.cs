using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityModel
{
    private EAbilityType _abilityType;
    private EAbilityActionType _abilityActionType;
    private float _abilityValue;
    private float _abilityDuration;
    private float _abilityDurationPast = 0f;
    private bool _active;

    public EAbilityType AbilityType => _abilityType;
    public EAbilityActionType AbilityActionType => _abilityActionType;
    public float AbilityValue => _abilityValue;
    public float AbilityDuration => _abilityDuration;
    public float AbilityDurationPast => _abilityDurationPast;
    
    public event Action<AbilityModel> ActivateAbility;
    
    public bool Active { 
        get => _active;
        set
        {
            _active = value;
            ActivateAbility?.Invoke(this);
        }
    }

    public AbilityModel(EAbilityType abilityType, EAbilityActionType abilityActionType, float abilityValue, float abilityDuration)
    {
        _abilityType = abilityType;
        _abilityActionType = abilityActionType;
        _abilityValue = abilityValue;
        _abilityDuration = abilityDuration;
    }
}
