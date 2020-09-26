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

            if(rawResponse[0] == '{')
            {
                int index = rawResponse.IndexOf('}');

                string oobStr = rawResponse.Substring(0, index + 1);
                string message = rawResponse.Substring(index + 1);

                var oob = JsonConvert.DeserializeObject<ChatScriptResponse>(oobStr);
                
                Message = message.Trim();
                DialogueOptions = oob.DialogueOptions;
                NewFacts = oob.NewFacts;
            }
            else
            {
                Message = rawResponse;
            }
        }

        public string Message { get; set; }
        public string[] DialogueOptions { get; set; }
        public string[] NewFacts { get; set; }
    }
}
