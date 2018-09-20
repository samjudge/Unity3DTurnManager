# Unity3DTurnManager
Example implementation of an event-driven turn manager and message system in Unity3d 👾🕹️🎲

Simple implementaion of an event-driven turn manager for gameobjects in unity3d.

The most basic implementaion of the interface can be seen in `Assets/Scripts/Game/GameManager.cs`


```csharp
  //these values injected via the inspector
  public GameObject Player;
  public GameObject Enemy;
  public GameObject EnemyB;
  public TurnManager TurnManager;
  
  //The game manager (and thusly, the turn manager) is to be accessed as a singleton in other parts of code
  private static GameManager GameManagerInstance;
  
  public static GameManager GetInstance(){
    return GameManagerInstance;
  }
  
  void Awake(){
    GameManagerInstance = this;
    //create the player turn, initalized with a list of game objects that need to give the OK/DONE signal to progress
    List<GameObject> PlayerTurnEntities = new List<GameObject>();
    PlayerTurnEntities.Add(Player);
    Turn PlayerTurn = new Turn(PlayerTurnEntities, (int) TurnSignals.Done);
    //create the enemy turn
    List<GameObject> EnemyTurnEntities = new List<GameObject>();
    EnemyTurnEntities.Add(Enemy);
    EnemyTurnEntities.Add(EnemyB);
    Turn EnemyTurn = new Turn(EnemyTurnEntities, (int) TurnSignals.Done);
    //add the turns to a turn manager instance
    TurnManager.AddTurn(PlayerTurn);
    TurnManager.AddTurn(EnemyTurn);
  }
```

In order to progress through each turn stage, each game object in the Turn must send a OK/DONE singal to the TurnManager (Check `Assets/Scripts/Enums` for the actual signal used).

Signals are sent to the TurnManager in the behaviour scripts, via the GameManager singleton reference

```csharp
//Where the TurnEventHandler is injected via the inspector, (or accessed via GetComponent<T>())
TurnEventHandler.OnTurnStart.AddListener(delegate(TurnEventData data){
  Debug.Log("player turn start!");
  GameManager.GetInstance().TurnManager.GetCurrentTurn().Signals.SendSignal(this.gameObject, (int) TurnSignals.Done);
});
```
