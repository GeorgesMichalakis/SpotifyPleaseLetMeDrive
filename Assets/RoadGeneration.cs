using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    public GameObject roadPrefab;
    [SerializeField] private int segmentsCount = 50; // total number of segments
    [SerializeField] private float segmentLength = 5f; // match prefab length
    [SerializeField] private float spawnInterval = 0.5f; // seconds between spawns

    private Vector3 position;
    private Vector3 forward = Vector3.forward; // direction to spawn segments
    private float currentRotationY = 0f;
    private float timer;

    void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        // Countdown until we spawn the next segment
        timer -= Time.deltaTime;

        if (timer <= 0 && segmentsCount > 0)
        {
            Instantiate(roadPrefab, position, Quaternion.identity);
            // Decide if we turn
            int turnDecision = Random.Range(0, 3); // 0=straight, 1=left, 2=right
            if (turnDecision == 1) currentRotationY -= 15f;
            else if (turnDecision == 2) currentRotationY += 15f;

            // Update forward vector
            forward = Quaternion.Euler(0, currentRotationY, 0) * Vector3.forward;
            // Move forward for the next segment
            position += forward * segmentLength;

            segmentsCount--;
            timer = spawnInterval; // reset timer
        }
    }
}
