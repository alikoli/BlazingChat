using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingChat.Shared.Services;

public class UIStateService
{
    private string _title;

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            StateChanged?.Invoke();
        }
    }

    public event Action StateChanged;
}
