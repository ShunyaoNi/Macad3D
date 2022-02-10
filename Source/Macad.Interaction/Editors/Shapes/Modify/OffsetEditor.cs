﻿using Macad.Core.Shapes;
using Macad.Interaction.Panels;

namespace Macad.Interaction.Editors.Shapes;

public class OffsetEditor : Editor<Offset>
{
    OffsetPropertyPanel _Panel;

    //--------------------------------------------------------------------------------------------------

    public override void Start()
    {
        _Panel = PropertyPanel.CreatePanel<OffsetPropertyPanel>(Entity);
        InteractiveContext.Current.PropertyPanelManager?.AddPanel(_Panel, PropertyPanelSortingKey.Shapes);
    }

    //--------------------------------------------------------------------------------------------------

    public override void Stop()
    {
        InteractiveContext.Current.PropertyPanelManager?.RemovePanel(_Panel);
    }

    //--------------------------------------------------------------------------------------------------

    [AutoRegister]
    internal static void Register()
    {
        RegisterEditor<OffsetEditor>();
    }

    //--------------------------------------------------------------------------------------------------

}