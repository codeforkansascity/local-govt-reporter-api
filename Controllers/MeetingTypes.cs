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
    public class MeetingTypes : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMeetingTypesAsync()
        {
            AmazonDynamoDBClient client = Methods.AWS.GetDynamoDBClient();

            Table table = Table.LoadTable(client, "Meeting");

            ScanOperationConfig config = new ScanOperationConfig()
            {
                AttributesToGet = new List<string> { "MeetingType" },
                Select = SelectValues.SpecificAttributes
            };

            Search search = table.Scan(config);

            var documents = await search.GetNextSetAsync();

            List<string> meetingTypes = new List<string>();
            foreach (Document document in documents)
            {
                foreach (var meetingType in document.Values)
                {
                    meetingTypes.Add(meetingType.AsString());
                }
            }

            return Ok(meetingTypes.ToList().Distinct());
        }
    }
}
