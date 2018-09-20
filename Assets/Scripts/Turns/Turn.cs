using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Turn {
    
    public enum Stage { Start, Update, End };

    public List<GameObject> Entities;
    public IntSignalManager Signals;
    public Stage CurrentStage;

    private int DoneSignal;

    public Turn(List<GameObject> Entities, int DoneSignal) {
        this.Entities = Entities;
        this.Signals = new IntSignalManager();
        this.CurrentStage = Stage.Start;
        this.DoneSignal = DoneSignal;
    }
    
    public bool AreAllTurnStageDoneSignalsSent(){
        bool allEntitiesReadyToProgress = true;
        foreach(GameObject e in Entities){
            List<int> sentSignals = Signals.GetSignals(e);
            if(!sentSignals.Contains(this.DoneSignal)){
                allEntitiesReadyToProgress = false;
                break;
            }
        }
        return allEntitiesReadyToProgress; //exit early
    }
}
