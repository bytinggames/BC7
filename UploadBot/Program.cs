namespace UploadBot
{
    class Program
    {
        public static void Main(string[] args)
        {
            string? name;
            while (true)
            {
                Console.WriteLine("Bot name to upload (Example_1.cs f.ex.) .zip, .7z, .gz are also possible:");

                name = Console.ReadLine();
                if (name == null)
                    continue;
                name = name.Replace('/', Path.DirectorySeparatorChar);
                name = name.Replace('\\', Path.DirectorySeparatorChar);

                if (!name.EndsWith(".cs") && !name.EndsWith(".zip") && !name.EndsWith(".7z") && !name.EndsWith(".gz"))
                    name += ".cs";

                name = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "BC7Runner", name);
                name = Path.GetFullPath(name);

                if (File.Exists(name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("file " + name + " doesn't exist. Try again!");
                }
            }

            Upload(name);
        }

        public static void Upload(string file)
        {
#pragma warning disable SYSLIB0014 // Type or member is obsolete
            System.Net.WebClient Client = new System.Net.WebClient();
#pragma warning restore SYSLIB0014 // Type or member is obsolete

            Client.Headers.Add("Content-Type", "binary/octet-stream");

            byte[] result = Client.UploadFile("https://bytinggames.com/bot-challenge/index.php",
                "POST",
                file);

            string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);

            Console.WriteLine();
            Console.WriteLine(s);

            Console.WriteLine("probably worked, idk");
        }
    }
}
