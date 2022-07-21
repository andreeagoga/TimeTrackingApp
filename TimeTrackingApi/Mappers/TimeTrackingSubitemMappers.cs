using TimeTrackingApi.Models;
 using TimeTrackingApi.DTOs;

 namespace TimeTrackingApi.Mappers;

 public class TimeTrackingSubitemMappers 
 {
 
    public static TimeTrackingSubitemDTO SubitemToDTO(TimeTrackingSubitem timeTrackingSubitem) =>
            new TimeTrackingSubitemDTO
                {
                    Id = timeTrackingSubitem.Id,
                    Descriere = timeTrackingSubitem.Descriere,
                    NumarOre = timeTrackingSubitem.NumarOre,
                    Dificultate = timeTrackingSubitem.Dificultate,
                };

    public static TimeTrackingSubitem DTOToSubitem (TimeTrackingSubitemDTO timeTrackingSubitemDTO) =>
        new TimeTrackingSubitem
    {
            Descriere = timeTrackingSubitemDTO.Descriere,
            NumarOre = timeTrackingSubitemDTO.NumarOre,
            Dificultate = timeTrackingSubitemDTO.Dificultate,
    };
 }