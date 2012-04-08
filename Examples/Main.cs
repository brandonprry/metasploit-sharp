using System;
using metasploitsharp;
using System.Collections.Generic;

namespace Examples
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (MetasploitSession session = new MetasploitSession("metasploit", "2c8X|a2!", "https://192.168.1.148:3790/api/1.1"))
			{
				using (MetasploitProManager manager = new MetasploitProManager(session))
				{
					Dictionary<object, object> modules = manager.GetCoreModuleStats();
					
					Console.WriteLine("Module stats:");
					foreach (KeyValuePair<object, object> pair in modules)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Dictionary<object, object> version = manager.GetCoreVersionInformation();
					
					Console.WriteLine("\n\nVersion information:");
					foreach (KeyValuePair<object, object> pair in version)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Console.WriteLine("\n\nCreating console...");
					Dictionary<object, object> consoleResponse = manager.CreateConsole();
					foreach (KeyValuePair<object, object> pair in consoleResponse)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					string consoleID = consoleResponse["id"] as string;
					
					Console.WriteLine("\n\nConsole created, getting list of consoles...");
					Dictionary<object, object> consoleList = manager.ListConsoles();
					foreach (KeyValuePair<object, object> pair in consoleList)
					{
						Console.WriteLine("\n" + pair.Key + ":");
						
						foreach (object obj in (pair.Value as object[]))
						{
							//each obj is a Dictionary<object, object> in this response
							foreach (KeyValuePair<object, object> p in obj as Dictionary<object, object>)
							{
								Console.WriteLine(p.Key + ": " + p.Value);
							}
						}
					}
					
					Console.WriteLine("\n\nDestroying our console: " + consoleID);
					Dictionary<object, object> destroyResponse = manager.DestroyConsole(consoleID);
					foreach (KeyValuePair<object, object> pair in destroyResponse)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					if (destroyResponse.ContainsKey((object)"result") && ((string)destroyResponse[((object)"result")]) == "success")
						Console.WriteLine("Destroyed.");
					else
						Console.WriteLine("Failed!");
					
					Dictionary<object, object> proVersion = manager.AboutPro();
					
					Console.WriteLine("\n\nInformation about pro:");
					foreach (KeyValuePair<object, object> pair in proVersion)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Dictionary<object, object> updateStatus = manager.ProUpdateStatus();
					Console.WriteLine("\n\nUpdate status:");
					
					foreach(KeyValuePair<object, object> pair in updateStatus)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
				}
			}
		}
	}
}
