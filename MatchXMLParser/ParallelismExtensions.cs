using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser
{
    public static class ParallelismExtensions
    {
        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> selector)
        {
            var matherialized = source.ToList();
            var maxDegreesOfParallelism = Environment.ProcessorCount; // TODO: Research partitioner in .NET
            var sortedResults = new TResult[matherialized.Count];

            var activeTasks = new HashSet<Task>();
            int itemIndex = 0;
            foreach (var item in matherialized)
            {
                // We need this copy of the index, when the tasks are so slow, 
                // that they are all not finished before the index had been finished 
                // before the last iteration when the regular index would be already 
                // fully incremented.  
                var threadSafeIndex = itemIndex;
                Task activeTask = Task.Factory.StartNew(() =>
                {
                    Task<TResult> originalTask = selector(item);
                    originalTask.Wait();
                    TResult result =  originalTask.Result;
                    sortedResults[threadSafeIndex] = result;
                });

                activeTasks.Add(activeTask);

                if (activeTasks.Count >= maxDegreesOfParallelism)
                {
                    var completed = await Task.WhenAny(activeTasks);
                    activeTasks.Remove(completed);
                }
                itemIndex++;
            }

            await Task.WhenAll(activeTasks);
            return sortedResults;
        }
    }
}
