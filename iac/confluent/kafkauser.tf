module "material_purchase_api_kafka_user" {
  source  = "gitlab.notino.com/ci-cd/confluent-kafka-user/local"
  version = "2.0.0"

  kafka_apikey_name        = local.kafka_apikey_name
  kafka_apikey_description = "[${local.system_owner}] material-purchase-api api key"

  consumer_groups = [local.kafka_consumer_group_name]
  consume_topics = [
    confluent_kafka_topic.material-supplying--material-purchase--test-topic.topic_name
  ]

  produce_topics = [
    confluent_kafka_topic.material-supplying--material-purchase--test-topic.topic_name
  ]

  confluent_environment_id = data.confluent_environment.selected-environment.id
  confluent_kafka_cluster = {
    id            = data.confluent_kafka_cluster.selected-cluster.id
    api_version   = data.confluent_kafka_cluster.selected-cluster.api_version
    kind          = data.confluent_kafka_cluster.selected-cluster.kind
    rest_endpoint = data.confluent_kafka_cluster.selected-cluster.rest_endpoint
    api_key       = var.CONFLUENT_KAFKA_API_KEY
    api_secret    = var.CONFLUENT_KAFKA_API_SECRET
  }
  confluent_service_account = {
    id          = confluent_service_account.usr-material-supplying.id
    api_version = confluent_service_account.usr-material-supplying.api_version
    kind        = confluent_service_account.usr-material-supplying.kind
  }
}