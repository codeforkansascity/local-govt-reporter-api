using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalGovtReporterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Counties : ControllerBase
    {
        public async Task<IActionResult> GetCountiesAsync()
        {
            AmazonDynamoDBClient client = Methods.AWS.GetDynamoDBClient();

            Table table = Table.LoadTable(client, "Meeting");

            ScanOperationConfig config = new ScanOperationConfig()
            {
                AttributesToGet = new List<string> { "County" },
                Select = SelectValues.SpecificAttributes
            };

            Search search = table.Scan(config);

            var documents = await search.GetNextSetAsync();

            List<string> counties = new List<string>();
            foreach (Document document in documents)
            {
                foreach (var county in document.Values)
                {
                    counties.Add(county.AsString());
                }
            }

            return Ok(counties.ToList().Distinct());
        }
    }
}