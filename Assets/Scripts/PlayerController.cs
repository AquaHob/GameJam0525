using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeedf = 1.0f;
    public int moveX = 0;
    public int moveZ = 0;

    [Header("Player Inputs")]
    [SerializeField] private KeyCode KeyUp;
    [SerializeField] private KeyCode KeyLeft;
    [SerializeField] private KeyCode KeyDown;
    [SerializeField] private KeyCode KeyRight;
    [SerializeField] private KeyCode KeyInteract;
    [SerializeField] private KeyCode KeyDrop;

    private bool itemHeld = false;

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
    }

    private void CheckPlayerInput(){
        moveX = 0;
        moveZ = 0;

        if(Input.GetKey(KeyUp)){
            moveZ++;
        }if(Input.GetKey(KeyLeft)){
            moveX--;
        }if(Input.GetKey(KeyDown)){
            moveZ--;
        }if(Input.GetKey(KeyRight)){
            moveX++;
        }if(Input.GetKeyDown(KeyInteract)){
            InteractWith();
        }if(Input.GetKeyDown(KeyDrop)){
            DropItem();
        }

        MoveCharacter(moveX, moveZ);
    }

    private void MoveCharacter(int moveX, int moveZ){
        transform.position = new Vector3(transform.position.x + (moveX*moveSpeedf*Time.deltaTime), 0, transform.position.z + (moveZ*moveSpeedf*Time.deltaTime));
    }

    private void InteractWith(){
        Debug.Log("Interacted");

        //Chest
        //Wenn noch kein Component in der Hand, aufnehmen 

        //WorkBench
        // Item abgeben

        //Baby
        // Item abgeben
    }

    private void DropItem(){
        if(itemHeld) itemHeld = false;
        Debug.Log("Item Dropped");
    }
}
