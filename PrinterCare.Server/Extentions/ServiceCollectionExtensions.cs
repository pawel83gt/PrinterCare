using PrinterCare.Server.Interfaces;
using PrinterCare.Server.Repositories;
using PrinterCare.Server.Services;

namespace PrinterCare.Server.Extentions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IOrganizationService, OrganizationService>();

        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IBranchService, BranchService>();

        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<IManufacturerService, ManufacturerService>();

        services.AddScoped<IEquipmentModelRepository, EquipmentModelRepository>();
        services.AddScoped<IEquipmentModelService, EquipmentModelService>();

        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IEquipmentService, EquipmentService>();

        services.AddScoped<ICartridgeRepository, CartridgeRepository>();
        services.AddScoped<ICartridgeService, CartridgeService>();

        return services;
    }
}
