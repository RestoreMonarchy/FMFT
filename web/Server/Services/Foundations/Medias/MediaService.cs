using FMFT.Web.Server.Brokers.Converts;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Medias;
using FMFT.Web.Server.Models.Medias.DTOs;
using FMFT.Web.Server.Models.Medias.Exceptions;
using FMFT.Web.Server.Models.Medias.Params;

namespace FMFT.Web.Server.Services.Foundations.Medias
{
    public class MediaService : IMediaService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IConvertBroker convertBroker;

        private const long MaxMediaFileSize = 5 * 1024 * 1024;

        public MediaService(IStorageBroker storageBroker, IConvertBroker convertBroker)
        {
            this.storageBroker = storageBroker;
            this.convertBroker = convertBroker;
        }

        public async ValueTask<Media> AddMediaFromFormFileAsync(IFormFile formFile, int? userId)
        {
            if (formFile == null)
            {
                throw new NotSuppliedFileMediaException();
            }

            if (formFile.Length >= MaxMediaFileSize)
            {
                throw new TooLargeFileMediaException();
            }

            AddMediaParams @params = new()
            {
                Name = formFile.FileName,
                ContentType = formFile.ContentType
            };

            @params.UserId = userId;
            @params.Content = await convertBroker.GetBytesFromFormFileAsync(formFile);

            return await AddMediaAsync(@params);
        }

        public async ValueTask<Media> AddMediaAsync(AddMediaParams @params)
        {
            if (@params.Content.Length >= MaxMediaFileSize)
            {
                throw new TooLargeFileMediaException();
            }

            InsertMediaDTO dto = new()
            {
                Name = @params.Name,
                Content = @params.Content,
                ContentType = @params.ContentType,
                UserId = @params.UserId,
            };

            return await storageBroker.InsertMediaAsync(dto);
        }

        public async ValueTask<Media> RetrieveMediaByIdAsync(Guid mediaId)
        {
            Media media = await storageBroker.SelectMediaByIdAsync(mediaId);

            if (media == null)
            {
                throw new NotFoundMediaException();
            }

            return media;
        }
    }
}
