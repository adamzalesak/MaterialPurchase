resource "confluent_service_account" "usr-material-supplying" {
  description  = "${local.system_description} kafka service account"
  display_name = local.kafka_service_account_name
}
