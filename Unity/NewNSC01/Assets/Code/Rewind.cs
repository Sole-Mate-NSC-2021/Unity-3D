using UnityEngine;
using Chronos;

public class Rewind : MonoBehaviour
{        
    public float localTimeScale = 1;
    void Update()
    {

        // Get the Enemies global clock
        Clock clock = Timekeeper.instance.Clock("Player");

        // Change its time scale on key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            localTimeScale = -1; // Rewind
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            localTimeScale = 0; // Pause
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            localTimeScale = 0.5f; // Slow
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            localTimeScale = 1; // Normal
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            localTimeScale = 2; // Accelerate
        }
        clock.localTimeScale = localTimeScale;
    }
}