using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuadTree : MonoBehaviour
{
    public int elementByCell;
    public int size;

    public int spriteToSpawn;

    public SpriteRenderer prefab;
    public List<SpriteRenderer> sprites;

    private QuadTreeData<int> _quadTreeData;
    private QuadTreeData<int> selected;

    private void Start()
    {
        _quadTreeData = new QuadTreeData<int>(transform.position, size, elementByCell);
        GenerateSprite();
    }

    void GenerateSprite()
    {
        for (int i = 0; i < spriteToSpawn; i++)
        {
            Vector2 position = Random.insideUnitCircle * 5;

            SpriteRenderer sp = Instantiate(prefab, position, Quaternion.identity);
            
            sprites.Add(sp);
            _quadTreeData.AddElementAt(new Tuple<Vector2,int>(position, i));
        }
    }

    public void ActivateSprites(Vector2 point)
    {
        var tree = _quadTreeData.SearchPoint(point);
        selected = tree;
        
        if (tree != null)
        {
            print("yes");
            print("the point " + point);
            print("position " + tree.position + "  size " + tree.size);
            foreach (var sprite in tree.data)
            {
                sprites[sprite.Item2].color = Color.green;
            }
        }
        else
        {
            print("fuck");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
            ActivateSprites(pos);

            Instantiate(prefab, pos, Quaternion.identity).GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    private void OnDrawGizmos()
    {
        if(_quadTreeData != null)
            DrawChildren(_quadTreeData);
    }

    private void DrawChildren(QuadTreeData<int> quadTreeData)
    {
        if (quadTreeData.children != null)
        {
            for (int i = 0; i < 4; i++)
            {
                DrawChildren(quadTreeData.children[i]);
            }
        }
        else
        {
            //Gizmos.color = Random.ColorHSV();
            Gizmos.DrawWireCube(quadTreeData.position, Vector3.one * quadTreeData.size);
        }
    }
}