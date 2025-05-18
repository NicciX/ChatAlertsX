using ImGuiNET;

namespace ChatAlertsX.Gui
{
    public static partial class ImGuiCustom
    {
        public static void HoverTooltip(string text)
        {
            if (ImGui.IsItemHovered())
                ImGui.SetTooltip(text);
        }
    }
}
