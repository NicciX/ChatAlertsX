using System;
using System.Collections.Generic;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace ChatAlertsX.Ipc;

internal sealed class ExternalPluginHandler
{
    private readonly IDalamudPluginInterface _pluginInterface;
    //private readonly IGameConfig _gameConfig;
    //private readonly Configuration _configuration;
    private readonly IPluginLog _pluginLog;
    private readonly WoLuaX _woLuaIpc;
    
    public ExternalPluginHandler(IDalamudPluginInterface pluginInterface, IPluginLog pluginLog)
    {
        _pluginInterface = pluginInterface;
        _pluginLog = pluginLog;
        _woLuaIpc = new WoLuaX(pluginInterface, pluginLog);
    }

    public bool Saved { get; private set; }

    public void Save()
    {
        if (Saved)
        {
            _pluginLog.Information("Not overwriting external plugin state");
            return;
        }

        _pluginLog.Information("Saving external plugin state...");
       Saved = true;
    }
    
    public bool UpdateChat(string msg, string sender, string chn, string match) => _woLuaIpc.UpdateChat(msg, sender, chn, match);

    public void RegisterFunctions() => _woLuaIpc.RegisterFunctions();
    

}
