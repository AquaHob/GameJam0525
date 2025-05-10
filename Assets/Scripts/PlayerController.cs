using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeedf = 1.0f;
    public int moveX = 0;
    public int moveY = 0;

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
        moveY = 0;

        if(Input.GetKey(KeyUp)){
            moveY++;
        }if(Input.GetKey(KeyLeft)){
            moveX--;
        }if(Input.GetKey(KeyDown)){
            moveY--;
        }if(Input.GetKey(KeyRight)){
            moveX++;
        }if(Input.GetKeyDown(KeyInteract)){
            InteractWith();
        }if(Input.GetKeyDown(KeyDrop)){
            DropItem();
        }

        MoveCharacter(moveX, moveY);
    }

    private void MoveCharacter(int moveX, int moveY){
        transform.position = new Vector3(transform.position.x + (moveX*moveSpeedf*Time.deltaTime), transform.position.y + (moveY*moveSpeedf*Time.deltaTime), 0);
    }

    private void InteractWith(){
        Debug.Log("Interacted");
    }

    private void DropItem(){
        if(itemHeld) itemHeld = false;
        Debug.Log("Item Dropped");
    }
}
