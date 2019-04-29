using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class LoadMap : MonoBehaviour
{
    public TextAsset tiledAsset;
    public Sprite[] sprites;

    
    void Start()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(tiledAsset.text);

        XmlNode rootNode = xmlDoc.SelectSingleNode("map");

        //map - width, height, tilewidth, tileheight
        int mapWidth = int.Parse(rootNode.Attributes["width"].Value);
        int mapHeight = int.Parse(rootNode.Attributes["height"].Value);
        int tileWidth = int.Parse(rootNode.Attributes["tilewidth"].Value);
        int tileHeight = int.Parse(rootNode.Attributes["tileheight"].Value);

        //tileset - source (image)        
        string imageName = rootNode.SelectSingleNode("tileset").SelectSingleNode("image").Attributes["source"].Value;
        Debug.Log("image name: " + imageName);
        string[] nameSplit = imageName.Split('/');
        sprites = Resources.LoadAll<Sprite>(nameSplit[nameSplit.Length - 1].Split('.')[0]);
        
        XmlNodeList allLayers = rootNode.SelectNodes("layer");
        XmlNodeList objectLayers = rootNode.SelectNodes("objectgroup");

        //grab all the special tiles
        XmlNodeList allSpecialTiles;
        Dictionary<int, XmlNode> specialTilesDict;
        allSpecialTiles = rootNode.SelectNodes("tileset/tile");
        Debug.Log("allspecial tiles count: " + allSpecialTiles.Count);
        //fill dict
        specialTilesDict = new Dictionary<int, XmlNode>();
        foreach(XmlNode specTileNode in allSpecialTiles)
        {
            specialTilesDict.Add(int.Parse(specTileNode.Attributes["id"].Value), specTileNode);
        }


        //debug stuff
        Debug.Log(allLayers.Count);
        Debug.Log(objectLayers.Count);

        //todo: render both layers,  with gameobjects as containers for all the tiles
        //map 
        //-> layer1 
        //  -> tile, tile ,tile
        //-> layer2
        //  -> tile, tile ,tile
        //etc

        GameObject mapGO = new GameObject();
        mapGO.name = "MAP";
        int layerCount = 1;
        foreach (XmlNode layer in allLayers)
        {
            float xpos = 0;
            float ypos = 0;
            int posCounter = 0;
            float pixelScale = tileHeight / sprites[0].pixelsPerUnit;

            GameObject layerGO = new GameObject();
            layerGO.name = layer.Attributes["name"].Value;
            layerGO.transform.parent = mapGO.transform;

            if (int.Parse(layer.Attributes["id"].Value) == layerCount)
            {
                for (int i = 0; i < mapWidth; i++)
                {
                    xpos = 0;
                    for (int j = 0; j < mapHeight; j++)
                    {
                        GameObject go = new GameObject();
                        go.name = "Tile from layer " + layer.Attributes["name"].Value;
                        go.transform.parent = layerGO.transform;
                        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                        sr.transform.position = new Vector3(xpos, ypos, 0);
                        sr.sortingOrder = layerCount;

                        int firstSprite = int.Parse(layer.SelectSingleNode("data").InnerText.Split(',')[posCounter]);
                        if (firstSprite != 0)
                        {
                            sr.sprite = sprites[firstSprite - 1];
                        }

                        AddStuff(go, layer.Attributes["name"].Value);

                        posCounter++;
                        xpos += pixelScale;
                    }//end for mapheight
                    ypos -= pixelScale;
                }//end for mapwidth
                layerCount++;
            }
        }

        int objLayerCount = allLayers.Count + 1;
        foreach (XmlNode objGroup in objectLayers)
        {
            GameObject objectGO = new GameObject();
            objectGO.name = objGroup.Attributes["name"].Value;
            objectGO.transform.parent = mapGO.transform;

            XmlNodeList actuallyObjects = objGroup.SelectNodes("object");

            foreach (XmlNode objectTile in actuallyObjects)
            {
                if (objectTile.Attributes["gid"] == null)
                {
                    Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH. its unique.");
                }
                else
                {
                    GameObject go = new GameObject();
                    go.name = objectTile.Attributes["id"].Value;
                    go.transform.parent = objectGO.transform;
                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    sr.transform.position = new Vector3(float.Parse(objectTile.Attributes["x"].Value) / tileWidth, -float.Parse(objectTile.Attributes["y"].Value) / tileHeight);
                    sr.sortingOrder = objLayerCount;
                    
                    int currentSprite = int.Parse(objectTile.Attributes["gid"].Value);
                    go.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite - 1];

                    //check if this is a special tile
                    if (specialTilesDict.ContainsKey(currentSprite))
                    {
                        Debug.Log("This is special: " + go.name);
                    }
                    //check if this is a door
                        


                }
            }//closes object layer loop
        }


        #region example
        //EXAMPLE
        ////get data 
        //int firstSprite = int.Parse(rootNode.SelectSingleNode("layer/data").InnerText.Split(',')[0]);
        //Debug.Log(firstSprite);

        ////display sprite
        //GameObject spriteObj = new GameObject();
        //SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();

        ////subtracting offset (inthis case 1)
        //sr.sprite = sprites[firstSprite - 1];

        //secondEXAMPLE
        //first row
        //float xpos = 0;
        //float ypos = 0;
        //int posCounter = 0;
        //float pixelScale = tileHeight / sprites[0].pixelsPerUnit;
        //for (int i = 0; i < mapWidth; i++)
        //{
        //    xpos = 0;
        //    for (int j = 0; j < mapHeight; j++)
        //    {
        //        GameObject go = new GameObject();
        //        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        //        sr.transform.position = new Vector3(xpos, ypos, 0);

        //        int firstSprite = int.Parse(rootNode.SelectSingleNode("layer/data").InnerText.Split(',')[posCounter]);
        //        sr.sprite = sprites[firstSprite-1];
        //        posCounter++;
        //        xpos += pixelScale;
        //    }
        //    ypos -= pixelScale;
        //}
        #endregion
    }
    
    void AddStuff(GameObject sprite, string layername)
    {
        if (layername == "Walls")
        {
            BoxCollider2D boxCollider = sprite.AddComponent<BoxCollider2D>();
        }
    }


}
//homework - rewrite the map parsing script to be more effiecent
//add comments!!! (especially for the loops)
//for special objects, creat an enum(with switch statements) for types(door(component/class), 
                      //pickup -> add new object layer, destructable(this shoued be a class))
//parse the special object type, add componenets based on type
