param name string
param shortName string = ''
param suffix string = ''
param location string = ''
param locationCode string = ''
@allowed([
    'dev'
    'test'
    'prod'
])
param env string = 'dev'

// Log Analytics Workspace
param workspaceToProvision bool = false
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

// Application Insights
param appInsightsToProvision bool = false
@allowed([
    'web'
    'other'
])
param appInsightsType string = 'web'

@allowed([
    'ApplicationInsights'
    'ApplicationInsightsWithDiagnosticSettings'
    'LogAnalytics'
])
param appInsightsIngestionMode string = 'LogAnalytics'

// API Management
param apiMgmtToProvision bool = false
@allowed([
    'Consumption'
    'Isolated'
    'Developer'
    'Basic'
    'Standard'
    'Premium'
])
param apiMgmtSkuName string = 'Consumption'
param apiMgmtSkuCapacity int = 0
param apiMgmtPublisherName string
param apiMgmtPublisherEmail string
@allowed([
    'rawxml'
    'rawxml-link'
    'xml'
    'xml-link'
])
param apiMgmtPolicyFormat string = 'xml'
param apiMgmtPolicyValue string = '<!--\r\n    IMPORTANT:\r\n    - Policy elements can appear only within the <inbound>, <outbound>, <backend> section elements.\r\n    - Only the <forward-request> policy element can appear within the <backend> section element.\r\n    - To apply a policy to the incoming request (before it is forwarded to the backend service), place a corresponding policy element within the <inbound> section element.\r\n    - To apply a policy to the outgoing response (before it is sent back to the caller), place a corresponding policy element within the <outbound> section element.\r\n    - To add a policy position the cursor at the desired insertion point and click on the round button associated with the policy.\r\n    - To remove a policy, delete the corresponding policy statement from the policy document.\r\n    - Policies are applied in the order of their appearance, from the top down.\r\n-->\r\n<policies>\r\n  <inbound></inbound>\r\n  <backend>\r\n    <forward-request />\r\n  </backend>\r\n  <outbound></outbound>\r\n</policies>'

var locationResolved = location == '' ? resourceGroup().location : location
var locationCodeMap = {
    koreacentral: 'krc'
    'Korea Central': 'krc'
    westus2: 'wus2'
    'West US 2': 'wus2'
}
var locationCodeResolved = locationCode == '' ? locationCodeMap[locationResolved] : locationCode

module wrkspcapimgmt './logAnalyticsWorkspace.bicep' = if (workspaceToProvision) {
    name: 'LogAnalyticsWorkspace'
    params: {
        name: name
        location: locationResolved
        locationCode: locationCodeResolved
        env: env
        workspaceSku: workspaceSku
    }
}

module appinsapimgmt './appInsights.bicep' = if (appInsightsToProvision) {
    name: 'ApplicationInsights'
    params: {
        name: name
        location: locationResolved
        locationCode: locationCodeResolved
        env: env
        appInsightsType: appInsightsType
        appInsightsIngestionMode: appInsightsIngestionMode
        workspaceId: wrkspcapimgmt.outputs.id
    }
}

module apim './apiManagement.bicep' = if (apiMgmtToProvision) {
    name: 'ApiManagement'
    params: {
        name: name
        location: locationResolved
        locationCode: locationCodeResolved
        env: env
        appInsightsId: appinsapimgmt.outputs.id
        appInsightsInstrumentationKey: appinsapimgmt.outputs.instrumentationKey
        apiMgmtSkuName: apiMgmtSkuName
        apiMgmtSkuCapacity: apiMgmtSkuCapacity
        apiMgmtPublisherName: apiMgmtPublisherName
        apiMgmtPublisherEmail: apiMgmtPublisherEmail
        apiMgmtPolicyFormat: apiMgmtPolicyFormat
        apiMgmtPolicyValue: apiMgmtPolicyValue
    }
}
