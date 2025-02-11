using UnityEngine;

public class Rotor : MonoBehaviour
{
    public bool counterclockwise = false;
    
    public bool animationActivated;

    void Update()
    {
        if (animationActivated) 
            transform.Rotate(0, 0, 2000 * Time.deltaTime * (counterclockwise ? -1 : 1));
    }
}