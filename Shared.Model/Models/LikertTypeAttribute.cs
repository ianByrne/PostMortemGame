using System;

namespace IanByrne.ResearchProject.Shared.Models
{
    [AttributeUsage(AttributeTargets.All)]
    public class LikertTypeAttribute : Attribute
    {
        public LikertTypeAttribute(LikertType likertType)
        {
            LikertType = likertType;
        }

        public LikertType LikertType { get; set; }
    }
}
