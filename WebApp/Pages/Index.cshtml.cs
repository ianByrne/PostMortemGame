using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared;
using IanByrne.ResearchProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IanByrne.ResearchProject.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<ActionResult> OnPostSendMessageToChatScript(SendMessageRequest request)
        {
            var response = new SendMessageResponse();

            try
            {
                var tcpClient = new TcpClient("localhost", 1024);
                using (ITcpClient client = new TcpClientHandler(tcpClient))
                {
                    var chatScript = new ChatScriptHandler(client);

                    response = chatScript.SendMessage(request);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            string responseJson = JsonConvert.SerializeObject(response);

            return Content(responseJson);
        }

        public async Task<ActionResult> OnPostEnsureUserCreated(User user)
        {
            user.EnsureCreated();

            return new NoContentResult();
        }

        public async Task<ActionResult> OnPostGetUserFromCookieId(string id)
        {
            try
            {
                Guid idGuid;

                if(!Guid.TryParse(id, out idGuid))
                {
                    return new NotFoundResult();
                }

                using (var db = new PostMortemContext())
                {
                    var user = db.Users.SingleOrDefault(u => u.CookieId == idGuid);

                    string responseJson = JsonConvert.SerializeObject(user);
                    
                    return Content(responseJson);
                }
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}
