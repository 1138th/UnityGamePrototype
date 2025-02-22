using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Character target;
    private Camera camera1;

    private void Start()
    {
        camera1 = Camera.main;
    }

    private void LateUpdate()
    {
        if (!camera1) return;
        if (!target)
        {
            target = GameManager.Instance.CharacterFactory.ActiveCharacters
                .Find(character => character.Type == CharacterType.Player);
        }

        camera1.transform.position = Vector3.Lerp(
            camera1.transform.position,
            target.transform.position + new Vector3(1.81f, 28, -19.21f),
            4.5f * Time.deltaTime);
    }
}