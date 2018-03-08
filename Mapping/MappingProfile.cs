using AutoMapper;
using LogWatcher.Models;
using LogWatcher.Resources;

namespace LogWatcher.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LogItem, LogItemResource>();
        }
    }
}