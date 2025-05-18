using System.Linq;
using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;
using Dalamud.Plugin.Ipc.Exceptions;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using DotNext.Reflection;
using DotNext.Collections.Generic;





namespace ChatAlertsX.Ipc;

public class WoLuaX
{
    //private readonly DalamudReflector _dalamudReflector;

    private readonly ICallGateSubscriber<string, string, string, string, bool?> _newChat;
    private readonly ICallGateSubscriber<bool?> _newRegisterFunctions;
    private readonly IPluginLog _pluginLog;
    public WoLuaX(IDalamudPluginInterface pluginInterface, IPluginLog pluginLog)
    {
        _pluginLog = pluginLog;
        //_newChat = pluginInterface.GetIpcSubscriber<string, bool?>("PandorasBox.GetFeatureEnabled");
        
        _newChat = pluginInterface.GetIpcSubscriber<string, string, string, string, bool?>("WoLuaX.UpdateChat");
        _newRegisterFunctions = pluginInterface.GetIpcSubscriber<bool?>("WoLuaX.RegisterFunctions");
    }

    public bool UpdateChat(string msg, string sender, string chn, string match)
    {
        try
        {
            var result = _newChat.InvokeFunc(msg, sender, chn, match);
            return true;
        }
        catch (IpcError)
        {
            _pluginLog.Information($"WoLuaX not loaded yet..");
            return false;
        }
     }
    public static void UpdChat(string msg, string sender, string chn, string match)
    {
        if (string.IsNullOrEmpty(msg) || chn == "Echo" || string.IsNullOrEmpty(chn))
            return;
        var x = new WoLuaX(Moon.PluginInterface, Moon.Log);
        x.UpdateChat(msg, sender, chn, match);
        //UpdateChat(msg, sender, chn);
        //return true;
    }
    public void RegisterFunctions()
    {
        try
        {
            var result = _newRegisterFunctions.InvokeFunc();
            _pluginLog.Information($"WoLua Ipc: RegisterFunctions(): {result}");
        }
        catch (IpcError)
        {
            _pluginLog.Information($"WoLuaX not loaded yet..");
        }
    }
}