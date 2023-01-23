using Newtonsoft.Json;
using PruebaTecnicaP2.Models.Dtos;
using PruebaTecnicaP2.Models.Enums;
using PruebaTecnicaP2.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.Helpers.Extensions
{
    public static class ResponseServiceExtensions
    {
        public static async Task<ResponseServiceDto<T>> GetResultSucces<T>(this ResponseServiceDto<T> responseServiceDto)
        {
            Messages messages = await Configuration(ResponseMessages.MESSAGE1);
            responseServiceDto.Code = TypeMessage.Succes;
            responseServiceDto.Message = messages.Message;
            return responseServiceDto;
        }

        public static async Task<ResponseServiceDto<T>> GetResultSucces<T>(this ResponseServiceDto<T> responseServiceDto, T param)
        {
            Messages messages = await Configuration(ResponseMessages.MESSAGE1);
            responseServiceDto.Code = TypeMessage.Succes;
            responseServiceDto.Message = messages.Message;
            responseServiceDto.Result = param;
            return responseServiceDto;
        }

        public static async Task<ResponseServiceDto<T>> GetResultError<T>(this ResponseServiceDto<T> responseServiceDto)
        {
            Messages messages = await Configuration(ResponseMessages.MESSAGE2);
            responseServiceDto.Code = TypeMessage.Error;
            responseServiceDto.Message = messages.Message;
            return responseServiceDto;
        }

        public static async Task<ResponseServiceDto<T>> GetResultError<T>(this ResponseServiceDto<T> responseServiceDto, T param)
        {
            Messages messages = await Configuration(ResponseMessages.MESSAGE2);
            responseServiceDto.Code = TypeMessage.Error;
            responseServiceDto.Message = messages.Message;
            responseServiceDto.Result = param;
            return responseServiceDto;
        }

        public static async Task<ResponseServiceDto<T>> GetResult<T, TMessage>(this ResponseServiceDto<T> responseServiceDto, TMessage responseMessages)
        {
            Messages messages = await Configuration(responseMessages);
            responseServiceDto.Code = messages.Type;
            responseServiceDto.Message = messages.Message;
            return responseServiceDto;
        }

        public static async Task<ResponseServiceDto<T>> GetResult<T, TMessage>(this ResponseServiceDto<T> responseServiceDto, TMessage responseMessages, T param)
        {
            Messages messages = await Configuration(responseMessages);
            responseServiceDto.Code = messages.Type;
            responseServiceDto.Message = messages.Message;
            responseServiceDto.Result = param;
            return responseServiceDto;
        }

        private static async Task<Messages> Configuration<TMessage>(TMessage responseMessagesEnum)
        {
            TMessage responseMessagesEnum2 = responseMessagesEnum;
            return JsonConvert.DeserializeObject<List<Messages>>(await File.ReadAllTextAsync(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + "\\Message.json"))!.FirstOrDefault((Messages x) => x.Code == Convert.ToInt32(responseMessagesEnum2))!;
        }
    }
}
