using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class RelativePosition : MonoBehaviour
{
    [SerializeField] GameObject enemy1;

    //[SerializeField] GameObject enemy2;
    [SerializeField] List<float>      relativePositions = new List<float>();
    [SerializeField] List<GameObject> enemiesInFocus    = new List<GameObject>();
    int                               minValIndex;
    [SerializeField] int              maxEnemiesToFocus;
    [SerializeField] Collider2D       col;
    bool                              propagationNotStarted = true;
    [SerializeField] int                               enemiesInCollider;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInFocus.Add(other.gameObject);
        }
    }

    IEnumerator StartPropagation()
    {
        yield return new WaitForSeconds(0.5f); // délai le temps que la liste soit à jour avec tous les ennemis présents dans le collider.
        DistanceBetweenEnemies();
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartPropagation());
        enemiesInFocus.Remove(enemy1);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void DistanceBetweenEnemies()
    {
        Debug.Log("Distance between enemies");
        foreach (GameObject inFocus in enemiesInFocus)
        {
            relativePositions.Add(Vector3.Distance(inFocus.transform.position, enemy1.transform.position));
        }
        LookForSmallestDistance();
    }
    
    void LookForSmallestDistance()
    {
        float minVal      = relativePositions.Min();
        minValIndex = relativePositions.IndexOf(minVal);
        ChangeColor();
    }
    void ChangeColor()
    {
        enemiesInFocus[minValIndex].GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    
}
