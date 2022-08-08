using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum SpawnType
    {
        BlueSpawn,
        RedSpawn,
        GreenSpawn,
        YellowSpawn
    }

    public SpawnType spawnType;

    private bool isOccupied;

    // Script References
    GameBoard gameBoard;

    #region Properties
    public bool IsOccupied 
    {
        get { return isOccupied; } 
        set { isOccupied = value; }
    }

    #endregion

    private void Start()
    {
        gameBoard = GameObject.FindObjectOfType<GameBoard>();

        OnStartUp();
    }

    private void OnStartUp() 
    {
        if (this.gameObject != null)
        {
            gameBoard.SpawnPointsList.Add(this);
        }
    }
}
