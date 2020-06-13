using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FarmAppServer.Extantions
{
    public static class DataHelper
    {
        public static bool TryParseJson<T>(this string @this, out T result, out string errorMsg)
        {
            bool success = true;
            errorMsg = string.Empty;
            var exception = string.Empty;

            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) =>
                {
                    success = false;
                    args.ErrorContext.Handled = true;
                    exception = args.ErrorContext.Error.InnerException?.Message ?? args.ErrorContext.Error.Message;
                },
                MissingMemberHandling = MissingMemberHandling.Error
            };
            
            result = JsonConvert.DeserializeObject<T>(@this, settings);
            errorMsg = exception;
            return success;
        }

        public static bool IsValidJson(this string input, out string errorMsg)
        {
            errorMsg = string.Empty;
            input = input.Trim();
            if ((input.StartsWith("{") && input.EndsWith("}")) || //For object
                (input.StartsWith("[") && input.EndsWith("]"))) //For array
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(input))
                        throw new Exception("Пустой параметр!");

                    //parse the input into a JObject
                    var jObject = JObject.Parse(input);

                    foreach (var jo in jObject)
                    {
                        string name = jo.Key;
                        JToken value = jo.Value;

                        //if the element has a missing value, it will be Undefined - this is invalid
                        if (value.Type == JTokenType.Undefined)
                        {
                            throw new Exception(JTokenType.Undefined.ToString());
                        }
                    }
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    errorMsg = jex.InnerException?.Message ?? jex.Message;
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    errorMsg = ex.InnerException?.Message ?? ex.Message;
                    return false;
                }
            }
            else
            {
                errorMsg = "Неверный формат Json!";
                return false;
            }
            return true;
        }
    }
}
