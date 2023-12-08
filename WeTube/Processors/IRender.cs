
namespace WeTube.Processors;

public interface IRender
{
    IResult Component<TComponent>(object data);
}
