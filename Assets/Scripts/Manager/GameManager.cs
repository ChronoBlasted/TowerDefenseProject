using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GAMESTATE GameState;

    private void Awake()
    {
        GameState = GAMESTATE.START;
    }

    public void StartGame()
    {
        GameState = GAMESTATE.GAME;
    }

    public void EndGame()
    {
        GameState = GAMESTATE.END;
    }

}
public enum GAMESTATE
{
    START,
    GAME,
    END
}