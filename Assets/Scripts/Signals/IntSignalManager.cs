using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IntSignalManager {
    private Dictionary<GameObject, List<int>> Signals = new Dictionary<GameObject, List<int>>();

    public void SendSignal(GameObject From, int Signal){
        if(!this.Signals.ContainsKey(From)){
            this.Signals.Add(From, new List<int>());
        }
        this.Signals[From].Add(Signal);
    }

    public List<int> GetSignals(GameObject Owner){
        if(!this.Signals.ContainsKey(Owner)){
            this.Signals.Add(Owner, new List<int>());
        }
        return this.Signals[Owner];
    }

    public void ResetSignals(){
        List<GameObject> Keys = new List<GameObject>(this.Signals.Keys);
        foreach(GameObject o in Keys){
            this.Signals[o] = new List<int>();
        }
    }
}
