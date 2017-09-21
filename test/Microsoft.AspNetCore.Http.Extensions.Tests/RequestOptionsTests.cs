// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Microsoft.AspNetCore.Http.Extensions
{
    public class RequestOptionsTests
    {
        [Fact]
        public async Task CanConfigureAsync()
        {
            var services = new ServiceCollection().AddRequestOptions();

            services.ConfigureAsync<TestOptions>((name, o, ctx) => {
                o.Context = ctx;
                return Task.FromResult(0);
            });

            var context = new DefaultHttpContext();
            context.RequestServices = services.BuildServiceProvider();

            var options = await context.GetOptionsAsync<TestOptions>(Options.DefaultName);
            Assert.Equal(context, options.Context);
        }

        public class TestOptions
        {
            public HttpContext Context { get; set; }
        }

    }
}
