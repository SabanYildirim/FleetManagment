# SabanYildirim

# Fleet Management

**Filo yönetim sistemi**

| End point | Özellikler |
| ------ | ------ |
| api/licenseplate |  Araç oluşturmak için kullanılan enpoint ile araç oluşturabilirsiniz. |
| api/deliverypoint | Teslimat noktaları oluşturur. |
| api/bag | Teslimat noktalarına iletmek üzere çanta oluşturur. |
| api/package | Teslimat noktalarına iletmek üzere paket oluşturur. |
| api/assignment | Paketleri çantalara atama yapar. |
| api/shipment | Paketlerin ve çantaların kargo sürecini başlatır ve kargo durumlarını günceller. |

## Tech

- .NET CORE
- MSSQL SERVER
- ELASTİC SEARCH 
- KİBANA
- DOCKER
- SWAGGER


## Docker



```sh
docker-compose build
docker-compose up
```

server

```sh
https://localhost:8081
```
Swagger Address
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
