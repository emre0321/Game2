using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UIElementModel
{
    public void SetPlayButton()
    {
        GameController.ChangeGameState(GameStates.Gameplay);

    }
}
