/*
 *	Used Bron To Create This State Machine: https://www.youtube.com/watch?v=D6hAftj3AgM
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBehaviour
{
	private IState currentState, previousState;

	//Changes state to given state
	public void ChangeState(IState newState)
	{
		if(currentState != null)
		{
			this.currentState.Exit();
		}

		this.previousState = this.currentState;
		this.currentState = newState;
		this.currentState.Enter();
	}

	//Run the state like MonoBehaviour 
	public void RunMonoExecute()
	{
		var runningState = this.currentState;
		if (runningState != null)
		{
			runningState.Execute();
		}
	}

	//Switch to previous state 
	public void SwitchToPreviousState()
	{
		this.currentState.Exit();
		this.currentState = this.previousState;
		this.currentState.Execute();
	}

	//Returns current state
	public IState GetCurrentState()
	{
		return this.currentState;
	}
}
