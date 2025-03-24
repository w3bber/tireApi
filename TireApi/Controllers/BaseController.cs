using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireApi.Common;

namespace TireApi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult CreateResponse(ApiResponse response)
        {
            return response.Code switch
            {
                "0" => Ok(response), // 200 OK
                "1" => StatusCode(500, response), // 500 Internal Server Error
                "2" => NotFound(response), // 404 Not Found
                "3" => BadRequest(response), // 400 Bad Request
                "4" => StatusCode(403, response), // 403 Forbidden
                "5" => UnprocessableEntity(response), // 422 Unprocessable Entity
                "201" => StatusCode(201, response), // 201 Created
                "204" => NoContent(), // 204 No Content
                _ => StatusCode(500, response) // Unexpected case, return Internal Server Error
            };
        }
    }
}
