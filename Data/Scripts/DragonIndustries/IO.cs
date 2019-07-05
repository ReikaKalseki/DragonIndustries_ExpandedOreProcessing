using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;

using Sandbox.ModAPI;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game.Entities;

using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRageMath;

using System.IO;

using IMyTerminalBlock = Sandbox.ModAPI.IMyTerminalBlock;

using MyObjectBuilder_UpgradeModule = Sandbox.Common.ObjectBuilders.MyObjectBuilder_UpgradeModule;

using MyLog = VRage.Utils.MyLog;

namespace DragonIndustries {
	
	public static class IO {
		
		public static void log(string s) {
        	MyLog.Default.WriteLineAndConsole("  DragonIndustries: "+s);
		}
			
		private static void loadConfigs() {
			/*
	        Configuration.settings.Clear();
			try {
				if (MyAPIGateway.Utilities.FileExistsInWorldStorage("config.cfg", typeof(ConfigEntry))) {
					TextReader reader = MyAPIGateway.Utilities.ReadFileInWorldStorage("config.cfg", typeof(ConfigEntry));
					string xmlText = reader.ReadToEnd();
					reader.Close();
                    List<ConfigEntry> li = MyAPIGateway.Utilities.SerializeFromXML<List<ConfigEntry>>(xmlText);
                    if (li != null) {
	                    foreach (ConfigEntry entry in li) {
	                    	entry.loadValue();
	                    	if (entry.ID == null) {
	                    		log("Could not parse config entry "+entry.Description+"; no ID!");
	                    		continue;
	                    	}
	                    	if (!entry.hasValue()) {
	                    		log("Could not parse config entry "+entry.Description+"!");
	                    		continue;
	                    	}
	                    	Configuration.settings.Add(entry.ID, entry);
	                    }
                    }
                }
        		IO.log("Loaded general config. Data = "+toUsefulString(Configuration.settings));
			}
			catch (Exception ex) {
				log("Threw exception reading general config: "+ex.ToString());
			}
			*/
		}

		public static void loadSavedData() {
			loadConfigs();
			
			string world = MyAPIGateway.Session.Name;
        }

		public static void savePersistentData() {
            try {				
				
			}
			catch (Exception ex2) {
				log("Threw exception writing cloaking list: "+ex2.ToString());
			}
        }

		public static void writeConfigs() {/*
        	List<ConfigEntry> li = new List<ConfigEntry>();
        	
        	log("Writing general config. Data = "+toUsefulString(Configuration.settings));
        	
        	if (MyAPIGateway.Utilities.FileExistsInWorldStorage("config.cfg", typeof(ConfigEntry)))
        		MyAPIGateway.Utilities.DeleteFileInWorldStorage("config.cfg", typeof(ConfigEntry));
			
			TextWriter writer = MyAPIGateway.Utilities.WriteFileInWorldStorage("config.cfg", typeof(ConfigEntry));
			foreach (var entry in Configuration.settings) {
				li.Add(entry.Value);
			}
			
            try {
				try {
					//writer.Write("Serializing a list of size "+Configuration.settings.Count+";");
					//IO.log("Serializing a list of size "+Configuration.settings.Count+";");
					//writer.Write("Serializes to:");
					writer.Write(MyAPIGateway.Utilities.SerializeToXML(li));
				}
				catch (Exception ex) {
					writer.Write("Error while writing config.");
					writer.Write("\n\nSize: "+Configuration.settings.Count);
					writer.Write("\n\nException: "+ex);
				}
				writer.Flush();
				writer.Close();
			}
			catch (Exception ex2) {
				log("Threw exception writing general config: "+ex2.ToString());
			}
			
			fileData = new List<EMPReaction>();
			if (MyAPIGateway.Utilities.FileExistsInWorldStorage("config_reactions.cfg", typeof(EMPReaction)))
        		MyAPIGateway.Utilities.DeleteFileInWorldStorage("config_reactions.cfg", typeof(EMPReaction));
        		
			writer = MyAPIGateway.Utilities.WriteFileInWorldStorage("config_reactions.cfg", typeof(EMPReaction));
			foreach (var entry in Configuration.reactionMap) {
				fileData.Add(entry.Value);
			}
			
            try {
				try {
					//writer.Write("Serializing a list of size "+fileData.Count+"; sample entry: "+fileData[0]+" for "+fileData[0].BlockType+" with "+fileData[0].Resistance+" @ "+fileData[0].MaxDistance);
					//writer.Write("Serializes to:");
					writer.Write(MyAPIGateway.Utilities.SerializeToXML(fileData));
				}
				catch (Exception ex) {
					writer.Write("Error while writing config.");
					writer.Write("\n\nData: "+fileData);
					writer.Write("\n\nException: "+ex);
				}
				writer.Flush();
				writer.Close();
			}
			catch (Exception ex2) {
				log("Threw exception writing reaction config: "+ex2.ToString());
			}*/
        }
        
        public static string toUsefulString<K, V>(Dictionary<K, V> dic) {
        	StringBuilder sb = new StringBuilder();
        	sb.Append(dic.Count+":"+"{");
        	foreach (var entry in dic) {
        		sb.Append("["+entry.Key+" = "+entry.Value+"], ");
        	}
        	sb.Append("}");
        	return sb.ToString();
        }
        
        public static string toUsefulString<E>(IEnumerable<E> li) {
        	StringBuilder sb = new StringBuilder();
        	string size = "";
        	if (li is ICollection<E>) {
        		size = (li as ICollection<E>).Count+":";
        	}
        	sb.Append(size+"[");
        	foreach (E entry in li) {
        		sb.Append(entry.ToString()+", ");
        	}
        	sb.Append("]");
        	return sb.ToString();
        }
	}
}