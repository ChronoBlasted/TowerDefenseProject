using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GAMESTATE GameState = GAMESTATE.START;

}
public enum GAMESTATE
{
    START,
    GAME,
    END
}