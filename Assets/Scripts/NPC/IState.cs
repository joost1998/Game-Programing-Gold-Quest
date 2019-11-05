/*
 *	Used Bron To Create This State Machine: https://www.youtube.com/watch?v=D6hAftj3AgM
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
	void Enter();
	void Execute();
	void Exit();
}
