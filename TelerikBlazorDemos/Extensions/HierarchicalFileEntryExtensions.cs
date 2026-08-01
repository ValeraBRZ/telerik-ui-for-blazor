using System;
using System.Collections.Generic;
using System.Text;
using TelerikBlazorDemos.Models.FileManager;

namespace TelerikBlazorDemos.Extensions
{
    public static class HierarchicalFileEntryExtensions
    {
        public static HierarchicalFileEntry FindRecursive(this List<HierarchicalFileEntry> data, Func<HierarchicalFileEntry, bool> condition)
        {
            for (int i = 0; i < data?.Count; i++)
            {
                var item = data[i];
                var matchedItem = item.FindRecursive(condition);

                if (matchedItem != null)
                {
                    return matchedItem;
                }
            }

            return default;
        }

        public static HierarchicalFileEntry FindRecursive(this HierarchicalFileEntry dataItem, Func<HierarchicalFileEntry, bool> selector)
        {
            if (selector(dataItem) == true)
            {
                return dataItem;
            }

            for (int i = 0; i < dataItem.Items?.Count; i++)
            {
                var item = dataItem.Items[i];

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
