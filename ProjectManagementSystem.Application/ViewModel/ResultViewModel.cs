using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.ViewModel
{
    public class ResultViewModel<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public static ResultViewModel<T> Sucess(T data, string message = "success operation")
        {
            return new ResultViewModel<T>
            {
                StatusCode = 200,
                Data = data,
                Message = message,
            };
        }

        public static ResultViewModel<T> Faliure(string message = "invalid operation")
        {
            return new ResultViewModel<T>
            {
                StatusCode = 400,
                Data = default,
                Message = message,
            };
        }
    }
}
