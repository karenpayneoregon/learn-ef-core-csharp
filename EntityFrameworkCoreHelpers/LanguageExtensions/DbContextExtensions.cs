﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreHelpers.LanguageExtensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Determine if a connection can be made asynchronously
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <returns>if a connection can be made</returns>
        public static async Task<bool> TestConnectionTask(this DbContext context) =>
            await Task.Run(async () => await context.Database.CanConnectAsync());

        /// <summary>
        /// Determine if a connection can be made asynchronously
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <param name="token">token which can be used to set the timeout</param>
        /// <returns>if a connection can be made</returns>
        public static async Task<bool> TestConnectionTask(this DbContext context, CancellationToken token) =>
            await Task.Run(async () => await context.Database.CanConnectAsync(token), token);

        /// <summary>
        /// Test connection with exception handling
        /// </summary>
        /// <param name="context"></param>
        /// <returns>if a connection can be made</returns>
        public static (bool success, Exception exception) CanConnect(this DbContext context)
        {
            try
            {
                return (context.Database.CanConnect(), null);
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }

        /// <summary>
        /// Test connection with exception handling
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <returns></returns>
        public static async Task<(bool success, Exception exception)> CanConnectAsync(this DbContext context)
        {
            try
            {
                var result = await Task.Run(async () => await context.Database.CanConnectAsync());
                return (result, null);
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }
    }
}
