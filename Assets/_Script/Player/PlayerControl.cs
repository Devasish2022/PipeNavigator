using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxYClamp = 4f;

    private Rigidbody2D rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if(rigidBody == null)
        {
            Debug.LogWarning("Rigidbody not found");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void Update()
    {
        Vector2 position = transform.position;
        position.y = Mathf.Clamp(position.y, -maxYClamp, maxYClamp);
        transform.position = position;
    }
}
