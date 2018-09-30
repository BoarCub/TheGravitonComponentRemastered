using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

//Generates Repeated Sprites From a Stetched Sprite
public class RepeatSprite : MonoBehaviour {
    SpriteRenderer sprite;

    public int NumberOfColumns = 1;
    public int NumberOfRows = 1;

	// Use this for initialization
	void Awake () {
        //Gets sprite unscaled
        sprite = GetComponent<SpriteRenderer>();
        Vector2 size = new Vector2(sprite.bounds.size.x / transform.localScale.x/ NumberOfColumns, sprite.bounds.size.y / transform.localScale.y / NumberOfRows);

        //Create child prefab
        GameObject childPrefab = new GameObject();
        SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
        childPrefab.transform.position = transform.position;
        childSprite.sprite = sprite.sprite;

        //Loop through tiles
        GameObject childObject;
        for (int i = 0, l = (int)Mathf.Round(sprite.bounds.size.x); i < l/NumberOfColumns; i++)
        {
            for (int j = 0, m = (int)Mathf.Round(sprite.bounds.size.y); j < m/NumberOfRows; j++)
            {
                childObject = Instantiate(childPrefab) as GameObject;
                childObject.transform.position = transform.position - (new Vector3(-size.x*i*NumberOfColumns, size.y*j*NumberOfRows, 0));
                childObject.transform.parent = transform;
            }
        }

        //Sets Parent
        childPrefab.transform.parent = transform;

        sprite.enabled = false;

	}

}
