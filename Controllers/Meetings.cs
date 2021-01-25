using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalGovtReporterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Meetings : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMeetingsAsync(string state, string jurisdiction, string county, string startDate, string endDate)
        {
            var credentials = new BasicAWSCredentials("accessKey", "secretKey");
            var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);

            Table table = Table.LoadTable(client, "Meeting");
            ScanFilter scanFilter = new ScanFilter();

            if (!string.IsNullOrEmpty(state)) 
                scanFilter.AddCondition("State", ScanOperator.Equal, state);
            if (!string.IsNullOrEmpty(jurisdiction))
                scanFilter.AddCondition("Jurisdiction", ScanOperator.Equal, jurisdiction);
            if (!string.IsNullOrEmpty(county))
                scanFilter.AddCondition("County", ScanOperator.Equal, county);
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                scanFilter.AddCondition("MeetingDate", ScanOperator.Between, startDate, endDate);

            ScanOperationConfig config = new ScanOperationConfig()
            {
                AttributesToGet = new List<string> { "MeetingID", "MeetingDate", "Jurisdiction", "State", "County", "MeetingType" },
                Select = SelectValues.SpecificAttributes,
                Filter = scanFilter
            };

            Search search = table.Scan(config);

            var documents = await search.GetNextSetAsync();

            return Ok(documents.ToArray().ToJsonPretty());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetingAsync(string id)
        {
            var credentials = new BasicAWSCredentials("accessKey", "secretKey");
            var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);

            Table table = Table.LoadTable(client, "Meeting");
            Document document = await table.GetItemAsync(id);

            return Ok(document.ToJsonPretty());
        }
    }
}