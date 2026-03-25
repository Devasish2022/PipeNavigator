using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private AudioClip gameOverSound;

    private PlayerCollisionHandler playerCollisionHandler;
    private PlayerControl playerControl;
    private PipeSpawnManager pipeSpawnManager;

    private Rigidbody2D rigidBody2d;
    private Collider2D playerCollider2D;

    private const string PIPE_TAG = "pipe";

    private void Start()
    {
        playerCollisionHandler = FindAnyObjectByType<PlayerCollisionHandler>();
        playerControl = FindAnyObjectByType<PlayerControl>();
        pipeSpawnManager = FindAnyObjectByType<PipeSpawnManager>();
        rigidBody2d = GetComponent<Rigidbody2D>();
        playerCollider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PIPE_TAG))
        {
            playerCollisionHandler.enabled = false;
            playerControl.enabled = false;
            pipeSpawnManager.enabled = false;

            rigidBody2d.linearVelocity = Vector2.zero;
            rigidBody2d.angularVelocity = 0f;
            rigidBody2d.gravityScale = 0f;

            playerCollider2D.enabled = false;

            AudioManager.Instance.PlayAndThenReload(gameOverSound);
        }
    }
}
