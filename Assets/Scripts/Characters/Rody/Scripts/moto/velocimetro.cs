using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class velocimetro : MonoBehaviour
{
    public Rigidbody target;

    public float maxSpeed = 0.0f; // The maximum speed of the target ** IN KM/H **

    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
    public Text speedLabel; // The label that displays the speed;
    public RectTransform arrow; // The arrow in the speedometer

    private float speed = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = target.velocity.magnitude * 3.6f;

        if (speedLabel != null)
            speedLabel.text = ((int)speed) + " km/h";
        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
    }
}
