using UnityEngine;

public class WeaponCOntroller : MonoBehaviour
{
    [Header("Weapon Stats")]
        public GameObject prefab; 
        public float damage;
        public float speed;
        public float cooldown;
        private float _currentCooldown;
        public int pierce;

        protected PlayerMovement pm;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        _currentCooldown = cooldown; //cooldown before starting to shoot
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _currentCooldown -= Time.deltaTime;
        if (_currentCooldown <= 0f)
        {
            Attack();
        }
        
    }

    protected virtual void Attack()
    {
        _currentCooldown = cooldown;
    }
}
