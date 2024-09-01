using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Extensions;
using NavMeshPlus.Components;

public class Button : MonoBehaviour, IInteractable
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
        yield return new WaitForSeconds(.5f);
        surface.BuildNavMeshAsync();
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
