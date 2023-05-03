using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace UTWalletsWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "get-balance")]
    public async Task GetBalance()
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
            Content = new StringContent("{\"id\":1,\"jsonrpc\":\"2.0\",\"params\":[\"0xe5cB067E90D5Cd1F8052B83562Ae670bA4A211a8\",\"latest\"],\"method\":\"eth_getBalance\"}")
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };
        
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        };
    }
}