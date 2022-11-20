using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using osu.Framework.IO.Stores;

namespace SpellCastSolver.Game;

public class DictionaryStore : IResourceStore<string[]>
{
    private readonly IResourceStore<byte[]> store;

    public DictionaryStore(IResourceStore<byte[]> resourceStore)
    {
        store = resourceStore;
    }

    public void Dispose()
    {
        store.Dispose();
    }

    private static IEnumerable<string> iterateLines(StreamReader reader)
    {
        while (!reader.EndOfStream)
        {
            yield return reader.ReadLine()!;
        }
    }

    public string[] Get(string name)
    {
        using Stream stream = store.GetStream(name);
        if (stream is null) return Array.Empty<string>();
        using StreamReader reader = new StreamReader(stream);
        return iterateLines(reader).ToArray();
    }

    public Task<string[]> GetAsync(string name, CancellationToken cancellationToken = new())
    {
        using Stream stream = store.GetStream(name);
        if (stream is null) return Task.FromResult(Array.Empty<string>());
        using StreamReader reader = new StreamReader(stream);
        return Task.FromResult(iterateLines(reader).ToArray());
    }

    public Stream GetStream(string name)
    {
        return store.GetStream(name);
    }

    public IEnumerable<string> GetAvailableResources()
    {
        return store.GetAvailableResources();
    }
}
