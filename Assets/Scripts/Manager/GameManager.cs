using BaseTemplate.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GAMESTATE
{
    START,
    GAME,
    END
}

public class GameManager : MonoSingleton<GameManager>
{
    public GAMESTATE gameState;

    public  event Action<GAMESTATE> OnGameStateChanged;



    private void Awake()
    {
        gameState = GAMESTATE.START;
    }

    public void StartGame()
    {
        gameState = GAMESTATE.GAME;
    }

    public void EndGame()
    {
        gameState = GAMESTATE.END;
    }

    public void UpdateGameState(GAMESTATE newState)
    {
        gameState = newState;

        Debug.Log("New GameState : " + gameState);

        switch (gameState)
        {
            case GAMESTATE.START:
                break;
            case GAMESTATE.GAME:
                StartGame();
                break;
            case GAMESTATE.END:
                EndGame();
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(gameState);
    }

    public void UpdateStateToStart() => UpdateGameState(GAMESTATE.START);
    public void UpdateStateToGame() => UpdateGameState(GAMESTATE.GAME);
    public void UpdateStateToEnd() => UpdateGameState(GAMESTATE.END);

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
