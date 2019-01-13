using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinksShortener.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace LinksShortener.Core
{
    public interface ILinksService
    {
        Task Create(Link link);

        Task<string> Hit(string url);

        Task<List<Link>> GetAll();

        Task<List<Link>> GetAllByOwner(string ownerId);
    }
    
    public class LinksService : ILinksService
    {
        private readonly ILinksRepository _linksRepository;
        private readonly ILogger<LinksService> _logger;

        public LinksService(
            ILinksRepository linksRepository,
            ILogger<LinksService> logger
        )
        {
            _linksRepository = linksRepository;
            _logger = logger;
        }
        
        public async Task Create(Link link)
        {
            const int maxCreationAttempts = 10;

            for (int i = 0; i < maxCreationAttempts; ++i)
            {
                try
                {
                    link.Url = "asd";
                    await _linksRepository.Create(link);

                    return;
                }
                catch (DuplicateUrlException)
                {
                }
            }
            
            _logger.LogError("failed to shorten link due to maxCreationAttempts limitation.");
            throw new Exception();
        }

        public async Task<string> Hit(string url)
        {
            var link = await _linksRepository.Hit(url);

            return link?.Destination ?? "/";
        }

        public Task<List<Link>> GetAll()
        {
            return _linksRepository.GetAll();
        }

        public Task<List<Link>> GetAllByOwner(string ownerId)
        {
            return _linksRepository.GetAllByOwner(ownerId);
        }
    }
}