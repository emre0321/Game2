using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : ControllerBaseModel
{
    public static GameController Instance;
    public GameStates CurrentGameState;

    public List<ControllerBaseModel> Controllers;


    public override void Initialize()
    {
        Instance = this;
        CurrentGameState = GameStates.MainMenu;
    }

    public T GetController<T>()
    {
        return (T)(object)Controllers.Find(x => x.GetType() == typeof(T));

    }


    private void Update()
    {
        ControllersUpdate();
    }


    void ControllersUpdate()
    {
        for (int i = 0; i < Controllers.Count; i++)
        {
            Controllers[i].ControllerUpdate(CurrentGameState);
        }

    }

    public static void ChangeGameState(GameStates gameState)
    {
        Instance.CurrentGameState = gameState;

        for (int i = 0; i < Instance.Controllers.Count; i++)
        {
            Instance.Controllers[i].OnGameStateChange(Instance.CurrentGameState);
        }
    }


}
