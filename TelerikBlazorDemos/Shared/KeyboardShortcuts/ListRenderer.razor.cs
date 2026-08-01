using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor.Components.Common;

namespace TelerikBlazorDemos.Shared.KeyboardShortcuts
{
    public partial class ListRenderer: ComponentBase
    {
        [Parameter]
        public DemoItem Demo { get; set; }

        private readonly string baseNamespace = "TelerikBlazorDemos.Shared.KeyboardShortcuts.";

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            string ns = $"{baseNamespace}{Demo.Component.Replace(" ", String.Empty)}List, TelerikBlazorDemos";
            builder.OpenComponent(0, Type.GetType(ns));
            builder.CloseComponent();

        }
    }
}
