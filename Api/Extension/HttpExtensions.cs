using System.Text.Json;
using Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace Api.Extension
{
    public static class HttpExtensions
    {
        public static void AddPageinationHeaders(this HttpResponse response,int currnetPage,int itemPerPage,
        int totalItem,int totalPage)
        {
            var pageinationHeader = new PageinationHeader(currnetPage,itemPerPage,totalItem,totalPage);
            var option = new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pageination",JsonSerializer.Serialize(pageinationHeader,option));
            response.Headers.Add("Access-Control-Expose-Headers","Pageination");

        }
        
    }
}