using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : ControllerBaseModel
{
    [SerializeField] List<UIElementModel> UIElements;

    public override void Initialize()
    {
        ChangeScreen(GameStates.MainMenu);
    }

    public override void OnGameStateChange(GameStates gameState)
    {
        ChangeScreen(gameState);
    }

    public void ChangeScreen(GameStates gameState)
    {
        for (int i = 0; i < UIElements.Count; i++)
        {
            UIElements[i].SetDeactive();
        }

        switch (gameState)
        {
            case GameStates.MainMenu:
                GetUIElement<UIMainMenu>().SetActive();
                break;
            case GameStates.Gameplay:
                GetUIElement<UIGameplay>().SetActive();
                break;
            case GameStates.LevelSuccess:
                GetUIElement<UILevelSuccess>().SetActive();
                break;
            case GameStates.LevelFail:
                GetUIElement<UILevelFailed>().SetActive();
                break;
          
        }
    }


    public T GetUIElement<T>()
    {
        return (T)(object)UIElements.Find(x => x.GetType() == typeof(T));
    }



}
