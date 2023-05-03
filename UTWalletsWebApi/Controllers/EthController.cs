using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace UTWalletsWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EthController : ControllerBase
{
    private readonly ILogger<EthController> _logger;

    public EthController(ILogger<EthController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "get-balance")]
    public async Task GetBalance(string walletNumber)
    {
        var client = new HttpClient();
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://eth-mainnet.g.alchemy.com/v2/docs-demo"),
            Headers =
            {
                { "accept", "application/json" },
            },
            Content = new StringContent("{\"id\":1,\"jsonrpc\":\"2.0\",\"params\":[\"{walletNumber}\",\"latest\"],\"method\":\"eth_getBalance\"}")
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        Console.WriteLine(body);
        ;
    }
}