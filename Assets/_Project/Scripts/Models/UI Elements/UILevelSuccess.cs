using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelSuccess : UIElementModel
{
 
    public void SetNextLevelButton()
    {
        GameController.ChangeGameState(GameStates.Gameplay);
        LevelController.Instance.NextLevel();
    }
}
