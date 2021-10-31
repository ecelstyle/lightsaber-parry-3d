using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saber : MonoBehaviour
{

    public GameObject visualComponent;

    public bool isPlayer = false;

    private float angle;
    private bool prediction;
    private Rigidbody rigidBody;
    private const float swingMaxAngle = -185;

    public delegate void OnCollision(Vector3 point, bool prediction);
    public static event OnCollision OnCollisionEvent;

    public delegate void OnSwingEnd(bool prediction);
    public static event OnSwingEnd OnSwingEndEvent;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        UIManager.OnAngleChangeEvent += OnAngleChange;
        SimulationManager.OnPhysicsTickEvent += OnPhysicsTick;
        SimulationManager.OnResetEvent += SetObjectsAngle;
    }

    void OnDestroy()
    {
        UIManager.OnAngleChangeEvent -= OnAngleChange;
        SimulationManager.OnPhysicsTickEvent -= OnPhysicsTick;
        SimulationManager.OnResetEvent -= SetObjectsAngle;
    }

    void Update()
    {
        if (!prediction)
        {
            if (visualComponent!=null) visualComponent.transform.rotation = transform.rotation;
        }
    }

    void OnPhysicsTick(bool predict)
    {
        prediction = predict;
        rigidBody.rotation = Quaternion.Euler(angle, Mathf.LerpAngle(rigidBody.rotation.eulerAngles.y,swingMaxAngle, Time.fixedDeltaTime * Parameters.simulationSpeed),0);

        if (Mathf.Round(transform.rotation.eulerAngles.y) == swingMaxAngle)
        {
            OnSwingEndEvent?.Invoke(prediction);
        }
 
    }

    void SetObjectsAngle()
    {
        transform.localRotation = Quaternion.Euler(angle, 0, 0);
        visualComponent.transform.rotation = transform.rotation;
    }

    void OnAngleChange(float value, string tag)
    {
        //check if message for this saber (player or opponent)
        if (!CompareTag(tag)) return;

        //Negative angle if this is our opponent's saber
        angle = value * (tag.Equals("Player") ? 1 : -1);
        SetObjectsAngle();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        //Only player fires event when it collides with opponent's saber
        if (collision.collider.CompareTag("Opponent"))
        {
            OnCollisionEvent?.Invoke(collision.contacts[0].point, prediction);
        }
    }

}
