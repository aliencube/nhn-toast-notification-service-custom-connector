# Provisions resources based on Flags
Param(
    [string]
    [Parameter(Mandatory=$false)]
    $ResourceGroupName = "",

    [string]
    [Parameter(Mandatory=$false)]
    $DeploymentName = "",

    [string]
    [Parameter(Mandatory=$false)]
    $ResourceName = "",

    [string]
    [Parameter(Mandatory=$false)]
    $ResourceShortName = "",

    [string]
    [Parameter(Mandatory=$false)]
    $ResourceNameSuffix = "",

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("", "koreacentral", "westus2")]
    $Location = "",

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("", "krc", "wus2")]
    $LocationCode = "",

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("dev", "test", "prod")]
    $Environment = "dev",

    ### Storage Account ###
    [bool]
    [Parameter(Mandatory=$false)]
    $ProvisionStorageAccount = $false,

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("Standard_GRS", "Standard_LRS", "Standard_ZRS", "Standard_GZRS", "Standard_RAGRS", "Standard_RAGZRS", "Premium_LRS", "Premium_ZRS")]
    $StorageAccountSku = "Standard_LRS",

    [string[]]
    [Parameter(Mandatory=$false)]
    $StorageAccountBlobContainers = @(),

    [string[]]
    [Parameter(Mandatory=$false)]
    $StorageAccountTables = @(),
    ### Storage Account ###

    ### Log Analytics ###
    [bool]
    [Parameter(Mandatory=$false)]
    $ProvisionLogAnalyticsWorkspace = $false,

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("Free", "Standard", "Premium", "Standalone", "LACluster", "PerGB2018", "PerNode", "CapacityReservation")]
    $LogAnalyticsWorkspaceSku = "PerGB2018",
    ### Log Analytics ###

    ### Application Insights ###
    [bool]
    [Parameter(Mandatory=$false)]
    $ProvisionAppInsights = $false,

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("web", "other")]
    $AppInsightsType = "web",

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("ApplicationInsights", "ApplicationInsightsWithDiagnosticSettings", "LogAnalytics")]
    $AppInsightsIngestionMode = "LogAnalytics",
    ### Application Insights ###

    ### API Management ###
    [bool]
    [Parameter(Mandatory=$false)]
    $ProvisionApiMangement = $false,

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("Consumption", "Isolated", "Developer", "Basic", "Standard", "Premium")]
    $ApiManagementSkuName = "Consumption",

    [int]
    [Parameter(Mandatory=$false)]
    $ApiManagementSkuCapacity = 0,

    [string]
    [Parameter(Mandatory=$false)]
    $ApiManagementPublisherName = "",

    [string]
    [Parameter(Mandatory=$false)]
    $ApiManagementPublisherEmail = "",
    ### API Management ###

    [switch]
    [Parameter(Mandatory=$false)]
    $WhatIf,

    [switch]
    [Parameter(Mandatory=$false)]
    $Help
)

function Show-Usage {
    Write-Output "    This provisions resources to Azure

    Usage: $(Split-Path $MyInvocation.ScriptName -Leaf) ``
            -ResourceGroupName <resource group name> ``
            -DeploymentName <deployment name> ``
            -ResourceName <resource name> ``
            [-ResourceShortName <resource short name> ``
            [-ResourceNameSuffix <resource name suffix>] ``
            [-Location <location>] ``
            [-LocationCode <location code>] ``
            [-Environment <environment>] ``

            [-ProvisionStorageAccount <`$true|`$false>] ``
            [-StorageAccountSku <Storage Account SKU>] ``
            [-StorageAccountBlobContainers <Storage Account blob containers>] ``
            [-StorageAccountTables <Storage Account tables>] ``

            [-ProvisionLogAnalyticsWorkspace <`$true|`$false>] ``
            [-LogAnalyticsWorkspaceSku <Log Analytics workspace SKU>] ``

            [-ProvisionAppInsights <`$true|`$false>] ``
            [-AppInsightsType <Application Insights type>] ``
            [-AppInsightsIngestionMode <Application Insights data ingestion mode>] ``

            [-ProvisionApiMangement <`$true|`$false>] ``
            [-ApiManagementSkuName <API Management SKU name>] ``
            [-ApiManagementSkuCapacity <API Management SKU capacity>] ``
            [-ApiManagementPublisherName <API Management publisher name>] ``
            [-ApiManagementPublisherEmail <API Management publisher email>] ``

            [-WhatIf] ``
            [-Help]

    Options:
        -ResourceGroupName                Resource group name.
        -DeploymentName                   Deployment name.
        -ResourceName                     Resource name.
        -ResourceShortName                Resource short name.
                                          Default is to use the resource name.
        -ResourceNameSuffix               Resource name suffix.
                                          Default is empty string.
        -Location                         Resource location.
                                          Default is empty string.
        -LocationCode                     location code.
                                          Default is empty string.
        -Environment                      environment.
                                          Default is 'dev'.

        -ProvisionStorageAccount          To provision Storage Account or not.
                                          Default is `$false.
        -StorageAccountSku                Storage Account SKU.
                                          Default is 'Standard_LRS'.
        -StorageAccountBlobContainers     Storage Account blob containers array.
                                          Default is empty array.
        -StorageAccountTables             Storage Account tables array.
                                          Default is empty array.

        -ProvisionLogAnalyticsWorkspace   To provision Log Analytics Workspace
                                          or not. Default is `$false.
        -LogAnalyticsWorkspaceSku         Log Analytics workspace SKU.
                                          Default is 'PerGB2018'.

        -ProvisionAppInsights             To provision Application Insights
                                          or not. Default is `$false.
        -AppInsightsType                  Application Insights type.
                                          Default is 'web'.
        -AppInsightsIngestionMode         Application Insights data ingestion
                                          mode. Default is 'ApplicationInsights'.

        -ProvisionApiMangement            To provision API Management or not.
                                          Default is `$false.
        -ApiManagementSkuName             API Management SKU name.
                                          Default is 'Consumption'.
        -ApiManagementSkuCapacity         API Management SKU capacity.
                                          Default is 0.
        -ApiManagementPublisherName       API Management publisher name.
                                          Default is empty string.
                                          If -ProvisionApiMangement is `$true,
                                          this parameter must have a value.
        -ApiManagementPublisherEmail      API Management publisher email.
                                          Default is empty string.
                                          If -ProvisionApiMangement is `$true,
                                          this parameter must have a value.

        -WhatIf:                          Show what would happen without
                                          actually provisioning resources.
        -Help:                            Show this message.
"

    Exit 0
}

# Show usage
$needHelp = ($ResourceGroupName -eq "") -or ($DeploymentName -eq "") -or ($ResourceName -eq "") -or ($Help -eq $true)
if ($needHelp -eq $true) {
    Show-Usage
    Exit 0
}
$needHelp = ($ProvisionApiMangement -eq $true) -and (($ApiManagementPublisherName -eq "") -or ($ApiManagementPublisherEmail -eq ""))
if ($needHelp -eq $true) {
    Show-Usage
    Exit 0
}

# Override resource short name with resource name if resource short name is not specified
if ($ResourceShortName -eq "") {
    $ResourceShortName = $ResourceName
}

# Force the dependencies to be provisioned - Application Insights
if ($ProvisionAppInsights -eq $true) {
    $ProvisionLogAnalyticsWorkspace = $true
}

# Force the dependencies to be provisioned - API Management
if ($ProvisionApiMangement -eq $true) {
    $ProvisionStorageAccount = $true
    $ProvisionAppInsights = $true
    $ProvisionLogAnalyticsWorkspace = $true
}

# Get global policy from the template
$apiManagementPolicyFormat = "rawxml-link"
$apiManagementPolicyValue = "https://raw.githubusercontent.com/aliencube/nhn-toast-notification-service-custom-connector/main/resources/apim-global-policy.xml"

# Build parameters
$params = @{
    name = @{ value = $ResourceName };
    shortName = @{ value = $ResourceShortName };
    suffix = @{ value = $ResourceNameSuffix };
    location = @{ value = $Location };
    locationCode = @{ value = $LocationCode };
    env = @{ value = $Environment };

    storageAccountToProvision = @{ value = $ProvisionStorageAccount };
    storageAccountSku = @{ value = $StorageAccountSku };
    storageAccountBlobContainers = @{ value = $StorageAccountBlobContainers };
    storageAccountTables = @{ value = $StorageAccountTables };

    workspaceToProvision = @{ value = $ProvisionLogAnalyticsWorkspace };
    workspaceSku = @{ value = $LogAnalyticsWorkspaceSku };

    appInsightsToProvision = @{ value = $ProvisionAppInsights };
    appInsightsType = @{ value = $AppInsightsType };
    appInsightsIngestionMode = @{ value = $AppInsightsIngestionMode };

    apiMgmtToProvision = @{ value = $ProvisionApiMangement };
    apiMgmtSkuName = @{ value = $ApiManagementSkuName };
    apiMgmtSkuCapacity = @{ value = $ApiManagementSkuCapacity };
    apiMgmtPublisherName = @{ value = $ApiManagementPublisherName };
    apiMgmtPublisherEmail = @{ value = $ApiManagementPublisherEmail };
    apiMgmtPolicyFormat = @{ value = $apiManagementPolicyFormat };
    apiMgmtPolicyValue = @{ value = $apiManagementPolicyValue };
}

# Uncomment to debug
# $params | ConvertTo-Json
# $params | ConvertTo-Json -Compress
# $params | ConvertTo-Json -Compress | ConvertTo-Json

$stringified = $params | ConvertTo-Json -Compress | ConvertTo-Json

# Provision the resources
if ($WhatIf -eq $true) {
    Write-Output "[$(Get-Date -Format "yyyy-MM-dd HH:mm:ss")] Provisioning resources as a test ..."
    az deployment group create -g $ResourceGroupName -n $DeploymentName `
        -f ./provision-apimanagement.bicep `
        -p $stringified `
        -w

        # -u https://raw.githubusercontent.com/aliencube/nhn-toast-notification-service-custom-connector/main/resources/provision-apimanagement.bicep `
} else {
    Write-Output "[$(Get-Date -Format "yyyy-MM-dd HH:mm:ss")] Provisioning resources ..."
    az deployment group create -g $ResourceGroupName -n $DeploymentName `
        -f ./provision-apimanagement.bicep `
        -p $stringified `
        --verbose

        # -u https://raw.githubusercontent.com/aliencube/nhn-toast-notification-service-custom-connector/main/resources/provision-apimanagement.bicep `

    Write-Output "[$(Get-Date -Format "yyyy-MM-dd HH:mm:ss")] Resources have been provisioned"
}
