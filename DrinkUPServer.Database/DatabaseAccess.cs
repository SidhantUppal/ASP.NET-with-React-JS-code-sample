using DrinkUPServer.Structures.Constructs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DrinkUPServer.Database
{
    public class DatabaseAccess
    {
        public async Task<bool> AddMachine ( Machine machine )
        {
            bool done = false;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                DataServer.Machines.Add( machine );

                if ( DataServer.SaveChanges() == 1 )
                {
                    done = true;
                }
            } );
            return done;
        }
        public async Task<List<Machine>> GetAllMachines ()
        {
            List<Machine> machines = null;

            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                machines = (
                from m in DataServer.Machines
                select m
                ).ToList();
            } );

            return machines;
        }

        public async Task<bool> UpdateMachine ( Machine machine )
        {
            bool done = false;

            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                Machine m = DataServer.Machines.Find( machine.Id );

                if ( m != null )
                {
                    DataServer.Entry( m ).CurrentValues.SetValues( machine );

                    if ( DataServer.SaveChanges() == 1 )
                    {
                        done = true;
                    }
                }
            } );

            return done;
        }
        public async Task<Machine> GetMachine ( string id )
        {
            Machine machine = null;

            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                machine = (

                from m in DataServer.Machines
                where m.Id == id
                select m

                ).FirstOrDefault();
            } );

            return machine;
        }

        public async Task<bool> AddCustomer ( Customer customer )
        {
            bool done = false;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                DataServer.Customers.Add( customer );

                if ( DataServer.SaveChanges() == 1 )
                {
                    done = true;
                }
            } );
            return done;
        }

        public async Task<bool> UpdateCustomer ( Customer customer )
        {
            bool done = false;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                Customer c = DataServer.Customers.Find( customer.Id );

                if ( c != null )
                {
                    DataServer.Entry( c ).CurrentValues.SetValues( customer );

                    if ( DataServer.SaveChanges() == 1 )
                    {
                        done = true;
                    }
                }
            } );
            return done;
        }

        public async Task<Customer> GetCustomerById ( string id )
        {
            Customer customer = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                customer = (

                from c in DataServer.Customers
                where c.Id == id
                select c

                ).FirstOrDefault();
            } );
            return customer;
        }

        public async Task<Customer> GetCustomerByEmail ( string email )
        {
            Customer customer = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                customer = (

                from c in DataServer.Customers
                where c.Email == email
                select c

                ).FirstOrDefault();
            } );
            return customer;
        }

        public async Task<bool> AddSize ( Size size )
        {
            bool done = false;

            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                DataServer.Sizes.Add( size );

                if ( DataServer.SaveChanges() == 1 )
                {
                    done = true;
                }
            } );

            return done;
        }

        public async Task<bool> UpdateSize ( Size size )
        {
            bool done = false;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                Size s = DataServer.Sizes.Find( size.Id );

                if ( s != null )
                {
                    DataServer.Entry( s ).CurrentValues.SetValues( size );

                    if ( DataServer.SaveChanges() == 1 )
                    {
                        done = true;
                    }
                }
            } );
            return done;
        }

        public async Task<List<Size>> GetSizes ( string machine )
        {
            List<Size> sizes = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                sizes = (

                from size in DataServer.Sizes
                where size.Machine.Id == machine && size.Enabled
                orderby size.Capacity ascending
                select size

                ).ToList();
            } );
            return sizes;
        }

        public async Task<List<T>> GetSizes<T> ( string machine ) where T : IBaseSize
        {
            List<T> sizes = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();
                IEnumerable<Size> enumerableSizes = (

                from size in DataServer.Sizes
                where size.Machine.Id == machine && size.Enabled
                orderby size.Capacity ascending
                select size

                );

                sizes = ( from s in enumerableSizes select (T) (IBaseSize) s ).ToList();
            } );
            return sizes;
        }

        public async Task<List<Size>> GetSizes ( Machine machine )
        {
            List<Size> sizes = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                sizes = (

                from size in DataServer.Sizes
                where size.Machine.Id == machine.Id && size.Enabled
                orderby size.Capacity ascending
                select size

                ).ToList();
            } );
            return sizes;
        }

        public async Task<List<T>> GetSizes<T> ( Machine machine ) where T : IBaseSize
        {
            List<T> sizes = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                IEnumerable<Size> enumerableSizes = (

                from size in DataServer.Sizes
                where size.Machine.Id == machine.Id && size.Enabled
                orderby size.Capacity ascending
                select size

                );

                sizes = (

                from s in enumerableSizes
                select (T) (IBaseSize) s

                ).ToList();
            } );
            return sizes;
        }

        public async Task<List<Size>> GetSizesAll ( string machine )
        {
            List<Size> sizes = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                sizes = (

                from size in DataServer.Sizes
                where size.Machine.Id == machine
                orderby size.Capacity ascending
                select size

                ).ToList();
            } );
            return sizes;
        }

        public async Task<List<Size>> GetSizesAll ( Machine machine )
        {
            List<Size> sizes = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                sizes = (

                from size in DataServer.Sizes
                where size.Machine.Id == machine.Id
                orderby size.Capacity ascending
                select size

                ).ToList();
            } );
            return sizes;
        }

        public async Task<bool> AddBoost ( Boost boost )
        {
            bool done = false;

            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                DataServer.Boosts.Add( boost );

                if ( DataServer.SaveChanges() == 1 )
                {
                    done = true;
                }
            } );

            return done;
        }

        public async Task<bool> UpdateBoost ( Boost boost )
        {
            bool done = false;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                Boost b = DataServer.Boosts.Find( boost.Id );

                if ( b != null )
                {
                    DataServer.Entry( b ).CurrentValues.SetValues( boost );

                    if ( DataServer.SaveChanges() == 1 )
                    {
                        done = true;
                    }
                }
            } );
            return done;
        }

        public async Task<List<Boost>> GetBoosts ( string machine )
        {
            List<Boost> boosts = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                boosts = (

                from boost in DataServer.Boosts
                where boost.Machine.Id == machine && boost.Enabled
                orderby boost.Price ascending
                select boost

                ).ToList();
            } );
            return boosts;
        }

        public async Task<List<T>> GetBoosts<T> ( string machine ) where T : IBaseBoost
        {
            List<T> boosts = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                IEnumerable<Boost> enumerableBoosts = (

                from boost in DataServer.Boosts
                where boost.Machine.Id == machine && boost.Enabled
                orderby boost.Price ascending
                select boost

                );

                boosts = (

                from b in enumerableBoosts
                select (T) (IBaseBoost) b

                ).ToList();

            } );
            return boosts;
        }

        public async Task<List<Boost>> GetBoosts ( Machine machine )
        {
            List<Boost> boosts = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                boosts = (

                from boost in DataServer.Boosts
                where boost.Machine.Id == machine.Id && boost.Enabled
                orderby boost.Price ascending
                select boost

                ).ToList();
            } );
            return boosts;
        }

        public async Task<List<T>> GetBoosts<T> ( Machine machine ) where T : IBaseBoost
        {
            List<T> boosts = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                IEnumerable<Boost> enumerableBoosts = (

                from boost in DataServer.Boosts
                where boost.Machine.Id == machine.Id && boost.Enabled
                orderby boost.Price ascending
                select boost

                ).ToList();

                boosts = (

                from b in enumerableBoosts
                select (T) (IBaseBoost) b

                ).ToList();
            } );
            return boosts;
        }

        public async Task<List<Boost>> GetBoostsAll ( string machine )
        {
            List<Boost> boosts = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                boosts = (

                from boost in DataServer.Boosts
                where boost.Machine.Id == machine
                orderby boost.Price ascending
                select boost

                ).ToList();
            } );
            return boosts;
        }

        public async Task<List<Boost>> GetBoostsAll ( Machine machine )
        {
            List<Boost> boosts = null;
            await Task.Run( () =>
            {
                using DataServer DataServer = new DataServer();

                boosts = (

                from boost in DataServer.Boosts
                where boost.Machine.Id == machine.Id
                orderby boost.Price ascending
                select boost

                ).ToList();
            } );
            return boosts;
        }

        public async Task<DataTable> GetAzureIPDetails() 
        {
            // Initialization.  
            DataTable responseObj = new DataTable();

            // HTTP GET.  
            using (var client = new HttpClient())
            {
                // Setting Base address.  
                client.BaseAddress = new Uri("http://dummy.restapiexample.com/api/v1/employees");

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Initialization.  
                HttpResponseMessage response = new HttpResponseMessage();

                // HTTP GET  
                response = await client.GetAsync("api/WebApi").ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<DataTable>(result);
                }
            }

            return responseObj;
        }
    }
}
