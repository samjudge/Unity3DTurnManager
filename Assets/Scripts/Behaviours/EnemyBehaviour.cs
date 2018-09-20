using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public OnTurnEventHandler TurnEventHandler;
	
	void Start () {
        TurnEventHandler.OnTurnStart.AddListener(delegate(TurnEventData data){
            Debug.Log("enemy turn start!");
            //GameManager.GetInstance().TurnManager.GetCurrentTurn().Signals.SendSignal(this.gameObject, (int) TurnSignals.Done);
        });
        TurnEventHandler.OnTurnUpdate.AddListener(delegate(TurnEventData data){
            Debug.Log("enemy turn update!");
            GameManager.GetInstance().TurnManager.GetCurrentTurn().Signals.SendSignal(this.gameObject, (int) TurnSignals.Done);
        });
        TurnEventHandler.OnTurnEnd.AddListener(delegate(TurnEventData data){
            Debug.Log("enemy turn end!");
            GameManager.GetInstance().TurnManager.GetCurrentTurn().Signals.SendSignal(this.gameObject, (int) TurnSignals.Done);
        });
	}

	void Update () {
		
	}
}
