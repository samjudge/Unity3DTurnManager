using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class OnTurnEventHandler : MonoBehaviour{

    [SerializeField]
    public OnTurnEvent OnTurnStart;

    [SerializeField]
    public OnTurnEvent OnTurnUpdate;

    [SerializeField]
    public OnTurnEvent OnTurnEnd;

    private Turn LastProcessedTurn;
    private Turn.Stage LastProcessedTurnStage;

    private bool IsTurnCycleOver = true;
    private bool IsFirstCycle = true;

    void Awake(){ }

    void Update(){
        //check if the current turn is relevant to this entity
        if(IsTurnCycleOver){
            if(GameManager.GetInstance().TurnManager.GetCurrentTurn() == null) return;
            Turn CurrentTurn = GameManager.GetInstance().TurnManager.GetCurrentTurn();
            if(CurrentTurn.Entities.Contains(this.gameObject)){
                LastProcessedTurn = GameManager.GetInstance().TurnManager.GetCurrentTurn();
                if(LastProcessedTurn.CurrentStage != LastProcessedTurnStage || IsFirstCycle){
                    IsFirstCycle  = false;
                    LastProcessedTurnStage = LastProcessedTurn.CurrentStage;
                    OnTurnStart.Invoke(new TurnEventData());
                    IsTurnCycleOver = false;
                }
            }
            return;
        }
        //check if the turn stage has changed
        if(LastProcessedTurn.CurrentStage != LastProcessedTurnStage){
            switch (LastProcessedTurn.CurrentStage){
                case Turn.Stage.Update:
                    LastProcessedTurnStage = LastProcessedTurn.CurrentStage;
                    //invoke handlers
                    OnTurnUpdate.Invoke(new TurnEventData());
                    break;
                case Turn.Stage.End:
                    LastProcessedTurnStage = LastProcessedTurn.CurrentStage;
                    //invoke handlers
                    OnTurnEnd.Invoke(new TurnEventData());
                    IsTurnCycleOver = true;
                    break;

            }
        }
        
    }
}