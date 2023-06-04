using System.IO;
using UnityEngine;

public class ReadFile 
{
    public static string ReaLine(string file, int row_id)
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/Resources/Data/" + file + ".txt");
        bool endOfFile = false;
        int id = 0;
        string data = null;

        while (!endOfFile)
        {

            data = reader.ReadLine();

            if (data == null)
            {
                endOfFile = true;
                break;
            }

            if (id == row_id)
            {
                break;
            }
            else
                id++;


        }

        return data;
    }
}
