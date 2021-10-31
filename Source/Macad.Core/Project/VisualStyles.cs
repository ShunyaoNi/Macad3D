﻿using System;
using System.Linq;
using Macad.Common;
using Macad.Occt;

namespace Macad.Core
{
    public enum PresentationMode
    {
        Wireframe,
        Solid,
        SolidWithBoundary
    }

    //--------------------------------------------------------------------------------------------------

    public enum LineStyle
    {
        Solid,
        Dash,
        ShortDash,
        Dot,
        DotDash
    }

    //--------------------------------------------------------------------------------------------------

    public enum FillMode
    {
        None,
        Solid
    }

    //--------------------------------------------------------------------------------------------------

    public enum LineThickness
    {
        Thin,
        Normal,
        Thick,
    }

    //--------------------------------------------------------------------------------------------------
    
    public static class Colors
    {
        public static readonly Quantity_Color Selection = Quantity_NameOfColor.Quantity_NOC_ANTIQUEWHITE.ToColor();
        public static readonly Quantity_Color Highlight = Quantity_NameOfColor.Quantity_NOC_GOLDENROD2.ToColor();
        public static readonly Quantity_Color FilteredSubshapes = Quantity_NameOfColor.Quantity_NOC_DEEPSKYBLUE2.ToColor();
        public static readonly Quantity_Color FilteredSubshapesHot = Quantity_NameOfColor.Quantity_NOC_RED.ToColor();
        public static readonly Quantity_Color Ghost = Quantity_NameOfColor.Quantity_NOC_LIGHTGRAY.ToColor();
        public static readonly Quantity_Color Auxillary = Quantity_NameOfColor.Quantity_NOC_GRAY25.ToColor();
        public static readonly Quantity_Color BallMarker = Quantity_NameOfColor.Quantity_NOC_YELLOW.ToColor();
        public static readonly Quantity_Color AttributeMarkerBackground = new Color(0.2f, 0.3f, 0.6f).ToQuantityColor();
        public static readonly Quantity_Color AttributeMarkerSelection = new Color(0.7f, 0.3f, 0.3f).ToQuantityColor();
        public static readonly Quantity_Color SketchEditorSegments = Quantity_NameOfColor.Quantity_NOC_WHITE.ToColor();
        public static readonly Quantity_Color SketchEditorHighlight = Quantity_NameOfColor.Quantity_NOC_GOLDENROD2.ToColor();
        public static readonly Quantity_Color SketchEditorSelection = Quantity_NameOfColor.Quantity_NOC_RED.ToColor();
        public static readonly Quantity_Color SketchEditorCreating = Quantity_NameOfColor.Quantity_NOC_GOLDENROD2.ToColor();
        public static readonly Quantity_Color SketchEditorAuxillary = Quantity_NameOfColor.Quantity_NOC_DEEPSKYBLUE3.ToColor();
    }

    //--------------------------------------------------------------------------------------------------

    public static class StyleHelper
    {
        #region Collections

        public struct LineStyleDescription
        {
            public LineStyle Style { get; }
            public string Name { get; }
            public double[] Pattern { get; }

            public LineStyleDescription(LineStyle style, string name, double[] pattern)
            {
                Style = style;
                Name = name;
                Pattern = pattern;
            }
        }

        public static readonly LineStyleDescription[] LineStyleDescriptions =
        {
            new (LineStyle.Solid, "Solid", new []{10.0, 0.0}),
            new (LineStyle.Dash, "Dash", new []{4.0, 2.0}),
            new (LineStyle.ShortDash, "Short Dash", new []{2.0, 2.0}),
            new (LineStyle.Dot, "Dot", new []{1.0, 1.0}),
            new (LineStyle.DotDash, "Dot-Dash", new []{5.0, 2.0, 1.0, 2.0}),
        };

        //--------------------------------------------------------------------------------------------------

        public struct LineThicknessDescription
        {
            public LineThickness Thickness { get; }
            public string Name { get; }
            public double Width { get; }

            public LineThicknessDescription(LineThickness thickness, string name, double width)
            {
                Thickness = thickness;
                Name = name;
                Width = width;
            }
        }

        public static readonly LineThicknessDescription[] LineThicknessDescriptions =
        {
            new (LineThickness.Thin, "Thin", 1.0),
            new (LineThickness.Normal, "Normal", 2.0),
            new (LineThickness.Thick, "Thick", 3.0),
        };

        //--------------------------------------------------------------------------------------------------

        #endregion

        #region Extensions
        
        //--------------------------------------------------------------------------------------------------

        public static double[] Pattern(this LineStyle lineStyle)
        {
            return LineStyleDescriptions[(int) lineStyle].Pattern;
        }

        //--------------------------------------------------------------------------------------------------

        public static Aspect_TypeOfLine TypeOfLine(this LineStyle lineStyle)
        {
            return lineStyle switch
            {
                LineStyle.Solid => Aspect_TypeOfLine.Aspect_TOL_SOLID,
                LineStyle.Dash => Aspect_TypeOfLine.Aspect_TOL_DASH,
                LineStyle.ShortDash => Aspect_TypeOfLine.Aspect_TOL_USERDEFINED,
                LineStyle.Dot => Aspect_TypeOfLine.Aspect_TOL_DOT,
                LineStyle.DotDash => Aspect_TypeOfLine.Aspect_TOL_DOTDASH,
                _ => Aspect_TypeOfLine.Aspect_TOL_SOLID
            };
        }

        //--------------------------------------------------------------------------------------------------

        public static void ApplyToAspect(this LineStyle lineStyle, Prs3d_LineAspect aspect)
        {
            var tol = lineStyle switch
            {
                LineStyle.Solid => Aspect_TypeOfLine.Aspect_TOL_SOLID,
                LineStyle.Dash => Aspect_TypeOfLine.Aspect_TOL_DASH,
                LineStyle.ShortDash => Aspect_TypeOfLine.Aspect_TOL_USERDEFINED,
                LineStyle.Dot => Aspect_TypeOfLine.Aspect_TOL_DOT,
                LineStyle.DotDash => Aspect_TypeOfLine.Aspect_TOL_DOTDASH,
                _ => Aspect_TypeOfLine.Aspect_TOL_SOLID
            };
            
            aspect.SetTypeOfLine(tol);

            if (tol == Aspect_TypeOfLine.Aspect_TOL_USERDEFINED)
            {
                UInt16 pattern = lineStyle switch
                {
                    LineStyle.ShortDash => 0xf8f8,
                    _ => 0xffff
                };
                aspect.Aspect().SetLinePattern(pattern);
            }
        }

        //--------------------------------------------------------------------------------------------------

        public static double LineWidth(this LineThickness thickness)
        {
            return LineThicknessDescriptions[(int) thickness].Width;
        }

        #endregion
    }

    //--------------------------------------------------------------------------------------------------

}