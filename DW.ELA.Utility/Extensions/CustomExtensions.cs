using System;
using System.Collections.Generic;
using System.Linq;

public static class CustomExtensions
{
    public static IEnumerable<IEnumerable<TSource>> CustomBatch<TSource>(
        this IEnumerable<TSource> source, int batchSize)
    {
        TSource[] bucket = null;
        var count = 0;

        foreach (var item in source)
        {
            if (bucket == null)
                bucket = new TSource[batchSize];

            bucket[count++] = item;

            if (count != batchSize)
                continue;

            yield return bucket;

            bucket = null;
            count = 0;
        }

        if (bucket != null && count > 0)
            yield return bucket.Take(count).ToArray();
    }
}