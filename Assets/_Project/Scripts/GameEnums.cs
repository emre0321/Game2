using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums{}

public enum InitiliazeType { None, OnAwake, OnStart }
public enum GameStates { None, MainMenu, Gameplay, LevelSuccess, LevelFail}

public enum ControllerTypes { None, GameController, LevelController}

public enum SoundIDs { None, NoteSound}

public enum PlatformType { None, Default, Finish}

public enum PlatformMovementType { None, Left, Right}
