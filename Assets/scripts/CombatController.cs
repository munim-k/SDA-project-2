using System;
using UnityEngine;
   
public class CombatController : MonoBehaviour
{
   private Animator characterAnimator;

   [SerializeField]
   private GameObject bullet;

   bool reloading = false;

   private bool startFiring = false;

   private bool leftMouseButtonPressed = false;

   [SerializeField] int magazineMaxCapacity = 20;
   [SerializeField] int magazineCurrentCapacity;

   [SerializeField] int reloadTime = 1; // seconds

   [SerializeField] private Transform crosshair;

   [SerializeField] private int CyclesPerBullet = 3; // More is slower firing rate
   private int CycleCount = 0;

   // Previous animations
   bool isAiming = false;


   private void Start()
   {
      magazineCurrentCapacity = magazineMaxCapacity;
      characterAnimator = GetComponent<Animator>();

      characterAnimator.SetFloat("reloadSpeed", ((float) 2 / reloadTime) * (float) 1.6 ); // Dynamically set animation speed
   }

   private void Update()
   {
      // Right Click
      if (Input.GetMouseButtonDown(1))  //right click to aim
      {
         characterAnimator.SetBool("isAiming", true);
      }
      else if (Input.GetMouseButtonUp(1))
      {
         characterAnimator.SetBool("isAiming", false);
      }

      // Left Click
      if (Input.GetMouseButtonDown(0))  //left click to shoot
      {
         leftMouseButtonPressed = true;
         if (!reloading) { 
            characterAnimator.SetBool("isFiring", true);
            isAiming = characterAnimator.GetBool("isAiming"); // Save previous animation state
            characterAnimator.SetBool("isAiming", false);
            startFiring = true;  //shooting bool true
         }
      }
      else if (Input.GetMouseButtonUp(0))
      {
         leftMouseButtonPressed = false;
         if (!reloading)
         {
            startFiring = false;
            characterAnimator.SetBool("isFiring", false);
            characterAnimator.SetBool("isAiming", isAiming);
         }
      }

      // R key
      if (Input.GetKeyDown(KeyCode.R) && magazineCurrentCapacity < magazineMaxCapacity)
      {
         Reload();
      }
   }

   private void FixedUpdate()
   {
      CycleCount++;
      if (startFiring && !reloading)  //if bool true and not reloading
      {
         if (CycleCount % CyclesPerBullet == 0)
         {
            Fire();  //fire the gun physically
         }
      }
      
   }

   private void Fire()
   {
      Instantiate(bullet, crosshair.position, crosshair.rotation);  //instantiate bullet at crosshair position (make a bullet)
      magazineCurrentCapacity--;
      if (magazineCurrentCapacity <= 0)
      {
         Reload();
      }
   }

   void Reload()
   {
      reloading = true;
      startFiring = false;
      characterAnimator.SetBool("isFiring", false);
      characterAnimator.SetBool("isAiming", false);
      characterAnimator.SetBool("isReloading", true);
      Invoke("ReloadInner", reloadTime);
   }

   private void ReloadInner()
   {
      magazineCurrentCapacity = magazineMaxCapacity;
      reloading = false;
      characterAnimator.SetBool("isReloading", false);

      if (leftMouseButtonPressed)
      {
         characterAnimator.SetBool("isFiring", true);
         startFiring = true;
      }
   }
}
