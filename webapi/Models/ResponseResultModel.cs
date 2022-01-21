using System;
namespace webapi.Models
{
	public class ResponseResultModel
	{
        public ResponseResultModel(bool success, string message, object responseData)
        {
            Success = success;
            Message = message;
            ResponseData = responseData;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object ResponseData { get; set; }
    }
}

