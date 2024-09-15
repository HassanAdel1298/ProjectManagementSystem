using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO
{
    public class ResultDTO<T>
    {

        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public static ResultDTO<T> Sucess(dynamic data, string message = "Success Operation")
        {
            return new ResultDTO<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        public static ResultDTO<T> Faliure(string message = "Invalid Operation")
        {
            return new ResultDTO<T>
            {
                IsSuccess = false,
                Data = default,
                Message = message,
            };
        }

    }
}
