using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject Player;
    public GameObject Enemy;
    public GameObject EnemyB;
    public TurnManager TurnManager;

    private static GameManager GameManagerInstance;

    public static GameManager GetInstance(){
        return GameManagerInstance;
    }

    void Awake(){
        GameManagerInstance = this;
        List<GameObject> PlayerTurnEntities = new List<GameObject>();
        PlayerTurnEntities.Add(Player);
        List<GameObject> EnemyTurnEntities = new List<GameObject>();
        EnemyTurnEntities.Add(Enemy);
        EnemyTurnEntities.Add(EnemyB);
        Turn PlayerTurn = new Turn(PlayerTurnEntities, (int) TurnSignals.Done);
        Turn EnemyTurn = new Turn(EnemyTurnEntities, (int) TurnSignals.Done);
        TurnManager.AddTurn(PlayerTurn);
        TurnManager.AddTurn(EnemyTurn);
    }

}
