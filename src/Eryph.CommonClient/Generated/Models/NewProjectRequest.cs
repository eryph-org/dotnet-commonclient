// <auto-generated>
// MIT
// </auto-generated>

namespace Eryph.CommonClient.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class NewProjectRequest
    {
        /// <summary>
        /// Initializes a new instance of the NewProjectRequest class.
        /// </summary>
        public NewProjectRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the NewProjectRequest class.
        /// </summary>
        public NewProjectRequest(string name, System.Guid? correlationId = default(System.Guid?))
        {
            CorrelationId = correlationId;
            Name = name;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "correlationId")]
        public System.Guid? CorrelationId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Name == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Name");
            }
        }
    }
}