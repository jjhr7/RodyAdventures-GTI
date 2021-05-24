using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MotoMovement : MonoBehaviour
{

	Rigidbody rigidBody;                    //A reference to the ship's rigidbody
	PlayerInput input;                      //A reference to the player's input	
	float m_deadZone = 0.1f;

	public float m_fordwardAcl = 100f;
	public float m_BackAcl = 25f;
	public float jump_power = 5f;
	public float brakingVelFactor = .95f;

	float m_currentThrust = 0f;

	[SerializeField] float m_speed = 0f;

	public float turnS = 10f;
	float currentTurn = 0.0f;

	int m_layerMask;

	public float m_hoverForce = 9f;
	public float m_hoverHeight = 2f;
	public GameObject[] m_hoverPoints;

	public float velocityImpact = 30;

	float timerJump;
	public float maxTimeJump = 1f;
	public bool jump;
	public bool braking;

	public Text time;
	float timer;
	void Start()
	{
		//Get references to the Rigidbody and PlayerInput components
		rigidBody = GetComponent<Rigidbody>();
		input = GetComponent<PlayerInput>();

		m_layerMask = 1 << LayerMask.NameToLayer("Characters");
		m_layerMask = ~m_layerMask;
	}

	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		m_speed = rigidBody.velocity.magnitude;
		timerJump += Time.deltaTime;
		jump = input.isJumping;
		braking = input.isBraking;
		m_currentThrust = 0.0f;
		float aclAxis = input.thruster;
		if (aclAxis > m_deadZone)
		{
			m_currentThrust = aclAxis * m_fordwardAcl;
		}
		else if (aclAxis < -m_deadZone)
		{
			m_currentThrust = aclAxis * m_BackAcl;
		}

		currentTurn = 0.0f;
		float turnAxis = input.rudder;
		if (Mathf.Abs(turnAxis) > m_deadZone)
		{
			currentTurn = turnAxis;
		}
		if (timerJump > maxTimeJump)
		{
			if (jump)
			{
				rigidBody.AddForce(transform.up * jump_power);
				timerJump = 0;
			}
		}

	}

	private void FixedUpdate()
	{


		if (time != null)
		{
			float minutes = Mathf.FloorToInt(timer / 60);
			float seconds = Mathf.FloorToInt(timer % 60);

			time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}
		RaycastHit hit;
		for (int i = 0; i < m_hoverPoints.Length; i++)
		{
			var hover_point = m_hoverPoints[i];
			if (Physics.Raycast(hover_point.transform.position, -Vector3.up, out hit, m_hoverHeight, m_layerMask))
				rigidBody.AddForceAtPosition(Vector3.up * m_hoverForce * (1.5f - (hit.distance / m_hoverHeight)), hover_point.transform.position);
			else
			{
				if (transform.position.y > hover_point.transform.position.y)
				{
					rigidBody.AddForceAtPosition(hover_point.transform.up * m_hoverForce, hover_point.transform.position);
				}
				else
				{
					rigidBody.AddForceAtPosition(transform.up * -m_hoverForce, hover_point.transform.position);

				}
			}
		}


		if (braking)
		{
			Debug.Log("brak");
			rigidBody.velocity *= brakingVelFactor;
		}


		if (Mathf.Abs(m_currentThrust) > 0)
		{
			rigidBody.AddForce(transform.forward * m_currentThrust);
		}

		if (currentTurn > 0)
		{
			rigidBody.AddRelativeTorque(Vector3.up * currentTurn * turnS);
		}
		else if (currentTurn < 0)
		{
			rigidBody.AddRelativeTorque(Vector3.up * currentTurn * turnS);
		}

	}

	private void OnCollisionEnter(Collision collision)
	{
		for (int i = 0; i < m_hoverPoints.Length; i++)
		{
			var hover_point = m_hoverPoints[i];
			rigidBody.AddForceAtPosition(hover_point.transform.up * jump_power / 16, hover_point.transform.position);
		}

		if (collision.gameObject.tag.Equals("ObstaculoMoto"))
		{
			if (m_speed > velocityImpact)
			{
				UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
			}
		}
	}
}
