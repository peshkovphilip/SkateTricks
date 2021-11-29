using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : IStarter
{
    private PlayerModel _playerModel;
    private EnvironmentView[] _environments;
    
    public EnvironmentController(PlayerModel playerModel, EnvironmentView[] environments)
    {
        _playerModel = playerModel;
        _environments = environments;
    }
    
    public void Starter()
    {
        _playerModel.SetHealth += SetDamage;
    }

    private void SetDamage(int damage)
    {
        int formulaDamage = Convert.ToInt32(damage / 2);
        foreach (EnvironmentView environment in _environments)
        {
            if (environment.Type == EEnvironmentType.Hole)
            {
                environment.Damage = formulaDamage;
            }
        }
    }

}
