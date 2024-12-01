# Material Purchase

## Deploy the Database


```bash
SqlPackage.exe /Action:Publish /SourceFile:"./database/bin/Debug/MaterialPurchase.Database.dacpac" /TargetServerName:"localhost" /TargetDatabaseName:"MaterialPurchase" /TargetUser:"sa" /TargetPassword:"YourStrongPassword123" /TargetTrustServerCertificate:True
```

## License
MIT License (MIT)
