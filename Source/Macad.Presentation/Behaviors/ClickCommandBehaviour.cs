﻿using System.Windows;
using System.Windows.Input;

namespace Macad.Presentation;

public class ClickCommandBehavior : Behavior<FrameworkElement>
{
    public static DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ClickCommandBehavior), new UIPropertyMetadata(null));

    public ICommand Command
    {
        get { return (ICommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    public static DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ClickCommandBehavior), new UIPropertyMetadata(null));

    public object CommandParameter
    {
        get { return GetValue(CommandParameterProperty); }
        set { SetValue(CommandParameterProperty, value); }
    }

    public override void OnAttached(FrameworkElement target)
    {
        target.MouseLeftButtonDown += target_MouseLeftButtonDown;
    }

    public override void OnDetached(FrameworkElement target)
    {
        target.MouseLeftButtonDown -= target_MouseLeftButtonDown;
    }

    private void target_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (Command == null) return;

        if (e.ClickCount == 1)
        {
            Command.Execute(CommandParameter);
        }
    }

}