﻿using Microsoft.AspNetCore.Mvc;

namespace TireApi.Common
{
    public class ApiResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? ResponseData { get; set; }
    }
    public enum ResponseType
    {
        Success,
        Created,
        NotFound,
        Failure,
        BadRequest,
        Forbidden,
        Deleted
    }


}
