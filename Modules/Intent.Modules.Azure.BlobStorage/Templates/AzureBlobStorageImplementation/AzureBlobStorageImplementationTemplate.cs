﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.Azure.BlobStorage.Templates.AzureBlobStorageImplementation
{
    using System.Collections.Generic;
    using System.Linq;
    using Intent.Modules.Common;
    using Intent.Modules.Common.Templates;
    using Intent.Modules.Common.CSharp.Templates;
    using Intent.Templates;
    using Intent.Metadata.Models;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Azure.BlobStorage\Templates\AzureBlobStorageImplementation\AzureBlobStorageImplementationTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class AzureBlobStorageImplementationTemplate : CSharpTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.IO;\r\nusing System.Runtime.CompilerServices;\r\nusing System.Threading;\r\nusing System.Threading.Tasks;\r\nusing Azure.Storage.Blobs;\r\nusing Azure.Storage.Blobs.Models;\r\nusing Microsoft.Extensions.Configuration;\r\n\r\n[assembly: DefaultIntentManaged(Mode.Fully)]\r\n\r\nnamespace ");
            
            #line 23 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Azure.BlobStorage\Templates\AzureBlobStorageImplementation\AzureBlobStorageImplementationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    public class ");
            
            #line 25 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Azure.BlobStorage\Templates\AzureBlobStorageImplementation\AzureBlobStorageImplementationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" : ");
            
            #line 25 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Azure.BlobStorage\Templates\AzureBlobStorageImplementation\AzureBlobStorageImplementationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetBlobStorageInterfaceName()));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        private const PublicAccessType ContainerPublicAccessType = PublicAccessType.None;\r\n        private readonly BlobServiceClient _client;\r\n\r\n        public ");
            
            #line 30 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Azure.BlobStorage\Templates\AzureBlobStorageImplementation\AzureBlobStorageImplementationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("(IConfiguration configuration)\r\n        {\r\n            _client = new BlobServiceClient(configuration.GetValue<string>(\"AzureBlobStorage\"));\r\n        }\r\n\r\n        public async Task<Uri> GetAsync(string containerName, string blobName, CancellationToken cancellationToken = default)\r\n        {\r\n            var blobClient = await GetBlobClient(containerName, blobName, cancellationToken).ConfigureAwait(false);\r\n            return blobClient.Uri;\r\n        }\r\n        \r\n        public async IAsyncEnumerable<Uri> ListAsync(string containerName, \r\n            [EnumeratorCancellation] CancellationToken cancellationToken = default)\r\n        {\r\n            var containerClient = await GetContainerClientAsync(containerName, cancellationToken).ConfigureAwait(false);\r\n            await foreach (var blobItem in containerClient.GetBlobsAsync(cancellationToken: cancellationToken))\r\n            {\r\n                yield return await GetAsync(containerName, blobItem.Name, cancellationToken);\r\n            }\r\n        }\r\n        \r\n        public Task<Uri> UploadAsync(Uri cloudStorageLocation, Stream dataStream, CancellationToken cancellationToken = default)\r\n        {\r\n            var blobUriBuilder = new BlobUriBuilder(cloudStorageLocation);\r\n            return UploadAsync(blobUriBuilder.BlobContainerName, blobUriBuilder.BlobName, dataStream, cancellationToken);\r\n        }\r\n\r\n        public async Task<Uri> UploadAsync(string containerName, string blobName, Stream dataStream, \r\n            CancellationToken cancellationToken = default)\r\n        {\r\n            var blobClient = await GetBlobClient(containerName, blobName, cancellationToken).ConfigureAwait(false);\r\n            await blobClient.UploadAsync(dataStream, overwrite: true, cancellationToken).ConfigureAwait(false);\r\n            return blobClient.Uri;\r\n        }\r\n        \r\n        public async IAsyncEnumerable<Uri> BulkUploadAsync(\r\n            string containerName,\r\n            IEnumerable<BulkBlobItem> blobs,\r\n            [EnumeratorCancellation] CancellationToken cancellationToken = default)\r\n        {\r\n            foreach (var blob in blobs)\r\n            {\r\n                yield return await UploadAsync(containerName, blob.Name, blob.DataStream, cancellationToken);\r\n            }\r\n        }\r\n\r\n        public Task<Stream> DownloadAsync(Uri cloudStorageLocation, CancellationToken cancellationToken = default)\r\n        {\r\n            var blobUriBuilder = new BlobUriBuilder(cloudStorageLocation);\r\n            return DownloadAsync(blobUriBuilder.BlobContainerName, blobUriBuilder.BlobName, cancellationToken);\r\n        }\r\n\r\n        public async Task<Stream> DownloadAsync(string containerName, string blobName, CancellationToken cancellationToken = default)\r\n        {\r\n            var blobClient = await GetBlobClient(containerName, blobName, cancellationToken).ConfigureAwait(false);\r\n            var result = await blobClient.DownloadAsync(cancellationToken: cancellationToken).ConfigureAwait(false);\r\n            return result.Value.Content;\r\n        }\r\n\r\n        public async Task DeleteAsync(Uri cloudStorageLocation, CancellationToken cancellationToken = default)\r\n        {\r\n            var blobUriBuilder = new BlobUriBuilder(cloudStorageLocation);\r\n            await DeleteAsync(blobUriBuilder.BlobContainerName, blobUriBuilder.BlobName, cancellationToken).ConfigureAwait(false);\r\n        }\r\n\r\n        public async Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken = default)\r\n        {\r\n            var blobClient = await GetBlobClient(containerName, blobName, cancellationToken);\r\n            await blobClient.DeleteAsync(cancellationToken: cancellationToken).ConfigureAwait(false);\r\n        }\r\n\r\n        private async Task<BlobContainerClient> GetContainerClientAsync(string containerName, CancellationToken cancellationToken)\r\n        {\r\n            var containerClient = _client.GetBlobContainerClient(containerName);\r\n            await containerClient.CreateIfNotExistsAsync(ContainerPublicAccessType, cancellationToken: cancellationToken); \r\n            return containerClient;\r\n        }\r\n\r\n        private async Task<BlobClient> GetBlobClient(string containerName, string blobName, CancellationToken cancellationToken)\r\n        {\r\n            var containerClient = await GetContainerClientAsync(containerName, cancellationToken: cancellationToken);\r\n            return containerClient.GetBlobClient(blobName);\r\n        }\r\n    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
}
