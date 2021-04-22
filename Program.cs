using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordNitroSniper
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Every working code will be printed to the console and outputed to working_gift_codes_txt!" +
                "\nRunning task... (Press any key to quit!)");

            Thread checkRandomCodeThread1 = new(new ThreadStart(() => CheckRandomCode(1)));
            Thread checkRandomCodeThread2 = new(new ThreadStart(() => CheckRandomCode(2)));
            Thread checkRandomCodeThread3 = new(new ThreadStart(() => CheckRandomCode(3)));
            Thread checkRandomCodeThread4 = new(new ThreadStart(() => CheckRandomCode(4)));
            Thread checkRandomCodeThread5 = new(new ThreadStart(() => CheckRandomCode(5)));
            Thread checkRandomCodeThread6 = new(new ThreadStart(() => CheckRandomCode(6)));
            Thread checkRandomCodeThread7 = new(new ThreadStart(() => CheckRandomCode(7)));
            Thread checkRandomCodeThread8 = new(new ThreadStart(() => CheckRandomCode(8)));
            Thread checkRandomCodeThread9 = new(new ThreadStart(() => CheckRandomCode(9)));
            Thread checkRandomCodeThread10 = new(new ThreadStart(() => CheckRandomCode(10)));
            Thread checkRandomCodeThread11 = new(new ThreadStart(() => CheckRandomCode(11)));
            Thread checkRandomCodeThread12 = new(new ThreadStart(() => CheckRandomCode(12)));
            Thread checkRandomCodeThread13 = new(new ThreadStart(() => CheckRandomCode(13)));
            Thread checkRandomCodeThread14 = new(new ThreadStart(() => CheckRandomCode(14)));
            Thread checkRandomCodeThread15 = new(new ThreadStart(() => CheckRandomCode(15)));
            Thread checkRandomCodeThread16 = new(new ThreadStart(() => CheckRandomCode(16)));
            checkRandomCodeThread1.Start();
            checkRandomCodeThread2.Start();
            checkRandomCodeThread3.Start();
            checkRandomCodeThread4.Start();
            checkRandomCodeThread5.Start();
            checkRandomCodeThread6.Start();
            checkRandomCodeThread7.Start();
            checkRandomCodeThread8.Start();
            checkRandomCodeThread9.Start();
            checkRandomCodeThread10.Start();
            checkRandomCodeThread11.Start();
            checkRandomCodeThread12.Start();
            checkRandomCodeThread13.Start();
            checkRandomCodeThread14.Start();
            checkRandomCodeThread15.Start();
            checkRandomCodeThread16.Start();
            Console.ReadKey();
            checkRandomCodeThread1.Abort();
            checkRandomCodeThread2.Abort();
            checkRandomCodeThread3.Abort();
            checkRandomCodeThread4.Abort();
            checkRandomCodeThread5.Abort();
            checkRandomCodeThread6.Abort();
            checkRandomCodeThread7.Abort();
            checkRandomCodeThread8.Abort();
            checkRandomCodeThread9.Abort();
            checkRandomCodeThread10.Abort();
            checkRandomCodeThread11.Abort();
            checkRandomCodeThread12.Abort();
            checkRandomCodeThread13.Abort();
            checkRandomCodeThread14.Abort();
            checkRandomCodeThread15.Abort();
            checkRandomCodeThread16.Abort();
        }

        static async Task CheckRandomCode(int threadNumber)
        {
            NitroSniperLib nitro = new();
            nitro.ProxiesFile = $"proxies_thread{threadNumber}.txt";
            await nitro.DownloadProxies();
            string proxy = await nitro.PickProxy();

            string giftCode = nitro.GetRandomNitroCode();
            while (true)
            {
                try
                {
                    string response = await nitro.CheckNitroCodeWithProxy(giftCode, proxy);
                    if (!response.Equals("invalid gift"))
                    {
                        Console.WriteLine($"Working code: {giftCode}\nWriting it to the file...");
                        await nitro.WriteCodeToFile(response);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid gift code! ({giftCode})");
                    }

                    giftCode = nitro.GetRandomNitroCode();
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Slow proxy, skipping...");
                    proxy = await nitro.PickProxy();
                    if (proxy.Equals("no proxies left"))
                    {
                        await nitro.DownloadProxies();
                        proxy = await nitro.PickProxy();
                    }
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    Console.WriteLine("Invalid proxy, skipping... (server refused the proxy)");
                    proxy = await nitro.PickProxy();
                    if (proxy.Equals("no proxies left"))
                    {
                        await nitro.DownloadProxies();
                        proxy = await nitro.PickProxy();
                    }
                }
                catch (UriFormatException)
                {
                    Console.WriteLine("Invalid proxy, skipping... (parsing url with proxy failed)");
                    proxy = await nitro.PickProxy();
                    if (proxy.Equals("no proxies left"))
                    {
                        await nitro.DownloadProxies();
                        proxy = await nitro.PickProxy();
                    }
                }
            }
        }
    }
}
