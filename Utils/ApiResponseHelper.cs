using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampeCrud.Utils
{
    public class ApiResponseHelper
    {
        public static ApiResponse<T> Success<T>(T data)
        {
            return new ApiResponse<T>
            {
                Status = "success",
                Data = data
            };
        }

        public static ApiResponse<T> Failed<T>(string error)
        {
            return new ApiResponse<T>
            {
                Status = "failed",
                Data = default!,
                Error = error
            };
        }
    }
}