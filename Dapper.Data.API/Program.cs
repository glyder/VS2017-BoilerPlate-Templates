using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dapper.Data.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }


        //public static void TODOTest()
        //{
        //    var connectionString = "your connection string";
        //    PersonRepository personRepo = new PersonRepository(connectionString);
        //    Person person = null;
        //    Guid Id = new Guid("{82B31BB2-85BF-480F-8927-BB2AB71CE2B3}");

        //    // Typically, you'd be doing this inside of an async Web API controller, not the main method of a console app.
        //    // So, we're just using Task.Factory to simulate an async Web API call.
        //    var task = new Task(async () =>
        //    {
        //        person = await personRepo.GetPersonById(Id);
        //    });

        //    // This just prevents the console app from exiting before the async work completes.
        //    Task.WaitAll(task);
        //}



        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
