# HackatonFin

Бекенд реализован на платформе Asp Net Core с частичной реализацией MVC паттерна (файлы модели в одноименной папке, контроллер реализован в корневом файле (планируется рефакторинг) и представление в виде фронта ). ORM  Entity Framework,
БД MySQlServer.

Построено Api для взаимодействия с фронтендом и логичстическим модулем Python для передачи элементов баз данных, с учетом возможных некоторотных запросов и дальнейшей валидации.

Сам бекенд задеплоен и доступен по ссылке https://a20062-2bbe.k.d-f.pw

По следующим Get запросам в POSTMAN можно получить:

https://a20062-2bbe.k.d-f.pw/api/category

Список заглвных категорий

https://a20062-2bbe.k.d-f.pw/api/category/subcategory/{id}

Получить список подкатегорий, который относятся к id главной категории либо получить весь список если не указать id

https://a20062-2bbe.k.d-f.pw/api/category/subcategory/sight

Получить список всех достопримечательностей

![изображение](https://github.com/HemulenEXE/HackatonFin/assets/84243256/5b27c20f-8d89-4435-9965-67f8aa2abfd4)

