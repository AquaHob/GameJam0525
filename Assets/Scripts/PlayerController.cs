using UnityEngine;
// using static UnityEngine.Rendering.DynamicArray<T>;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeedf = 1.0f;
    public float moveX = 0.0f;
    public float moveZ = 0.0f;

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

    void Start()
    {
        moveX = 0.0f;
        moveZ = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
    }

    private void CheckPlayerInput(){
        //moveX = 0.0f;
        //moveZ = 0.0f;

        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        /*if(Input.GetKey(KeyUp)){
            //moveZ++;
            moveZ = Input.GetAxis("Vertical2");
        }if(Input.GetKey(KeyLeft)){
            //moveX--;
            moveX = Input.GetAxis("Horizontal2");
        }
        if(Input.GetKey(KeyDown) ){
            //moveZ--;
            moveZ = Input.GetAxis("Vertical2");
        }
        if(Input.GetKey(KeyRight)){
            // moveX++;
            moveX = Input.GetAxis("Horizontal2");
        }*/

        if (Input.GetKeyDown(KeyInteract)){
            InteractWith();
        }if(Input.GetKeyDown(KeyDrop)){
            DropItem();
        }

        MoveCharacter(moveX, moveZ);
    }

    private void MoveCharacter(float moveX, float moveZ){
        transform.position = new Vector3(transform.position.x + (moveX*moveSpeedf*Time.deltaTime), 1, transform.position.z + (moveZ*moveSpeedf*Time.deltaTime));
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
