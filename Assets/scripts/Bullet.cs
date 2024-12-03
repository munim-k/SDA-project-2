using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private int speed = 10;
   [SerializeField] private int damage = 15;
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      // Move the bullet forward
      transform.Translate(Vector3.forward * Time.deltaTime * speed);      //move bullet forward with every frame

      // Destroy the bullet after 3 seconds
      Destroy(gameObject, 3);  //destroy after 3 seconds
   }

   private void OnTriggerEnter(Collider collision) //if bullet collides with enemy, take damage and destroy bullet
   {
      if (collision.gameObject.CompareTag("Enemy")) //compare tag to enemy
      {
         collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage); //take damage function in enemy call
      }

      // Destroy the bullet
      Destroy(gameObject); //destroy the bullet
   }
}
