using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    [Header("Car Prefabs")]
    public GameObject carPrefab1;
    public GameObject carPrefab2;
    public GameObject carPrefab3;

    [Header("Spawn Locations")]
    public GameObject topSpawnLocation;
    public GameObject bottomSpawnLocation;
    public GameObject leftSpawnLocation;
    public GameObject rightSpawnLocation;

    [Header("Speed Range")]
    public float minSpeed = 5f;
    public float maxSpeed = 15f;

    [Header("Spawn Interval Range")]
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    void Start()
    {
        // Start the coroutine to spawn cars at random intervals
        StartCoroutine(SpawnCarsAtRandomIntervals());
    }

    void Update()
    {
        // Check if R key is pressed to reload the scene
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null && keyboard.rKey.wasPressedThisFrame)
        {
            ReloadScene();
        }
    }

    /// <summary>
    /// Coroutine that spawns cars at random intervals within the defined range
    /// </summary>
    IEnumerator SpawnCarsAtRandomIntervals()
    {
        while (true)
        {
            // Calculate random spawn interval
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            
            // Wait for the calculated interval
            yield return new WaitForSeconds(spawnInterval);
            
            // Spawn a car
            SpawnCar();
        }
    }

    /// <summary>
    /// Spawns a car at a random spawn location with a random prefab
    /// </summary>

    public void SpawnCar()
    {
        // Randomly select a car prefab
        GameObject[] carPrefabs = { carPrefab1, carPrefab2, carPrefab3 };
        GameObject selectedCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // Randomly select a spawn location
        GameObject[] spawnLocations = { topSpawnLocation, bottomSpawnLocation, leftSpawnLocation, rightSpawnLocation };
        int spawnIndex = Random.Range(0, spawnLocations.Length);
        GameObject selectedSpawnLocation = spawnLocations[spawnIndex];

        if (selectedCarPrefab == null || selectedSpawnLocation == null)
        {
            Debug.LogWarning("Car prefab or spawn location is not assigned!");
            return;
        }

        // Determine movement direction based on spawn location
        Vector3 movementDirection = GetMovementDirection(spawnIndex);

        // Calculate rotation to face the movement direction
        Quaternion spawnRotation = Quaternion.LookRotation(movementDirection);

        // Randomly select speed from range
        float speed = Random.Range(minSpeed, maxSpeed);

        // Instantiate the car at the spawn location's position, facing the movement direction
        GameObject spawnedCar = Instantiate(selectedCarPrefab, selectedSpawnLocation.transform.position, spawnRotation);

        // Add movement component to the spawned car
        CarMovement carMovement = spawnedCar.GetComponent<CarMovement>();
        if (carMovement == null)
        {
            carMovement = spawnedCar.AddComponent<CarMovement>();
        }
        carMovement.Initialize(movementDirection, speed);
    }

    /// <summary>
    /// Gets the movement direction based on spawn location index
    /// 0 = Top (move in negative Z)
    /// 1 = Bottom (move in positive Z)
    /// 2 = Left (move in positive X)
    /// 3 = Right (move in negative X)
    /// </summary>
    private Vector3 GetMovementDirection(int spawnIndex)
    {
        switch (spawnIndex)
        {
            case 0: // Top - move in negative Z direction
                return Vector3.back;
            case 1: // Bottom - move in positive Z direction
                return Vector3.forward;
            case 2: // Left - move in positive X direction
                return Vector3.right;
            case 3: // Right - move in negative X direction
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }

    /// <summary>
    /// Reloads the current scene
    /// </summary>
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
