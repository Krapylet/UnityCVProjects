using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float speedSprint;
    public float interactionDistance;

    private bool canMove;
    private bool canSprint;
    private Rigidbody rBody;
    private Animator animC;
    private GameObject lastHighlightedObj;
    [SerializeField] private TIHandler TICanvas;

    private void Start()
    {
        //setup initial values
        canMove = true;
        rBody = gameObject.GetComponent<Rigidbody>();
        animC = gameObject.GetComponentInChildren<Animator>();
        lastHighlightedObj = null;
    }

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftShift)) {
                MovementHandler(speedSprint); 
            } else {
                MovementHandler(speed);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractionHandler();
            }
        }
    }

    //handles movement and movement input
    private void MovementHandler(float currentSpeed)
    {
        //calculate direction
        float horizontal = 0;
        float vertical = 0;

        //move left or right
        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
            //rotate player to the right
            //rBody.MoveRotation(Quaternion.Euler(new Vector3(0, -90, 0)));
            animC.SetBool("IsLeft", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
            //rotate player to the left
            //rBody.MoveRotation(Quaternion.Euler(new Vector3(0, 90, 0)));
            animC.SetBool("IsLeft", false);
        }

        //move up or down
        if (Input.GetKey(KeyCode.S)) {
            vertical = -1;
            animC.SetBool("BackTurned", false);
        }
        else if (Input.GetKey(KeyCode.W)) {
            vertical = 1;
            animC.SetBool("BackTurned", true);
        }

        if ((vertical != 0) || (horizontal != 0)) {
            animC.SetBool("IsWalking", true); 
        } else {
            animC.SetBool("IsWalking", false);
            animC.SetBool("BackTurned", false);
        }

        // Apply movement
        rBody.velocity = new Vector3(horizontal, 0, vertical).normalized * currentSpeed + new Vector3(0, rBody.velocity.y, 0);
    }

    //Hightlights incteractable objects in reach of the player, and also handles interaction input from the player
    private void InteractionHandler()
    {
        //Stop any lingering playermovement when interaction starts.
        rBody.velocity = new Vector3(0, rBody.velocity.y, 0);
        animC.SetBool("IsWalking", false);

        Vector3 direction;

        //point the ray in the direction the player is facing
        if (animC.GetBool("IsLeft")) { 

            direction = -transform.forward;
        }
        else
        {
            direction = transform.forward;
        }
        
        //find the closest object in the reach of the player
        RaycastHit hit;
        GameObject reachedObj = null;
        Ray ray = new Ray(transform.position, direction);

        //draw the interaction ray
        Debug.DrawRay(transform.position, direction * interactionDistance, Color.red, 1f, false);
        
        //check if object is in reach
        if (Physics.Raycast(ray, out hit) && hit.distance <= interactionDistance)
        {
            reachedObj = hit.transform.gameObject;

            //Check if object is interactable
            IInteractable interactionScript = null;
            GetInterface<IInteractable>(reachedObj, out interactionScript);
            if (interactionScript != null)
            {
                //avoid edgecase where no previous objects has been highlighted
                if (lastHighlightedObj == null) { lastHighlightedObj = reachedObj; }

                //highlight object in reach
                interactionScript.AddHighlight();

                //interact with object in front of player
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionScript.Interact();
                }
            }
        }

        //remove highlight from the previously highlighted object, if was higlightable in the first place
        if (reachedObj != lastHighlightedObj && lastHighlightedObj != null)
        {
            IInteractable interactionScript;
            GetInterface<IInteractable>(lastHighlightedObj, out interactionScript);
            if(interactionScript != null)
            {
                interactionScript.RemoveHighlight();
            }
        }

        lastHighlightedObj = reachedObj;
    }

    //Activates the TIcanvas
    public void InspectTicket(Passenger p)
    {
        TICanvas.gameObject.SetActive(true);
        TICanvas.GetComponent<TIHandler>().LoadPassenger(p);
    }

    //Disables playermovement and interaction
    public void DisableMovement()
    {
        canMove = false;
    }

    //Enables playermovement and interaction
    public void EnableMovement()
    {
        canMove = true;
    }

    // Checks if a any script attached to the gameobject is a subtype of T, and returns the first one found.
    // This method should probably be moved to a library. It really lowers the cohesion of this class
    public static void GetInterface<T>(GameObject objectToSearch, out T result) where T : class
    {
        result = null;

        MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is T)
            {
                //found one
                result = (T)((System.Object)mb);
            }
        }
    }
}
