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
    public class States : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetStatesAsync()
        {
            AmazonDynamoDBClient client = Methods.AWS.GetDynamoDBClient();

            Table table = Table.LoadTable(client, "Meeting");

            ScanOperationConfig config = new ScanOperationConfig()
            {
                AttributesToGet = new List<string> { "State" },
                Select = SelectValues.SpecificAttributes
            };

            Search search = table.Scan(config);

            var documents = await search.GetNextSetAsync();

            List<string> states = new List<string>();
            foreach (Document document in documents)
            {
                foreach (var state in document.Values)
                {
                    states.Add(state.AsString());
                }
            }

            return Ok(states.ToList().Distinct());
        }
    }
}