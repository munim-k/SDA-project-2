using UnityEngine;

public class EnemyController : MonoBehaviour
{

   GameObject player;
   [SerializeField] private int health = 30;
   [SerializeField] private int damage = 10;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player");
   }

   // Update is called once per frame
   void Update()
   {
      // Move towards player
      transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.01f);

      transform.transform.LookAt(player.transform);

      // Check if the enemy is close to the player
      if (Vector3.Distance(transform.position, player.transform.position) < 1.0f)
      {
         // Attack the player
         //player.GetComponent<PlayerController>().TakeDamage(damage);
      }

   }

   public void TakeDamage(int damage)
   {
      health -= damage;
      if (health <= 0)
      {
         Die();
      }
   }

   private void Die()
   {
      Destroy(gameObject);
   }
}
