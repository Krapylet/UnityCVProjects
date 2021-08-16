using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // Adds a highlight around the object
    void AddHighlight();

    // removes the higlight from the object
    void RemoveHighlight();

    // Carries out the objects task
    void Interact();
}
