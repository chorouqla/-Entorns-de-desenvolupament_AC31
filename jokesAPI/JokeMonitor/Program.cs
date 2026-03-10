using System;
using System.Net.Http;// it gives you Internet tools like HttpClient
using System.Threading;// it gives you Time tools like Thread.Sleep
using System.Text.Json;// it gives you SON tools like JsonDocument

class Program
{
    static HttpClient client = new HttpClient();// create a phone that can call websites 

    static string chisteAnterior = ""; //to remember the last jk we got 

    static string urlApi = "https://v2.jokeapi.dev/joke/Programming?blacklistFlags=nsfw,religious,political,racist,sexist,explicit&safe-mode";

    static void Main()
    {
        Console.WriteLine("=========================================");
        Console.WriteLine(" JOKE MONITOR PROGRAM ");
        Console.WriteLine("=========================================");
        Console.WriteLine($"loading...");
        Console.WriteLine($"Using: {urlApi}\n");

        while (true)
        {
            try
            {
                string chisteActual = obtenerChiste(); //get a joke from API

                //SHOW THE JOKE con tiempo
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 📢 CURRENT JOKE:");
                Console.WriteLine(chisteActual);
                Console.WriteLine(new string('-', 40));//This creates a line of 40 dashes

                if (chisteAnterior != "" && chisteActual != chisteAnterior)
                {
                    Console.WriteLine("CHANGE DETECTED! New joke received!\n"); // the n to start a  new line 
                }
                else if (chisteAnterior == "")
                {
                    Console.WriteLine("First joke ever. Starting monitoring...\n");
                }
                else
                {
                    Console.WriteLine("Same joke as before (rare with JokeAPI).\n");
                }

                chisteAnterior = chisteActual;

                Console.WriteLine($"⏳ Waiting 10 seconds...\n"); // wait 10 seconds
                Thread.Sleep(10000); // 10000 milliseconds = 10 seconds
            }
            catch (Exception ex) //if something goes wrong like no internet
            {
                Console.WriteLine($" Error: {ex.Message}");
                Console.WriteLine("Will try again in 10 seconds...\n");
                Thread.Sleep(10000);

            }
        }
    }

    static string obtenerChiste()
    {
        try
        {
            //call the joke website 
            HttpResponseMessage response = client.GetAsync(urlApi).Result;//.GetAsync = "Call this number" (make a web request)

            response.EnsureSuccessStatusCode(); //make sure the call worked

            string json = response.Content.ReadAsStringAsync().Result;//get the response as text

            //convert json into something we can read
            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            string tipo = root.GetProperty("type").GetString(); //chek what type of joke it is

            if (tipo == "single")
            {
                return root.GetProperty("joke").GetString();
            }
            else // "twopart" joke (setup + punchline)
            {
                string setup = root.GetProperty("setup").GetString();
                string delivery = root.GetProperty("delivery").GetString();
                return $"{setup}\n{delivery}";
            }

        }
        catch (Exception ex)
        {
            return $"Error getting joke:{ex.Message}";
        }
    }
}
