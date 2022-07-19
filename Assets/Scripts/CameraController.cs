using UnityEngine;

public class CameraController : MonoBehaviour {

    private float lookAhead;
    
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;

    private void Update() {
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        
        // Gradually changes value of lookAhead from inital to final
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * speed);
    }
}
