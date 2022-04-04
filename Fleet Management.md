# Fleet Management

**Filo yönetim sistemi**

| End-Pointler | Özellikler |
| ------ | ------ |
| api/licenseplate | [ Araç oluşturmak için kullanılan enpoint ile araç oluşturabilirsiniz.][PlDb] |
| api/deliverypoint | [Teslimat noktaları oluşturur.][PlGh] |
| api/bag | [Teslimat noktalarına iletmek üzere çanta oluşturur.][PlGd] |
| api/package | [Teslimat noktalarına iletmek üzere çanta oluşturur.][PlOd] |
| api/assignment | [Paketleri çantalara atama yapar.][PlMe] |
| api/shipment | [Paketlerin ve çantaların kargo sürecini başlatır ve kargo durumlarını günceller.][PlGa] |

## Tech

- .NET CORE
- MSSQL SERVER
- ELASTİC SEARCH 
- KİBANA
- DOCKER
- SWAGGER


## Docker



```sh
cd fleet-management
docker-compose buil.
docker-compose up
```

server address

```sh
https://localhost:8081/swagger
```

## Database

![N|Solid](https://i.ibb.co/HHQMgj5/fleet-Managment-Schema.png)


```sh
Package Manager Console
Defult Project: src/FleetManagement.Api
PM> update-database
```




