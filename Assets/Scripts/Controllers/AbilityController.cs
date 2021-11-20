using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController: IStarter
{
    private List<AbilityModel> abilities = new List<AbilityModel>();
    private PlayerModel _playerModel;

    public AbilityController(PlayerModel playerModel)
    {
        _playerModel = playerModel;
    }
    public void Starter()
    {
        abilities.Add(new AbilityModel(EAbilityType.Jump, EAbilityActionType.Multi, 2f, 10f));
        foreach (AbilityModel ability in abilities)
        {
            ability.ActivateAbility += SetAbility;
        }
    }

    private void SetAbility(AbilityModel ability)
    {
        switch (ability.AbilityType)
        {
            case EAbilityType.Life:
                switch (ability.AbilityActionType)
                {
                    case EAbilityActionType.Clear:
                        _playerModel.Lifes = 0;
                        break;
                    case EAbilityActionType.Equals:
                        _playerModel.Lifes = (int)ability.AbilityValue;
                        break;
                    case EAbilityActionType.Plus:
                        _playerModel.Lifes += (int)ability.AbilityValue;
                        break;
                    case EAbilityActionType.Minus:
                        _playerModel.Lifes -= (int)ability.AbilityValue;
                        break;
                }
                break;
            case EAbilityType.Jump:
                switch (ability.AbilityActionType)
                {
                    case EAbilityActionType.Clear:
                        _playerModel.JumpForce = 0;
                        break;
                    case EAbilityActionType.Equals:
                        _playerModel.JumpForce = ability.AbilityValue;
                        break;
                    case EAbilityActionType.Plus:
                        _playerModel.JumpForce += ability.AbilityValue;
                        break;
                    case EAbilityActionType.Minus:
                        _playerModel.JumpForce -= ability.AbilityValue;
                        break;
                    case EAbilityActionType.Multi:
                        _playerModel.JumpForce *= ability.AbilityValue;
                        break;
                }
                break;
            case EAbilityType.Push:
                switch (ability.AbilityActionType)
                {
                    case EAbilityActionType.Clear:
                        _playerModel.PushForce = 0;
                        break;
                    case EAbilityActionType.Equals:
                        _playerModel.PushForce = ability.AbilityValue;
                        break;
                    case EAbilityActionType.Plus:
                        _playerModel.PushForce += ability.AbilityValue;
                        break;
                    case EAbilityActionType.Minus:
                        _playerModel.PushForce -= ability.AbilityValue;
                        break;
                    case EAbilityActionType.Multi:
                        _playerModel.PushForce *= ability.AbilityValue;
                        break;
                }
                break;
        }
        
    }

    public List<AbilityModel> GetAbilities()
    {
        return abilities;
    }
}
