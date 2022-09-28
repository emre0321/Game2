using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBaseModel : ObjectModel
{
    public virtual void ControllerUpdate(GameStates gameState) { }

    public virtual void OnGameStateChange(GameStates gameState)
    {

    }
}
