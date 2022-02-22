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

@allowed([
    'Free'
    'Standard'
    'Premium'
    'Standalone'
    'LACluster'
    'PerGB2018'
    'PerNode'
    'CapacityReservation'
])
param workspaceSku string = 'PerGB2018'

var metadata = {
    longName: '{0}-${name}{1}-${env}-${locationCode}'
    shortName: '{0}${name}{1}${env}${locationCode}'
}

var workspace = {
    name: suffix == '' ? format(metadata.longName, 'wrkspc', '') : format(metadata.longName, 'wrkspc', '-${suffix}')
    location: location
    sku: workspaceSku
}

resource wrkspc 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
    name: workspace.name
    location: workspace.location
    properties: {
        sku: {
            name: workspace.sku
        }
        retentionInDays: 30
        workspaceCapping: {
            dailyQuotaGb: -1
        }
        publicNetworkAccessForIngestion: 'Enabled'
        publicNetworkAccessForQuery: 'Enabled'
    }
}

output id string = wrkspc.id
output name string = wrkspc.name
