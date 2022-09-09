using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CoreSampleWebjob.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;
using CoreSampleWebjob.Models;
using System.Linq;

namespace CoreSampleWebjob
{
    public class Functions
    {
        private static List<BookModel> books;

        public static void Run([TimerTrigger("%TimerInfo%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Time :{DateTime.Now}");
            string apiUrl = "https://fakerapi.it/api/v1/books?_quantity=1";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                var o = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
                books = o.Value<JArray>("data")
                    .ToObject<List<BookModel>>();

                IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

                var connectionString = configuration.GetConnectionString("TestApi");

                var options = new DbContextOptionsBuilder<TestAPISampleContext>()
                                 .UseSqlServer(new SqlConnection(connectionString))
                                 .Options;

                using (var context = new TestAPISampleContext(options)) // <-- Pass the options here
                {
                    var count = context.Books.Count();
                    log.LogInformation($"Count : {count}");
                    try
                    {
                        foreach (var book in books)
                        {
                            Book bookdb = new Book();
                            bookdb.title = book.title;
                            bookdb.author = book.author;
                            bookdb.genre = book.genre;
                            bookdb.description = book.description;
                            bookdb.isbn = book.isbn;
                            bookdb.image = book.image;
                            bookdb.published = book.published + DateTime.Now.ToString();
                            bookdb.publisher = book.publisher;
                            context.Books.Add(bookdb);
                            context.SaveChanges();
                        }
                    }
                    catch
                    {

                    }

                }

            }

        }
    }


}
