using UnityEngine;
// using static UnityEngine.Rendering.DynamicArray<T>;

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

    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius;
    [SerializeField] private LayerMask _interactableMask;
    private readonly Collider[] _colliders = new Collider[1];
    [SerializeField] private bool hasItem = false;
    [SerializeField] private Item robotItem;

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
        }if(Input.GetKey(KeyDown) ){
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

    private void InteractWith()
    {
        Debug.Log("Interacted");

        int _numFound;
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound == 1)
        {
            var interactableWorkStation = _colliders[0].GetComponent<WorkStation>();
            var interactableChest = _colliders[0].GetComponent<ComponentChest>();
            var interactableBaby = _colliders[0].GetComponent<BabySystem>();


            if (interactableChest != null && !hasItem)
            {
                robotItem = interactableChest.GiveComponent();
                hasItem = true;
            }
            else if (interactableWorkStation != null)
            {
                if (hasItem)
                {
                    if (robotItem != null)
                    {
                        robotItem = interactableWorkStation.AddNewComponent(robotItem);
                        if (robotItem == null)
                        {
                            hasItem = false;
                        }
                    }
                    Debug.Log("The robotItem is NULL!!!!!");
                }
                else
                {
                    interactableWorkStation.ClearWorkSpace();
                }
            }
            else if (interactableBaby != null && hasItem)
            {
                if (robotItem != null)
                {
                    robotItem = interactableBaby.DeliverItem(robotItem);
                    if (robotItem == null)
                    {
                        hasItem = false;
                    }
                }
                Debug.Log("The robotItem is NULL!!!!!");
            }
        }
    }

    private void DropItem()
    {
        if (hasItem)
        {
            hasItem = false;
            robotItem = null;
        }
        Debug.Log("Item Dropped");
    }
}
