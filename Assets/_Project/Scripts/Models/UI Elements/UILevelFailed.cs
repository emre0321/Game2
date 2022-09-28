using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelFailed : UIElementModel
{
    public void SetRetryButton()
    {
        GameController.ChangeGameState(GameStates.Gameplay);
        LevelController.Instance.RetryLevel();
    }
}
