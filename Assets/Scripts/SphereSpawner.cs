using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    // Public fields for setting in the Unity Inspector
    public GameObject objectToSpawn;  // Object to spawn
    public float sphereRadius = 10f, timeThreshold;  // Radius of the invisible sphere
    public int spawnAmount = 10;      // Number of objects to spawn
    public float yPosition = 5f;      // Fixed Y position for spawning

    private Vector3[] spawnPositions; // To store calculated spawn positions
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the array to store spawn positions
        spawnPositions = new Vector3[spawnAmount];
    }

    private void Update()
    {
        time += Time.deltaTime;

        // Call the spawn method when the game starts
        SpawnObjectsOnSphere();
    }

    // Method to spawn objects at random points on the sphere
    void SpawnObjectsOnSphere()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            // Get a random point on the circumference of a circle in the XZ plane
            Vector3 spawnPosition = GetRandomPointOnSphereBorder();

            // Store the position in the array for visualization
            spawnPositions[i] = spawnPosition;

            if (time > timeThreshold)
            {
                // Instantiate the object at the calculated spawn position
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
                time = 0;
            }
        }
    }

    // Method to calculate a random point on the sphere's border, restricted to Y-axis
    Vector3 GetRandomPointOnSphereBorder()
    {
        // Get random angle in radians for XZ plane
        float angle = Random.Range(0f, Mathf.PI * 2);

        // Calculate the X and Z positions based on the angle and radius
        float x = Mathf.Cos(angle) * sphereRadius;
        float z = Mathf.Sin(angle) * sphereRadius;

        // Return the calculated position, but keep the Y position fixed
        return new Vector3(x, yPosition, z);
    }

    // Gizmo to visualize the sphere and the spawn points in the Scene view
    private void OnDrawGizmos()
    {
        // Set the color for the sphere
        Gizmos.color = Color.green;
        // Draw the wireframe sphere to visualize the boundary
        Gizmos.DrawWireSphere(new Vector3(0, yPosition, 0), sphereRadius);

        // If spawn positions have been calculated, visualize them
        if (spawnPositions != null)
        {
            // Set color for spawn points
            Gizmos.color = Color.red;

            // Loop through each spawn position and draw a small sphere
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                // Draw spheres at each spawn position
                Gizmos.DrawSphere(spawnPositions[i], 0.3f);
            }
        }
    }
}
