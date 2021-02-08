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
    public class Meetings : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMeetingsAsync(string state, string jurisdiction, string meetingType, string county, string tags, string startDate, string endDate)
        {
            AmazonDynamoDBClient client = Methods.AWS.GetDynamoDBClient();

            Table table = Table.LoadTable(client, "Meeting");
            ScanFilter scanFilter = new ScanFilter();
            
            if (!string.IsNullOrEmpty(state))
                scanFilter.AddCondition("State", ScanOperator.Equal, state);
            if (!string.IsNullOrEmpty(jurisdiction))
                scanFilter.AddCondition("Jurisdiction", ScanOperator.Equal, jurisdiction);
            if (!string.IsNullOrEmpty(meetingType))
                scanFilter.AddCondition("MeetingType", ScanOperator.Equal, meetingType);
            if (!string.IsNullOrEmpty(county))
                scanFilter.AddCondition("County", ScanOperator.Equal, county);
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                scanFilter.AddCondition("MeetingDate", ScanOperator.Between, startDate, endDate);
            if (!string.IsNullOrEmpty(tags))
            {
                List<string> tagsList = tags.Split("|").ToList();

                foreach (string tag in tagsList)
                    scanFilter.AddCondition("Tags", ScanOperator.Contains, tag);
            }

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                CollectResults = true
            };

            Search search = table.Scan(config);

            var documents = await search.GetNextSetAsync();

            return Ok(documents.ToArray().ToJsonPretty());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetingAsync(string id)
        {
            AmazonDynamoDBClient client = Methods.AWS.GetDynamoDBClient();

            Table table = Table.LoadTable(client, "Meeting");
            Document document = await table.GetItemAsync(id);

            return Ok(document.ToJsonPretty());         
        }
    }
}