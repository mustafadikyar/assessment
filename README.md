# rise-assessment

## Senaryo

### Rise.Assessment.Phonebook.API/Person

- DDD & CQRS
- Automapper
- MediatR
- RabbitMQ
- Background Service
- MS Sql Server
- ClosedXML

![](https://github.com/mustafadikyar/rise-assessment/blob/master/files/phonebook.png)

-- Rise.Assessment.Phonebook.Infrastructure & Rise.Assessment.Phonebook.API üzerinden migration yapılmalı. <br/>

- *GET* **​/api​/persons​/{personId}** => İlgili personelin bilgilerini döner. <br/>
- *GET* **​/api​/persons​/{personId}​/details** => ilgili personeli detaylarıyla birlikte döner. <br/>
- *POST* **​/api​/persons** => Yeni personel oluşturur. <br/>
- *DELETE* **​/api​/persons** => Varolan bir personel siler. <br/>
- *POST* **​/api​/persons​/detail** => Varolan bir personele detay bilgisi ekler. <br/> 
- *DELETE* **​/api​/persons​/detail** => Personelin varolan bir detay kaydını siler. <br/>

<br/>

### Rise.Assessment.Report.API/Report

- RabbitMQ
- MS Sql Server

![](https://github.com/mustafadikyar/rise-assessment/blob/master/files/report.png)

-- Rise.Assessment.Report.API üzerinden migration yapılmalı. <br/>

GET **​/api​/reports​/getall** => Oluşturulan tüm raporların bir listesini döner. <br/>
GET **​/api​/reports** => Yeni bir rapor için istek oluşturur.<br/>
POST **​/api​/reports** => (Arka planda çalışıyor.) Phonebook API tarafında oluşturulan excel(rapor)'in karşılandığı end point. <br/>

<br/>

- Report API üzerinden yeni bir rapor talep edilir ve cevap olarak *Creating* döner.
- Phonebook API üzerinde bir background service aracılığıyla rapor excel formatında oluşturulur. 
- Rapor oluştuktan sonra oluşan excel dosyası Report API  **​/api​/reports** endpoint'ine *Completed* mesajıyla istek yapılır.
- İstek sonucunda *Report/wwwroot/files* klasörüne rapor kaydedilir.
- Oluşturulan raporların bir listesini **​/api​/reports​/getall** endpoint'i üzerinden gözlemlenebilir.

### Rise.Assessment.Phonebook.API.Test
- xunit
- Moq
- Fluent Assertions
