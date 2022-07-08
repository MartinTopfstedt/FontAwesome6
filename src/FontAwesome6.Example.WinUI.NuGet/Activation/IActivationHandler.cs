using System.Threading.Tasks;

namespace FontAwesome6.Example.WinUI2.Activation;

public interface IActivationHandler
{
    bool CanHandle(object args);

    Task HandleAsync(object args);
}
