using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sp_hw5
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var service = new UserService(@"Server=DESKTOP-8ISC2JM;Database=UserDb;Integrated Security=True; TrustServerCertificate=True;");
            Task task1 = Task.Run(async () =>
            {
                var user = new User { Name = "Bob", Age = 25 };
                await service.CreateUser(user);
                Console.WriteLine($"User {user.Name} added");
            });
            Task task2 = Task.Run(async () =>
            {
                var user = new User { Name = "John", Age = 35 };
                await service.CreateUser(user);
                Console.WriteLine($"User {user.Name} added");
            });
            Task task3 = Task.Run(async () =>
            {
                await Task.WhenAll(task1, task2);
                Console.WriteLine("All users:");
                foreach (var item in await service.ReadUsers()) 
                {  
                    Console.WriteLine(item); 
                }
            });
            Task task4 = Task.Run(async () =>
            {
                await Task.WhenAll(task3);
                await service.DeleteUser(2);
                Console.WriteLine("User deleted");
            });
            Task task5 = Task.Run(async () =>
            {
                await Task.WhenAll(task4);
                Console.WriteLine("All updated users:");
                foreach (var item in await service.ReadUsers())
                {
                    Console.WriteLine(item);
                }
            });
            await task5;
            Console.ReadKey();
        }
    }
}
