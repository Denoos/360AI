using System.Text.Json;
using System.Text.Json.Serialization;

namespace _360AI_BlazorApp.Components.Models
{
    public class OurModels
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string MediaType { get; set; }
        public double Relevance { get; set; }
        public string Source { get; set; }
    }
}
