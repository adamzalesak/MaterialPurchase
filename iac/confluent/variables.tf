variable "CONFLUENT_CLOUD_API_KEY" {
  description = "Confluent Cloud API Key (also referred as Cloud API ID). Predefined in the Gitlab CI/CD variable."
  type        = string
  nullable    = false
}

variable "CONFLUENT_CLOUD_API_SECRET" {
  description = "Confluent Cloud API Secret. Predefined in the Gitlab CI/CD variable."
  type        = string
  sensitive   = true
  nullable    = false
}

variable "CONFLUENT_KAFKA_API_KEY" {
  description = "Kafka Cluster Admin API Key. Predefined in the Gitlab CI/CD variable."
  type        = string
  sensitive   = true
  nullable    = false
}

variable "CONFLUENT_KAFKA_API_SECRET" {
  description = "Kafka Cluster Admin API Secret. Predefined in the Gitlab CI/CD variable."
  type        = string
  sensitive   = true
  nullable    = false
}

variable "ACP_ENVIRONMENT_PREFIX" {
  description = "The prefix for the acceptance environment. Predefined in the Gitlab CI/CD pipeline."
  type        = string
  default     = ""
}

variable "CONFLUENT_KAFKA_REST_ENDPOINT" {
  description = "The REST Endpoint of the Kafka cluster. Predefined in the Gitlab CI/CD variable."
  type        = string
  nullable    = false
}

variable "CONFLUENT_KAFKA_CLUSTER_ID" {
  description = "The ID the the Kafka cluster of the form 'lkc-'. Predefined in the Gitlab CI/CD variable."
  type        = string
  nullable    = false
}

variable "confluent_environment_name" {
  description = "The name of the Confluent Cloud environment."
  type        = string
  nullable    = false
}

variable "notino_topic_partitions_count" {
  description = "Value for the number of partitions for the topic. See https://docs.confluent.io/kafka/operations-tools/partition-determination.html for more information."
  type        = number
  default     = 1
}

variable "notino_topic_retention_ms" {
  description = "Value for the retention time for the topic. See https://docs.confluent.io/platform/current/installation/configuration/topic-configs.html#retention-ms for more information."
  type        = string
  default     = "7200000" // 2 hours
}

variable "environment_name" {
  description = "The short name of the environment."
  type        = string
  nullable    = false
}