﻿using Aether.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Aether.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ErrorHandlingMiddleware>();
        public static IApplicationBuilder UseGrafanaControllerMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<GrafanaControllersMiddleware>();
        public static IApplicationBuilder UseQualityCheckMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<QualityCheckMiddleware>();

        /// <summary>
        /// Adds Strict Transport Security and XSS Protection headers to responses
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="isDevelopment"></param>
        public static void UseSecurityHeaders(this IApplicationBuilder builder, bool isDevelopment)
        {
            if (!isDevelopment)
            {
                builder.UseHsts();
            }

            builder.UseHttpsRedirection();

            builder.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1");
                await next();
            });
        }
    }
}