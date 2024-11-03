
resource "confluent_kafka_topic" "material-supplying--material-purchase--test-topic" {
  topic_name       = "material-supplying--material-purchase--test"
  partitions_count = var.notino_topic_partitions_count

  config = {
    "retention.ms" = var.notino_topic_retention_ms
  }

  rest_endpoint = data.confluent_kafka_cluster.selected-cluster.rest_endpoint
  kafka_cluster {
    id = data.confluent_kafka_cluster.selected-cluster.id
  }
  credentials {
    key    = var.CONFLUENT_KAFKA_API_KEY
    secret = var.CONFLUENT_KAFKA_API_SECRET
  }
}
