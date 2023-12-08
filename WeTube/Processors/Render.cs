using WeTube.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WeTube.Processors;

public class Render : IRender
{
    public IResult Component<TComponent>(object data)
    {
        var componentData = data.ToDictionary();

        return new RazorComponentResult(typeof(TComponent), componentData);
    }
}
