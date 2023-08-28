using System.IO;
using cfg;
using SimpleJSON;
using UnityEngine;

public class LubanTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    Tables table = new Tables(Loader);
	    Item item = table.TbItem.Get(10000);
		Debug.Log(item.Name + " "+ item.Desc);
    }

    // Update is called once per frame
    JSONNode Loader(string fileName)
    {
	    return JSON.Parse(File.ReadAllText(Application.dataPath + "/Luban/AutoGen/Data/json/" + fileName + ".json"));
    }
}
