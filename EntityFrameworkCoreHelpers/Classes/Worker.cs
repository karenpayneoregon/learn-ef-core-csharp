
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EntityFrameworkCoreHelpers.Classes
{

    public class Worker
    {
        public static void CompareValueTexture<T>(IQueryable<T> sender)
        {
            foreach (var item in sender)
            {
                if (item is IBase itemData)
                {
                    Debug.WriteLine(itemData.Id);
                }
            }
        }

        public static void CompareValueTexture<T>(List<T> sender) where T : class,IBase
        {
            foreach (var item in sender)
            {
                Debug.WriteLine(item.Id);

            }
        }

    }
}
