using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformdata
{
    public Vector3 position;
    public int scenenumber;

    public Transformdata(Transform transform, int number)
    {
        position= transform.position;
        scenenumber = number;
    }
}
[Serializable]
public class TransformData
{
    public string localDirectioryPath = "Settings";                                                         // 폴더 명 (Assets/Settings)
    public string fileName = "TransformData";                                                               // 파일 이름 (TransformData.txt)
    public string extName = "txt";                                                                          // 확장자
    public string id = "001";                                                                               // 고유번호

    public Transformdata data;
    public void SaveTransform(Transform Save_position)
    {
        Transformdata data = new Transformdata(Save_position, Utils.Instance.StageNumber);
        string jsonStr=JsonUtility.ToJson(data);
        LocalFileIOHandler.Save(jsonStr, localDirectioryPath, $"{fileName}_{id}", extName);
    }

    public bool LoadTransform()
    {
        string jsonStr = LocalFileIOHandler.Load(localDirectioryPath, $"{fileName}_{id}", extName);

        if (jsonStr == null)
            return false;

        data = JsonUtility.FromJson<Transformdata>(jsonStr);
        return true;
    }
}
