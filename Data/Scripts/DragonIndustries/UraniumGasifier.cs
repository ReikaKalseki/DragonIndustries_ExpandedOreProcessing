using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Definitions;
using Sandbox.Engine;
using Sandbox.Game;
using Sandbox.ModAPI;
using VRage.Game.ModAPI.Interfaces;
using VRage;
using VRage.ObjectBuilders;
using VRage.Game;
using VRage.ModAPI;
using VRage.Game.Components;
using VRageMath;
using Sandbox.Engine.Multiplayer;
using VRage.Game.ModAPI;

using Sandbox.ModAPI.Interfaces.Terminal;
using MyInventoryItem = VRage.Game.ModAPI.Ingame.MyInventoryItem;

namespace DragonIndustries
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_OxygenTank), false, "UraniumGasTank")]
    public class UraniumGasTank : LogicCore
    {
        public long lastTick = 0;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder) {	            
        	doSetup("Factory", 0.72F, MyEntityUpdateEnum.EACH_10TH_FRAME);
            
            lastTick = DateTime.UtcNow.Ticks;
        }

        protected override void updateInfo(IMyTerminalBlock block, StringBuilder sb) {

        }

        public override void Close() {
            thisBlock.AppendingCustomInfo -= updateInfo;
            base.Close();
        }

        protected override bool shouldUsePower() {
        	return thisBlock.Enabled && thisBlock.IsFunctional;
        }         

        public override void MarkForClose() {
        	
        }
        
        protected override void onEnergyLoss() {
        	
        }

        public override void UpdateAfterSimulation10() {
        	base.UpdateAfterSimulation10();
            bool running = shouldUsePower();
      
            if (running) {
           		//check for gas; if has gas, make "internal processable" item which the centrifuge can refine to ore; try to move it there if possible
           		//appears to be impossible to take gas from the tank; what about secret bottle refilling?
           		IMyGasTank tank = thisBlock as IMyGasTank;
           		IMyInventory inv = tank.GetInventory();
           		List<MyInventoryItem> li = new List<MyInventoryItem>();
           		inv.GetItems(li, isFullUraniumBottle);
            }
            else {
           		
            }
            
            sync();
            thisBlock.RefreshCustomInfo();        
        }
        
        private bool isFullUraniumBottle(MyInventoryItem item) {
        	return item.Type.SubtypeId == "UraniumGasBottle"; //how to tell if full?!
        }

        protected override float getRequiredPower() {
            float neededPower = 0.001F;
            lastTick = DateTime.UtcNow.Ticks;
            return neededPower;
        }

        private bool isActualGasTank(IMyTerminalBlock block) {
            return block.BlockDefinition.SubtypeName.Contains("Oxygen");
        }
        
        private void hideIrrelevantOxyGenSettings() {
            try {
	            List<IMyTerminalAction> actions = new List<IMyTerminalAction>();
	            MyAPIGateway.TerminalControls.GetActions<IMyGasTank>(out actions);
	            foreach (IMyTerminalAction action in actions) {
            		//IO.log("Checking action '"+action.Id);
            		if (action.Id.ToString().Contains("Refill"))
	            		action.Enabled = isActualGasTank;
	            }
	
	            List<IMyTerminalControl> controls = new List<IMyTerminalControl>();
            	MyAPIGateway.TerminalControls.GetControls<IMyGasTank>(out controls);
	            foreach (IMyTerminalControl action in controls) {
            		//IO.log("Checking control '"+action.Id);
	            	if (action.Id.ToString().Contains("Refill"))
	            		action.Enabled = action.Visible = isActualGasTank;
	            }
            }
            catch (Exception e) {
            	IO.log(e.ToString());
            }
        }
        
        private bool variable;
        
        public void setVariable(bool var) {
        	variable = var;
        }
        
        protected override void doGuiInit() {
        	//MyAPIGateway.Utilities.ShowNotification("Running GUI init");
        	
        	/*
 			hideIrrelevantOxyGenSettings();
            Func<IMyTerminalBlock, bool> cur = block => block.GameLogic.GetAs<UraniumGasTank>().variable;
            Action<IMyTerminalBlock, bool> set = (block, flag) => block.GameLogic.GetAs<UraniumGasTank>().setVariable(flag);
            string desc = "This is a test.";
            new ToggleButton<IMyGasGenerator>("athing", "Do A Thing", desc, cur, set).register();
            */
        }
    }
}
