using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoInput : MonoBehaviour
{
	InputHandlerMoto input;   //The name of the brake button

	//We hide these in the inspector because we want 
	//them public but we don't want people trying to change them
	[HideInInspector] public float thruster;            //The current thruster value
	[HideInInspector] public float rudder;              //The current rudder value
	[HideInInspector] public bool isBraking;            //The current brake value
	[HideInInspector] public bool isJumping;

	private void Awake()
	{
		input = GetComponent<InputHandlerMoto>();
	}
	void Update()
	{
		input.TickInput(Time.deltaTime);
		isJumping = input.jump_Input;
		isBraking = input.brak_Input;
		//Get the values of the thruster, rudder, and brake from the input class
		thruster = input.vertical;
		rudder = input.horizontal;

	}
}
