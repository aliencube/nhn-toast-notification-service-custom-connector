param name string
param suffix string = ''
param location string = resourceGroup().location
param locationCode string = 'wus2'

@allowed([
    'dev'
    'test'
    'prod'
])
param env string = 'dev'

param consumptionPlanIsLinux bool = false

var metadata = {
    longName: '{0}-${name}{1}-${env}-${locationCode}'
    shortName: '{0}${name}{1}${env}${locationCode}'
}

var consumption = {
    name: suffix == '' ? format(metadata.longName, 'csplan', '') : format(metadata.longName, 'csplan', '-${suffix}')
    location: location
    isLinux: consumptionPlanIsLinux
}

resource csplan 'Microsoft.Web/serverfarms@2021-02-01' = {
    name: consumption.name
    location: consumption.location
    kind: 'functionApp'
    sku: {
        name: 'Y1'
        tier: 'Dynamic'
    }
    properties: {
        reserved: consumption.isLinux
    }
}

output id string = csplan.id
output name string = csplan.name
