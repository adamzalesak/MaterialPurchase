terraform {
  required_version = ">=0.12"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~>3.71"
    }

    confluent = {
      source  = "confluentinc/confluent"
      version = "1.55.0"
    }
  }

  backend "azurerm" {
    resource_group_name  = "rg-terraform-dev"
    storage_account_name = "stnotinoterraformdev"
    container_name       = "materialsupplying"
    key                  = "confluent-kafka----material-purchase-api.tfstate"
    use_azuread_auth     = true
  }
}

data "confluent_environment" "selected-environment" {
  display_name = var.confluent_environment_name
}

data "confluent_kafka_cluster" "selected-cluster" {
  id = var.CONFLUENT_KAFKA_CLUSTER_ID
  environment {
    id = data.confluent_environment.selected-environment.id
  }
}

