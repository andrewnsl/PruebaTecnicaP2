using AutoMapper;
using PruebaTecnicaP2.Models.Dtos;
using PruebaTecnicaP2.Models.Entities;
using PruebaTecnicaP2.Models.PayLoads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.Helpers.AutoMapping
{
    public class AutoMappingHelper : Profile
    {
        public AutoMappingHelper()
        {
            CreateMap<PuntosLoad, Puntos>();
            CreateMap<Puntos, PuntosDto>();
        }
    }
}
