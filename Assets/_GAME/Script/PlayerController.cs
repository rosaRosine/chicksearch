using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    private CharacterController controller;
    private Vector2 moveInput; //Eingabevektor für die horizontale Bewegung
    private Vector3 velocity; //Geschwindigkeitsvektor für die vertikale Bewegung
    public float rotateSpeed = 300f;

    //Doppelsprung
    private int jumpCount = 0;
    private int maxJumpCount = 1;

    private Transform camTransform;

    public int chickNumber = 0;
    public int maxChickNumber = 5;
    private HUDManager hudManager;
    private Animator animController;
    private bool gameWon = false;

    void Start()
    {
        animController = GetComponent<Animator>();

        controller = GetComponent<CharacterController>(); //Hole CharacterController-Komponente
        camTransform = Camera.main.transform; //Hole die Transform-Komponente der Hauptkamera

        maxChickNumber = GameObject.FindObjectsByType<Chick>(FindObjectsSortMode.None).Length;

        hudManager = GameObject.FindAnyObjectByType<HUDManager>();
        if(hudManager)
            hudManager.UpdateChickCount(chickNumber, maxChickNumber);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Move" + context.performed);
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump" + context.performed);

        /* if (context.performed && controller.isGrounded && jumpCount < maxJumpCount)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Setze die y-Komponente der Geschwindigkeit für den Sprung
            jumpCount += 1; //Erster Sprung
        } */

        if (context.performed && jumpCount < maxJumpCount)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Setze die y-Komponente der Geschwindigkeit für den Sprung
            jumpCount += 1; //Erster Sprung
        }
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        Debug.Log("Rotate" + context.performed);
        Vector2 rotateInput = context.ReadValue<Vector2>();
        float targetAngle = Mathf.Atan2(rotateInput.x, rotateInput.y) * Mathf.Rad2Deg;
        if (rotateInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //Kleine negative Zahl, um sicherzustellen, dass der Spieler auf dem Boden bleibt
            jumpCount = 0; //Setze den Sprungzähler zurück, wenn der Spieler den Boden berührt
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameWon) return;
        //transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0f, 0f);

        //MOVEMENT AUF EBENE

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y); //Bewegung in x-Richtung
        move = Quaternion.Euler(0f, camTransform.eulerAngles.y, 0f) * move; //Bewege relativ zur Kamerarotation
        //move = camTransform.right * moveInput.x + camTransform.forward * moveInput.y; //Alternative Methode, um relativ zur Kamera zu bewegen
        controller.Move(move * speed * Time.deltaTime); //Nutze CharacterController, um die Bewegung auszuführen

        //Vertikale BEWEGUNG / GRAVITY

        velocity.y += gravity * Time.deltaTime; //Schwerkraft auf die y-Komponente der Geschwindigkeit anwenden
        controller.Move(velocity * Time.deltaTime); //Bewegung basierend auf der Geschwindigkeit ausführen

        if(move != Vector3.zero)    //Vector3.zero = Vector3(0,0,0)
        transform.forward = move; //Kamera in Bewegungsrichtung ausrichten

        if (controller.isGrounded)
        {
            jumpCount = 0;
        }

        //Animation
        animController.SetBool("inAir", !controller.isGrounded);
        animController.SetFloat("speed", moveInput.magnitude);
    }

    public void CollectChick()
    {
        Debug.Log("Chick collected!");
        chickNumber += 1;

        if(hudManager)
        hudManager.UpdateChickCount(chickNumber, maxChickNumber);

        if (chickNumber >= maxChickNumber)
        {
            Debug.Log("All chicks collected! You win!");
            if(hudManager)                            //Alternative schreibweise: if(hudManager != null)
            hudManager.ShowWinPanel();

            gameWon = true;
        }
    }

    public void SetMaxJump()
    {
        maxJumpCount = 2;
    }

}
