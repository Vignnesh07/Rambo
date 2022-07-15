using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private float cooldownTimer = Mathf.Infinity;

    private Animator animator;
    private PlayerMovement playerMovement;

    [SerializeField] private GameObject[] shots;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackCooldown;

    // Called when instance of script is loaded
    private void Awake() {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack() {
        animator.SetTrigger("shoot");
        cooldownTimer = 0;

        // Object pooling - to improve performance by reducing instantiation 
        // This allows for the object to be deactivated and activated later  
        shots[FindShot()].transform.position = firePoint.position;
        shots[FindShot()].GetComponent<ShotEffect>().SetDirection(Mathf.Sign(transform.localScale.x));
    } 

    private int FindShot(){
        for (int i = 0; i < shots.Length; i++){
            if (!shots[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
}
