using Newtonsoft.Json;
using System;

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

                // The OOB data ends with "} }" so need to add 3 to get thems last chars
                string oobStr = rawResponse.Substring(0, index + 3);
                string message = rawResponse.Substring(index + 3);

                var oob = JsonConvert.DeserializeObject<ChatScriptResponse>(oobStr);

                Messages = message.Trim().Split(new[] { "\\n" }, StringSplitOptions.None);
                DialogueOptions = oob.DialogueOptions;
                NewFacts = oob.NewFacts;
            }
            else
            {
                Messages = rawResponse.Split(new[] { "\\n" }, StringSplitOptions.None);
            }
        }

        public string[] Messages { get; set; }
        public string[] DialogueOptions { get; set; }
        public string[] NewFacts { get; set; }
    }
}
