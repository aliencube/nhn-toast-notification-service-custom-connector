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
    $GroupName = "",

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

    ### API Management ###
    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("http", "soap", "websocket", "graphql")]
    $ApiManagementApiType = "http",

    [string]
    [Parameter(Mandatory=$false)]
    $ApiManagementApiName = "",

    [string]
    [Parameter(Mandatory=$false)]
    $ApiManagementApiDisplayName = "",

    [string]
    [Parameter(Mandatory=$false)]
    $ApiManagementApiDescription = "",

    [string]
    [Parameter(Mandatory=$false)]
    $ApiManagementApiPath = "",

    [bool]
    [Parameter(Mandatory=$false)]
    $ApiManagementApiSubscriptionRequired = $false,

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("swagger-json", "swagger-link-json", "openapi", "openapi+json", "openapi+json-link", "openapi-link", "wadl-link-json", "wadl-xml", "wsdl", "wsdl-link", "graphql-link")]
    $ApiManagementApiFormat = "openapi+json-link",

    [string]
    [Parameter(Mandatory=$false)]
    $ApiManagementApiValue = "",

    [string]
    [Parameter(Mandatory=$false)]
    [ValidateSet("rawxml", "rawxml-link", "xml", "xml-link")]
    $ApiManagementApiPolicyFormat = "xml",

    [string]
    [Parameter(Mandatory=$false)]
    $ApiManagementApiPolicyValue = "",
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
            -GroupName <group name> ``
            -ResourceName <resource name> ``
            [-ResourceShortName <resource short name> ``
            [-ResourceNameSuffix <resource name suffix>] ``
            [-Location <location>] ``
            [-LocationCode <location code>] ``
            [-Environment <environment>] ``

            [-ApiManagementApiType <API Management API type>] ``
            [-ApiManagementApiName <API Management API name>] ``
            [-ApiManagementApiDisplayName <API Management API display name>] ``
            [-ApiManagementApiDescription <API Management API description>] ``
            [-ApiManagementApiPath <API Management API path>] ``
            [-ApiManagementApiSubscriptionRequired <`$true|`$false>] ``
            [-ApiManagementApiFormat <API Management API format>] ``
            [-ApiManagementApiValue <API Management API value>] ``

            [-WhatIf] ``
            [-Help]

    Options:
        -ResourceGroupName                Resource group name.
        -DeploymentName                   Deployment name.
        -GroupName                        Resource name.
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

        -ProvisionApiMangement            To provision API Management or not.
                                          Default is `$false.
        -ApiManagementApiType             API Management API type.
                                          Default is 'http'.
        -ApiManagementApiName             API Management API name.
        -ApiManagementApiDisplayName      API Management API display name.
        -ApiManagementApiDescription      API Management API description.
        -ApiManagementApiPath             API Management API path.
        -ApiManagementApiSubscriptionRequired
                                          Value indicating whether the API
                                          subscription is required or not.
                                          Default is `$false.
        -ApiManagementApiFormat           API Management API format.
        -ApiManagementApiValue            API Management API value.
        
        -WhatIf:                          Show what would happen without
                                          actually provisioning resources.
        -Help:                            Show this message.
"

    Exit 0
}

# Show usage
$needHelp = ($ResourceGroupName -eq "") -or ($DeploymentName -eq "") -or ($GroupName -eq "") -or ($ResourceName -eq "") -or ($Help -eq $true)
if ($needHelp -eq $true) {
    Show-Usage
    Exit 0
}
$needHelp = ($ApiManagementApiName -eq "") -or ($ApiManagementApiDisplayName -eq "") -or ($ApiManagementApiDescription -eq "") -or ($ApiManagementApiPath -eq "") -or ($ApiManagementApiValue -eq "") -or ($ApiManagementApiPolicyValue -eq "")
if ($needHelp -eq $true) {
    Show-Usage
    Exit 0
}

# Build parameters
$params = @{
    groupName = @{ value = $GroupName };
    name = @{ value = $ResourceName };
    location = @{ value = $Location };
    locationCode = @{ value = $LocationCode };
    env = @{ value = $Environment };

    apiMgmtApiType = @{ value = $ApiManagementApiType };
    apiMgmtApiName = @{ value = $ApiManagementApiName };
    apiMgmtApiDisplayName = @{ value = $ApiManagementApiDisplayName };
    apiMgmtApiDescription = @{ value = $ApiManagementApiDescription };
    apiMgmtApiPath = @{ value = $ApiManagementApiPath };
    apiMgmtApiSubscriptionRequired = @{ value = $ApiManagementApiSubscriptionRequired };
    apiMgmtApiFormat = @{ value = $ApiManagementApiFormat };
    apiMgmtApiValue = @{ value = $ApiManagementApiValue };
    apiMgmtApiPolicyFormat = @{ value = $ApiManagementApiPolicyFormat };
    apiMgmtApiPolicyValue = @{ value = $ApiManagementApiPolicyValue };
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
        -f ./provision-apimanagementapi.bicep `
        -p $stringified `
        -w

        # -u https://raw.githubusercontent.com/aliencube/nhn-toast-notification-service-custom-connector/main/resources/provision-apimanagementapi.bicep `
} else {
    Write-Output "[$(Get-Date -Format "yyyy-MM-dd HH:mm:ss")] Provisioning resources ..."
    az deployment group create -g $ResourceGroupName -n $DeploymentName `
        -f ./provision-apimanagementapi.bicep `
        -p $stringified `
        --verbose

        # -u https://raw.githubusercontent.com/aliencube/nhn-toast-notification-service-custom-connector/main/resources/provision-apimanagementapi.bicep `

    Write-Output "[$(Get-Date -Format "yyyy-MM-dd HH:mm:ss")] Resources have been provisioned"
}
