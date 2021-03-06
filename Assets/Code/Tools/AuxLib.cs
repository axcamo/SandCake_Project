﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum WorldModuleConnect { TOP = 0, MIDDLE = 1, BOTTOM = 2 }

[System.Serializable]
public enum WorldModuleType { VOID = 0, SIMPLE_JUMP = 1, SIMPLE_PAINT = 2, PAINT_JUMP = 3}

public static class AuxLib {

    public static float Map(float value, float minIn, float maxIn, float minOut, float maxOut)
    {
        return minOut + (value - minIn) * (maxOut - minOut) / (maxIn - minIn);
    }

    public static Vector3 SetPositionOnRaycastHit2D(Vector3 pos, string tg, Vector2 dir, float height)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, 100, 1 << 8);
        RaycastHit2D hit2 = Physics2D.Raycast(pos, dir, 100, 1 << 9);

        RaycastHit2D extraChecker = Physics2D.Raycast(pos + new Vector3(2, 0, 0), dir, 1 << 9);
        RaycastHit2D extraChecker2 = Physics2D.Raycast(pos - new Vector3(2, 0, 0), dir, 1 << 9);

        if (hit.collider != null && hit2.collider != null) 
        {
            if (extraChecker.collider != null && extraChecker2.collider != null)
            {
                if (hit.collider.tag == tg && hit2.collider.tag == tg)
                {
                    //Debug.Log("Found Terrain: " + hit.transform.name);
                    pos = new Vector3(pos.x, hit.point.y + height, pos.z);

                }
            }              

        }

       return pos;
    }
}

[System.Serializable]
public struct DualTexture
{
    public float speed;
    public Texture day;
    public Texture night;
    public ParallaxController.ParallaxLayerOrder order;
}

[System.Serializable]
public class WorldModuleData
{
    private uint ID;
    public WorldConstructor.Stage stage;
    public WorldModuleType type;
    public WorldModuleConnect beginConnection;
    public WorldModuleConnect endConnection;
    public GameObject module;

    public WorldModuleData(WorldModuleConnect b, WorldModuleConnect e, GameObject m)
    {
        beginConnection = b;
        endConnection = e;
        module = m;
    }

    public void SetID(int i)
    {
        ID = (uint)((int)stage * 100 + 100 + i);
        //Debug.Log(ID);
    }
    public uint GetID()
    {
        return ID;
    }
}

[System.Serializable]
public struct WorldDictionaryList
{
    public List<WorldModuleData> worldModules;
}
