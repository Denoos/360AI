using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _360AI_API.Constrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextSearchController : ControllerBase
    {
        [HttpGet("text-search")]
        public object SearchByText(string searchText)
        {
            object result = "";
            return result;
        }
        [HttpGet("img-search")]
        public List<byte[]> SearchByImg(byte[] image)
        {
            return new List<byte[]>();
        }
    }
}
