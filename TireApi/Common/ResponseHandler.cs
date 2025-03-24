namespace TireApi.Common
{
    public class ResponseHandler
    {
        public static ApiResponse GetExceptionResponse(Exception ex)
        {
            ApiResponse response = new ApiResponse();
            response.Code = "1";
            response.ResponseData = ex.Message;
            return response;
        }
        public static ApiResponse GetAppResponse(ResponseType type, object? contract)
        {
            ApiResponse response;
            response = new ApiResponse { ResponseData = contract };
            switch (type)
            {
                case ResponseType.Success:
                    response.Code = "0";
                    response.Message = "Success";

                    break;
                case ResponseType.NotFound:
                    response.Code = "2";
                    response.Message = "No record available";
                    break;
                case ResponseType.Failure:
                    response.Code = "3";
                    response.Message = "Failed";
                    break;
                case ResponseType.BadRequest:
                    response.Code = "4";
                    response.Message = "Bad Request";
                    break;
                case ResponseType.Forbidden:
                    response.Code = "5";
                    response.Message = "Forbidden";
                    break;
                case ResponseType.Created:
                    response.Code = "201";
                    response.Message = "Created";
                    break;
                case ResponseType.Deleted:
                    response.Code = "204";
                    response.Message = "Deleted";
                    break;

            }
            return response;
        }
    }
}
