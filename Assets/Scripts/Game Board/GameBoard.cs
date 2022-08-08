using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField]
    private GameObject spawnPrefab;
    private List<SpawnPoint> spawnPoints;

    [Header("Dots")]
    [SerializeField]
    private List<Dot> dots;

    [Header("Script Properties")]
    private SpawnPoint spawnPoint;

    [Header("Game Board Bounds")]
    [SerializeField]
    private Vector2 bounds;

    #region Properties

    public List<SpawnPoint> SpawnPointsList
    {
        get { return spawnPoints; }
        set { spawnPoints = value; }
    }

    #endregion
    private void Start()
    {
        // Setup list
        spawnPoints = new List<SpawnPoint>();
        dots = new List<Dot>();

        SetupGameBoard();
    }

    private void SetupGameBoard()
    {
        foreach (SpawnPoint spawn in spawnPoints)
        {
            foreach (Dot dot in dots)
            {
                if (!spawn.IsOccupied)
                {
                    if (spawn.spawnType.ToString().Contains(dot.name))
                    {
                        Dot newDot = Instantiate(dot, bounds, Quaternion.identity);
                        newDot.transform.position = spawn.transform.position;
                        newDot.transform.parent = this.transform;  
                    }
                }
            }
        }
    }
}
