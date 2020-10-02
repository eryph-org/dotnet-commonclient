// <auto-generated>
// MIT
// </auto-generated>

namespace Haipa.CommonClient
{
    using Haipa.ClientRuntime;
    using Haipa.ClientRuntime.OData;
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Operations operations.
    /// </summary>
    public partial interface IOperations
    {
        /// <param name='odataQuery'>
        /// OData parameters to apply to the operation.
        /// </param>
        /// <param name='select'>
        /// Limits the properties returned in the result.
        /// </param>
        /// <param name='count'>
        /// Indicates whether the total count of items within a collection are
        /// returned in the result.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Haipa.ClientRuntime.ApiServiceException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        Task<Haipa.ClientRuntime.HaipaOperationResponse<OperationList>> ListWithHttpMessagesAsync(Haipa.ClientRuntime.OData.ODataQuery<Operation> odataQuery = default(Haipa.ClientRuntime.OData.ODataQuery<Operation>), string select = default(string), bool? count = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <param name='key'>
        /// </param>
        /// <param name='select'>
        /// Limits the properties returned in the result.
        /// </param>
        /// <param name='expand'>
        /// Indicates the related entities to be represented inline. The
        /// maximum depth is 2.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Haipa.ClientRuntime.ApiServiceException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        Task<Haipa.ClientRuntime.HaipaOperationResponse<Operation>> GetWithHttpMessagesAsync(System.Guid key, string select = default(string), string expand = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <param name='key'>
        /// </param>
        /// <param name='odataQuery'>
        /// OData parameters to apply to the operation.
        /// </param>
        /// <param name='select'>
        /// Limits the properties returned in the result.
        /// </param>
        /// <param name='count'>
        /// Indicates whether the total count of items within a collection are
        /// returned in the result.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Haipa.ClientRuntime.ApiServiceException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        Task<Haipa.ClientRuntime.HaipaOperationResponse<OperationLogEntryList>> GetLogEntriesWithHttpMessagesAsync(System.Guid key, Haipa.ClientRuntime.OData.ODataQuery<OperationLogEntry> odataQuery = default(Haipa.ClientRuntime.OData.ODataQuery<OperationLogEntry>), string select = default(string), bool? count = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}