using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private float cooldownTimer = Mathf.Infinity;

    private Animator animator;
    private PlayerMovement playerMovement;

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
    }
}
