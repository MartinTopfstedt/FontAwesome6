using System;

namespace FontAwesome6.Example.WinUI2.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);
}
