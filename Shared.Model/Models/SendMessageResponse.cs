using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace IanByrne.ResearchProject.Shared.Models
{
    public class SendMessageResponse
    {
        public SendMessageResponse()
        {
            //
        }

        public SendMessageResponse(string rawResponse)
        {
            rawResponse = rawResponse.Trim();

            if(rawResponse[0] == '[')
            {
                int index = rawResponse.IndexOf(']');

                string oob = rawResponse.Substring(0, index + 1);
                string message = rawResponse.Substring(index + 1);

                Message = message.Trim();
                DialogueOptions = JsonConvert.DeserializeObject<string[]>(oob);
            }
            else
            {
                Message = rawResponse;
            }
        }

        public string Message { get; set; }
        public string[] DialogueOptions { get; set; }
        public List<Objective> Objectives { get; set; }
        public List<string> Facts { get; set; }
    }
}
