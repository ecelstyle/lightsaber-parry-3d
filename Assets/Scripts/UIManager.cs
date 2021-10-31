using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Slider sliderPlayer;
    public Slider sliderOpponent;
    public Text textCollideMessage;
    public Button buttonSimulate;
    public ParticleSystem particle;

    public delegate void OnAngleChangeDelegate(float angle, string tag);
    public static event OnAngleChangeDelegate OnAngleChangeEvent;

    public delegate void OnSimulateDelegate(bool predict);
    public static event OnSimulateDelegate OnSimulateEvent;

    void Start()
    {
        if (sliderPlayer!=null)
        {
            sliderPlayer.minValue = 0f;
            sliderPlayer.maxValue = 90f;
            sliderPlayer?.onValueChanged.AddListener(delegate { SetPlayerAngle(); });
        }
        if (sliderOpponent!= null)
        {
            sliderOpponent.minValue = 0f;
            sliderOpponent.maxValue = 90f;
            sliderOpponent?.onValueChanged.AddListener(delegate { SetOpponentAngle(); });
        }
        if (sliderOpponent != null)
        {
            buttonSimulate?.onClick.AddListener(delegate { Simulate(); });
        }
        if (textCollideMessage != null)
        {
            textCollideMessage.enabled = false;
            textCollideMessage.text = Parameters.collideMessage;
        }

        Saber.OnCollisionEvent += ShowCollideMessage;

    }

    void OnDestroy()
    {
        Saber.OnCollisionEvent -= ShowCollideMessage;
    }

    void ShowCollideMessage(Vector3 point, bool prediction)
    {
        if (prediction)
        {
            SetMessageVisible(true);
        } else
        {
            if (particle!=null)
            {
                particle.transform.position = point;
                particle.Play();
            }
        }
    }

    void SetMessageVisible(bool value)
    {
        if (textCollideMessage != null)
        {
            textCollideMessage.enabled = value;
        }
    }

    public void SetPlayerAngle()
    {
        SetMessageVisible(false);

        if (sliderPlayer != null)
            OnAngleChangeEvent?.Invoke(sliderPlayer.value, Parameters.playerTag);
    }

    public void SetOpponentAngle()
    {
        SetMessageVisible(false);

        if (sliderOpponent != null)
            OnAngleChangeEvent?.Invoke(sliderOpponent.value, Parameters.opponentTag);
    }

    public void Simulate()
    {
        OnSimulateEvent?.Invoke(false);
    }


}
