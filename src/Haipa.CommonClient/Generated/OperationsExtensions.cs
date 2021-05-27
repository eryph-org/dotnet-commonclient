// <auto-generated>
// MIT
// </auto-generated>

namespace Haipa.CommonClient
{
    using Haipa.ClientRuntime;
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for Operations.
    /// </summary>
    public static partial class OperationsExtensions
    {
            /// <summary>
            /// Get a operation
            /// </summary>
            /// <remarks>
            /// Get a operation
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='logTimeStamp'>
            /// </param>
            public static Operation Get(this IOperations operations, System.Guid id, System.DateTime? logTimeStamp = default(System.DateTime?))
            {
                return operations.GetAsync(id, logTimeStamp).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get a operation
            /// </summary>
            /// <remarks>
            /// Get a operation
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='logTimeStamp'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Operation> GetAsync(this IOperations operations, System.Guid id, System.DateTime? logTimeStamp = default(System.DateTime?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(id, logTimeStamp, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// List all Operations
            /// </summary>
            /// <remarks>
            /// List all Operations
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logTimeStamp'>
            /// </param>
            /// <param name='count'>
            /// </param>
            public static Haipa.ClientRuntime.IPage<Operation> List(this IOperations operations, System.DateTime? logTimeStamp = default(System.DateTime?), bool? count = default(bool?))
            {
                return operations.ListAsync(logTimeStamp, count).GetAwaiter().GetResult();
            }

            /// <summary>
            /// List all Operations
            /// </summary>
            /// <remarks>
            /// List all Operations
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logTimeStamp'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Haipa.ClientRuntime.IPage<Operation>> ListAsync(this IOperations operations, System.DateTime? logTimeStamp = default(System.DateTime?), bool? count = default(bool?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ListWithHttpMessagesAsync(logTimeStamp, count, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// List all Operations
            /// </summary>
            /// <remarks>
            /// List all Operations
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nextPageLink'>
            /// The NextLink from the previous successful call to List operation.
            /// </param>
            public static Haipa.ClientRuntime.IPage<Operation> ListNext(this IOperations operations, string nextPageLink)
            {
                return operations.ListNextAsync(nextPageLink).GetAwaiter().GetResult();
            }

            /// <summary>
            /// List all Operations
            /// </summary>
            /// <remarks>
            /// List all Operations
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nextPageLink'>
            /// The NextLink from the previous successful call to List operation.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Haipa.ClientRuntime.IPage<Operation>> ListNextAsync(this IOperations operations, string nextPageLink, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.ListNextWithHttpMessagesAsync(nextPageLink, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
