using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    public LeverValue leverValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the last lever scene index
            int lastLeverSceneIndex = leverValue.lastLeverScence;

            // Generate a random scene index between 2 and 4, excluding the last lever scene
            int randomSceneIndex = Random.Range(2, 6);
            while (randomSceneIndex == lastLeverSceneIndex)
            {
                randomSceneIndex = Random.Range(2, 6);
            }

            // Load the randomly selected scene
            SceneManager.LoadScene(randomSceneIndex);

            leverValue.lastLeverScence = randomSceneIndex;
        }
    }
}
