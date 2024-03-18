using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporToScence : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(Random.Range(2, 5));
        }
    }
}
