using System;
using System.Collections.Generic;

namespace Abp.Collections.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IList{T}"/>.
    /// 强类型列表的扩展方法
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Sort a list by a topological sorting, which consider their  dependencies
        /// 按依赖排序，
        /// </summary>
        /// <typeparam name="T">The type of the members of values. 类型</typeparam>
        /// <param name="source">A list of objects to sort 被排序的列表</param>
        /// <param name="getDependencies">Function to resolve the dependencies </param>
        /// <returns></returns>
        public static List<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {
            /* See: http://www.codeproject.com/Articles/869059/Topological-sorting-in-Csharp
             *      http://en.wikipedia.org/wiki/Topological_sorting
             */

            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>();

            foreach (var item in source)
            {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        /// <summary>
        /// 按依赖访问排序
        /// </summary>
        /// <typeparam name="T">The type of the members of values. 类型</typeparam>
        /// <param name="item">Item to resolve</param>
        /// <param name="getDependencies">Function to resolve the dependencies</param>
        /// <param name="sorted">List with the sortet items</param>
        /// <param name="visited">Dictionary with the visited items</param>
        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found!");
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
}
