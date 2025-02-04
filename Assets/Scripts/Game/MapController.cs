using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> planes;

    private Character player;

    private const float StepSize = 150;

    private void FixedUpdate()
    {
        if (player != null)
        {
            Physics.Raycast(player.transform.position, Vector3.down, out var hitCurrentPlane, 3);

            foreach (var plane in planes)
            {
                if (!hitCurrentPlane.collider.bounds.Intersects(plane.GetComponent<Collider>().bounds))
                {
                    MovePlane(plane, Vector3Int.RoundToInt((hitCurrentPlane.transform.position - plane.transform.position).normalized));
                }
            }
        }
        else player = GameManager.Instance.CharacterFactory.Player;
    }

    private void MovePlane(GameObject planeToMove, Vector3 direction)
    {
        planeToMove.transform.position += direction * StepSize;
    }
}