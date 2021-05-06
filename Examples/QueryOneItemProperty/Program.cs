using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPC.Common;
using OPC.Data;

namespace QueryOneItemProperty
{
    class Program
    {
        static void Main(string[] args)
        {
            string progID = args.Length > 0 ? args[0] : "HWHsc.OPCServer";
            string itemID = args.Length > 1 ? args[1] : "/ASSETS.Guid";
            string propID = args.Length > 2 ? args[2] : "2";

            OpcServer opcServer = new OpcServer();
            opcServer.Connect(progID);
            System.Threading.Thread.Sleep(1000); // we are faster than some servers!

            OpcProperty[] props = opcServer.QueryAvailableProperties(itemID);
            for (int i = 0; i < props.Length; i++)
            {
                Console.WriteLine($"{props[i].PropertyID} - {props[i].Description}");
            }
            OpcPropertyData[] data = opcServer.GetItemProperties(itemID, new int[] {int.Parse(propID)});

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Error == HRESULTS.S_OK)
                    Console.WriteLine(data[i].Data);
                else
                    Console.WriteLine("!ERROR:{0}", data[i].Error);
            }

            opcServer.Disconnect();
        }
    }
}
