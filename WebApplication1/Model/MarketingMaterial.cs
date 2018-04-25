using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Repository;

namespace WebApplication1.Model
{
    public class MarketingMaterial:IStorable
    {

        public MarketingMaterial()
        {
            id = Guid.NewGuid().ToString("N");// assign a new Globally Unique Identifier
        }
        public string id { get; set; }

        [Required(ErrorMessage = "Description field must be filled in.")]
        [Display(Description = "Description")]
        [StringLength(1000, MinimumLength = 5,
        ErrorMessage = "First Name must be longer than {2} characters and less than {1} characters.")]
        public string Description { get; set; }
        public String Resource { get; set; }
        public String Comments { get; set; }

        public Event AEvent { get; set; }

        public static MarketingMaterial Copy(MarketingMaterial mkt)
        {
            return new MarketingMaterial
            {
                Comments = mkt.Comments,
                Description = mkt.Description,
                Resource = mkt.Resource,
                AEvent = mkt.AEvent
            };
        }

    }
}