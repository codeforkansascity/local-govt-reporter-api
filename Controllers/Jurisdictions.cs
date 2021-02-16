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
    public class Jurisdictions : ControllerBase
    {
        public async Task<IActionResult> GetJurisdictionsAsync(string state, string county)
        {
            AmazonDynamoDBClient client = Methods.AWS.GetDynamoDBClient();

            Table table = Table.LoadTable(client, "Meeting");
            ScanFilter scanFilter = new ScanFilter();

            if (!string.IsNullOrEmpty(state))
                scanFilter.AddCondition("State", ScanOperator.Equal, state);
            if (!string.IsNullOrEmpty(county))
                scanFilter.AddCondition("County", ScanOperator.Equal, county);

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                AttributesToGet = new List<string> { "Jurisdiction" },
                Select = SelectValues.SpecificAttributes
            };

            Search search = table.Scan(config);

            var documents = await search.GetNextSetAsync();

            List<string> jurisdictions = new List<string>();
            foreach (Document document in documents)
            {
                foreach (var jurisdiction in document.Values)
                {
                    jurisdictions.Add(jurisdiction.AsString());
                }
            }

            return Ok(jurisdictions.ToList().Distinct());
        }
    }
}