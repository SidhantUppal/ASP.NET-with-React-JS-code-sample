This is the DrinkUPServer solution. It is written using .NET Core.

The solution contains four projects.
- DrinkUPServer.Database
- DrinkUPServer.MachineServer
- DrinkUPServer.Structures
- DrinkUPServer.Web

Of these, DrinkUPServer.Web is the main running project that references the others.

---

The following are their functions in the solution.

FUNCTIONALITY

DrinkUPServer.Structures does not contain any executable functions, but rather contains all
class structures required by the rest of the solution due to the structures being in common
with more than one component of the solution. It is the more encompassing equivalent to
DrinkUP.Structures.

DrinkUPServer.Database is the component of the solution that handles all of the database
related actions. This includes all of the Add, Get and Update functions that have been
devised so far. All functions are written to be async to allow non-blocking access of data.

DrinkUPServer.MachineServer is the component that handles all machine related
communication. It consists of the Client component that references individual machines, the
Server component which contains a TCP Listener to generate Client handlers, and methods of
writing messages to the client and receiving messages from them in C# object forms.

DrinkUPServer.Web contains the React WebApp and the API controllers used to facilitate
communication between the WebApp/NativeMobileApp and the machines. This is the main project
as it runs on IIS and IIS allows easy management of the server including changing the
address, addition of SSL and it can run the server program on reboot of the host server.

---

Before intial debug, the following will need to be done.

SETUP

1. Set up the Database
Initialize a new database table in Microsoft SQL. This can either be done in MSSQL Server
or directly in Visual Studio using the SQL Server Object Explorer.

After initializing the database, populate it with seed data through the use of
the following command from the Package Manager Console (using DrinkUPServer.Database as 
the "Default Project" target).

``
Update-Database
``

That command will build the database in accordance with the instructions within the
Migrations folder of the project. The folder contains timestamped files known as migrations
which are meant to run sequentially to construct or deconstruct the required database
formation. The folder also contains a Configuration file which allows for adjustments to
seeding the database. Right now, it's set up to add one Machine and all of its constituent
sizes and boosts to the database.

The formed database has tables with the same structures as that of the constructs that they
reference from DrinkUPServer.Structures

Whenever changes are made to those constructs or more tables are to be added, run the
following command in the Package Manager Database.

``
Add-Migration 'MigrationName'
``

Follow this with Update-Database to commit the changes to the database.

2. Set debug server IP address
In DrinkUPServer.MachineServer.Communication.Facilitators.Server, set the IP address of the
local system. This will run the machine server at that IP address on port 3687, which can
also be changed by making change to that field.

---

Run the server. It doesn't matter which is run first, but the DrinkUP solution is mostly
inactive unless DrinkUPServer is running.