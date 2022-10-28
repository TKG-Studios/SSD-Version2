using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(EnemyInitialization))]
    public class EnemyIntializationEditor : Editor
{

   
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnemyInitialization script = (EnemyInitialization)target;

        if (GUILayout.Button("Initialize New Enemy"))
        {
            

            //Create new GFX Object
            GameObject GFX = new GameObject();
            GFX.transform.parent = script.transform;
            GFX.transform.localPosition = new Vector3(0,0,0);
            GFX.name = "GFX";
            Animator anim= GFX.AddComponent<Animator>();
            GFX.AddComponent<EnemyAnimator>();
            SpriteRenderer GFXSprite = GFX.AddComponent<SpriteRenderer>();
            GFXSprite.sprite = script.enemySprite;
         

            script.AddComponent<EnemyAIExtension>();
            script.AddComponent<HealthSystemExtension>();
        

            //Create new Shadow Object
            GameObject Shadow = new GameObject();
          
            Shadow.name = "Shadow";
            SpriteRenderer ShadowSprite = Shadow.AddComponent<SpriteRenderer>();
            ShadowSprite.sprite = script.shadowSprite;
            float BottomY = GFXSprite.bounds.size.y / -2;
            Shadow.transform.parent = script.transform;
            Shadow.transform.localPosition = new Vector3(0, BottomY, 0);

            //Create new Xp Spawn Location Object
            GameObject XpSpawnTransform = new GameObject();
            XpSpawnTransform.transform.parent = script.transform;
            XpSpawnTransform.transform.localPosition = new Vector3(0, 0, 0);
            XpSpawnTransform.name = "XPSpawnTransform";

            BoxCollider2D box = script.GetComponent<BoxCollider2D>();
            float boxColliderX = GFXSprite.bounds.size.x;
            float boxColliderY = GFXSprite.bounds.size.y;
            box.size = new Vector2(boxColliderX, boxColliderY);


            DestroyImmediate(script.GetComponent<EnemyInitialization>());
        }
    }

}
