using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardManager : MonoBehaviour
{
    GameBoard gameBoard;

    private void Start()
    {
        gameBoard = GameObject.FindObjectOfType<GameBoard>();
    }
}
