using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    private bool predicting;

    public delegate void OnPhysicsTick(bool predicting);
    public static event OnPhysicsTick OnPhysicsTickEvent;

    public delegate void OnReset();
    public static event OnReset OnResetEvent;

    void Start()
    {
        UIManager.OnSimulateEvent += Simulate;
        UIManager.OnAngleChangeEvent += OnAngleChange;
        Saber.OnSwingEndEvent += OnSwingEnd;
        Saber.OnCollisionEvent += OnCollision;

        
    }

    void OnAngleChange(float value, string tag)
    {
        //after angles changed start predicting
        Simulate(true);
    }

    private void OnDestroy()
    {
        UIManager.OnSimulateEvent -= Simulate;
        Saber.OnSwingEndEvent -= OnSwingEnd;
        Saber.OnCollisionEvent -= OnCollision;
    }

    void OnCollision(Vector3 point, bool prediction)
    {
        StopAllCoroutines();
    }

    void OnSwingEnd(bool prediction)
    {
        StopAllCoroutines();
    }

    void Simulate(bool predict)
    {
        predicting = predict;

        OnResetEvent?.Invoke();

        StopAllCoroutines();
        StartCoroutine(UpdatePhysicsJob());
    }

    IEnumerator UpdatePhysicsJob()
    {
        while (true)
        {
            OnPhysicsTickEvent?.Invoke(predicting);
            yield return new WaitForFixedUpdate();
        }
    }


}
