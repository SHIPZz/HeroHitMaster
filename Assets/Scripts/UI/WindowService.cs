using System.Collections.Generic;
using Enums;
using Services.Providers;

public class WindowService
{
    private readonly Dictionary<WindowTypeId, Window> _windows;
    private readonly Dictionary<WindowTypeId, Window> _hudWindows;

    public WindowService(WindowProvider windowProvider)
    {
        _windows = windowProvider.Windows;
        _hudWindows = windowProvider.HudWindows;
    }

    public void OpenHud()
    {
        Open(_hudWindows);
    }

    public void CloseHud()
    {
        Close(_hudWindows);
    }

    public void Open(WindowTypeId windowTypeId)
    {
        _windows[windowTypeId].Open();
    }

    public void Close(WindowTypeId windowTypeId)
    {
        _windows[windowTypeId].Close();
    }

    private void Open(Dictionary<WindowTypeId, Window> windows)
    {
        foreach (Window window in windows.Values)
        {
            window.Open();
        }
    }

    private void Close(Dictionary<WindowTypeId, Window> windows)
    {
        foreach (Window window in windows.Values)
        {
            window.Close();
        }
    }
}
