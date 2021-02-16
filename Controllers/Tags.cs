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
    public class Tags : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTagsAsync(string state, string county, string jurisdiction)
        {
            AmazonDynamoDBClient client = Methods.AWS.GetDynamoDBClient();

            Table table = Table.LoadTable(client, "Meeting");
            ScanFilter scanFilter = new ScanFilter();

            if (!string.IsNullOrEmpty(state))
                scanFilter.AddCondition("State", ScanOperator.Equal, state);
            if (!string.IsNullOrEmpty(jurisdiction))
                scanFilter.AddCondition("Jurisdiction", ScanOperator.Equal, jurisdiction);
            if (!string.IsNullOrEmpty(county))
                scanFilter.AddCondition("County", ScanOperator.Equal, county);

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                AttributesToGet = new List<string> { "Tags" },
                Select = SelectValues.SpecificAttributes
            };

            Search search = table.Scan(config);

            var documents = await search.GetNextSetAsync();

            List<string> tags = new List<string>();
            foreach (Document document in documents)
            {               
                foreach (var tag in document.Values)
                {
                    foreach (var t in tag.AsArrayOfString())
                    {
                        tags.Add(t);
                    }
                }
            }

            return Ok(tags.Distinct());
        }
    }
}