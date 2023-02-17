namespace Fly.WebUI.Interfaces;

public interface IParametersParser
{
    public string? Parse(params object[] objects);
}
