using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("api/StringGenerator")]
    [ApiController]
    public class StringGeneratorController : Controller
    {
        // GET: /<controller>/
        /*public IActionResult Index()
        {
            return View();
        }*/
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateUniqueStrings(int count)
        {
            if (count <= 0)
            {
                return BadRequest("Invalid input. Please provide a proper input count.");
            }

            var uniqueStrings = await GenerateUniqueStringsAsync(count);
            var result = new ApiResult(uniqueStrings.ToList());
            return Ok(result);
        }

        private async Task<ConcurrentBag<string>> GenerateUniqueStringsAsync(int count)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var uniqueStrings = new ConcurrentBag<string>();
            var generatedSet = new HashSet<string>();

            var tasks = Enumerable.Range(0, count).Select(async i =>
            {
                string generatedString;

                do
                {
                    generatedString = new string(Enumerable.Repeat(characters, MaxStringLength)
                        .Select(s => s[new Random().Next(s.Length)]).ToArray());
                }
                while (!generatedSet.Add(generatedString)); // Keep generating until it's unique

                uniqueStrings.Add(generatedString);
            });

            await Task.WhenAll(tasks);
            return uniqueStrings;
        }

        private const int MaxStringLength = 5;
    }
    public class ApiResult
    {
        public List<string> UniqueStrings { get; }
        public int Count => UniqueStrings.Distinct().Count();
        public ApiResult(List<string> strings)
        {
            UniqueStrings = strings;
        }
    }
}

