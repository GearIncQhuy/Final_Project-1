using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class enum five phases for Object
public enum Phases
{
    Fire,
    Earth,
    Metal,
    Water,
    Wood
}

// class check nourishment and restraint of elemental
public class NourishmentRestraintFuction : MonoBehaviour
{
    // Array enum five phases
    private Phases[] arrPhases = (Phases[])Enum.GetValues(typeof(Phases));

    // location elemental of GameObject
    private int indexObj1;
    private int indexObj2;

    /** 
     * The function get index elemental
     * @param: name enum Phase 
     * @return: location elemental in array enum five phases
     */
    private int getIndexElemental(Phases name)
    {
        for (int i = 0; i < arrPhases.Length; i++)
        {
            if(name == arrPhases[i])
            {
                return i;
            }
        }
        return -1;
    }

    /**
     * The function compares the mutual nourishment of two elementals
     * @param: obj1 enum Phases GameObject 1
     * @param: obj2 enum Phases GameObject 2
     * @return: if(mutual nourishment) return true else return false
     */
    public bool checkMutualNourishment(Phases obj1, Phases obj2)
    {
        indexObj1 = getIndexElemental(obj1);
        indexObj2 = getIndexElemental(obj2);

        // check index null
        if (indexObj1 == -1 || indexObj2 == -1)
        {
            return false;
        }

        // check location index 
        if (arrPhases[indexObj1] == arrPhases[indexObj2])
        {
            return true;
        }

        // check location index
        if (indexObj1 >= 0 && indexObj1 < 4)
        {
            indexObj1 += 1;
        }
        else if (indexObj1 == 4)
        {
            indexObj1 = 0;
        }

        if (arrPhases[indexObj1] == arrPhases[indexObj2])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * The function compares the mutual restraint of two elementals
     * @param: obj1 enum Phases GameObject 1
     * @param: obj2 enum Phases GameObject 2
     * @return: if(mutual restraint) return true else return false
     */
    public bool checkMutualRestraint(Phases obj1, Phases obj2)
    {
        indexObj1 = getIndexElemental(obj1);
        indexObj2 = getIndexElemental(obj2);

        // check index null
        if (indexObj1 == -1 || indexObj2 == -1)
        {
            return false;
        }

        // check location index
        if (indexObj1 >= 0 && indexObj1 < 3)
        {
            indexObj1 += 2;
        }
        else if (indexObj1 == 3)
        {
            indexObj1 = 0;
        }else if(indexObj1 == 4)
        {
            indexObj1 = 1;
        }

        if (arrPhases[indexObj1] == arrPhases[indexObj2])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
