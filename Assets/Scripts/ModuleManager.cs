using System;
using System.Collections;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    public bool propagationBool = true;
    

    public void Propagation()
    {
      if (propagationBool == true)
          {
              Debug.Log("propagationBool = false");
              Debug.Log("prend l'enemies frappé par l'epper et le met dans la variable enemyTargeted");
              Debug.Log("créer un while qui dur aussi longtemp que les stat du scriptable object du module propagation");
              Debug.Log("Appelle la fonction propagation Effect");
              Debug.Log("créé hit box autour de enemy targeted");
              Debug.Log("récupere tout les ennemies dans la hitbox");
              Debug.Log("prend le premiere ennemie stock le dans la variable closestEnemy ");
              Debug.Log("prend le deuxiemme enemy et compare si il est plus proche de enemy targeted que cosestEnemy n'est");
              Debug.Log("remplace l'enemy dans la variable si oui");
              Debug.Log("continue a faire ça pour chaque enemies dans la hit box");
              Debug.Log("puis met closestEnemy dans enemyTargeted");
              Debug.Log("une fois la boucle finis propagationbool = true");
              
          }  
    }


    public void PropagationEffect()
    {
        Debug.Log("Met x degat par frame a l'ennemie et set la vitesse de l'enemie a 0.5");  
    }


    
    
    public void TirEnergie()
    {
        Debug.Log("tire energie");
    }
}
