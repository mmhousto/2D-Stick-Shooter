using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Extensions;
using NavMeshPlus.Components;

public class MyButton : MonoBehaviour, IInteractable
{
    public bool Interactable { get; set; }
    public GameObject door;
    public NavMeshSurface surface;

    private void Start()
    {
        Interactable = true;
    }

    private IEnumerator BuildNavMesh()
    {
        yield return null;
        AsyncOperation operation = surface.BuildNavMeshAsync();
        while (!operation.isDone)
        {
            yield return operation;
        }
        
    }

    public void Interact()
    {
        if (Interactable)
        {
            Interactable = false;
            Destroy(door);
            StartCoroutine(BuildNavMesh());
        }
    }
}
