using System.Collections.Concurrent;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETWebApiDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class RandomTextController
{
    const string AllowedChars = "0123456789ABCDEFGHIJKLMONPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private static List<string> randomStringHistory = new();


    [Route("/GetRandomText")]
    [HttpPost]
    public string PostText()
    {
        var rng = new Random();
        var sb = new StringBuilder();

        for (int i = 0; i < 20; i++)
        {
            sb.Append(AllowedChars[rng.Next(0, AllowedChars.Length)]);
        }

        randomStringHistory.Add(sb.ToString());

        return sb.ToString();
    }

    [Route("/GetStringHistory/{numberOfEntries:int}")]
    [HttpGet]
    public List<string> GetHistory([FromRoute]int  numberOfEntries)
    {
        if (numberOfEntries > randomStringHistory.Count) numberOfEntries = randomStringHistory.Count;
        
        return randomStringHistory.GetRange(0, numberOfEntries);
    }

}