using TimeTrackingApi.Models;
using TimeTrackingApi.DTOs;

namespace TimeTrackingApi.Mappers;

public class TimeTrackingItemMappers 
{

    public static TimeTrackingItemDTO ItemToDTO(TimeTrackingItem timeTrackingItem) =>
        new()
        {
            Id = timeTrackingItem.Id,
            Nume = timeTrackingItem.Nume,
            Descriere = timeTrackingItem.Descriere,
            Data = timeTrackingItem.Data,
            NumarOre = timeTrackingItem.NumarOre,
            TimeTrackingSubitems = timeTrackingItem.TimeTrackingSubitems?.Select(si => TimeTrackingSubitemMappers.SubitemToDTO(si)).ToList(),
        };

    public static TimeTrackingItem DTOToItem (TimeTrackingItemDTO timeTrackingItemDTO) =>
        new TimeTrackingItem
    {
            Nume = timeTrackingItemDTO.Nume,
            Descriere = timeTrackingItemDTO.Descriere,
            Data = timeTrackingItemDTO.Data,
            NumarOre = timeTrackingItemDTO.NumarOre,
            TimeTrackingSubitems = timeTrackingItemDTO.TimeTrackingSubitems?.Select(subitemDTO => TimeTrackingSubitemMappers.DTOToSubitem(subitemDTO)).ToList(),
    };
}