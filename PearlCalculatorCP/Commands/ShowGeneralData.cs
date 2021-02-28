using System;
using PearlCalculatorCP.Models;
using PearlCalculatorLib.General;

namespace PearlCalculatorCP.Commands
{
    public class ShowGeneralData : ICommand
    {
        public void Excute(string[]? parameters, string? cmdName, Action<ConsoleOutputItemModel> messageSender)
        {
            messageSender(DefineCmdOutput.MsgTemplate("Show General Data:"));
            
            messageSender(DefineCmdOutput.MsgTemplate("North West TNT:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.NorthWestTNT.ToString()));

            messageSender(DefineCmdOutput.MsgTemplate("North East TNT:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.NorthEastTNT.ToString()));
            
            messageSender(DefineCmdOutput.MsgTemplate("South West TNT:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.SouthWestTNT.ToString()));

            messageSender(DefineCmdOutput.MsgTemplate("South East TNT:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.SouthEastTNT.ToString()));
            
            messageSender(DefineCmdOutput.MsgTemplate(nameof(Data.Destination)));
            messageSender(DefineCmdOutput.MsgTemplate(Data.Destination.ToString()));
            
            messageSender(DefineCmdOutput.MsgTemplate("Pearl Pos:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.Pearl.Position.ToString()));

            messageSender(DefineCmdOutput.MsgTemplate("Pearl Motion:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.Pearl.Motion.ToString()));

            messageSender(DefineCmdOutput.MsgTemplate("Pearl Offset:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.PearlOffset.ToString()));

            messageSender(DefineCmdOutput.MsgTemplate("Red TNT:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.RedTNT.ToString()));
            
            messageSender(DefineCmdOutput.MsgTemplate("Blue TNT:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.BlueTNT.ToString()));
            
            messageSender(DefineCmdOutput.MsgTemplate("TNT Weight:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.TNTWeight.ToString()));

            messageSender(DefineCmdOutput.MsgTemplate("Max TNT:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.MaxTNT.ToString()));

            messageSender(DefineCmdOutput.MsgTemplate("Direction:"));
            messageSender(DefineCmdOutput.MsgTemplate(Data.Direction.ToString()));
            
        }
    }
}