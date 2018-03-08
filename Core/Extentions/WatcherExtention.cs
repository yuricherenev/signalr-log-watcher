using LogWatcher.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class CacheWarmerExtensions
{
    public static void UseWatcher(this IApplicationBuilder app)
    {
        var watcher = app.ApplicationServices.GetRequiredService<IWatcherService>();
        watcher.Watch();
    }
}