using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class MapLoader2 : MonoBehaviour
{
    public TextAsset tiledAsset;
    public Sprite[] sprites;
    public GameObject enemyObj;
    public int mapWidth, mapHeight, tileWidth, tileHeight;

    void Start()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(tiledAsset.text);

        //get root node
        XmlNode rootNode = xmlDoc.SelectSingleNode("map");

        //get map width, height, tilewidth, and tileheight
        mapWidth = int.Parse(rootNode.Attributes["width"].Value);
        mapHeight = int.Parse(rootNode.Attributes["height"].Value);
        tileWidth = int.Parse(rootNode.Attributes["tilewidth"].Value);
        tileHeight = int.Parse(rootNode.Attributes["tileheight"].Value);

        //set source image
        GetSourceAndLoadSprites(rootNode);

        //get the layers
        XmlNodeList allLayers = rootNode.SelectNodes("layer");
        XmlNodeList objectLayers = rootNode.SelectNodes("objectgroup");

        //grab all the special tiles
        Dictionary<int, XmlNode> specialTilesDict;
        specialTilesDict = GrabSpecialTiles(rootNode);

        //make the game objects
        LoadGameObjects(allLayers, objectLayers, specialTilesDict);
    }

    void GetSourceAndLoadSprites(XmlNode rootNode)
    {
        string imageName = rootNode.SelectSingleNode("tileset").SelectSingleNode("image").Attributes["source"].Value;
        string[] nameSplit = imageName.Split('/');
        sprites = Resources.LoadAll<Sprite>(nameSplit[nameSplit.Length - 1].Split('.')[0]);
    }//end GetSourceAndLoadSprites

    public Dictionary<int, XmlNode> GrabSpecialTiles(XmlNode rootNode)
    {
        XmlNodeList allSpecialTiles;
        Dictionary<int, XmlNode> specialTilesDict;
        allSpecialTiles = rootNode.SelectNodes("tileset/tile");

        //Debug.Log("allspecial tiles count: " + allSpecialTiles.Count);

        //fill dict
        specialTilesDict = new Dictionary<int, XmlNode>();
        foreach (XmlNode specTileNode in allSpecialTiles)
        {
            specialTilesDict.Add(int.Parse(specTileNode.Attributes["id"].Value), specTileNode);
        }
        return specialTilesDict;
    }//end GrabSpecialTiles

    /// <summary>
    /// this loads the game objects in the xml file
    /// </summary>
    /// <param name="allLayers">the list of nodes for each individual layer</param>
    /// <param name="objectLayers">list of nodes for the object layers</param>
    /// <param name="specialTilesDict">dictionary for id to object nodes</param>
    void LoadGameObjects(XmlNodeList allLayers, XmlNodeList objectLayers, Dictionary<int, XmlNode> specialTilesDict)
    {
        //make base object
        GameObject mapGO = new GameObject();
        mapGO.name = "MAP";

        //load regular tiles
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
                        //check if the sprite is 0
                        //go from layer that we have to data layer's inner text, split that, then see if its a 0
                        int spriteNum = int.Parse(layer.SelectSingleNode("data").InnerText.Split(',')[posCounter]);
                        if (spriteNum == 0)
                        {
                            posCounter++;
                            xpos += pixelScale;
                            continue;
                        }

                        GameObject go = new GameObject();
                        go.name = "Tile from layer " + layer.Attributes["name"].Value;
                        go.transform.parent = layerGO.transform;
                        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                        sr.transform.position = new Vector3(xpos, ypos, 0);
                        sr.sortingOrder = layerCount;

                        sr.sprite = sprites[spriteNum - 1];

                        AddStuff(go, layer.Attributes["name"].Value);

                        posCounter++;
                        xpos += pixelScale;
                    }//end for mapheight
                    ypos -= pixelScale;
                }//end for mapwidth
                layerCount++;
            }
        }//end foreach layer in all layers

        //now load the objects
        int objLayerCount = allLayers.Count + 1;
        foreach (XmlNode objGroup in objectLayers)
        {
            //Debug.Log("doing a layer group`````````````````````````````````");

            GameObject objectGO = new GameObject();
            objectGO.name = objGroup.Attributes["name"].Value;
            objectGO.transform.parent = mapGO.transform;

            XmlNodeList actuallyObjects = objGroup.SelectNodes("object");

            foreach (XmlNode objectTile in actuallyObjects)
            {
                if (objectTile.Attributes["gid"] == null)
                {
                    //Debug.Log("its unique.");
                    //check the name of the unique obj
                    if (objectTile.SelectSingleNode("properties").SelectSingleNode("property").Attributes["name"].Value == "PlayerSpawn")
                    {
                        //find the char and move it here
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        Vector2 playerPos = new Vector2(float.Parse(objectTile.Attributes["x"].Value) / tileWidth, -float.Parse(objectTile.Attributes["y"].Value) / tileHeight);
                        player.transform.position = playerPos;
                    }


                }
                else
                {
                    GameObject go = new GameObject();
                    go.name = objectTile.Attributes["id"].Value;
                    go.transform.parent = objectGO.transform;

                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    sr.transform.position = new Vector3(float.Parse(objectTile.Attributes["x"].Value) / tileWidth, -float.Parse(objectTile.Attributes["y"].Value) / tileHeight);
                    sr.sortingOrder = objLayerCount;

                    int currentSprite = int.Parse(objectTile.Attributes["gid"].Value) - 1;
                    go.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];

                    //check if this is a special tile
                    if (specialTilesDict.ContainsKey(currentSprite))
                    {
                        //Debug.Log("This is special: " + go.name);

                        //check what type of tile
                        //Debug.Log(specialTilesDict[currentSprite].Attributes["id"].Value);
                        XmlNodeList propertyList = specialTilesDict[currentSprite].SelectSingleNode("properties").SelectNodes("property");
                        //Debug.Log(propertyList[propertyList.Count - 1].Attributes["value"].Value);
                        ObjectPropertyType tileType = (ObjectPropertyType)Enum.Parse(typeof(ObjectPropertyType), propertyList[propertyList.Count - 1].Attributes["value"].Value);
                        switch (tileType)
                        {
                            case ObjectPropertyType.Door:
                                go.AddComponent<Door>().Initialize();
                                break;
                            case ObjectPropertyType.Pickup:
                                go.AddComponent<Pickup>().Initialize();
                                break;
                            case ObjectPropertyType.Enemy:

                                float xPos = float.Parse(objectTile.Attributes["x"].Value);
                                float yPos = float.Parse(objectTile.Attributes["y"].Value);
                                go = Instantiate(enemyObj, go.transform);
                                enemyObj.transform.position = new Vector3(xPos, yPos);


                                go.AddComponent<Enemy>().Initialize();
                                break;
                            default:
                                Debug.Log("what do i do with this special tile");
                                break;
                        }
                    }
                    //check if this is a door
                }
            }//closes object layer loop
        }//end foreach object in the object layer
    }//end LoadGameObjects

    void AddStuff(GameObject sprite, string layername)
    {
        if (layername == "Walls")
        {
            BoxCollider2D boxCollider = sprite.AddComponent<BoxCollider2D>();
            sprite.layer = 10;
        }
    }//end add stuff

}//end mapLoder2.0

