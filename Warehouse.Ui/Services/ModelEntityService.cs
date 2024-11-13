using Radzen;
using Warehouse.Model.Model;

namespace Warehouse.Ui.Services;

public class ModelEntityService
{
    private readonly NotificationService _notificationService;

    public ModelEntityService(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public static string GetAreaNameByPicket(WarehouseResponse warehouse, PicketResponse picket)
    {
        var area = warehouse.Areas.FirstOrDefault(a => a.Pickets.Any(p => p.Id == picket.Id));

        return area?.Name ?? "Отсутствует";
    }

    public static string GetAreaCargoByPicket(WarehouseResponse warehouse, PicketResponse picket)
    {
        var area = warehouse.Areas.FirstOrDefault(a => a.Pickets.Any(p => p.Id == picket.Id));

        return area?.Cargo.FirstOrDefault()?.Weight ?? "Отсутствует";
    }


    public void SendNotificationMessage(string title, string message, NotificationSeverity severity = NotificationSeverity.Info)
    {
        _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Info, Summary = title, Detail = message });
    }
}