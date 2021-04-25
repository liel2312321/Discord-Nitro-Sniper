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
                "\nThe program uses proxies in order to avoid Rate Limit. The use and changing of proxies is done in the background." +
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
                    if (response.Contains(NitroSniperLib.CodeUrlFirst))
                    {
                        Console.WriteLine($"Working code: {giftCode}\nWriting it to the file...");
                        await nitro.WriteCodeToFile(response);
                    }
                    else if (response.Equals("rate limited"))
                    {
                        proxy = await nitro.PickProxy();
                        if (proxy.Equals("no proxies left"))
                        {
                            await nitro.DownloadProxies();
                            proxy = await nitro.PickProxy();
                        }
                    }
                    else if (response.Equals("invalid gift"))
                    {
                        Console.WriteLine($"Invalid gift code! ({giftCode})");
                    }
                    else
                    {
                        proxy = await nitro.PickProxy();
                        if (proxy.Equals("no proxies left"))
                        {
                            await nitro.DownloadProxies();
                            proxy = await nitro.PickProxy();
                        }
                    }

                    giftCode = nitro.GetRandomNitroCode();
                }
                catch (TaskCanceledException)
                {
                    proxy = await nitro.PickProxy();
                    if (proxy.Equals("no proxies left"))
                    {
                        await nitro.DownloadProxies();
                        proxy = await nitro.PickProxy();
                    }
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    proxy = await nitro.PickProxy();
                    if (proxy.Equals("no proxies left"))
                    {
                        await nitro.DownloadProxies();
                        proxy = await nitro.PickProxy();
                    }
                }
                catch (UriFormatException)
                {
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
