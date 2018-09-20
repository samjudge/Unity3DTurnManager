using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    private List<Turn> Turns = new List<Turn>();
    private List<Turn> RanTurns = new List<Turn>();
    private int TurnIndex = 0;

    void Update() {
        Turn CurrentTurn = Turns[TurnIndex];
        if(CurrentTurn.CurrentStage == Turn.Stage.Start){
            if(CurrentTurn.AreAllTurnStageDoneSignalsSent()){
                CurrentTurn.CurrentStage = Turn.Stage.Update;
                CurrentTurn.Signals.ResetSignals();
            }
        } else if(CurrentTurn.CurrentStage == Turn.Stage.Update){
            if(CurrentTurn.AreAllTurnStageDoneSignalsSent()){
                CurrentTurn.CurrentStage = Turn.Stage.End;
                CurrentTurn.Signals.ResetSignals();
            }
        } else if(CurrentTurn.CurrentStage == Turn.Stage.End){
            if(CurrentTurn.AreAllTurnStageDoneSignalsSent()){
                CurrentTurn.CurrentStage = Turn.Stage.Start;
                CurrentTurn.Signals.ResetSignals();
                TurnIndex++;
                if(TurnIndex >= Turns.Count){
                    RanTurns.Clear();
                    TurnIndex = 0;
                }
            }
        }
    }

    //add a new turn to the manager
    public void AddTurn(Turn t){
        this.Turns.Add(t);
    }

    public Turn GetCurrentTurn(){
        return Turns[TurnIndex];
    }

    public List<Turn> GetTurnsOf(GameObject o){
        List<Turn> Turns = new List<Turn>();
        foreach(Turn t in Turns){
            if(t.Entities.Contains(o)){
                Turns.Add(t);
            }
        }
        return Turns;
    }
}
