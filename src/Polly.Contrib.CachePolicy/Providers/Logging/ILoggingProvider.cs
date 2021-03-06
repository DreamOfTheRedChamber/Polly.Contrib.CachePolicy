﻿using System;

using Polly;
using Polly.Contrib.CachePolicy.Models;

namespace Polly.Contrib.CachePolicy.Providers.Logging
{
    /// <summary>
    /// Provides the contract to logging <see cref="AsyncCachePolicy{TResult}"/> operations.
    /// </summary>
    /// <typeparam name="TResult">The type of return values this logging provider will handle.</typeparam>
    public interface ILoggingProvider<TResult>
        where TResult : CacheValue
    {
        /// <summary>
        /// Logs the metrics for any single cache get operation.
        /// </summary>
        /// <param name="key">The key against which the data is stored in the cache.</param>
        /// <param name="isSuccess">Whether the cache get operation succeeds without any exception.</param>
        /// <param name="isCacheHit">Whether there is a cache hit for the key.</param>
        /// <param name="isCacheFresh">Whether the cache hit is considered fresh for the key.</param>
        /// <param name="latencyInMilliSeconds">The overall latency for a cache get operation.</param>
        /// <param name="failureException">The failure exception case happened during the cache get operation.</param>
        /// <param name="context">The execution context.</param>
        void OnCacheGet(string key, bool isSuccess, bool isCacheHit, bool isCacheFresh, long latencyInMilliSeconds, Exception failureException, Context context);

        /// <summary>
        /// Logs the metrics for any single cache set operation.
        /// </summary>
        /// <param name="key">The key against which the data is stored in the cache.</param>
        /// <param name="isSuccess">Whether the cache put operation succeeds without any exception.</param>
        /// <param name="latencyInMilliSeconds">The overall latency for a cache put operation.</param>
        /// <param name="failureException">The failure exception case happened during the cache set operation.</param>
        /// <param name="context">The execution context.</param>
        void OnCacheSet(string key, bool isSuccess, long latencyInMilliSeconds, Exception failureException, Context context);

        /// <summary>
        /// Logs the metrics/traces for any single operation to get value from backend services.
        /// </summary>
        /// <param name="key">The key against which the data is stored in the cache.</param>
        /// <param name="isSuccess">Whether the fetch function finished without any exception and error responses.</param>
        /// <param name="isFallbackToCache">Whether falling back to cache is being triggered.</param>
        /// <param name="latencyInMilliSeconds">The overall latency for a single operation to fetch <see cref="TResult"/> from backend services with the option to fall back to cache.</param>
        /// <param name="delegateFailureOutcome">The captured outcome of a single failure operation to fetch <see cref="TResult"/> from backend services with the option to fall back to cache.</param>
        /// <param name="context">The execution context.</param>
        void OnBackendGet(string key, bool isSuccess, bool isFallbackToCache, long latencyInMilliSeconds, DelegateResult<TResult> delegateFailureOutcome, Context context);
    }
}
