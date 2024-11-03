locals {
  domain_name                = ""
  system_name                = "material-supplying"
  system_owner               = "purchase"
  system_description         = "[${local.system_owner}] material-supplying system"
  component_name             = "material-purchase-api"
  kafka_service_account_name = "${var.environment_name}--${local.system_name}"
  kafka_apikey_name          = "${var.environment_name}--${local.component_name}"
  kafka_consumer_group_name  = "group--${local.domain_name}--${local.component_name}"
  k8s_secret_name            = "${var.ACP_ENVIRONMENT_PREFIX}${local.component_name}-confluent-cloud-secrets"
}
