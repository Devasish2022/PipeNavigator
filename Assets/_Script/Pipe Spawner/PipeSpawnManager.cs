using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PipeSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private Transform pipeContainer;
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private float pipeSpeed = 2f;
    [SerializeField] private float pipeXLimit = -14f;
    [SerializeField] private float maxPipeYLimit = 8f;
    [SerializeField] private float minPipeYLimit = 3f;
    [SerializeField] private int defaultPoolCapacity = 10;
    [SerializeField] private int maxPoolCapacity = 20;

    private List<GameObject> activePipes = new List<GameObject>();

    private ObjectPool<GameObject> pipePool;

    private void Awake()
    {
        pipePool = new ObjectPool<GameObject>
            (
                createFunc: CreatePipe,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,
                defaultCapacity: defaultPoolCapacity,
                maxSize: maxPoolCapacity
            );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GeneratePipes), 0, spawnInterval);
    }

    void Update()
    {
        UpdatePipeMovement();
    }

    private void GeneratePipes()
    {
        GameObject pipe = pipePool.Get();
        Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(minPipeYLimit, maxPipeYLimit));
        pipe.transform.position = spawnPosition;
        activePipes.Add(pipe);
    }

    private void UpdatePipeMovement()
    {
        for (int i = 0; i < activePipes.Count; i++)
        {
            GameObject currentPipe = activePipes[i];
            currentPipe.transform.position += Vector3.left * pipeSpeed * Time.deltaTime;

            if (currentPipe.transform.position.x < pipeXLimit)
            {
                activePipes.RemoveAt(i);
                pipePool.Release(currentPipe);
            }
        }
    }

    // Object Pooling Function

    private GameObject CreatePipe()
    {
        GameObject pipe = Instantiate(pipePrefab);
        pipe.name = "Pipe";
        pipe.SetActive(false);
        return pipe;
    }

    private void OnGet(GameObject pipe)
    {
        pipe.SetActive(true);
    }

    private void OnRelease(GameObject pipe)
    {
        pipe.SetActive(false);
    }

    private void OnDestroyItem(GameObject pipe)
    {
        Destroy(pipe);
    }
}
