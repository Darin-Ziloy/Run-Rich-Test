using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelNumber = 1;
    public PathCreator pathCreator;
    public RoadMeshCreator roadMeshCreator;

    void Start()
    {
        roadMeshCreator.TriggerUpdate();
    }
}