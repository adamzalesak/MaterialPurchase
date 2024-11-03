output "kafka_user" {
  value = <<-EOT
    kind: Secret
    apiVersion: v1
    metadata:
      name: ${local.k8s_secret_name}
    stringData:
      username: ${module.material_purchase_api_kafka_user.api_key_id}
      password: ${module.material_purchase_api_kafka_user.api_key_secret}
    EOT

  sensitive = true
}
