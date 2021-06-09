using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalGovtReporterAPI.Types;
using System;

namespace LocalGovtReporterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouncilMembers : ControllerBase
    {
        public async Task<IActionResult> GetCouncilMembersAsync(string jurisdiction)
        {
            AmazonDynamoDBClient client = Methods.AWS.GetDynamoDBClient();
            Table table = null;
            try
            {
                table = Table.LoadTable(client, "CouncilMember");
            }
            catch(Exception ex)
			{
                var x = ex.Message;
			}
            ScanFilter scanFilter = new ScanFilter();

            if (!string.IsNullOrEmpty(jurisdiction))
                scanFilter.AddCondition("Jurisdiction", ScanOperator.Equal, jurisdiction);

            ScanOperationConfig config = new ScanOperationConfig()
            {
                //Filter = scanFilter,
                CollectResults = true
            };

            Search search = table.Scan(config);

            var documents = await search.GetNextSetAsync();

            var councilmembers = documents.ToArray().ToList().Where(i => i["Jurisdiction"].ToString().Equals(jurisdiction, StringComparison.OrdinalIgnoreCase));

            string initialJSON = councilmembers.ToJson();

            // convert to structured object
            IEnumerable<CouncilMember> response = JsonConvert.DeserializeObject<List<CouncilMember>>(initialJSON);


            //var jObj = Newtonsoft.Json.Linq.JObject.Parse(JSON);
            //var formatted = jObj.ToString(Formatting.Indented);

            return Ok(response);

        }
    }
}