using System;
using System.Collections.Generic;

namespace TelerikBlazorDemos.Shared
{
    public class StorageItem
    {
        public string Text { get; set; }

        public string Icon { get; set; }

        public bool Expanded { get; set; }

        public bool HasChildren => Items.Count > 0;

        public List<StorageItem> Items { get; set; }

        public StorageItem(string text, string icon)
        {
            Text = text;
            Icon = icon;

            Expanded = true;
            Items = new List<StorageItem>();
        }
    }

    public static class StorageItemExtensions
    {
        public static StorageItem FindRecursive(this List<StorageItem> storageItems, Func<StorageItem, bool> condition)
        {
            for (int i = 0; i < storageItems?.Count; i++)
            {
                var storageItem = storageItems[i];
                var matchedItem = storageItem.FindRecursive(condition);

                if (matchedItem != null)
                {
                    return matchedItem;
                }
            }

            return default;
        }

        public static StorageItem FindRecursive(this StorageItem storageItem, Func<StorageItem, bool> selector)
        {
            if (selector(storageItem) == true)
            {
                return storageItem;
            }

            for (int i = 0; i < storageItem.Items.Count; i++)
            {
                var item = storageItem.Items[i];

                if (selector(item))
                {
                    return item;
                }
                else
                {
                    var childItem = item.Items.FindRecursive(selector);

                    if (childItem != null)
                    {
                        return childItem;
                    }
                }
            }

            return default;
        }
    }
}
