using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
   [SerializeField] private BoundaryPointsProvider _boundaryPointsProvider;
   private Rigidbody2D _rigidbody2D;
   private float _fallTarget ;

   private void Awake()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
   }

   public void EnableGravity()
   {
      _fallTarget = Mathf.Clamp(transform.position.y, 
         _boundaryPointsProvider.BottomBoundary.position.y, 
         _boundaryPointsProvider.TopBoundary.position.y);
      
      _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
      _rigidbody2D.velocity = Vector2.zero;
   }

   private void Update()
   {
      if (_rigidbody2D.bodyType == RigidbodyType2D.Dynamic && transform.position.y <= _fallTarget)
      {
        DisableGravity();
      }
   }

   
   private void OnTriggerEnter2D(Collider2D col)
   {
      DisableGravity();
   }

   private void DisableGravity()
   {
      _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
      _rigidbody2D.velocity = Vector2.zero; 
   }
}
