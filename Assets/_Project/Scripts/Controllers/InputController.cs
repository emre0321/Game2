using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : ControllerBaseModel
{
    [SerializeField] PointerController pointerController;

    public override void ControllerUpdate(GameStates gameState)
    {
        pointerController.ControllerUpdate();
    }
}
