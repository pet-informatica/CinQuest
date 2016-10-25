using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class MenuStatus
	{

		private Dictionary<string,bool> menuStatus;
		private Dictionary<string,List<string>> menuProblem;

		public MenuStatus ()
		{
			this.menuStatus = new Dictionary<string,bool> ()
			{
				{"Pause", false},
				{"Control", false},
				{"Feedback", false},
				{"Inventory", false},
				{"Map", false},
				{"Quest", false}
			};
//			this.menuProblem = new Dictionary<string,bool> () {
//				{ "Pause", new List<string> { } },
//				{ "Control", new List<string> { } },
//				{ "Feedback", new List<string> { } },
//				{ "Inventory", new List<string> { "Pause", "Controle" } },
//				{ "Map", new List<string> { "Pause", "Controle" } },
//				{ "Quest", new List<string> { "Pause", "Controle" } }
//			};

		}

		/// <summary>
		/// Open the specified menu.
		/// </summary>
		/// <param name="menu">Menu.</param>
		public void open(string menu){
			menuStatus [menu] = true;
			//Debug.Log (menu + " " + menuStatus [menu].ToString());
		}

		/// <summary>
		/// Close the specified menu.
		/// </summary>
		/// <param name="menu">Menu.</param>
		public void close(string menu){
			menuStatus [menu] = false;
			//Debug.Log (menu + " " + menuStatus [menu].ToString());
		}
			
		public bool openProblem(string menu){
			bool problem = false;

			//quando o feedback está aberto, nenhuma tecla deve funcionar para abrir um menu
			if (menuStatus ["Feedback"]) {
				problem = true;
			} else {
				switch (menu) {
				//Feedback não é necessário
				case "Pause":
					// need to do test with map (map was disabled in 25/10/2016)
					if(menuStatus ["Inventory"] || menuStatus ["Quest"] /*|| menuStatus ["Map"]*/){
						problem = true;
						Debug.Log (menu);
					}
					break;
				case "Inventory":
					// need to do test with map (map was disabled in 25/10/2016)
					if (menuStatus ["Pause"] /*|| menuStatus ["Map"]*/) {
						problem = true;
						Debug.Log (menu);
					}
					break;
				case "Quest":
					// need to do test with map (map was disabled in 25/10/2016)
					if (menuStatus ["Pause"] /*|| menuStatus ["Map"]*/) {
						problem = true;
						Debug.Log (menu);
					}
					break;
				case "Map": // need to do test with map (map was disabled in 25/10/2016)
//					if(menuStatus ["Pause"] || menuStatus ["Inventory"] || menuStatus ["Quest"]){
//						problem = true;
//					}
					break;
				}
			}
				
			return problem;
		}
	}
}

