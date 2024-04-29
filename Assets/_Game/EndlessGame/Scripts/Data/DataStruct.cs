using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VANH.EndlessGame
{
    public enum GameTag
    {
        Player, Block, DeadZone
    }

    public enum GameLayer
    {
        Dead, Player, Block
    }

    public enum CharacterAnim
    {
        Idle, Jump, Land, Dead
    }

    public enum GamePref
    {
        BestScore, LevelPrefix, CurPlayerId, IsMusicOn, IsSoundOn
    }

    public enum GameScene
    {
        MainMenu, GamePlay
    }

    public enum MoveDirection
    {
        Left, Right
    }

    public enum GameState
    {
        Starting, Playing, Gameover
    }
}
