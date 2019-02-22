 using UnityEngine;
 using System.Collections;
 
 public class PlayerSortingLayer : MonoBehaviour
 {
     public int sortingOrder = 0;
     private SpriteRenderer sprite;
 
     void Start()
     {
         sprite = GetComponent<SpriteRenderer>();
     }
 
     void Update()
     {
         if (sprite)
         sprite.sortingOrder = sortingOrder;
     }
 }