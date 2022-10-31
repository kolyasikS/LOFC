using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL.UIProcess
{
    public static class VisibilityPageELement
    {
        public static List<Label> initLabelList(string UID, StackPanel mainStacklPanel)
        {
            List<Label> labelList = new List<Label>();

            for (int i = 0; i < mainStacklPanel.Children.Count; i++)
            {
                if (mainStacklPanel.Children[i] is DockPanel)
                {
                    DockPanel dockPanel = (DockPanel)mainStacklPanel.Children[i];
                    for (int j = 0; j < dockPanel.Children.Count; j++)
                    {
                        if (dockPanel.Children[j] is StackPanel)
                        {
                            StackPanel stackPanel = (StackPanel)dockPanel.Children[j];
                            for (int k = 0; k < stackPanel.Children.Count; k++)
                            {
                                if (stackPanel.Children[k] is Label && stackPanel.Children[k].Uid == UID)
                                {
                                    labelList.Add((Label)stackPanel.Children[k]);
                                }
                            }
                        }
                    }
                }
            }

            return labelList;
        }
        public static List<UIElement> initMainBoxList(string UID, Grid grid)
        {
            List<UIElement> childrenList = new List<UIElement>();

            for (int i = 0; i < grid.Children.Count; i++)
            {
                if (grid.Children[i].Uid == UID)
                {
                    childrenList.Add(grid.Children[i]);
                }
            }

            return childrenList;
        }
        public static List<UIElement> initBoxList(string UID, StackPanel mainStacklPanel)
        {
            List<UIElement> childrenList = new List<UIElement>();

            for (int i = 0; i < mainStacklPanel.Children.Count; i++)
            {
                if (mainStacklPanel.Children[i] is DockPanel)
                {
                    DockPanel dockPanel = (DockPanel)mainStacklPanel.Children[i];
                    for (int j = 0; j < dockPanel.Children.Count; j++)
                    {
                        if (dockPanel.Children[j] is StackPanel)
                        {
                            StackPanel stackPanel = (StackPanel)dockPanel.Children[j];
                            for (int k = 0; k < stackPanel.Children.Count; k++)
                            {
                                if ((stackPanel.Children[k] is Canvas || stackPanel.Children[k] is ComboBox || stackPanel.Children[k] is TextBox) &&
                                     stackPanel.Children[k].Uid == UID)
                                {
                                    childrenList.Add(stackPanel.Children[k]);
                                }
                            }
                        }
                    }
                }
            }

            return childrenList;
        }
        public static void SetVisibility(List<Label> labels, Visibility LabelVis, List<UIElement> children, Visibility ChildVis)
        {
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Visibility = LabelVis;
            }
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Visibility = ChildVis;
            }
        }
        public static void SetVisibility(List<UIElement> children, Visibility ChildVis)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Visibility = ChildVis;
            }
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Visibility = ChildVis;
            }
        }
        public static void SetVisibility(UIElement element, Visibility visibility)
        {
            element.Visibility = visibility;
        }

    }
}
