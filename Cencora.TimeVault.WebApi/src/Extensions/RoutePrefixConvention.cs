// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Cencora.TimeVault.WebApi.Extensions;

/// <summary>
/// Represents a convention for adding a route prefix to all controllers.
/// </summary>
public class RoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoutePrefixConvention"/> class.
    /// </summary>
    /// <param name="prefix">The route prefix.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="prefix"/> is <see langword="null"/>.</exception>
    public RoutePrefixConvention(string prefix)
    {
        ArgumentNullException.ThrowIfNull(prefix, nameof(prefix));

        _routePrefix = new AttributeRouteModel(new RouteAttribute(prefix));
    }

    /// <inheritdoc/>
    public void Apply(ApplicationModel application)
    {
        ArgumentNullException.ThrowIfNull(application, nameof(application));

        foreach (var controller in application.Controllers)
        foreach (var selector in controller.Selectors)
            selector.AttributeRouteModel = selector.AttributeRouteModel != null
                ? AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel)
                : _routePrefix;
    }
}