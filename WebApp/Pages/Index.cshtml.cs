using System;
using System.Linq;
using System.Net.Sockets;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared;
using IanByrne.ResearchProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IanByrne.ResearchProject.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PostMortemContext _context;

        public IndexModel(ILogger<IndexModel> logger, PostMortemContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }

        public PartialViewResult OnGetSurveyModalPartial()
        {
            return new PartialViewResult
            {
                ViewName = "_SurveyModalPartial",
                ViewData = new ViewDataDictionary<Survey>(ViewData, new Survey { })
            };
        }

        public PartialViewResult OnGetParticipantInformationModalPartial()
        {
            return new PartialViewResult
            {
                ViewName = "_ParticipantInformationModalPartial"
            };
        }

        public ActionResult OnPostSendMessageToChatScript(SendMessageRequest request)
        {
            var response = new SendMessageResponse();

            try
            {
                var tcpClient = new TcpClient("localhost", 1024);
                using (ITcpClient client = new TcpClientHandler(tcpClient))
                {
                    var chatScript = new ChatScriptHandler(client);

                    response = chatScript.SendMessage(request, _context);
                }
            }
            catch (Exception ex)
            {
                response.Messages = new string[] { ex.Message };
            }

            string responseJson = JsonConvert.SerializeObject(response);

            return Content(responseJson);
        }

        public ActionResult OnPostEnsureUserCreated(User user)
        {
            user.EnsureCreated(_context);

            return new NoContentResult();
        }

        public ActionResult OnPostGetUserFromCookieId(string id)
        {
            try
            {
                Guid idGuid;

                if (!Guid.TryParse(id, out idGuid))
                {
                    return new NotFoundResult();
                }

                var user = _context
                    .Users
                    .Include(u => u.Survey)
                    .Include(u => u.Objectives)
                    .SingleOrDefault(u => u.CookieId == idGuid);

                string responseJson = JsonConvert.SerializeObject(user, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                return Content(responseJson);
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        public ActionResult OnPostSaveUser(User user)
        {
            if (user != null)
            {
                user.Save(_context);
            }

            return new NoContentResult();
        }

        public ActionResult OnPostSetWinTime(User user)
        {
            if (user != null && user.WinDateTime == null)
            {
                user.WinDateTime = DateTime.UtcNow;
                user.Save(_context);
            }

            string userJson = JsonConvert.SerializeObject(user, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return Content(userJson);
        }

        public PartialViewResult OnPostSurveyModalPartial(Survey survey)
        {
            if(ModelState.IsValid)
            {
                var user = _context
                    .Users
                    .Include(u => u.Survey)
                    .SingleOrDefault(u => u.CookieId == survey.User.CookieId);

                if (user != null && user.Survey == null)
                {
                    user.Survey = survey;
                    _context.SaveChanges();
                }
            }

            return new PartialViewResult()
            {
                ViewName = "_SurveyModalPartial",
                ViewData = new ViewDataDictionary<Survey>(ViewData, survey)
            };
        }
    }
}
