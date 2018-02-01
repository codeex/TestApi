# TestApi
.net core Test Api HttpClient
#examples
 
	INSERT INTO scmapi(Uri ,PostParam ,TestNum ,Sort ,Type) VALUES('SCMConfigrationService/GetOwnerEntity'  ,'{"Id":"William"}' ,1  ,1  ,1);
	INSERT INTO scmapi(Uri ,PostParam ,TestNum ,Sort ,Type) VALUES('SCMConfigrationService/GetCustomerEntity'  ,'{"Id":"01000079"}' ,1  ,1  ,1);
	INSERT INTO scmapi(Uri ,PostParam ,TestNum ,Sort ,Type) VALUES('SCMConfigrationService/GetCarrierEntity'  ,'{"Id":"ailytest"}' ,1  ,1  ,1);
	INSERT INTO scmapi(Uri ,PostParam ,TestNum ,Sort ,Type) VALUES('SCMConfigrationService/GetVendorEntity'  ,'{"Id":"BBBB"}' ,1  ,1  ,1);
	INSERT INTO scmapi(Uri ,PostParam ,TestNum ,Sort ,Type) VALUES('SCMConfigrationService/GetOwnerListByPage'  ,'{"QueryModel":{"Items": []},"PageInfo":{"CurrentPage": 1,"SkipCount": 2,"PageSize": 3,"SortField": "OwnerName","SortDirection": "Desc","IsPaging": true,"IsGetTotalCount": true,"TotalCount": 8}}' ,1  ,1  ,1);
