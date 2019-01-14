using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinksShortener.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace LinksShortener.Core
{
    public interface ILinksService
    {
        Task<Link> Create(Link link);

        Task<string> Hit(string url);

        Task<List<Link>> GetAll();

        Task<List<Link>> GetAllByOwner(string ownerId);
    }
    
    public class LinksService : ILinksService
    {
        private readonly ILinksRepository _linksRepository;
        private readonly ICounterRepository _counterRepository;
        
        private readonly ILogger<LinksService> _logger;

        public LinksService(
            ILinksRepository linksRepository,
            ICounterRepository counterRepository,
            ILogger<LinksService> logger
        )
        {
            _linksRepository = linksRepository;
            _counterRepository = counterRepository;
            
            _logger = logger;
        }
        
        public async Task<Link> Create(Link link)
        {   
            ValidateAndThrow(link);
            
            link.CreatedAt = DateTime.Now;
            var counter = await _counterRepository.Inc();
            link.Url = CounterHelper.ToUrl(counter);

            await _linksRepository.Create(link);
            _logger.LogInformation($"Created link from {link.Url} to {link.Destination} owned by {link.OwnerId}");
            
            return link;
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

        private void ValidateAndThrow(Link link)
        {
            if (!Uri.TryCreate(link.Destination, UriKind.Absolute, out var uriResult)
                || !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                throw new Exception();
        }
    }
}