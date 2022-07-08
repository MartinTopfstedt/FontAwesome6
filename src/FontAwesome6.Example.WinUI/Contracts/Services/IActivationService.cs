using System.Threading.Tasks;

namespace FontAwesome6.Example.WinUI2.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
