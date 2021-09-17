# Provides configuration details for Terraform
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~>2.31.1"
    }
  }

}

# Provides configuration details for the Azure Terraform provider
provider "azurerm" {
  features {

  }
}

# Provides the resource group to Logically contain resources
resource "azurerm_resource_group" "rg" {
  name     = "${var.name}"
  location = "${var.location}"
  tags = {
    environment = "dev"
    source      = "Terraform"
  }
}

# Provide the sql server resource details to create sql server
resource "azurerm_sql_server" "example" {
  name                         = "${var.servername}"
  resource_group_name          = "${var.name}"
  location                     = "${var.location}"
  version                      = "12.0"
  administrator_login          = "yohan"
  administrator_login_password = "studmanager@123"

  tags = {
    environment = "${var.devtag}"
  }
}

# Provide azure storgae account resource details for create storage account 
resource "azurerm_storage_account" "example" {
  name                     = "${var.storagename}"
  resource_group_name      = "${var.name}"
  location                 = "${var.location}"
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

#Provide azure sql database resource details for create sql database
resource "azurerm_sql_database" "example" {
  name                = "${var.dbname}"
  resource_group_name = "${var.name}"
  location            = "${var.location}"
  server_name         = "${var.servername}"

  extended_auditing_policy {
    storage_endpoint                        = azurerm_storage_account.example.primary_blob_endpoint
    storage_account_access_key              = azurerm_storage_account.example.primary_access_key
    storage_account_access_key_is_secondary = true
    retention_in_days                       = 6
  }

  tags = {
    environment = "${var.devtag}"
  }
}