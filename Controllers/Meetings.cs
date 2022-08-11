using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Newtonsoft.Json;
using LocalGovtReporterAPI.Types;

namespace LocalGovtReporterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Meetings : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMeetingsAsync(string state, string jurisdiction, string meetingType, string county, string tags, string startDate, string endDate, int start, int length, string sortBy = "MeetingDate", string sortDirection = "desc")
        {
            if (length > 1000)
            {
                return BadRequest("Items per page may not exceed 1000");
            }
            else
            {
                AmazonDynamoDBClient client = null;
                Table table = null;
                try
                {
                    client = Methods.AWS.GetDynamoDBClient();
                    table = Table.LoadTable(client, "Meeting");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
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

                int totalItems = documents.Count;
                int totalPages = (int)Math.Ceiling(totalItems / (decimal)length) - 1;

                // get unstructured JSON represention from database
                string initialJSON = documents.ToArray().ToList().ToJson();

                // convert to structured object
                IEnumerable<Meeting> response = JsonConvert.DeserializeObject<List<Meeting>>(initialJSON);

                // sort based on passed query string params
                response = response.AsQueryable().OrderBy(string.Format("{0} {1}", sortBy, sortDirection)).Skip(length * start).Take(length);

                var JSON = "{" + string.Format("currentPage:{0}, numberOfPages:{1}, itemsPerPage:{2}, totalItems:{3}, data:{4}", start, totalPages, length, totalItems, JsonConvert.SerializeObject(response)) + "}";
                var jObj = Newtonsoft.Json.Linq.JObject.Parse(JSON);
                var formatted = jObj.ToString(Formatting.Indented);

                return Ok(formatted);
            }
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