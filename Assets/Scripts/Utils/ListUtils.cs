using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUtils  {

	public static List<Object> deleteObjectsAtEndOfList(List<Object> list)
    {

        for (var i = 0; i< list.Count; i++)
        {

            if (i > 30)
            {
                list.Remove(list[i]);
            }
          
        }

        return list;

    }


}
