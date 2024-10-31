using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] public Character target;

    void Update () {
        transform.position = target.transform.position + new Vector3(5, 20, -20);
    }
}
