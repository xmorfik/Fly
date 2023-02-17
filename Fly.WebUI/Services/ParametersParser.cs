using Cysharp.Web;
using Fly.WebUI.Interfaces;
using System.Text;

namespace Fly.WebUI.Services;

public class ParametersParser : IParametersParser
{
    public string? Parse(params object[] objects)
    {
        var result = new StringBuilder();
        foreach (var item in objects)
        {
            var obj = WebSerializer.ToQueryString(item).TrimStart('&') + '&';
            result.Append(obj);
        }
        return result.ToString().TrimEnd('&');
    }
}
