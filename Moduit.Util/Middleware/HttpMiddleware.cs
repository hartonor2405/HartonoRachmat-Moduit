using Microsoft.AspNetCore.Http;
using Moduit.DTO.Response.BaseResponse;
using Newtonsoft.Json;
using System.Net;

namespace Moduit.Util.Middleware
{
    public class HttpMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {            
            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }               

            string resposeText = await GetResponse(context.Response);
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var baseResponseDto = new BaseResponseDto();
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected     
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            baseResponseDto.ErrorMessage = ex.Message;
            var result = JsonConvert.SerializeObject(baseResponseDto);
            return context.Response.WriteAsync(result);
        }

        private static async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            response.Headers["X-Content-Type-Options"] = "nosniff";
            response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            response.Headers["Pragma"] = "no-cache";
            response.Headers["x-frame-options"] = "SAMEORIGIN";
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}
