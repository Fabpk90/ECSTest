using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuadTreeData<T>
{
    public QuadTreeData<T>[] children;
    public List<Tuple<Vector2,T>> data;
    public Vector2 position;
    public float size;
    private int maxElement;
    
    
    public QuadTreeData(Vector2 position, float size, int maxElement)
    {
        children = null;
        data = new List<Tuple<Vector2,T>>();
        this.position = position;
        this.size = size;
        this.maxElement = maxElement;
    }

    public bool AddElementAt(Tuple<Vector2,T> dataToAdd)
    {
        if (position.x + size / 2 > dataToAdd.Item1.x
            && position.x - size / 2 < dataToAdd.Item1.x
            && position.y + size / 2 > dataToAdd.Item1.y
            && position.y - size / 2 < dataToAdd.Item1.y)
        {
            if (children == null)
            {
                
                data.Add(dataToAdd);
                if (data.Count > maxElement)
                {
                    SubDivide();
                }
                else
                {
                    Debug.Log("Adding element ");
                }

                return true;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (children[i].AddElementAt(dataToAdd))
                        return true;
                }
                
                Debug.Log("The point at " + dataToAdd.Item1 + " was not found in children");
            }
        }
        
        Debug.Log("The point at " + dataToAdd.Item1 + " was not found in " + size + " position" + position);
        
        return false;
    }

    /// <summary>
    /// Used when the maxelement in a cell is reached
    /// subdividing the cell in 4 and passing the data
    /// </summary>
    private void SubDivide()
    {
        Debug.Log("Subdividing");
        children = new QuadTreeData<T>[4];
        

        float newSize = size * 0.5f;

        //clockwise setting, starting from top left        
        var positionChild1 = position;
        positionChild1.x -= newSize / 2;
        positionChild1.y += newSize / 2;

        children[0] = new QuadTreeData<T>(positionChild1, newSize, maxElement);
        
        var positionChild2 = position;
        positionChild2.x += newSize / 2;
        positionChild2.y += newSize / 2;

        children[1] = new QuadTreeData<T>(positionChild2, newSize, maxElement);
        
        var positionChild3 = position;
        positionChild3.x += newSize / 2;
        positionChild3.y -= newSize / 2;

        children[2] = new QuadTreeData<T>(positionChild3, newSize, maxElement);
        
        var positionChild4 = position;
        positionChild4.x -= newSize / 2;
        positionChild4.y -= newSize / 2;
        
        children[3] = new QuadTreeData<T>(positionChild4, newSize, maxElement);
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < data.Count; j++)
            {
                children[i].AddElementAt(data[j]);
            }
        }
    }

    public QuadTreeData<T> SearchPoint(Vector2 point)
    {
        if (position.x + size / 2 > point.x
            && position.x - size / 2 < point.x
            && position.y + size / 2 > point.y
            && position.y - size / 2 < point.y)
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    var tmp = children[i].SearchPoint(point);

                    if (tmp != null)
                        return tmp;
                }
            }
            else
            {
                return this;
            }
        }
       
        return null;
    }
}