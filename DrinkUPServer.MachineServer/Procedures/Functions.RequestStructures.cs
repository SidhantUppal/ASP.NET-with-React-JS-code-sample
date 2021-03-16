using DrinkUPServer.MachineServer.Communication;
using DrinkUPServer.MachineServer.Messages;
using DrinkUPServer.Structures.Constructs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Procedures
{
    internal static partial class Functions
    {
        internal static void RequestStructures()
        {
            Utility.LogFile("RequestStructures", "Functions");
            Pool.Connection.RegisterMessageListener<StructuresRequest>(RequestStructuresHandler);

            StructuresRequest message = new StructuresRequest();
            message.From = "mach-one";
            RequestStructuresHandler(message);
        }

        private static async void RequestStructuresHandler(StructuresRequest message)
        {
            try
            {
                Utility.LogFile("Call RequestStructuresHandler", "RequestStructuresHandler");

                Machine machine = new Machine
                {
                    Id = "mach-one",
                    Name = "The First Machine",
                    Location = "Earth",
                    InitializationToken = "",
                    Isdeleted = false,
                    Description = ""
                };
                Utility.LogFile(machine.Name, "Get Machine Name - RequestStructuresHandler");

                List<Size> Sizes = new List<Size>();
                Sizes.Add(new Size
                {
                    Id = "mach-one-size-large",
                    Target = 1,
                    Enabled = true,
                    Title = "Large",
                    Image = "SizeLarge",
                    Capacity = 40,
                    Price = 103,
                    Machine = machine,
                    MediaID = null,
                    Isdeleted = false
                });

                SizesDelivery sizeDelivery = new SizesDelivery
                {
                    For = message.From,
                    Sizes = Sizes,
                };
                Pool.Connection.Send(sizeDelivery);
                Utility.LogFile("Send size", "Functions");

                List<Boost> boosts = new List<Boost>();
                boosts.Add(new Boost
                {
                    Id = "mach-one-boost-electrolyte",
                    Target = 1,
                    Enabled = true,
                    Percentage = 5,
                    Duration = 1000,
                    Title = "Electrolyte",
                    SubTitle = "Strawberry Peach",
                    Image = "BoostElectrolyte",
                    Price = 103,
                    Ingredients = "Strawberries and Peaches",
                    Details = "test",
                    MediaID = null,
                    Comment = "test",
                    Isdeleted = false,
                    Machine = machine
                });

                BoostsDelivery boostDelivery = new BoostsDelivery
                {
                    For = message.From,
                    Boosts = boosts,
                };

                Pool.Connection.Send(boostDelivery);
                Utility.LogFile("Send boost", "Functions");
            }
            catch (Exception ex)
            {
                Utility.LogFile(ex.Message, "RequestStructuresHandler - Error");
            }
        }
    }
}
