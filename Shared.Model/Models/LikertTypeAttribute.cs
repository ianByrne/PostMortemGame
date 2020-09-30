using System;

namespace IanByrne.ResearchProject.Shared.Models
{
    [AttributeUsage(AttributeTargets.All)]
    public class LikertLabelsAttribute : Attribute
    {
        public string PreContent { get; set; }
        public string PostContent { get; set; }
    }
}
