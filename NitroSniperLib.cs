using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace DiscordNitroSniper
{
    class NitroSniperLib
    {
        private const string _proxiesUrl = "https://api.proxyscrape.com/v2/?request=getproxies&protocol=http&timeout=5000&country=all&ssl=all&anonymity=all&simplified=true";
        private const string _codeCheckUrlFirst = "https://discord.com/api/v8/entitlements/gift-codes/";
        private const string _codeCheckUrlLast = "?with_application=false&with_subscription_plan=true";
        private const string _codeUrlFirst = "https://discord.gift/";
        private string _proxiesFile = "proxies.txt";

        public static string ProxiesUrl
        {
            get
            {
                return _proxiesUrl;
            }
        }

        public static string CodeCheckUrlFirst
        {
            get
            {
                return _codeCheckUrlFirst;
            }
        }

        public static string CodeCheckUrlLast
        {
            get
            {
                return _codeCheckUrlLast;
            }
        }

        public string ProxiesFile
        {
            get
            {
                return _proxiesFile;
            }
            set
            {
                _proxiesFile = value;
            }
        }

        public static string CodeUrlFirst
        {
            get
            {
                return _codeUrlFirst;
            }
        }

        public async Task DownloadProxies()
        {
            using WebClient client = new();
            await client.DownloadFileTaskAsync(new Uri(ProxiesUrl), ProxiesFile);
        }

        public async Task<string> PickProxy()
        {
            string proxyString = await File.ReadAllTextAsync(ProxiesFile);
            List<string> proxies = new();
            proxies.AddRange(proxyString.Split("\n"));
            Random random = new();
            string picked_proxy = proxies[random.Next(proxies.Count)];

            if (picked_proxy.Equals(""))
                return "no proxies left";

            proxies.Remove(picked_proxy);
            await File.WriteAllLinesAsync(ProxiesFile, proxies);
            return picked_proxy;
        }

        public string GetRandomNitroCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new();

            string nitroCode = "";
            for (int i = 0; i < 16; i++)
            {
                nitroCode += chars[random.Next(62)];
            }
            return nitroCode;
        }

        public async Task<string> CheckNitroCodeWithProxy(string giftCode, string proxy)
        {
            HttpClientHandler clientHandler = new()
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                Proxy = new WebProxy(new Uri($"http://{proxy}"))
            };

            HttpResponseMessage response;
            using (HttpClient client = new(clientHandler))
            {
                client.Timeout = TimeSpan.FromSeconds(3);
                response = await client.GetAsync(CodeCheckUrlFirst + giftCode + CodeCheckUrlLast);
            }

            if (response.StatusCode.Equals(200))
            {
                return CodeUrlFirst + giftCode;
            }
            else
            {
                return "invalid gift";
            }
        }

        public async Task<string> CheckNitroCodeWithoutProxy(string giftCode)
        {
            HttpResponseMessage response;
            using (HttpClient client = new())
            {
                response = await client.GetAsync(CodeCheckUrlFirst + giftCode + CodeCheckUrlLast);
            }

            if (response.StatusCode.Equals(200))
            {
                return CodeUrlFirst + giftCode;
            }
            else
            {
                return "invalid gift";
            }
        }

        public async Task WriteCodeToFile(string giftCode)
        {
            await File.AppendAllTextAsync("working_codes.txt", giftCode + "\n");
        }
    }
}
