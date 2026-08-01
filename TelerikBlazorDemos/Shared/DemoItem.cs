using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Shared
{
    public class DemoItem
    {
        [JsonIgnore]
        public string ComponentPath => string.IsNullOrEmpty(SubComponent) ? Component : SubComponent;

        public string Text { get; set; }

        public string Path { get; set; }

        public string BasePath { get; set; }

        public string Component { get; set; }

        public string Category { get; set; }

        public string SubComponent { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool ExcludeFromThemeBuilder { get; set; }

        [JsonIgnore]
        public bool EditableInThemeBuilder => !ExcludeFromThemeBuilder == true;

        public string Description { get; set; }

        public string SeoDescription { get; set; }

        public string SeoTitle { get; set; }

        public string ThemeBuilderButtonLink { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsNew { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsUpdated { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool ReleasedInMajorVersion { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Expanded { get; set; }

        public string SnippetId { get; set; }

        public string StagingSnippetId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool SkipReplSnippetGeneration { get; set; }

        public List<DemoSuggestedLink> Links { get; set; }

        public List<DemoItem> Items { get; set; } = new List<DemoItem>();

        public KeyboardNavigationDemoSettings KeyboardNavigationDemoSettings { get; set; }

        public DemoItem Parent { get; set; }

        public List<DemoFile> Files { get; set; } = new List<DemoFile>();
    }

    public class KeyboardNavigationDemoSettings
    {
        public string Selector { get; set; }
    }

    public class DemoSuggestedLink
    {
        public string Text { get; set; }

        public string Url { get; set; }
    }

    public class DemoFile
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }
    }

    public static class DemoItemExtensions
    {
        public static DemoItem FindRecursive(this IEnumerable<DemoItem> demos, Func<DemoItem, bool?> condition)
        {
            for (int i = 0; i < demos?.Count(); i++)
            {
                var demo = demos.ElementAt(i);
                var demoItem = demo.FindRecursive(condition);

                if (demoItem != null)
                {
                    return demoItem;
                }
            }

            return null;
        }

        public static DemoItem FindRecursive(this DemoItem demo, Func<DemoItem, bool?> condition)
        {
            if (condition(demo) == true)
            {
                return demo;
            }

            DemoItem result = null;

            demo.ForEachItem((demoItem, parentItem) =>
            {
                if (condition(demoItem) == true)
                {
                    result = demoItem;
                    return;
                }
            });

            return result;
        }

        public static void ForEachItem(this IEnumerable<DemoItem> demos, Action<DemoItem, DemoItem> action)
        {
            for (int i = 0; i < demos?.Count(); i++)
            {
                var demo = demos.ElementAt(i);
                demo.ForEachItem(action);
            }
        }

        public static void ForEachItem(this DemoItem demo, Action<DemoItem, DemoItem> action)
        {
            for (int i = 0; i < demo?.Items?.Count(); i++)
            {
                var demoItem = demo.Items[i];

                action(demoItem, demo);

                demoItem.ForEachItem(action);
            }
        }

        public static bool ExistsRecursive(this IEnumerable<DemoItem> demo, Func<DemoItem, bool?> predicate)
        {
            var item = demo.FindRecursive(predicate);
            return item != null;
        }

        public static bool IsExpanded(this DemoItem demo)
        {
            var hasExpandedParent = demo?.Parent == null ? true : demo?.Parent.IsExpanded() == true;
            return demo?.Expanded == true && hasExpandedParent;
        }

        public static string GetFileName(this DemoItem demo, string extension = ".razor")
        {
            var fileName = demo.Path?.Split("/")?.LastOrDefault() ?? "Index";
            return $"{fileName}{extension}";
        }
    }
}
