using System;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] public float rich = 10f;
    [SerializeField] private float maxRich = 100f;
    [SerializeField] private float minRich = 0f;
    [SerializeField] private int currentMoney = 0;

    [SerializeField] private MoveByFinger moveByFinger;
    [SerializeField] private PathFollower pathFollower;
    [SerializeField] private PlayerRichBar playerRichBar;
    [SerializeField] private GameObject model;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Models models;

    void Start()
    {
        GameManager.instance.OnGameStart.AddListener(OnGameStart);
        GameManager.instance.OnGameOver.AddListener(OnGameOver);
        GameManager.instance.OnGameWin.AddListener(OnGameWin);

        pathFollower.pathCreator = FindFirstObjectByType<PathCreator>();
        var distanceTravelled = pathFollower.pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        pathFollower.distanceTravelled = distanceTravelled;

        var modelToSet = models.GetModelByRich(rich);
        if (modelToSet != null)
        {
            model.SetActive(false);
            modelToSet.model.SetActive(true);
            model = modelToSet.model;
            playerRichBar.SetRichBar(rich / maxRich, modelToSet.name, modelToSet.color);
            animator.Play("IDLE");
        }

        currentMoney = PlayerPrefs.GetInt("Money", 0);
    }

    void OnGameWin()
    {
        pathFollower.speed = 0;
        animator.CrossFade("IDLE", 0.1f);
    }

    void OnGameOver()
    {
        playerCollider.enabled = false;
        pathFollower.speed = 0;
        animator.CrossFade("IDLE", 0.1f);
    }

    void OnGameStart()
    {
        pathFollower.speed = speed;
        var modelToSet = models.GetModelByRich(rich);
        animator.CrossFade(modelToSet.animationName, 0.1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IPickbleObject pickableObject))
        {
            pickableObject.PickUp();

            rich += pickableObject.Rich;
            if (rich > maxRich)
            {
                rich = maxRich;
            }

            if (rich <= minRich)
            {
                GameManager.instance.GameOver();
                return;
            }
            
            SetModelByRich();
            
            int currentMoney = PlayerPrefs.GetInt("Money", 0);
            currentMoney += pickableObject.Money;
            PlayerPrefs.SetInt("Money", currentMoney);
            UI.instance.moneyUI.moneyText.text = currentMoney.ToString();
        }

        if(other.TryGetComponent(out Gate gate))
        {
            gate.EnterGate();
            int currentMoney = PlayerPrefs.GetInt("Money", 0);
            currentMoney += gate.moneyToAdd;
            PlayerPrefs.SetInt("Money", currentMoney);
            UI.instance.moneyUI.moneyText.text = currentMoney.ToString();
        }

        if(other.TryGetComponent(out Finish finish))
        {
            GameManager.instance.WinGame();
        }

        if(other.TryGetComponent(out CheckPoint checkPoint))
        {
            checkPoint.SetCheckPoint();
        }        
    }

    void SetModelByRich()
    {
        var modelToSet = models.GetModelByRich(rich);
        if (modelToSet != null)
        {
            model.SetActive(false);
            modelToSet.model.SetActive(true);
            model = modelToSet.model;
            playerRichBar.SetRichBar(rich / maxRich, modelToSet.name, modelToSet.color);
            animator.CrossFade(modelToSet.animationName, 0.1f);
        }
    }
}

[Serializable]
public class Models
{
    public List<Model> models = new();

    public Model GetModelByRich(float rich)
    {
        foreach (var model in models)
        {
            if (rich >= model.rich)
            {
                return model;
            }
        }

        return null;
    }
}

[Serializable]
public class Model
{
    public string name;
    public string animationName;
    public Color color;
    [Range(0, 100)] public float rich = 0.1f;
    public GameObject model;
}