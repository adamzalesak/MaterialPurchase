IF ('$(environment)' IN ('dev', 'test', 'prod'))
BEGIN
	PRINT 'should be automatically replace with file from repository devops/ci/database'
	RAISERROR('should be automatically replace with file from repository devops/ci/database',16,1)
END