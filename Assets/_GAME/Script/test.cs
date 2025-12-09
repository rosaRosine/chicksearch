using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int number = 10;
    bool isGrounded = true;
    bool jumpButtonPressed = false;
    int AddNumbers(int a, int b)
    {
        return a + b;
    }
    void Start()
    {
        /*
        number = AddNumbers(5, 15);
        Debug.Log("the sum is: " + number);
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded && jumpButtonPressed)
        {
            Debug.Log("Jump!");
        }
        else
        {
            Debug.Log("Not Jumping.");
        }
    }
}
