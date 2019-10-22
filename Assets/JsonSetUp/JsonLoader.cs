using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class JsonLoader<T> : MonoBehaviour
{
    string _path;
    string _jsonString;

   // protected JArray jArray { get; private set; }

   protected JObject jObject { get; private set; }


    public virtual void Init(string filePath)
    {
        _path = filePath;
        using (StreamReader r = new StreamReader(_path))
        {
            _jsonString = r.ReadToEnd();
            jObject = JObject.Parse(_jsonString);
        }
    }

    protected List<T> GetObjectListFromFilePathByString(string indexer)
    {
        List<T> toReturn = new List<T>();
        
        foreach (var item in jObject[indexer])
        { 
            toReturn.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
        }
        return toReturn;
    }

}
